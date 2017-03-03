using UFX.impl;

namespace UFX.subscriber
{
    /// <summary>
    /// The UFX message center will publish some types of the message. It will be implemented the message handler for each type
    /// of the message. Here define the interface to decoupling the module.
    /// </summary>
    public interface IUFXMessageHandlerBase
    {
        int Handle(DataParser dataParser);
    }
}
