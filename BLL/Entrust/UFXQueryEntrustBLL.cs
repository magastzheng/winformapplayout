using BLL.UFX.impl;
using log4net;
using Model.Binding.BindingUtil;
using Model.Data;
using Model.t2sdk;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Entrust
{
    public class UFXQueryEntrustBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;
        //private TradeCommandBLL _tradeCommandBLL = null;
        private AutoResetEvent _waitEvent = new AutoResetEvent(false);

        public UFXQueryEntrustBLL()
        {
            this._securityBLL = BLLManager.Instance.SecurityBLL;
        }

        public List<EntrustFlowItem> QueryToday(CallerCallback callback)
        {
            List<UFXQueryEntrustRequest> requests = new List<UFXQueryEntrustRequest>();
            UFXQueryEntrustRequest request = new UFXQueryEntrustRequest();
            request.RequestNum = 10000;
            request.CombiNo = "30";
            requests.Add(request);

            Callbacker callbacker = new Callbacker
            {
                Token = new CallerToken
                {
                    SubmitId = 11111,
                    CommandId = 22222,
                    Caller = callback,
                },

                Callback = QueryToday,
            };

            var result = _securityBLL.QueryEntrust(requests, callbacker);

            //_waitEvent.WaitOne(30 * 1000);

            return null;
        }

        private int QueryToday(CallerToken token, DataParser dataParser)
        {
            List<UFXQueryEntrustResponse> responseItems = new List<UFXQueryEntrustResponse>();

            var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXQueryEntrustResponse>();
            for (int i = 1, count = dataParser.DataSets.Count; i < count; i++)
            {
                var dataSet = dataParser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    UFXQueryEntrustResponse p = new UFXQueryEntrustResponse();
                    UFXDataSetHelper.SetValue<UFXQueryEntrustResponse>(ref p, dataRow.Columns, dataFieldMap);
                    responseItems.Add(p);
                }
            }

            if (token.Caller != null)
            {
                var entrustFlowItems = new List<EntrustFlowItem>();
                foreach (var responseItem in responseItems)
                {
                    EntrustFlowItem efItem = new EntrustFlowItem 
                    { 
                        CommandNo = token.CommandId,
                        Market = responseItem.MarketNo,
                        SecuCode = responseItem.StockCode,
                        EntrustDirection = responseItem.EntrustDirection,
                        PriceType = responseItem.PriceType,
                        EntrustPrice = responseItem.EntrustPrice,
                        EntrustAmount = responseItem.EntrustAmount,
                        EntrustStatus = responseItem.EntrustState,
                        DealAmount = responseItem.DealAmount,
                        DealMoney = responseItem.DealBalance,
                        DealTimes = responseItem.DealTimes,
                        EntrustedDate = string.Format("{0}",responseItem.EntrustDate),
                        FirstDealDate = string.Format("{0}", responseItem.FirstDealTime),
                        EntrustedTime = string.Format("{0}", responseItem.EntrustTime),
                        EntrustBatchNo = responseItem.BatchNo,
                        EntrustNo = responseItem.EntrustNo,
                        DeclareSeat = responseItem.ReportSeat,
                        DeclareNo = Convert.ToInt32(responseItem.ReportNo),
                    };

                    entrustFlowItems.Add(efItem);
                }

                token.Caller(token, entrustFlowItems);
            }

            //_waitEvent.Set();

            return responseItems.Count();
        }
    }
}
