using BLL;
using ServiceInterface;
using log4net;
using System;
using System.Timers;

namespace Service
{
    public class UFXHeartBeatService:IService
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private int _interval = 60 * 1000; //1m
        private Timer _timer;
        private Connected _connectCallback;
        private Notify _notify;

        public UFXHeartBeatService(Connected cb, Notify notify)
        {
            _connectCallback = cb;
            _notify = notify;
            _timer = new Timer(_interval);
            _timer.Elapsed += new ElapsedEventHandler(TimerHandler);
            _timer.AutoReset = true;
        }

        #region IService interface
        public void Start()
        {
            _timer.Enabled = true;
            _timer.Start();
            if (_connectCallback != null)
            {
                _connectCallback(ServiceType.UFXHeart, 0, "Start timer");
            }
        }

        public void Stop()
        {
            _timer.Stop();
        }

        //public void Connected(Connected cb)
        //{
        //    _connectCallback = cb;
        //}

        //public void Notify(Notify notify)
        //{
        //    _notify = notify;
        //}

        #endregion

        private void TimerHandler(object sender, ElapsedEventArgs e)
        {
            //The timer trigger its callback every interval. It don't concern the real callback cost.
            //So we need to stop the timer before running the task, and start it after finishing task.
            _timer.Stop();

            //Send the heartbeat message
            var result = BLLManager.Instance.LoginBLL.HeartBeat();
            if (result != Model.ConnectionCode.Success)
            { 
                //TODO: to reconnnect
                logger.Error("Fail to check heartbeat");

                if (_notify != null)
                {
                    NotifyArgs arg = new NotifyArgs 
                    {
                        Code = (int)result,
                        Message = "UFX心跳检测失败！",
                    };

                    _notify(arg);
                }
            }

            _timer.Start();
        }
    }
}
