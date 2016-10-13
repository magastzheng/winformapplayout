using log4net;
using Quote;
using Quote.TDF;
using System.Threading;

namespace Service
{
    public class QuoteService : IService
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //private bool _terminal = false;
        //private EventWaitHandle _waitHandle = new AutoResetEvent(false);
        private object _locker = new object();
        private Thread _startThread = null;
        private Thread _stopThread = null;

        private TDFQuote _tdfQuote = null;

        public QuoteService(IQuote quote)
        {
            _tdfQuote = new TDFQuote(quote);
        }

        #region IService Interface

        public void Start()
        {
            _startThread = new Thread(InitServiceThread);
            _startThread.Start(1000);
        }

        public void Stop()
        {
            _stopThread = new Thread(StopService);
            _stopThread.Start();
        }

        #endregion

        private void InitServiceThread(object obj)
        {
            int sleepTime = (int)obj;

            Thread.Sleep(sleepTime);

            //TODO: start the service
            _tdfQuote.Start();

            //_waitHandle.WaitOne();
        }

        private void StopService()
        {
            //Monitor.Enter(_locker);
            //_terminal = true;
            //Monitor.Exit(_locker);
            //TODO: stop the service

            _tdfQuote.Stop();

            //_waitHandle.Set();
        }
    }
}
