
namespace BLL.UFX.impl
{
    public delegate void DataHandlerCallback(DataParser dataParser);

    public delegate int UFXDataHandlerCallback(CallerToken token, DataParser dataParser);

    public delegate int CallerCallback(CallerToken token, object data);

    public class CallerToken
    {
        public int SubmitId { get; set; }

        public int CommandId { get; set; }

        public CallerCallback Caller { get; set; }
    }

    public class Callbacker
    {
        public CallerToken Token { get; set; }
        public UFXDataHandlerCallback Callback { get; set; }
    }

    public interface IDataHandler
    {
        void Handle(DataParser dataParser);
    }
}
