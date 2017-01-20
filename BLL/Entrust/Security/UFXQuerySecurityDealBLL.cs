using BLL.Manager;
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
using System.Threading;
using Util;

namespace BLL.Entrust.Security
{
    public class UFXQuerySecurityDealBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;

        public UFXQuerySecurityDealBLL()
        {
            _securityBLL = UFXBLLManager.Instance.SecurityBLL;
        }

        public List<DealFlowItem> QueryToday(List<Portfolio> portfolios, int timeOut, CallerCallback callback)
        {
            List<DealFlowItem> dealItems = new List<DealFlowItem>();
            foreach (var portfolio in portfolios)
            {
                List<UFXQueryDealRequest> requests = new List<UFXQueryDealRequest>();

                UFXQueryDealRequest request = new UFXQueryDealRequest();

                request.AccountCode = portfolio.FundCode;
                request.CombiNo = portfolio.PortfolioNo;
                requests.Add(request);

                Callbacker callbacker = new Callbacker
                {
                    Token = new CallerToken
                    {
                        SubmitId = -1,
                        CommandId = -1,
                        InArgs = portfolio,
                        OutArgs = dealItems,
                        WaitEvent = new AutoResetEvent(false),
                        Caller = callback,
                    },

                    DataHandler = QueryDataHandler,
                };

                var result = _securityBLL.QueryDeal(requests, callbacker);

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

        public List<DealFlowItem> QueryHistory(List<Portfolio> portfolios, DateTime startDate, DateTime endDate, int timeOut, CallerCallback callback)
        {
            List<DealFlowItem> dealItems = new List<DealFlowItem>();

            foreach (var portfolio in portfolios)
            {
                List<UFXQueryHistDealRequest> requests = new List<UFXQueryHistDealRequest>();
                UFXQueryHistDealRequest request = new UFXQueryHistDealRequest();
                request.StartDate = DateUtil.GetIntDate(startDate);
                request.EndDate = DateUtil.GetIntDate(endDate);
                request.AccountCode = portfolio.FundCode;
                request.CombiNo = portfolio.PortfolioNo;

                requests.Add(request);

                Callbacker callbacker = new Callbacker
                {
                    Token = new CallerToken
                    {
                        SubmitId = -2,
                        CommandId = -2,
                        InArgs = portfolio,
                        OutArgs = dealItems,
                        WaitEvent = new AutoResetEvent(false),
                        Caller = callback,
                    },

                    DataHandler = QueryDataHandler,
                };

                var result = _securityBLL.QueryDealHistory(requests, callbacker);
                BLLResponse bllResponse = new BLLResponse();
                if (result == Model.ConnectionCode.Success)
                {
                    if (callbacker.Token.WaitEvent.WaitOne(timeOut))
                    {
                        var errorResponse = callbacker.Token.ErrorResponse as UFXErrorResponse;
                        if (errorResponse != null && T2ErrorHandler.Success(errorResponse.ErrorCode))
                        {
                            bllResponse.Code = ConnectionCode.Success;
                            bllResponse.Message = "Success QueryHistory";
                        }
                        else
                        {
                            bllResponse.Code = ConnectionCode.FailQueryDeal;
                            bllResponse.Message = "Fail QueryHistory: " + errorResponse.ErrorMessage;
                        }
                    }
                    else
                    {
                        bllResponse.Code = ConnectionCode.FailTimeoutQueryDeal;
                        bllResponse.Message = "Fail QueryHistory: Timeout!";
                    }
                }
                else
                {
                    bllResponse.Code = result;
                    bllResponse.Message = "Fail to QueryHistory in ufx.";
                }
            }

            return dealItems;
        }

        private int QueryDataHandler(CallerToken token, DataParser dataParser)
        {
            List<UFXQueryDealResponse> responseItems = new List<UFXQueryDealResponse>();
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            token.ErrorResponse = errorResponse;

            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                responseItems = UFXDataSetHelper.ParseData<UFXQueryDealResponse>(dataParser);
            }

            try
            {
                if (token.Caller != null)
                {
                    var dealFlowItems = GetDealItems(token, responseItems);
                    
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

        private List<DealFlowItem> GetDealItems(CallerToken token, List<UFXQueryDealResponse> responseItems)
        {
            List<DealFlowItem> dealItems = new List<DealFlowItem>();

            if (responseItems == null || responseItems.Count == 0)
            {
                return dealItems;
            }

            Portfolio portfolio = (Portfolio)token.InArgs;
            string portfolioCode = string.Empty;
            string portfolioName = string.Empty;
            string fundCode = string.Empty;
            string fundName = string.Empty;
            if (portfolio != null)
            {
                portfolioCode = portfolio.PortfolioNo;
                portfolioName = portfolio.PortfolioName;
                fundCode = portfolio.FundCode;
                fundName = portfolio.FundName;
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
                var entrustDirection = UFXTypeConverter.GetEntrustDirection(responseItem.EntrustDirection);

                DealFlowItem efItem = new DealFlowItem
                {
                    CommandNo = commandId,
                    SecuCode = responseItem.StockCode,
                    FundNo = responseItem.AccountCode,
                    FundName = fundName,
                    PortfolioCode = responseItem.CombiNo,
                    PortfolioName = portfolioName,
                    EDirection = EntrustDirectionConverter.GetSecurityEntrustDirection(entrustDirection),
                    DealPrice = responseItem.DealPrice,
                    DealAmount = responseItem.DealAmount,
                    DealMoney = responseItem.DealBalance,
                    DealTime = string.Format("{0}", responseItem.DealTime),
                    ShareHolderCode = responseItem.StockHolderId,
                    EntrustNo = string.Format("{0}", responseItem.EntrustNo),
                    DealNo = string.Format("{0}", responseItem.DealNo),
                    ExchangeCode = UFXTypeConverter.GetMarketCode(marketCode),
                };

                dealItems.Add(efItem);
            }

            return dealItems;
        }
    }
}
