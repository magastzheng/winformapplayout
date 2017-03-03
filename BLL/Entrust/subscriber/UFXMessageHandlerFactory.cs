using Model.UFX;
using System;
using UFX.subscriber;

namespace BLL.Entrust.subscriber
{
    public class UFXMessageHandlerFactory : IUFXMessageHandlerFactory
    {
        public IUFXMessageHandlerBase Create(UFXPushMessageType msgType)
        {
            IUFXMessageHandlerBase handler = null;
            switch (msgType)
            {
                case UFXPushMessageType.EntrustCommit:
                    {
                        handler = new UFXEntrustCommitHandler();
                    }
                    break;
                case UFXPushMessageType.EntrustFailed:
                    {
                        handler = new UFXEntrustFailedHandler();
                    }
                    break;
                case UFXPushMessageType.EntrustWithdrawDone:
                    {
                        handler = new UFXWithdrawCompletedHandler();
                    }
                    break;
                case UFXPushMessageType.EntrustWithdrawFailed:
                    {
                        handler = new UFXWithdrawFailedHandler();
                    }
                    break;
                case UFXPushMessageType.EntrustDeal:
                    {
                        handler = new UFXEntrustDealHandler();
                    }
                    break;
                default:
                    {
                        throw new NotSupportedException(msgType.ToString());
                    }
                    break;
            }

            return handler;
        }
    }
}
