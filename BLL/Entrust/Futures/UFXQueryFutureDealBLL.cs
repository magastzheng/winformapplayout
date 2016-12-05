using BLL.Product;
using BLL.UFX;
using BLL.UFX.impl;
using Config;
using Config.ParamConverter;
using log4net;
using Model;
using Model.Binding.BindingUtil;
using Model.BLL;
using Model.Converter;
using Model.UFX;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Util;

namespace BLL.Entrust.Futures
{
    public class UFXQueryFutureDealBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;

        public UFXQueryFutureDealBLL()
        {
            _securityBLL = BLLManager.Instance.SecurityBLL;
        }

        public List<DealFlowItem> QueryToday(List<Portfolio> portfolios, int timeOut, CallerCallback callback)
        {
            List<DealFlowItem> dealItems = new List<DealFlowItem>();

            foreach (var portfolio in portfolios)
            {
                List<UFXQueryFuturesDealRequest> requests = new List<UFXQueryFuturesDealRequest>();

                UFXQueryFuturesDealRequest request = new UFXQueryFuturesDealRequest();

                request.AccountCode = portfolio.FundCode;
                request.CombiNo = portfolio.PortfolioNo;
                requests.Add(request);

                Callbacker callbacker = new Callbacker
                {
                    Token = new CallerToken
                    {
                        SubmitId = -1,
                        CommandId = -1,
                        InArgs = request.CombiNo,
                        OutArgs = dealItems,
                        WaitEvent = new AutoResetEvent(false),
                        Caller = callback,
                    },

                    DataHandler = QueryDataHandler,
                };

                var result = _securityBLL.QueryFuturesDeal(requests, callbacker);

                BLLResponse bllResponse = new BLLResponse();
                if (result == Model.ConnectionCode.Success)
                {
                    if (callbacker.Token.WaitEvent.WaitOne(timeOut))
                    {
                        var errorResponse = callbacker.Token.ErrorResponse as UFXErrorResponse;
                        if (errorResponse != null && T2ErrorHandler.Success(errorResponse.ErrorCode))
                        {
                            bllResponse.Code = ConnectionCode.Success;
                            bllResponse.Message = "Success QueryDeal";
                        }
                        else
                        {
                            bllResponse.Code = ConnectionCode.FailQueryDeal;
                            bllResponse.Message = "Fail QueryDeal: " + errorResponse.ErrorMessage;
                        }
                    }
                    else
                    {
                        bllResponse.Code = ConnectionCode.FailTimeoutQueryDeal;
                        bllResponse.Message = "Fail QueryDeal: Timeout!";
                    }
                }
                else
                {
                    bllResponse.Code = result;
                    bllResponse.Message = "Fail to QueryDeal in ufx.";
                }
            }

            return dealItems;
        }

        public int QueryHistory(List<Portfolio> portfolios, DateTime startDate, DateTime endDate, int timeOut, CallerCallback callback)
        {
            List<UFXQueryFuturesHistDealRequest> requests = new List<UFXQueryFuturesHistDealRequest>();
            UFXQueryFuturesHistDealRequest request = new UFXQueryFuturesHistDealRequest();
            request.StartDate = DateUtil.GetIntDate(startDate);
            request.EndDate = DateUtil.GetIntDate(endDate);
            request.CombiNo = "30";

            requests.Add(request);

            Callbacker callbacker = new Callbacker
            {
                Token = new CallerToken
                {
                    SubmitId = -2,
                    CommandId = -2,
                    Caller = callback,
                },

                DataHandler = QueryDataHandler,
            };

            var result = _securityBLL.QueryFuturesDealHistory(requests, callbacker);

            return 1;
        }

        private int QueryDataHandler(CallerToken token, DataParser dataParser)
        {
            List<UFXQueryFuturesDealResponse> responseItems = new List<UFXQueryFuturesDealResponse>();
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            token.ErrorResponse = errorResponse;

            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXQueryFuturesDealResponse>();
                for (int i = 1, count = dataParser.DataSets.Count; i < count; i++)
                {
                    var dataSet = dataParser.DataSets[i];
                    foreach (var dataRow in dataSet.Rows)
                    {
                        UFXQueryFuturesDealResponse p = new UFXQueryFuturesDealResponse();
                        UFXDataSetHelper.SetValue<UFXQueryFuturesDealResponse>(ref p, dataRow.Columns, dataFieldMap);
                        responseItems.Add(p);
                    }
                }
            }

            try
            {
                if (token.Caller != null)
                {
                    var dealFlowItems = GetDealItems(responseItems);
                    
                    if (token.OutArgs != null
                        && token.OutArgs is List<DealFlowItem>
                        && dealFlowItems != null
                        && dealFlowItems.Count > 0)
                    {
                        ((List<DealFlowItem>)token.OutArgs).AddRange(dealFlowItems);
                    }

                    token.Caller(token, dealFlowItems, errorResponse);
                }
            }
            finally
            {
                if (token.WaitEvent != null)
                {
                    token.WaitEvent.Set();
                }
            }

            return responseItems.Count();
        }

        private List<DealFlowItem> GetDealItems(List<UFXQueryFuturesDealResponse> responseItems)
        {
            var dealFlowItems = new List<DealFlowItem>();
            if (responseItems == null || responseItems.Count == 0)
            {
                return dealFlowItems;
            }

            foreach (var responseItem in responseItems)
            {
                int commandId = 0;
                int submitId = 0;
                int requestId = 0;
                int temp1, temp2, temp3;
                if (EntrustRequestHelper.TryParseThirdReff(responseItem.ThirdReff, out temp1, out temp2, out temp3))
                {
                    commandId = temp1;
                    submitId = temp2;
                    requestId = temp3;
                }

                var marketCode = UFXTypeConverter.GetMarketCode(responseItem.MarketNo);

                DealFlowItem efItem = new DealFlowItem
                {
                    CommandNo = commandId,
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
                    ExchangeCode = UFXTypeConverter.GetMarketCode(marketCode),
                };

                dealFlowItems.Add(efItem);
            }

            return dealFlowItems;
        }
    }
}
