using log4net;
using System.Threading;

namespace Service
{
    public class QuoteService : IService
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private EventWaitHandle _waitHandle = new AutoResetEvent(false);
        private object _locker = new object();
        private Thread serviceThread = null;

        public void Start()
        {
            serviceThread = new Thread(InitServiceThread);
            serviceThread.Start(1000);
        }

        private void InitServiceThread(object obj)
        {
            int sleepTime = (int)obj;

            Thread.Sleep(sleepTime);

            //TODO: start the service
            

            _waitHandle.WaitOne();
        }

        public void Stop()
        {
            //TODO: stop the service
            _waitHandle.Set();
            return;
        }
    }
}
