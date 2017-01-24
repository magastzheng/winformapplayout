using BLL.UFX;
using BLL.UFX.impl;

namespace BLL.Manager
{
    public class UFXBLLManager
    {
        private readonly static UFXBLLManager _instance = new UFXBLLManager();
        public static UFXBLLManager Instance { get { return _instance; } }

        private readonly object _locker = new object();

        private LoginSyncBLL _loginBLL;
        private AccountBLL _accountBLL;
        private SecurityBLL _securityBLL;
        private QuerySyncBLL _querySyncBLL;
        private WithdrawSyncBLL _withdrawSyncBLL;
        private T2Subscriber _subscriber;

        public LoginSyncBLL LoginBLL
        {
            get { return _loginBLL; }
        }

        public AccountBLL AccountBLL
        {
            get { return _accountBLL; }
        }

        public SecurityBLL SecurityBLL
        {
            get { return _securityBLL; }
        }

        public QuerySyncBLL QuerySyncBLL
        {
            get { return _querySyncBLL; }
        }

        public WithdrawSyncBLL WithdrawSyncBLL
        {
            get { return _withdrawSyncBLL; }
        }

        public T2Subscriber Subscriber
        {
            get { return _subscriber; }
            set { _subscriber = value; }
        }

        private UFXBLLManager()
        {
        }

        public void Init(T2SDKWrap t2SDKWrap)
        {
            lock (_locker)
            {
                _loginBLL = new LoginSyncBLL(t2SDKWrap);
                _accountBLL = new AccountBLL(t2SDKWrap);
                _securityBLL = new SecurityBLL(t2SDKWrap);
                _querySyncBLL = new UFX.impl.QuerySyncBLL(t2SDKWrap);
                _withdrawSyncBLL = new UFX.impl.WithdrawSyncBLL(t2SDKWrap);
            }
        }
    }
}
