using Model.UFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entrust.subscriber
{
    public class UFXSubscriberBLLFactory
    {
        public static IUFXSubsriberBLLBase Create(UFXPushMessageType msgType)
        {
            IUFXSubsriberBLLBase bll = null;
            switch (msgType)
            {
                case UFXPushMessageType.EntrustCommit:
                    {
                        bll = new UFXEntrustCompletedBLL();
                    }
                    break;
                case UFXPushMessageType.EntrustWithdrawDone:
                    {
                        bll = new UFXWithdrawCompletedBLL();
                    }
                    break;
                case UFXPushMessageType.EntrustDeal:
                    {
                        bll = new UFXEntrustDealBLL();
                    }
                    break;
                default:
                    break;
            }

            return bll;
        }
    }
}
