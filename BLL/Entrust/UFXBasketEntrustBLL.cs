using BLL.EntrustCommand;
using BLL.Manager;
using BLL.SecurityInfo;
using BLL.TradeCommand;
using BLL.UFX;
using BLL.UFX.impl;
using Config;
using Config.ParamConverter;
using log4net;
using Model;
using Model.Binding.BindingUtil;
using Model.BLL;
using Model.Database;
using Model.UFX;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BLL.Entrust
{
    public class UFXBasketEntrustBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;
        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private EntrustCommandBLL _entrustCommandBLL = new EntrustCommandBLL();
        private EntrustCombineBLL _entrustCombineBLL = new EntrustCombineBLL();
        private int _timeOut = 30 * 1000;
        
        private double _limitEntrustRatio = 100.0;
        private double _futuLimitEntrustRatio = 100.0;
        private double _optLimitEntrustRatio = 100;

        public UFXBasketEntrustBLL()
        {
            _securityBLL = UFXBLLManager.Instance.SecurityBLL;

            _timeOut = ConfigManager.Instance.GetDefaultSettingConfig().DefaultSetting.UFXSetting.Timeout;
            _limitEntrustRatio = ConfigManager.Instance.GetDefaultSettingConfig().DefaultSetting.UFXSetting.LimitEntrustRatio;
            _futuLimitEntrustRatio = ConfigManager.Instance.GetDefaultSettingConfig().DefaultSetting.UFXSetting.FutuLimitEntrustRatio;
            _optLimitEntrustRatio = ConfigManager.Instance.GetDefaultSettingConfig().DefaultSetting.UFXSetting.OptLimitEntrustRatio;
        }

        public BLLResponse Submit(Model.Database.EntrustCommand cmdItem, List<EntrustSecurity> entrustItems, CallerCallback callerCallback)
        {
            var cmdEntrustItems = entrustItems.Where(p => p.CommandId == cmdItem.CommandId && p.SubmitId == cmdItem.SubmitId).ToList();
            if (cmdEntrustItems == null || cmdEntrustItems.Count == 0)
            {
                return new BLLResponse(ConnectionCode.EmptyEntrustItem, "Empty EntrustCommandItem or EntrustSecurityItem.");
            }
            var tradeCommandItem = _tradeCommandBLL.GetTradeCommand(cmdItem.CommandId);
            var portfolio = LoginManager.Instance.GetPortfolio(tradeCommandItem.PortfolioCode);
            //var stockholder = LoginManager.Instance.GetHolder(tradeCommandItem.

            var ufxRequests = new List<UFXBasketEntrustRequest>();
            var futuItem = cmdEntrustItems.Find(p => p.SecuType == Model.SecurityInfo.SecurityType.Futures);
            foreach (var secuItem in cmdEntrustItems)
            {
                UFXBasketEntrustRequest request = new UFXBasketEntrustRequest
                {
                    StockCode = secuItem.SecuCode,
                    EntrustPrice = secuItem.EntrustPrice,
                    EntrustAmount = secuItem.EntrustAmount,
                    PriceType = EntrustRequestHelper.GetEntrustPriceType(secuItem.EntrustPriceType),
                    ExtSystemId = secuItem.RequestId,
                    ThirdReff = EntrustRequestHelper.GenerateThirdReff(secuItem.CommandId, secuItem.SubmitId, secuItem.RequestId),
                    LimitEntrustRatio = _limitEntrustRatio,
                    FutuLimitEntrustRatio = _futuLimitEntrustRatio,
                    OptLimitEntrustRatio = _optLimitEntrustRatio,
                };

                if (secuItem.SecuType == Model.SecurityInfo.SecurityType.Stock)
                {
                    request.EntrustDirection = EntrustRequestHelper.GetEntrustDirection(secuItem.EntrustDirection);
                    request.FuturesDirection = string.Empty;
                }
                else if (secuItem.SecuType == Model.SecurityInfo.SecurityType.Futures)
                {
                    request.EntrustDirection = EntrustRequestHelper.GetEntrustDirection(secuItem.EntrustDirection);
                    request.FuturesDirection = EntrustRequestHelper.GetFuturesDirection(secuItem.EntrustDirection);
                }

                var secuInfo = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                if (secuInfo != null)
                {
                    request.MarketNo = EntrustRequestHelper.GetMarketNo(secuInfo.ExchangeCode);
                }

                if (tradeCommandItem != null)
                {
                    request.AccountCode = tradeCommandItem.AccountCode;
                    request.CombiNo = tradeCommandItem.PortfolioCode;
                }

                ufxRequests.Add(request);
            }

            Callbacker callbacker = new Callbacker
            {
                Token = new CallerToken
                {
                    SubmitId = cmdItem.SubmitId,
                    CommandId = cmdItem.CommandId,
                    Caller = callerCallback,
                    WaitEvent = new AutoResetEvent(false),
                },

                DataHandler = EntrustDataHandler,
            };

            var result = _securityBLL.EntrustBasket(ufxRequests, callbacker);

            BLLResponse bllResponse = new BLLResponse();
            if (result == Model.ConnectionCode.Success)
            {
                if (callbacker.Token.WaitEvent.WaitOne(_timeOut))
                {
                    var errorResponse = callbacker.Token.ErrorResponse as UFXErrorResponse;
                    if (errorResponse != null && T2ErrorHandler.Success(errorResponse.ErrorCode))
                    {
                        bllResponse.Code = ConnectionCode.Success;
                        bllResponse.Message = "Success Entrust";
                    }
                    else
                    {
                        bllResponse.Code = ConnectionCode.FailEntrust;
                        bllResponse.Message = "Fail Entrust: " + errorResponse.ErrorCode + " " + errorResponse.ErrorMessage + " " + errorResponse.MessageDetail;
                    }
                }
                else
                {
                    bllResponse.Code = ConnectionCode.FailTimeoutEntrust;
                    bllResponse.Message = "Timeout to entrust.";
                }
            }
            else
            {
                bllResponse.Code = result;
                bllResponse.Message = "Fail to submit in ufx.";
            }

            return bllResponse;
        }

        private int EntrustDataHandler(CallerToken token, DataParser dataParser)
        {
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            token.ErrorResponse = errorResponse;

            List<UFXBasketEntrustResponse> responseItems = UFXDataSetHelper.ParseData<UFXBasketEntrustResponse>(dataParser);
            
            int ret = -1;
            List<EntrustSecurity> entrustSecuItems = new List<EntrustSecurity>();
            foreach (var responseItem in responseItems)
            {
                var entrustItem = new EntrustSecurity
                {
                    SubmitId = token.SubmitId,
                    RequestId = responseItem.ExtSystemId,
                    SecuCode = responseItem.StockCode,
                    EntrustNo = responseItem.EntrustNo,
                    BatchNo = responseItem.BatchNo,
                    EntrustFailCode = responseItem.EntrustFailCode,
                    EntrustFailCause = responseItem.FailCause,
                };

                entrustSecuItems.Add(entrustItem);
            }

            ret = _entrustCombineBLL.UpdateSecurityEntrustResponseByRequestId(entrustSecuItems);

            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                int batchNo = 0;
                var batchNos = responseItems.Select(p => p.BatchNo).Distinct().ToList();
                if (batchNos.Count == 1)
                {
                    batchNo = batchNos[0];
                }

                ret = _entrustCommandBLL.UpdateEntrustCommandBatchNo(token.SubmitId, batchNo, Model.EnumType.EntrustStatus.Completed, errorResponse.ErrorCode, errorResponse.ErrorMessage);
            }
            else
            {
                ret = _entrustCommandBLL.UpdateEntrustCommandBatchNo(token.SubmitId, 0, Model.EnumType.EntrustStatus.EntrustFailed, errorResponse.ErrorCode, errorResponse.ErrorMessage);

                //TODO:
                string msg = string.Format("The SubmitId [{0}] fail to entrust. ErrorCode: {1}, ErrorMessage: {2}, MessageDetail: {3}", token.SubmitId, errorResponse.ErrorCode, errorResponse.ErrorMessage, errorResponse.MessageDetail);
                logger.Error(msg);
            }

            try
            {
                if (token.Caller != null)
                {
                    token.Caller(token, entrustSecuItems, errorResponse);
                }
            }
            finally
            {
                if (token.WaitEvent != null)
                {
                    token.WaitEvent.Set();
                }
            }

            return ret;
        }
    }
}
