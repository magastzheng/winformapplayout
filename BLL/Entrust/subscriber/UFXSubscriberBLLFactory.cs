using Model.UFX;

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
                        bll = new UFXEntrustCommitBLL();
                    }
                    break;
                case UFXPushMessageType.EntrustFailed:
                    {
                        bll = new UFXEntrustFailedBLL();
                    }
                    break;
                case UFXPushMessageType.EntrustWithdrawDone:
                    {
                        bll = new UFXWithdrawCompletedBLL();
                    }
                    break;
                case UFXPushMessageType.EntrustWithdrawFailed:
                    {
                        bll = new UFXWithdrawFailedBLL();
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
