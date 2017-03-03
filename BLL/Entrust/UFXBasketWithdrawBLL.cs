using BLL.EntrustCommand;
using BLL.Manager;
using log4net;
using Model.BLL;
using Model.Database;
using Model.UFX;
using System.Collections.Generic;
using System.Threading;
using UFX;
using UFX.impl;

namespace BLL.Entrust
{
    public class UFXBasketWithdrawBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;
        private EntrustCommandBLL _entrustCommandBLL = new EntrustCommandBLL();
        private EntrustCombineBLL _entrustCombineBLL = new EntrustCombineBLL();
        //TODO:撤销是否要等到有返回结果之后，才可以继续进行。
        private int _timeOut = 30 * 1000;

        private readonly object _locker = new object();
        private Dictionary<int, List<UFXBasketWithdrawResponse>> _responseDataMap = new Dictionary<int, List<UFXBasketWithdrawResponse>>();
        private Dictionary<int, UFXErrorResponse> _responseErrorMap = new Dictionary<int, UFXErrorResponse>();

        public UFXBasketWithdrawBLL()
        {
            _securityBLL = UFXBLLManager.Instance.SecurityBLL;
            _timeOut = SettingManager.Instance.Get().UFXSetting.Timeout;
        }

        public BLLResponse Withdraw(Model.Database.EntrustCommand cmdItem, CallerCallback callerCallback)
        {
            BLLResponse bllResponse = new BLLResponse();

            UFXBasketWithdrawRequest request = new UFXBasketWithdrawRequest
            {
                BatchNo = cmdItem.BatchNo,
            };

            Callbacker callbacker = new Callbacker
            {
                Token = new CallerToken
                {
                    SubmitId = cmdItem.SubmitId,
                    CommandId = cmdItem.CommandId,
                    WaitEvent = new AutoResetEvent(false),
                    Caller = callerCallback,
                },

                DataHandler = WithdrawDataHandler,
            };

            var result = _securityBLL.WithdrawBasket(request, callbacker);
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
                    bllResponse.Message = "Fail to submit the basket withdraw to UFX!";
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
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            token.ErrorResponse = errorResponse;

            List<UFXBasketWithdrawResponse> responseItems = UFXDataSetHelper.ParseData<UFXBasketWithdrawResponse>(dataParser);

            //TODO: It needs to verify the response data. Only the can set cancel successfully in those without no error.
            int ret = -1;
            List<EntrustSecurity> entrustSecuItems = new List<EntrustSecurity>();
            if (token.SubmitId > 0)
            {
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
                    ret = _entrustCombineBLL.UpdateSecurityEntrustStatus(entrustSecuItems, Model.EnumType.EntrustStatus.CancelSuccess);
                    ret = _entrustCommandBLL.UpdateEntrustCommandStatus(token.SubmitId, Model.EnumType.EntrustStatus.CancelSuccess);
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
