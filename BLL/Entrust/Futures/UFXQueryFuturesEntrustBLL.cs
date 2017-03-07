using BLL.Manager;
using BLL.TradeCommand;
using Config.ParamConverter;
using log4net;
using Model;
using Model.BLL;
using Model.Converter;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using Model.UFX;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UFX;
using UFX.impl;
using Util;

namespace BLL.Entrust.Futures
{
    public class UFXQueryFuturesEntrustBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;
        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();

        public UFXQueryFuturesEntrustBLL()
        {
            this._securityBLL = UFXBLLManager.Instance.SecurityBLL;
        }

        public List<EntrustFlowItem> QueryToday(List<Portfolio> portfolios, int timeOut, CallerCallback callback)
        {
            List<EntrustFlowItem> entrustItems = new List<EntrustFlowItem>();
            foreach (var portfolio in portfolios)
            {
                List<UFXQueryFuturesEntrustRequest> requests = new List<UFXQueryFuturesEntrustRequest>();

                UFXQueryFuturesEntrustRequest request = new UFXQueryFuturesEntrustRequest();
                request.AccountCode = portfolio.FundCode;
                request.AssetNo = portfolio.AssetNo;
                request.CombiNo = portfolio.PortfolioNo;
                requests.Add(request);

                Callbacker callbacker = new Callbacker
                {
                    Token = new CallerToken
                    {
                        SubmitId = -1,
                        CommandId = -1,
                        InArgs = portfolio,
                        OutArgs = entrustItems,
                        WaitEvent = new AutoResetEvent(false),
                        Caller = callback,
                    },

                    DataHandler = QueryDataHandler,
                };

                var result = _securityBLL.QueryFuturesEntrust(requests, callbacker);

                BLLResponse bllResponse = new BLLResponse();
                if (result == Model.ConnectionCode.Success)
                {
                    if (callbacker.Token.WaitEvent.WaitOne(timeOut))
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
                            bllResponse.Message = "Fail Entrust: " + errorResponse.ErrorMessage;
                        }
                    }
                    else
                    {
                        bllResponse.Code = ConnectionCode.FailTimeoutQueryEntrust;
                        bllResponse.Message = "Fail Entrust: Timeout";
                    }
                }
                else
                {
                    bllResponse.Code = result;
                    bllResponse.Message = "Fail to submit in ufx.";
                }
            }

            return entrustItems;
        }

        public List<EntrustFlowItem> QueryHistory(List<Portfolio> portfolios, DateTime startDate, DateTime endDate, int timeOut, CallerCallback callback)
        {
            List<EntrustFlowItem> entrustItems = new List<EntrustFlowItem>();
            int intStart = DateUtil.GetIntDate(startDate);
            int intEnd = DateUtil.GetIntDate(endDate);

            foreach (var portfolio in portfolios)
            {
                List<UFXQueryFuturesHistEntrustRequest> requests = new List<UFXQueryFuturesHistEntrustRequest>();

                UFXQueryFuturesHistEntrustRequest request = new UFXQueryFuturesHistEntrustRequest();
                request.StartDate = intStart;
                request.EndDate = intEnd;
                request.CombiNo = portfolio.PortfolioNo;

                requests.Add(request);

                Callbacker callbacker = new Callbacker
                {
                    Token = new CallerToken
                    {
                        SubmitId = -2,
                        CommandId = -2,
                        InArgs = portfolio,
                        OutArgs = entrustItems,
                        WaitEvent = new AutoResetEvent(false),
                        Caller = callback,
                    },

                    DataHandler = QueryDataHandler,
                };

                var result = _securityBLL.QueryFuturesEntrustHistory(requests, callbacker);

                BLLResponse bllResponse = new BLLResponse();
                if (result == Model.ConnectionCode.Success)
                {
                    if (callbacker.Token.WaitEvent.WaitOne(timeOut))
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
                            bllResponse.Message = "Fail Entrust: " + errorResponse.ErrorMessage;
                        }
                    }
                    else
                    {
                        bllResponse.Code = ConnectionCode.FailEntrust;
                        bllResponse.Message = "Fail Entrust: Timeout";
                    }
                }
                else
                {
                    bllResponse.Code = result;
                    bllResponse.Message = "Fail to submit in ufx.";
                }
            }

            return entrustItems;
        }

        private int QueryDataHandler(CallerToken token, DataParser dataParser)
        {
            List<UFXQueryFuturesEntrustResponse> responseItems = new List<UFXQueryFuturesEntrustResponse>();
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            token.ErrorResponse = errorResponse;

            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                responseItems = UFXDataSetHelper.ParseData<UFXQueryFuturesEntrustResponse>(dataParser);
            }

            try
            {
                if (token.Caller != null)
                {
                    var entrustFlowItems = GetFlowItems(token, responseItems);

                    if (token.OutArgs != null
                        && token.OutArgs is List<EntrustFlowItem>
                        && entrustFlowItems != null
                        && entrustFlowItems.Count > 0
                    )
                    {
                        ((List<EntrustFlowItem>)token.OutArgs).AddRange(entrustFlowItems);
                    }

                    token.Caller(token, entrustFlowItems, errorResponse);
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

        private List<EntrustFlowItem> GetFlowItems(CallerToken token, List<UFXQueryFuturesEntrustResponse> responseItems)
        {
            var entrustFlowItems = new List<EntrustFlowItem>();
            if (responseItems == null || responseItems.Count == 0)
            {
                return entrustFlowItems;
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

            Dictionary<int, Model.UI.TradeInstance> cmdInstMap = new Dictionary<int, Model.UI.TradeInstance>();
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

                int instanceId = 0;
                string instanceCode = string.Empty;
                if (!cmdInstMap.ContainsKey(commandId))
                {
                    var tradeInstance = _tradeCommandBLL.GetTradeInstance(commandId);
                    if (tradeInstance != null)
                    {
                        instanceId = tradeInstance.InstanceId;
                        instanceCode = tradeInstance.InstanceCode;
                        cmdInstMap.Add(commandId, tradeInstance);
                    }
                }
                else
                {
                    instanceId = cmdInstMap[commandId].InstanceId;
                    instanceCode = cmdInstMap[commandId].InstanceCode;
                }

                var entrustDirection = UFXTypeConverter.GetEntrustDirection(responseItem.EntrustDirection);
                var futuresDirection = UFXTypeConverter.GetFuturesDirection(responseItem.FuturesDirection);
                EntrustDirection eDirection = EntrustDirectionConverter.GetFuturesEntrustDirection(entrustDirection, futuresDirection);

                EntrustFlowItem efItem = new EntrustFlowItem
                {
                    CommandNo = token.CommandId,
                    SubmitId = token.SubmitId,
                    //Market = responseItem.MarketNo,
                    SecuCode = responseItem.StockCode,
                    //EntrustDirection = responseItem.EntrustDirection,
                    EEntrustPriceType = EntrustPriceTypeConverter.GetPriceType(responseItem.PriceType),
                    EntrustPrice = responseItem.EntrustPrice,
                    EntrustAmount = responseItem.EntrustAmount,
                    //EntrustStatus = responseItem.EntrustState,
                    DealAmount = responseItem.DealAmount,
                    DealMoney = responseItem.DealBalance,
                    DealTimes = responseItem.DealTimes,
                    DEntrustDate = DateUtil.GetDateTimeFromInt(responseItem.EntrustDate, responseItem.EntrustTime),
                    EntrustBatchNo = responseItem.BatchNo,
                    EntrustNo = responseItem.EntrustNo,
                    DeclareSeat = responseItem.ReportSeat,
                    DeclareNo = Convert.ToInt32(responseItem.ReportNo),
                    RequestId = responseItem.ExtSystemId,
                    FundCode = responseItem.AccountCode,
                    FundName = fundName,
                    PortfolioCode = portfolioCode,
                    PortfolioName = portfolioName,
                    EDirection = eDirection,
                    //EEntrustDirection = UFXTypeConverter.GetEntrustDirection(responseItem.EntrustDirection),
                    EMarketCode = UFXTypeConverter.GetMarketCode(responseItem.MarketNo),
                    EEntrustState = UFXTypeConverter.GetEntrustState(responseItem.EntrustState),
                    WithdrawCause = responseItem.WithdrawCause,
                    InstanceId = instanceId,
                    InstanceNo = instanceCode,
                };

                if (responseItem.FirstDealTime > 0)
                {
                    efItem.DFirstDealDate = DateUtil.GetDateTimeFromInt(responseItem.EntrustDate, responseItem.FirstDealTime);
                }

                efItem.ExchangeCode = UFXTypeConverter.GetMarketCode(efItem.EMarketCode);
                efItem.CommandNo = commandId;
                efItem.SubmitId = submitId;
                efItem.RequestId = requestId;

                entrustFlowItems.Add(efItem);
            }

            return entrustFlowItems;
        }
    }
}
