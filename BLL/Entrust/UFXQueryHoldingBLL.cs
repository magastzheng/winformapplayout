using BLL.Product;
using BLL.UFX;
using BLL.UFX.impl;
using log4net;
using Model;
using Model.Binding.BindingUtil;
using Model.BLL;
using Model.UFX;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BLL.Entrust
{
    public class UFXQueryHoldingBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;
        private ProductBLL _productBLL = new ProductBLL();
        private int _timeOut = 30 * 1000;

        public UFXQueryHoldingBLL()
        {
            _securityBLL = BLLManager.Instance.SecurityBLL;
        }

        public List<UFXHoldingResponse> Query(CallerCallback callback)
        {
            List<UFXHoldingResponse> holdingItems = new List<UFXHoldingResponse>();
            var portfolios = _productBLL.GetAll();
            foreach (var portfolio in portfolios)
            {
                var ufxRequests = new List<UFXHoldingRequest>();
                var ufxRequest = new UFXHoldingRequest
                {
                    CombiNo = portfolio.PortfolioNo,
                };

                ufxRequests.Add(ufxRequest);

                Callbacker callbacker = new Callbacker
                {
                    Token = new CallerToken
                    {
                        SubmitId = 90000,
                        CommandId = 90001,
                        InArgs = portfolio.PortfolioNo,
                        OutArgs = holdingItems,
                        WaitEvent = new AutoResetEvent(false),
                        Caller = callback,
                    },

                    DataHandler = DataHandlerCallback,
                };

                var result = _securityBLL.QueryHolding(ufxRequests, callbacker);

                BLLResponse bllResponse = new BLLResponse();
                if (result == Model.ConnectionCode.Success)
                {
                    if (callbacker.Token.WaitEvent.WaitOne(_timeOut))
                    {
                        var errorResponse = callbacker.Token.ErrorResponse as UFXErrorResponse;
                        if (errorResponse != null && T2ErrorHandler.Success(errorResponse.ErrorCode))
                        {
                            bllResponse.Code = ConnectionCode.Success;
                            bllResponse.Message = "Success QueryHolding";
                        }
                        else
                        {
                            bllResponse.Code = ConnectionCode.FailQueryHolding;
                            bllResponse.Message = "Fail QueryHolding: " + errorResponse.ErrorMessage;
                        }
                    }
                    else
                    { 
                        bllResponse.Code = ConnectionCode.FailTimeoutQueryHolding;
                        bllResponse.Message = "Fail QueryHolding: Timeout";
                    }
                }
                else
                {
                    bllResponse.Code = result;
                    bllResponse.Message = "Fail to QueryHolding in ufx.";
                }
            }
        
            return holdingItems;
        }

        private int DataHandlerCallback(CallerToken token, DataParser dataParser)
        {
            int ret = -1;
            
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            token.ErrorResponse = errorResponse;

            List<UFXHoldingResponse> responseItems = new List<UFXHoldingResponse>();
            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXHoldingResponse>();
                for (int i = 1, count = dataParser.DataSets.Count; i < count; i++)
                {
                    var dataSet = dataParser.DataSets[i];
                    foreach (var dataRow in dataSet.Rows)
                    {
                        UFXHoldingResponse p = new UFXHoldingResponse();
                        UFXDataSetHelper.SetValue<UFXHoldingResponse>(ref p, dataRow.Columns, dataFieldMap);
                        responseItems.Add(p);
                    }
                }

                var futures = responseItems.Where(p => p.MarketNo == "7").ToList();

                var validItems = responseItems.Where(p => p.CurrentAmount > 0).ToList();

                if (validItems != null && validItems.Count > 0 
                    && token.OutArgs != null && token.OutArgs is List<UFXHoldingResponse>)
                {
                    ((List<UFXHoldingResponse>)token.OutArgs).AddRange(validItems);
                }
            }
            else
            {
                ret = -1;
            }

            if (token.Caller != null)
            {
                token.Caller(token, null, errorResponse);
            }

            if (token.WaitEvent != null)
            {
                token.WaitEvent.Set();
            }

            return ret;
        }
    }
}
