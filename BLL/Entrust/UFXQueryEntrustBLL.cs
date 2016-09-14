using BLL.Product;
using BLL.SecurityInfo;
using BLL.UFX;
using BLL.UFX.impl;
using log4net;
using Model;
using Model.Binding.BindingUtil;
using Model.BLL;
using Model.Converter;
using Model.Data;
using Model.UFX;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Util;

namespace BLL.Entrust
{
    public class UFXQueryEntrustBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;
        private ProductBLL _productBLL = new ProductBLL();
        private int _timeOut = 30 * 1000;

        public UFXQueryEntrustBLL()
        {
            this._securityBLL = BLLManager.Instance.SecurityBLL;
        }

        public List<EntrustFlowItem> QueryToday(CallerCallback callback)
        {
            var portfolios = _productBLL.GetAll();
            foreach (var portfolio in portfolios)
            {
                List<UFXQueryEntrustRequest> requests = new List<UFXQueryEntrustRequest>();

                UFXQueryEntrustRequest request = new UFXQueryEntrustRequest();
                //request.RequestNum = 9000;
                request.CombiNo = portfolio.PortfolioNo;
                requests.Add(request);

                Callbacker callbacker = new Callbacker
                {
                    Token = new CallerToken
                    {
                        SubmitId = 11111,
                        CommandId = 22222,
                        InArgs = portfolio.PortfolioNo,
                        WaitEvent = new AutoResetEvent(false),
                        Caller = callback,
                    },

                    DataHandler = QueryDataHandler,
                };

                var result = _securityBLL.QueryEntrust(requests, callbacker);

                BLLResponse bllResponse = new BLLResponse();
                if (result == Model.ConnectionCode.Success)
                {
                    if (callbacker.Token.WaitEvent.WaitOne(_timeOut))
                    {
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
           
            return null;
        }

        public int QueryHistory(DateTime startDate, DateTime endDate, CallerCallback callback)
        {
            List<UFXQueryHistEntrustRequest> requests = new List<UFXQueryHistEntrustRequest>();

            int intStart = DateUtil.GetIntDate(startDate);
            int intEnd = DateUtil.GetIntDate(endDate);
            var portfolios = _productBLL.GetAll();
            foreach (var portfolio in portfolios)
            {
                UFXQueryHistEntrustRequest request = new UFXQueryHistEntrustRequest();
                request.StartDate = intStart;
                request.EndDate = intEnd;
                request.CombiNo = portfolio.PortfolioNo;

                requests.Add(request);
            }

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

            var result = _securityBLL.QueryEntrustHistory(requests, callbacker);

            return 1;
        }

        private int QueryDataHandler(CallerToken token, DataParser dataParser)
        {
            List<UFXQueryEntrustResponse> responseItems = new List<UFXQueryEntrustResponse>();
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            token.OutArgs = errorResponse;

            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
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
            }

            if (token.Caller != null)
            {
                var entrustFlowItems = new List<EntrustFlowItem>();
                foreach (var responseItem in responseItems)
                {
                    EntrustFlowItem efItem = new EntrustFlowItem 
                    { 
                        CommandNo = token.CommandId,
                        //Market = responseItem.MarketNo,
                        SecuCode = responseItem.StockCode,
                        //EntrustDirection = responseItem.EntrustDirection,
                        PriceType = responseItem.PriceType,
                        EntrustPrice = responseItem.EntrustPrice,
                        EntrustAmount = responseItem.EntrustAmount,
                        //EntrustStatus = responseItem.EntrustState,
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
                        RequestId = responseItem.ExtSystemId,
                        FundCode = responseItem.AccountCode,
                        PortfolioCode = (string)token.InArgs,
                        EEntrustDirection = UFXTypeConverter.GetEntrustDirection(responseItem.EntrustDirection),
                        EMarketCode = UFXTypeConverter.GetMarketCode(responseItem.MarketNo),
                        EEntrustState = UFXTypeConverter.GetEntrustState(responseItem.EntrustState),
                    };

                    entrustFlowItems.Add(efItem);
                }

                token.Caller(token, entrustFlowItems, errorResponse);
            }

            if (token.WaitEvent != null)
            {
                token.WaitEvent.Set();
            }

            return responseItems.Count();
        }
    }
}
