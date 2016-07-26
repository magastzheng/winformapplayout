
using System.Threading;
namespace BLL.UFX.impl
{
    public delegate int DataHandlerCallback(DataParser dataParser);

    public interface IDataHandler
    {
        int Handle(DataParser dataParser);
    }
}
