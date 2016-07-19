using Model.t2sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entrust.subscriber
{
    public class UFXSubscriberBLLFactory
    {
        public static IUFXSubsriberBLLBase Create(PushMessageType msgType)
        {
            IUFXSubsriberBLLBase bll = null;
            switch (msgType)
            {
                case PushMessageType.EntrustCommit:
                    {
                        bll = new UFXEntrustCompletedBLL();
                    }
                    break;
                case PushMessageType.EntrustWithdrawDone:
                    {
                        bll = new UFXWithdrawCompletedBLL();
                    }
                    break;
                case PushMessageType.EntrustDeal:
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
