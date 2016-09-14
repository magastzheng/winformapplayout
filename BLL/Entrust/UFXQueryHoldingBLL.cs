using BLL.Product;
using BLL.UFX;
using BLL.UFX.impl;
using log4net;
using Model;
using Model.Binding.BindingUtil;
using Model.BLL;
using Model.UFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public int Query(CallerCallback callback)
        {
            int ret = -1;

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
                        WaitEvent = new AutoResetEvent(false),
                        Caller = callback,
                    },

                    DataHandler = DataHandlerCallback,
                };

                var result = _securityBLL.QueryHolding(ufxRequests, callbacker);

                BLLResponse bllResponse = new BLLResponse();
                if (result == Model.ConnectionCode.Success)
                {
                    callbacker.Token.WaitEvent.WaitOne(_timeOut);
                    var errorResponse = callbacker.Token.OutArgs as UFXErrorResponse;
                    if (errorResponse != null && T2ErrorHandler.Success(errorResponse.ErrorCode))
                    {
                        ret = 1;
                        bllResponse.Code = ConnectionCode.Success;
                        bllResponse.Message = "Success QueryHolding";
                    }
                    else
                    {
                        bllResponse.Code = ConnectionCode.FailEntrust;
                        bllResponse.Message = "Fail QueryHolding: " + errorResponse.ErrorMessage;
                    }
                }
                else
                {
                    bllResponse.Code = result;
                    bllResponse.Message = "Fail to QueryHolding in ufx.";
                }
            }
        
            return ret;
        }

        private int DataHandlerCallback(CallerToken token, DataParser dataParser)
        {
            int ret = -1;
            
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            token.OutArgs = errorResponse;

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
