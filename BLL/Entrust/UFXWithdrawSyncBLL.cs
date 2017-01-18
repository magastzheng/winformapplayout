using BLL.Manager;
using BLL.UFX.impl;
using log4net;
using Model.BLL;
using Model.Database;
using Model.UFX;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Entrust
{
    public class UFXWithdrawSyncBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private WithdrawSyncBLL _withdrawSyncBLL = null;

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

            return GetResponse(responseItems);
        }

        public BLLResponse WithdrawBasket(Model.Database.EntrustCommand cmdItem)
        {
            UFXBasketWithdrawRequest request = new UFXBasketWithdrawRequest
            {
                BatchNo = cmdItem.BatchNo,
            };

            var responseItems = _withdrawSyncBLL.WithdrawBasket(request);

            return GetResponse(responseItems);
        }

        private BLLResponse GetResponse(List<UFXBasketWithdrawResponse> responseItems)
        {
            BLLResponse bllResponse = null;

            var successItems = responseItems.Where(p => p.SuccessFlag.Equals("1")).ToList();
            var failItems = responseItems.Where(p => p.SuccessFlag.Equals("2")).ToList();
            if (responseItems.Count > 0 && ((successItems.Count == responseItems.Count) || failItems.Count == 0))
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
