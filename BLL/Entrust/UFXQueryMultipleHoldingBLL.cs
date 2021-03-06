﻿using BLL.Manager;
using log4net;
using Model.UFX;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UFX;
using UFX.impl;

namespace BLL.Entrust
{
    public class UFXQueryMultipleHoldingBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;
        private int _timeOut = 30 * 1000;

        public UFXQueryMultipleHoldingBLL()
        {
            _securityBLL = UFXBLLManager.Instance.SecurityBLL;
            _timeOut = SettingManager.Instance.Get().UFXSetting.Timeout;
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

            var result = _securityBLL.QueryMultipleHolding(ufxRequests, callbacker);

            if (result == Model.ConnectionCode.Success)
            {
                if (callbacker.Token.WaitEvent.WaitOne(_timeOut))
                {
                    var errorResponse = callbacker.Token.ErrorResponse as UFXErrorResponse;
                    if (errorResponse != null && T2ErrorHandler.Success(errorResponse.ErrorCode))
                    {
                        ret = 1;
                    }
                }
                else
                {
                    ret = -1;
                }
            }

            return ret;
        }

        private int DataHandlerCallback(CallerToken token, DataParser dataParser)
        {
            int ret = -1;
            
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            token.ErrorResponse = errorResponse;

            List<UFXMultipleHoldingResponse> responseItems = new List<UFXMultipleHoldingResponse>();
            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                responseItems = UFXDataSetHelper.ParseData<UFXMultipleHoldingResponse>(dataParser);
                
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
