
using System.Threading;
namespace BLL.UFX.impl
{
    public delegate void DataHandlerCallback(DataParser dataParser);

    public interface IDataHandler
    {
        void Handle(DataParser dataParser);
    }
}
