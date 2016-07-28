using BLL.UFX;
using BLL.UFX.impl;
using log4net;
using Model.Binding.BindingUtil;
using Model.t2sdk;
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

        public UFXQueryHoldingBLL()
        {
            _securityBLL = BLLManager.Instance.SecurityBLL;
        }

        public int Query()
        {
            int ret = -1;

            //var portfolio = LoginManager.Instance.GetPortfolio(tradeCommandItem.PortfolioCode);
            //var stockholder = LoginManager.Instance.GetHolder(tradeCommandItem.

            var ufxRequests = new List<UFXHoldingRequest>();
            var ufxRequest = new UFXHoldingRequest 
            {
                CombiNo = "30",
            };

            ufxRequests.Add(ufxRequest);

            Callbacker callbacker = new Callbacker
            {
                Token = new CallerToken
                {
                    SubmitId = 90000,
                    CommandId = 90001,
                    WaitEvent = new AutoResetEvent(false),
                },

                DataHandler = DataHandlerCallback,
            };

            var result = _securityBLL.QueryHolding(ufxRequests, callbacker);

            if (result == Model.ConnectionCode.Success)
            {
                callbacker.Token.WaitEvent.WaitOne();
                var errorResponse = callbacker.Token.OutArgs as UFXErrorResponse;
                if (errorResponse != null && T2ErrorHandler.Success(errorResponse.ErrorCode))
                {
                    ret = 1;
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

            if (token.WaitEvent != null)
            {
                token.WaitEvent.Set();
            }

            return ret;
        }
    }
}
