using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Service
{
    public class UFXHeartBeatService:IService
    {
        private int _timeOut = 5000;
        private Timer _timer;

        public UFXHeartBeatService()
        {
            _timer = new Timer(_timeOut);
            _timer.Elapsed += new ElapsedEventHandler(TimerHandler);
            _timer.AutoReset = false;
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
        }
    }
}
