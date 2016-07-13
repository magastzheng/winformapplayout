
namespace BLL.UFX.impl
{
    public delegate void DataHandlerCallback(DataParser dataParser);

    public delegate int DataHandlerCallback2(CallbackToken token, DataParser dataParser);

    public class CallbackToken
    {
        public int SubmitId { get; set; }

        public int CommandId { get; set; }
    }

    public class Callbacker
    {
        public CallbackToken Token { get; set; }
        public DataHandlerCallback2 Callback { get; set; }
    }

    public interface IDataHandler
    {
        void Handle(DataParser dataParser);
    }
}
