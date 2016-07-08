using BLL;
using log4net;
using System.Timers;

namespace Service
{
    public class UFXHeartBeatService:IService
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private int _timeOut = 10000;
        private Timer _timer;

        public UFXHeartBeatService()
        {
            _timer = new Timer(_timeOut);
            _timer.Elapsed += new ElapsedEventHandler(TimerHandler);
            _timer.AutoReset = true;
        }

        public void Start()
        {
            _timer.Enabled = true;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void TimerHandler(object sender, ElapsedEventArgs e)
        {
            //Send the heartbeat message
            var result = BLLManager.Instance.LoginBLL.HeartBeat();
            if (result != Model.ConnectionCode.Success)
            { 
                //TODO: to reconnnect
                logger.Error("Fail to check heartbeat");
            }
        }
    }
}
