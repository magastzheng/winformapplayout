using BLL.EntrustCommand;
using BLL.Manager;
using log4net;
using Model.BLL;
using Model.Database;
using Model.UFX;
using System.Collections.Generic;
using UFX.impl;

namespace BLL.Entrust
{
    public class UFXWithdrawSyncBLL
    {
        private const string SuccessFlag = "1";
        private const string FailFlag = "2";

        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private WithdrawSyncBLL _withdrawSyncBLL = null;
        private EntrustCombineBLL _entrustCombineBLL = new EntrustCombineBLL();

        public UFXWithdrawSyncBLL()
        {
            _withdrawSyncBLL = UFXBLLManager.Instance.WithdrawSyncBLL;
        }

        public BLLResponse Withdraw(int submitId, int commandId, List<EntrustSecurity> entrustItems)
        {
            List<UFXWithdrawRequest> requests = new List<UFXWithdrawRequest>();
            foreach (var entrustItem in entrustItems)
            {
                UFXWithdrawRequest request = new UFXWithdrawRequest
                {
                    EntrustNo = entrustItem.EntrustNo,
                };

                requests.Add(request);
            }

            var responseItems = _withdrawSyncBLL.Withdraw(requests);

            return GetResponse(submitId, commandId, responseItems);
        }

        public BLLResponse WithdrawBasket(Model.Database.EntrustCommand cmdItem)
        {
            UFXBasketWithdrawRequest request = new UFXBasketWithdrawRequest
            {
                BatchNo = cmdItem.BatchNo,
            };

            var responseItems = _withdrawSyncBLL.WithdrawBasket(request);

            return GetResponse(cmdItem.SubmitId, cmdItem.CommandId, responseItems);
        }

        private BLLResponse GetResponse(int submitId, int commandId, List<UFXBasketWithdrawResponse> responseItems)
        {
            BLLResponse bllResponse = null;

            List<EntrustSecurity> successCancelSecuItems = new List<EntrustSecurity>();
            List<EntrustSecurity> failCancelSecuItems = new List<EntrustSecurity>();
            int ret = -1;
            if (submitId > 0)
            {
                //TODO: check the withdraw status
                foreach (var responseItem in responseItems)
                {
                    var entrustItem = new EntrustSecurity
                    {
                        SubmitId = submitId,
                        CommandId = commandId,
                        SecuCode = responseItem.StockCode,
                        EntrustNo = responseItem.EntrustNo,
                    };

                    if (FailFlag.Equals(responseItem.SuccessFlag))
                    {
                        failCancelSecuItems.Add(entrustItem);
                    }
                    else
                    {
                        successCancelSecuItems.Add(entrustItem);
                    }
                }

                if (successCancelSecuItems.Count > 0)
                {
                    ret = _entrustCombineBLL.UpdateSecurityEntrustStatus(successCancelSecuItems, Model.EnumType.EntrustStatus.CancelSuccess);
                }

                if (failCancelSecuItems.Count > 0)
                {
                    ret = _entrustCombineBLL.UpdateSecurityEntrustStatus(successCancelSecuItems, Model.EnumType.EntrustStatus.CancelFail);
                }
            }

            //var successItems = responseItems.Where(p => SuccessFlag.Equals(p.SuccessFlag)).ToList();
            //var failItems = responseItems.Where(p => FailFlag.Equals(p.SuccessFlag)).ToList();
            if (successCancelSecuItems.Count == responseItems.Count)
            {
                bllResponse = new BLLResponse { Code = Model.ConnectionCode.Success, Message = "Withdraw success!" };
            }
            else
            {
                bllResponse = new BLLResponse { Code = Model.ConnectionCode.FailWithdraw, Message = "Withdraw failure!" };
            }

            return bllResponse;
        }
    }
}
