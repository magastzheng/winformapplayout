using BLL.EntrustCommand;
using BLL.Product;
using BLL.UFX;
using BLL.UFX.impl;
using Config;
using log4net;
using Model;
using Model.Binding.BindingUtil;
using Model.BLL;
using Model.Converter;
using Model.EnumType.EnumTypeConverter;
using Model.UFX;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Util;

namespace BLL.Entrust
{
    public class UFXQueryEntrustBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;
        private ProductBLL _productBLL = new ProductBLL();
        private EntrustSecurityBLL _entrustSecuBLL = new EntrustSecurityBLL();

        private int _timeOut = 30 * 1000;

        public UFXQueryEntrustBLL()
        {
            this._securityBLL = BLLManager.Instance.SecurityBLL;
            _timeOut = ConfigManager.Instance.GetDefaultSettingConfig().DefaultSetting.UFXSetting.Timeout;
        }

        public List<EntrustFlowItem> QueryToday(CallerCallback callback)
        {
            List<EntrustFlowItem> entrustItems = new List<EntrustFlowItem>();
            var portfolios = _productBLL.GetAll();
            foreach (var portfolio in portfolios)
            {
                List<UFXQueryEntrustRequest> requests = new List<UFXQueryEntrustRequest>();

                UFXQueryEntrustRequest request = new UFXQueryEntrustRequest();
                //request.RequestNum = 9000;
                request.AccountCode = portfolio.FundCode;
                request.AssetNo = portfolio.AssetNo;
                request.CombiNo = portfolio.PortfolioNo;
                requests.Add(request);

                Callbacker callbacker = new Callbacker
                {
                    Token = new CallerToken
                    {
                        SubmitId = 11111,
                        CommandId = 22222,
                        InArgs = portfolio.PortfolioNo,
                        OutArgs = entrustItems,
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

        public List<EntrustFlowItem> QueryHistory(DateTime startDate, DateTime endDate, CallerCallback callback)
        {
            List<EntrustFlowItem> entrustItems = new List<EntrustFlowItem>();
            int intStart = DateUtil.GetIntDate(startDate);
            int intEnd = DateUtil.GetIntDate(endDate);
            var portfolios = _productBLL.GetAll();
            foreach (var portfolio in portfolios)
            {
                List<UFXQueryHistEntrustRequest> requests = new List<UFXQueryHistEntrustRequest>();

                UFXQueryHistEntrustRequest request = new UFXQueryHistEntrustRequest();
                request.StartDate = intStart;
                request.EndDate = intEnd;
                request.CombiNo = portfolio.PortfolioNo;

                requests.Add(request);

                Callbacker callbacker = new Callbacker
                {
                    Token = new CallerToken
                    {
                        SubmitId = 333333,
                        CommandId = 444444,
                        InArgs = portfolio.PortfolioNo,
                        OutArgs = entrustItems,
                        Caller = callback,
                    },

                    DataHandler = QueryDataHandler,
                };

                var result = _securityBLL.QueryEntrustHistory(requests, callbacker);

                BLLResponse bllResponse = new BLLResponse();
                if (result == Model.ConnectionCode.Success)
                {
                    if (callbacker.Token.WaitEvent.WaitOne(_timeOut))
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
            List<UFXQueryEntrustResponse> responseItems = new List<UFXQueryEntrustResponse>();
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            token.ErrorResponse = errorResponse;

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

        private List<EntrustFlowItem> GetFlowItems(CallerToken token, List<UFXQueryEntrustResponse> responseItems)
        {
            var entrustFlowItems = new List<EntrustFlowItem>();
            if (responseItems == null || responseItems.Count == 0)
            {
                return entrustFlowItems;
            }

            var entrustSecuItem = _entrustSecuBLL.GetAllCombine();
            foreach (var responseItem in responseItems)
            {
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
                    DFirstDealDate = DateUtil.GetDateTimeFromInt(responseItem.EntrustDate, responseItem.FirstDealTime),
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
                    WithdrawCause = responseItem.WithdrawCause,
                };

                efItem.ExchangeCode = UFXTypeConverter.GetMarketCode(efItem.EMarketCode);

                var findItem = entrustSecuItem.Find(p => p.SecuCode.Equals(efItem.SecuCode) && p.EntrustNo == efItem.EntrustNo);
                if (findItem != null)
                {
                    efItem.CommandNo = findItem.CommandId;
                    efItem.SubmitId = findItem.SubmitId;
                    efItem.EDirection = findItem.EntrustDirection;
                }
                else
                {
                    efItem.CommandNo = -1;
                    efItem.SubmitId = -1;
                }

                entrustFlowItems.Add(efItem);
            }

            return entrustFlowItems;
        }
    }
}
