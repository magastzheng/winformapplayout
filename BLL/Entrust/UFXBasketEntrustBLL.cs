using BLL.SecurityInfo;
using BLL.TradeCommand;
using BLL.UFX.impl;
using Config;
using Config.ParamConverter;
using DBAccess;
using log4net;
using Model.Binding.BindingUtil;
using Model.t2sdk;
using Model.UI;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Entrust
{
    public class UFXBasketEntrustBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;
        private TradeCommandBLL _tradeCommandBLL = null;
        private EntrustDAO _entrustdao = new EntrustDAO();
        private EntrustCommandDAO _entrustcmddao = new EntrustCommandDAO();

        public UFXBasketEntrustBLL()
        {
            _securityBLL = BLLManager.Instance.SecurityBLL;
        }

        public int Submit(EntrustCommandItem cmdItem, List<EntrustSecurityItem> entrustItems, CallerCallback callerCallback)
        {
            int ret = -1;

            var cmdEntrustItems = entrustItems.Where(p => p.CommandId == cmdItem.CommandId && p.SubmitId == cmdItem.SubmitId).ToList();
            if (cmdEntrustItems == null || cmdEntrustItems.Count == 0)
            {
                return ret;
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
                    request.AccountCode = tradeCommandItem.FundCode;
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
                },

                DataHandler = EntrustDataHandler,
            };

            var result = _securityBLL.EntrustBasket(ufxRequests, callbacker);

            if (result == Model.ConnectionCode.Success)
            {
                ret = 1;
            }

            return ret;
        }

        private int EntrustDataHandler(CallerToken token, DataParser dataParser)
        {
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
                };

                entrustSecuItems.Add(entrustItem);
            }

            ret = _entrustdao.UpdateSecurityEntrustResponseByRequestId(entrustSecuItems);

            var batchNos = responseItems.Select(p => p.BatchNo).Distinct().ToList();
            if (batchNos.Count == 1)
            {
                int batchNo = batchNos[0];

                ret = _entrustcmddao.UpdateEntrustCommandBatchNo(token.SubmitId, batchNo, Model.EnumType.EntrustStatus.Completed);
            }
            else
            {
                //TODO:
                string msg = string.Format("The SubmitId [{0}] was split into several batch no", token.SubmitId);
                logger.Warn(msg);
            }

            if (token.Caller != null)
            {
                token.Caller(token, entrustSecuItems);
            }

            return ret;
        }
    }
}
