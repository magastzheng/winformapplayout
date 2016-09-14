using BLL.EntrustCommand;
using BLL.Frontend;
using BLL.SecurityInfo;
using BLL.UFX;
using BLL.UFX.impl;
using Config;
using Config.ParamConverter;
using DBAccess.Entrust;
using log4net;
using Model;
using Model.Binding.BindingUtil;
using Model.BLL;
using Model.UFX;
using Model.UI;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BLL.Entrust
{
    public class UFXBasketEntrustBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;
        private TradeCommandBLL _tradeCommandBLL = null;
        private EntrustCommandBLL _entrustCommandBLL = new EntrustCommandBLL();
        private EntrustDAO _entrustdao = new EntrustDAO();
        
        public UFXBasketEntrustBLL()
        {
            _securityBLL = BLLManager.Instance.SecurityBLL;
            _tradeCommandBLL = new TradeCommandBLL();
        }

        public BLLResponse Submit(EntrustCommandItem cmdItem, List<EntrustSecurityItem> entrustItems, CallerCallback callerCallback)
        {
            var cmdEntrustItems = entrustItems.Where(p => p.CommandId == cmdItem.CommandId && p.SubmitId == cmdItem.SubmitId).ToList();
            if (cmdEntrustItems == null || cmdEntrustItems.Count == 0)
            {
                return new BLLResponse(ConnectionCode.EmptyEntrustItem, "Empty EntrustCommandItem or EntrustSecurityItem.");
            }
            var tradeCommandItem = _tradeCommandBLL.GetTradeCommandItem(cmdItem.CommandId);
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
                    LimitEntrustRatio = 100,
                    FutuLimitEntrustRatio = 100,
                    OptLimitEntrustRatio = 100,
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
                callbacker.Token.WaitEvent.WaitOne();
                var errorResponse = callbacker.Token.OutArgs as UFXErrorResponse;
                if (errorResponse != null && T2ErrorHandler.Success(errorResponse.ErrorCode))
                {
                    bllResponse.Code = ConnectionCode.Success;
                    bllResponse.Message = "Success Entrust";
                }
                else
                {
                    bllResponse.Code = ConnectionCode.FailEntrust;
                    bllResponse.Message = "Fail Entrust: " + errorResponse.ErrorMessage;
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
            token.OutArgs = errorResponse;
            
            List<UFXBasketEntrustResponse> responseItems = new List<UFXBasketEntrustResponse>();
            var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXBasketEntrustResponse>();
            for (int i = 1, count = dataParser.DataSets.Count; i < count; i++)
            {
                var dataSet = dataParser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    UFXBasketEntrustResponse p = new UFXBasketEntrustResponse();
                    UFXDataSetHelper.SetValue<UFXBasketEntrustResponse>(ref p, dataRow.Columns, dataFieldMap);
                    responseItems.Add(p);
                }
            }

            int ret = -1;
            List<EntrustSecurityItem> entrustSecuItems = new List<EntrustSecurityItem>();
            foreach (var responseItem in responseItems)
            {
                var entrustItem = new EntrustSecurityItem
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

            ret = _entrustdao.UpdateSecurityEntrustResponseByRequestId(entrustSecuItems);

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
                string msg = string.Format("The SubmitId [{0}] was split into several batch no", token.SubmitId);
                logger.Warn(msg);
            }

            if (token.Caller != null)
            {
                token.Caller(token, entrustSecuItems, errorResponse);
            }

            if (token.WaitEvent != null)
            {
                token.WaitEvent.Set();
            }

            return ret;
        }
    }
}
