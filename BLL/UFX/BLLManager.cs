using BLL.UFX;
using BLL.UFX.impl;

namespace BLL
{
    public class BLLManager
    {
        private readonly static BLLManager _instance = new BLLManager();
        public static BLLManager Instance { get { return _instance; } }

        private readonly object _locker = new object();

        private LoginBLL _loginBLL;
        private SecurityBLL _securityBLL;
        private StrategyBLL _strategyBLL;
        private T2Subscriber _subscriber;

        public LoginBLL LoginBLL
        {
            get { return _loginBLL; }
        }

        public SecurityBLL SecurityBLL
        {
            get { return _securityBLL; }
        }

        public StrategyBLL StrategyBLL
        {
            get { return _strategyBLL; }
        }

        public T2Subscriber Subscriber
        {
            get { return _subscriber; }
            set { _subscriber = value; }
        }

        private BLLManager()
        {
        }

        public void Init(T2SDKWrap t2SDKWrap)
        {
            lock (_locker)
            {
                _loginBLL = new LoginBLL(t2SDKWrap);
                _securityBLL = new SecurityBLL(t2SDKWrap);
                _strategyBLL = new StrategyBLL(t2SDKWrap);
            }
        }
    }
}
