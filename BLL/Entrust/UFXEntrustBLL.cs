using BLL.SecurityInfo;
using BLL.TradeCommand;
using BLL.UFX.impl;
using Config;
using Config.ParamConverter;
using DBAccess;
using Model.strategy;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Entrust
{
    public class UFXEntrustBLL
    {
        private SecurityBLL _securityBLL = null;
        private TradeCommandBLL _tradeCommandBLL = null;
        private EntrustDAO _entrustdao = new EntrustDAO();
        private EntrustCommandDAO _entrustcmddao = new EntrustCommandDAO();

        private AutoResetEvent _waitEvent = new AutoResetEvent(false);
        private Dictionary<int, List<UFXBasketEntrustResponse>> _responseMap = new Dictionary<int, List<UFXBasketEntrustResponse>>();

        public UFXEntrustBLL()
        {
            _securityBLL = BLLManager.Instance.SecurityBLL;
            _tradeCommandBLL = new TradeCommandBLL();
        }

        public int Submit(EntrustCommandItem cmdItem, List<EntrustSecurityItem> entrustItems)
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
                    EntrustDirection = EntrustRequestHelper.GetEntrustDirection(secuItem.EntrustDirection),
                    EntrustPrice = secuItem.EntrustPrice,
                    EntrustAmount = secuItem.EntrustAmount,
                    PriceType = EntrustRequestHelper.GetEntrustPriceType(secuItem.EntrustPriceType),
                    ExtSystemId = secuItem.SubmitId,
                    ThirdReff = string.Format("{0};{1}", secuItem.CommandId, secuItem.SubmitId),
                    LimitEntrustRatio = 100,
                    FutuLimitEntrustRatio = 100,
                    OptLimitEntrustRatio = 100,
                };

                if (secuItem.SecuType == Model.SecurityInfo.SecurityType.Futures)
                {
                    request.FuturesDirection = EntrustRequestHelper.GetEntrustDirection(secuItem.EntrustDirection);
                }
                else if (futuItem != null)
                {
                    request.FuturesDirection = EntrustRequestHelper.GetEntrustDirection(futuItem.EntrustDirection);
                }
                else
                { 
                    //do nothing
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
                    //request.StockHolderId = tradeCommandItem
                }

                if (portfolio != null)
                {
                    request.AssetNo = portfolio.AssetNo;

                    var holder = LoginManager.Instance.GetHolder(request.CombiNo, request.MarketNo);
                    if (holder != null && !string.IsNullOrEmpty(holder.StockHolderId))
                    {
                        request.StockHolderId = holder.StockHolderId;
                    }
                }

                //TODO: fix the futures cannot entrust
                //if (secuItem.SecuType == Model.SecurityInfo.SecurityType.Stock)
                //{
                    ufxRequests.Add(request);
                //}
            }

            //call ufx
            var result = _securityBLL.EntrustBasket(ufxRequests, new DataHandlerCallback(EntrustBasketCallback));
            //
            _waitEvent.WaitOne();
            
            List<UFXBasketEntrustResponse> responsedItems = null;
            lock (_responseMap)
            {
                if (_responseMap.ContainsKey(cmdItem.SubmitId) && _responseMap[cmdItem.SubmitId].Count > 0)
                {
                    //ret = _responseMap[cmdItem.SubmitId].Count;

                    responsedItems = _responseMap[cmdItem.SubmitId];
                }
            }

            if (responsedItems != null)
            {
                List<EntrustSecurityItem> entrustSecuItems = new List<EntrustSecurityItem>();
                //update the status
                foreach (var responseItem in responsedItems)
                {
                    var entrustItem = entrustItems.Find(p => p.Equals(responseItem.StockCode));
                    if (entrustItem == null)
                    {
                        entrustItem = new EntrustSecurityItem
                        {
                            SubmitId = cmdItem.SubmitId,
                            CommandId = cmdItem.CommandId,
                            SecuCode = responseItem.StockCode,
                        };
                    }
                    
                    entrustSecuItems.Add(entrustItem);
                }

                if (entrustSecuItems.Count > 0)
                {
                    ret = _entrustdao.UpdateSecurityEntrustStatus(entrustSecuItems, Model.EnumType.EntrustStatus.Completed);

                    if (entrustSecuItems.Count == cmdEntrustItems.Count)
                    {
                        ret = _entrustcmddao.UpdateEntrustCommandStatus(cmdItem.SubmitId, Model.EnumType.EntrustStatus.Completed);
                    }
                }
            }

            return ret;
        }

        private void EntrustBasketCallback(DataParser dataParser)
        {
            List<UFXBasketEntrustResponse> responseItems = new List<UFXBasketEntrustResponse>();
            int submitId = -1;
            for (int i = 1, count = dataParser.DataSets.Count; i < count; i++)
            {
                var dataSet = dataParser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    UFXBasketEntrustResponse p = new UFXBasketEntrustResponse();
                    p.BatchNo = dataRow.Columns["batch_no"].GetInt();
                    p.EntrustNo = dataRow.Columns["entrust_no"].GetInt();
                    p.RequestOrder = dataRow.Columns["request_order"].GetInt();
                    p.ExtSystemId = dataRow.Columns["extsystem_id"].GetInt();
                    p.EntrustFailCode = dataRow.Columns["entrust_fail_code"].GetInt();
                    p.FailCause = dataRow.Columns["fail_cause"].GetStr();
                    p.RiskSerialNo = dataRow.Columns["risk_serial_no"].GetInt();
                    p.MarketNo = dataRow.Columns["market_no"].GetStr();
                    p.StockCode = dataRow.Columns["stock_code"].GetStr();
                    p.EntrustDirection = dataRow.Columns["entrust_direction"].GetStr();
                    p.FuturesDirection = dataRow.Columns["futures_direction"].GetStr();
                    p.RiskNo = dataRow.Columns["risk_no"].GetInt();
                    p.RiskType = dataRow.Columns["risk_type"].GetInt();
                    p.RiskSummary = dataRow.Columns["risk_summary"].GetStr();
                    p.RiskOperation = dataRow.Columns["risk_operation"].GetStr();
                    p.RemarkShort = dataRow.Columns["remark_short"].GetStr();
                    p.RiskThresholdValue = dataRow.Columns["risk_threshold_value"].GetDouble();
                    p.RiskValue = dataRow.Columns["risk_value"].GetDouble();

                    if(submitId == -1)
                    {
                        submitId = p.ExtSystemId;
                    }

                    responseItems.Add(p);
                }
                break;
            }

            if(submitId > 0)
            {
                lock (_responseMap)
                {
                    if (_responseMap.ContainsKey(submitId))
                    {
                        _responseMap[submitId].Clear();
                        _responseMap[submitId].AddRange(responseItems);
                    }
                    else
                    {
                        _responseMap[submitId] = responseItems;
                    }
                }
            }

            _waitEvent.Set();
        }
    }
}
