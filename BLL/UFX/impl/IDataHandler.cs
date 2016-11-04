
using Model;
using System.Threading;
namespace BLL.UFX.impl
{
    public delegate int DataHandlerCallback(FunctionCode functionCode, int hSend, DataParser dataParser);

    public interface IDataHandler
    {
        int Handle(FunctionCode functionCode, int hSend, DataParser dataParser);
    }
}
