using Model.UFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFX.subscriber
{
    /// <summary>
    /// The interface will be used to create the handler for the different type of message.
    /// </summary>
    public interface IUFXMessageHandlerFactory
    {
        IUFXMessageHandlerBase Create(UFXPushMessageType msgType);
    }
}
