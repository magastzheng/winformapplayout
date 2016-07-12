
namespace BLL.UFX.impl
{
    public delegate void DataHandlerCallback(DataParser dataParser);

    public delegate int DataHandlerCallback2(int tokenId, DataParser dataParser);

    public class Callbacker
    {
        public int TokenId { get; set; }
        public DataHandlerCallback2 Callback { get; set; }
    }

    public interface IDataHandler
    {
        void Handle(DataParser dataParser);
    }
}
