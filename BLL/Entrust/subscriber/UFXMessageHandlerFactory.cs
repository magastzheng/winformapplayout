using log4net;
using Model.UFX;
using System;
using UFX.subscriber;

namespace BLL.Entrust.subscriber
{
    public class UFXMessageHandlerFactory : IUFXMessageHandlerFactory
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                case UFXPushMessageType.EntrustConfirm:
                    {
                        handler = new UFXEntrustConfirmHandler();
                    }
                    break;
                case UFXPushMessageType.EntrustFailed:
                    {
                        handler = new UFXEntrustFailedHandler();
                    }
                    break;
                case UFXPushMessageType.EntrustWithdraw:
                    {
                        handler = new UFXWithdrawHandler();
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
                        string msg = string.Format("Not supported the message type: {0}", msgType);
                        logger.Error(msg);

                        throw new NotSupportedException(msg);
                    }
            }

            return handler;
        }
    }
}
