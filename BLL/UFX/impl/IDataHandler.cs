
using Model.UFX;
namespace BLL.UFX.impl
{
    public delegate int DataHandlerCallback(UFXFunctionCode functionCode, int hSend, DataParser dataParser);

    public interface IDataHandler
    {
        int Handle(UFXFunctionCode functionCode, int hSend, DataParser dataParser);
    }
}
