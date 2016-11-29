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

namespace BLL.Entrust
{
    public class UFXQueryDealBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        private ProductBLL _productBLL = new ProductBLL();
        private SecurityBLL _securityBLL = null;
        private int _timeOut = 30 * 1000;

        public UFXQueryDealBLL()
        {
            _securityBLL = BLLManager.Instance.SecurityBLL;
            _timeOut = ConfigManager.Instance.GetDefaultSettingConfig().DefaultSetting.UFXSetting.Timeout;
        }

        public int QueryToday(CallerCallback callback)
        {
            var portfolios = _productBLL.GetAll();
            foreach (var portfolio in portfolios)
            {
                List<UFXQueryDealRequest> requests = new List<UFXQueryDealRequest>();

                UFXQueryDealRequest request = new UFXQueryDealRequest();

                //request.AccountCode = "850010";
                //request.CombiNo = "10000000";
                request.AccountCode = portfolio.FundCode;
                request.CombiNo = portfolio.PortfolioNo;
                requests.Add(request);

                Callbacker callbacker = new Callbacker
                {
                    Token = new CallerToken
                    {
                        SubmitId = 11111,
                        CommandId = 22222,
                        InArgs = request.CombiNo,
                        WaitEvent = new AutoResetEvent(false),
                        Caller = callback,
                    },

                    DataHandler = QueryDataHandler,
                };

                var result = _securityBLL.QueryDeal(requests, callbacker);

                BLLResponse bllResponse = new BLLResponse();
                if (result == Model.ConnectionCode.Success)
                {
                    if (callbacker.Token.WaitEvent.WaitOne(_timeOut))
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
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            token.ErrorResponse = errorResponse;

            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
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
            }

            try
            {
                if (token.Caller != null)
                {
                    var dealFlowItems = new List<DealFlowItem>();
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
    }
}
