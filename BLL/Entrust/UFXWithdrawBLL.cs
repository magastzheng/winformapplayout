using BLL.UFX;
using BLL.UFX.impl;
using Config;
using DBAccess.EntrustCommand;
using log4net;
using Model.Binding.BindingUtil;
using Model.BLL;
using Model.Database;
using Model.UFX;
using System.Collections.Generic;
using System.Threading;

namespace BLL.Entrust
{
    public class UFXWithdrawBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;

        private EntrustDAO _entrustdao = new EntrustDAO();

        //TODO: 撤单需要等待所有单都完成之后，才可以继续进行
        private int _timeOut = 30 * 1000;

        public UFXWithdrawBLL()
        {
            _securityBLL = BLLManager.Instance.SecurityBLL;
            _timeOut = ConfigManager.Instance.GetDefaultSettingConfig().DefaultSetting.UFXSetting.Timeout;
        }

        /// <summary>
        /// Withdraw the entrusted securites.
        /// NOTE: the entrust_no is  necessary.
        /// </summary>
        /// <param name="submitId">The entrustsecurity SubmitId.</param>
        /// <param name="commandId">The tradingcommand CommandId.</param>
        /// <param name="entrustItems">The entrustsecurity item.</param>
        /// <param name="callerCallback"></param>
        /// <returns></returns>
        public BLLResponse Withdraw(int submitId, int commandId, List<EntrustSecurity> entrustItems, CallerCallback callerCallback)
        {
            BLLResponse bllResponse = new BLLResponse();

            List<UFXWithdrawRequest> requests = new List<UFXWithdrawRequest>();

            foreach (var entrustItem in entrustItems)
            {
                UFXWithdrawRequest request = new UFXWithdrawRequest
                {
                    EntrustNo = entrustItem.EntrustNo,
                };

                requests.Add(request);
            }

            Callbacker callbacker = new Callbacker
            {
                Token = new CallerToken
                {
                    SubmitId = submitId,
                    CommandId = commandId,
                    WaitEvent = new AutoResetEvent(false),
                    Caller = callerCallback,
                },

                DataHandler = WithdrawDataHandler,
            };

            var result = _securityBLL.Withdraw(requests, callbacker);
            if (result == Model.ConnectionCode.Success)
            {
                if (callbacker.Token.WaitEvent.WaitOne(_timeOut))
                {
                    var errorResponse = callbacker.Token.ErrorResponse as UFXErrorResponse;
                    if (errorResponse != null && T2ErrorHandler.Success(errorResponse.ErrorCode))
                    {
                        bllResponse.Code = Model.ConnectionCode.Success;
                        bllResponse.Message = "Success Withdraw";
                    }
                    else
                    {
                        bllResponse.Code = Model.ConnectionCode.FailWithdraw;
                        bllResponse.Message = errorResponse.ErrorMessage;
                    }

                }
                else
                {
                    bllResponse.Code = Model.ConnectionCode.FailTimeoutWithdraw;
                    bllResponse.Message = "Fail to submit the basket withdraw to UFX: Timeout";
                }
            }
            else
            {
                bllResponse.Code = Model.ConnectionCode.FailSubmit;
                bllResponse.Message = "Fail to submit the basket withdraw to UFX!";
            }

            return bllResponse;
        }

        private int WithdrawDataHandler(CallerToken token, DataParser dataParser)
        {
            List<UFXBasketWithdrawResponse> responseItems = new List<UFXBasketWithdrawResponse>();

            var errorResponse = T2ErrorHandler.Handle(dataParser);
            token.ErrorResponse = errorResponse;

            //Verify the dataParser before reading the data.
            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                if (dataParser.DataSets.Count > 1)
                {
                    var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXBasketWithdrawResponse>();
                    for (int i = 1, count = dataParser.DataSets.Count; i < count; i++)
                    {
                        var dataSet = dataParser.DataSets[i];
                        foreach (var dataRow in dataSet.Rows)
                        {
                            UFXBasketWithdrawResponse p = new UFXBasketWithdrawResponse();
                            UFXDataSetHelper.SetValue<UFXBasketWithdrawResponse>(ref p, dataRow.Columns, dataFieldMap);
                            responseItems.Add(p);
                        }
                    }
                }
            }

            int ret = -1;
            List<EntrustSecurity> entrustSecuItems = new List<EntrustSecurity>();
            if (token.SubmitId > 0)
            {
                //TODO: check the withdraw status
                foreach (var responseItem in responseItems)
                {
                    var entrustItem = new EntrustSecurity
                    {
                        SubmitId = token.SubmitId,
                        CommandId = token.CommandId,
                        SecuCode = responseItem.StockCode,
                        EntrustNo = responseItem.EntrustNo,
                    };

                    entrustSecuItems.Add(entrustItem);
                }

                if (entrustSecuItems.Count > 0)
                {
                    ret = _entrustdao.UpdateSecurityEntrustStatus(entrustSecuItems, Model.EnumType.EntrustStatus.CancelSuccess);
                }
            }

            try
            {
                if (token.Caller != null)
                {
                    token.Caller(token, entrustSecuItems, errorResponse);
                }
            }
            finally
            {
                if (token.WaitEvent != null)
                {
                    token.WaitEvent.Set();
                }
            }

            return ret;
        }
    }
}
