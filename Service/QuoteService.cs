using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    public class QuoteService : IService
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
        }

        public void Stop()
        {
            //TODO: stop the service

            return;
        }
    }
}
