using BLL.UFX.impl;
using System;
namespace BLL.Entrust.subscriber
{
    public interface IUFXSubsriberBLLBase
    {
        int Handle(DataParser dataParser);
    }
}
