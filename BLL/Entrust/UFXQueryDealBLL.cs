using BLL.UFX.impl;
using log4net;
using Model.Binding.BindingUtil;
using Model.t2sdk;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace BLL.Entrust
{
    public class UFXQueryDealBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;

        public UFXQueryDealBLL()
        {
            _securityBLL = BLLManager.Instance.SecurityBLL;
        }

        public int QueryToday(CallerCallback callback)
        {
            List<UFXQueryDealRequest> requests = new List<UFXQueryDealRequest>();

            UFXQueryDealRequest request = new UFXQueryDealRequest();
            request.ExtSystemId = 100000999;
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

                DataHandler = QueryDataHandler,
            };

            var result = _securityBLL.QueryDeal(requests, callbacker);

            return 1;
        }

        public int QueryHistory(DateTime startDate, DateTime endDate, CallerCallback callback)
        {
            List<UFXQueryHistDealRequest> requests = new List<UFXQueryHistDealRequest>();
            UFXQueryHistDealRequest request = new UFXQueryHistDealRequest();
            request.StartDate = DateUtil.GetIntDate(startDate);
            request.EndDate = DateUtil.GetIntDate(endDate);
            request.CombiNo = "30";

            requests.Add(request);

            Callbacker callbacker = new Callbacker
            {
                Token = new CallerToken
                {
                    SubmitId = 333333,
                    CommandId = 444444,
                    Caller = callback,
                },

                DataHandler = QueryDataHandler,
            };

            var result = _securityBLL.QueryDealHistory(requests, callbacker);

            return 1;
        }

        private int QueryDataHandler(CallerToken token, DataParser dataParser)
        {
            List<UFXQueryDealResponse> responseItems = new List<UFXQueryDealResponse>();

            var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXQueryDealResponse>();
            for (int i = 1, count = dataParser.DataSets.Count; i < count; i++)
            {
                var dataSet = dataParser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    UFXQueryDealResponse p = new UFXQueryDealResponse();
                    UFXDataSetHelper.SetValue<UFXQueryDealResponse>(ref p, dataRow.Columns, dataFieldMap);
                    responseItems.Add(p);
                }
            }

            if (token.Caller != null)
            {
                var dealFlowItems = new List<DealFlowItem>();
                foreach (var responseItem in responseItems)
                {
                    DealFlowItem efItem = new DealFlowItem
                    {
                        CommandNo = token.CommandId,
                        SecuCode = responseItem.StockCode,
                        FundNo = responseItem.AccountCode,
                        PortfolioCode = responseItem.CombiNo,
                        EntrustDirection = responseItem.EntrustDirection,
                        DealPrice = responseItem.DealPrice,
                        DealAmount = responseItem.DealAmount,
                        DealMoney = responseItem.DealBalance,
                        DealTime = string.Format("{0}", responseItem.DealTime),
                        ShareHolderCode = responseItem.StockHolderId,
                        EntrustNo = string.Format("{0}", responseItem.EntrustNo),
                        DealNo = string.Format("{0}", responseItem.DealNo),
                    };

                    dealFlowItems.Add(efItem);
                }

                token.Caller(token, dealFlowItems);
            }

            return responseItems.Count();
        }
    }
}
