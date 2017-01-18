using BLL.Manager;
using BLL.Permission;
using BLL.Product;
using BLL.UFX;
using Config;
using Model;
using ServiceInterface;
using TradingSystem.View;

namespace TradingSystem.Controller
{
    public class LoginController
    {
        private LoginForm _loginForm;
        private T2SDKWrap _t2SDKWrap;
        private ProductBLL _productBLL;
        private UserBLL _userBLL;
        //private CountdownEvent _cdEvent = new CountdownEvent(4);
        //private LoginBLL _loginBLL;

        private bool _isFirst = true;
        private ConnectionCode _lastRetCode;
        
        public LoginForm LoginForm
        {
            get { return _loginForm; }
        }

        //public T2SDKWrap T2SDKWrap
        //{
        //    get { return _t2SDKWrap; }
        //}

        public LoginController(LoginForm loginForm, T2SDKWrap t2SDKWrap)
        {
            this._t2SDKWrap = t2SDKWrap;
            //this._loginBLL = new LoginBLL(t2SDKWrap);
            //this._loginBLL
            this._productBLL = new ProductBLL();
            this._userBLL = new UserBLL();

            this._loginForm = loginForm;
            this._loginForm.LoginController = this;
        }

        public ConnectionCode Login(string userName, string password)
        {
            if (_isFirst)
            {
                _isFirst = false;
                _lastRetCode = LoginInternal(userName, password);
            }
            else if (_lastRetCode == ConnectionCode.ErrorFailStartService)
            {
                _lastRetCode = InitService();
            }
            else
            {
                _lastRetCode = LoginInternal(userName, password);
            }

            if (_lastRetCode == ConnectionCode.Success)
            {
                StartMainForm();
            }

            return _lastRetCode;
        }

        #region login private method

        private ConnectionCode LoginInternal(string userName, string password)
        {
            LoginUser user = new LoginUser
            {
                Operator = userName,
                Password = password
            };

            ConnectionCode retCode = UFXBLLManager.Instance.LoginBLL.Login(user);
            if (retCode == ConnectionCode.Success)
            {
                InitializeAccount(LoginManager.Instance.LoginUser);
                retCode = Subscribe(LoginManager.Instance.LoginUser);
                if (retCode == ConnectionCode.SuccessSubscribe)
                {
                    retCode = InitService();
                }
                else
                {
                    retCode = ConnectionCode.ErrorFailSubscribe;
                }
            }

            return retCode;
        }

        private void InitializeAccount(LoginUser loginUser)
        {
            //get or create the user into the database.
            var user = _userBLL.GetUser(loginUser.Operator);
            if (user != null && user.Id > 0)
            {
                LoginManager.Instance.LoginUser.LocalUser = user;
            }

            //fetch the Fund, AssetUnit, Portfolio, Holder by UFX.
            UFXBLLManager.Instance.AccountBLL.QueryAccount();
            UFXBLLManager.Instance.AccountBLL.QueryAssetUnit();
            UFXBLLManager.Instance.AccountBLL.QueryPortfolio();
            UFXBLLManager.Instance.AccountBLL.QueryHolder();

            //sync the fund, AssetUnit, Portfolio into database.
            _productBLL.Create(LoginManager.Instance.Accounts, LoginManager.Instance.Assets, LoginManager.Instance.Portfolios);
        }

        private ConnectionCode Subscribe(LoginUser loginUser)
        {
            return UFXBLLManager.Instance.Subscriber.Subscribe(loginUser);
        }

        private ConnectionCode InitService()
        {
            ServiceManager.Instance.Init();

            //TODO: register the notify/callback method
            //Add another form the show the message???

            //ServiceManager.Instance.
            ServiceManager.Instance.Start();

            if (ServiceManager.Instance.Wait())
            {
                return ConnectionCode.Success;
            }
            else
            {
                return ConnectionCode.ErrorFailStartService;
            }
        }

        private void StartMainForm()
        {
            var gridConfig = ConfigManager.Instance.GetGridConfig();
            MainForm mainForm = new MainForm(gridConfig, this._t2SDKWrap);
            MainController mainController = new MainController(mainForm, this._t2SDKWrap);
            Program._s_mainfrmController = mainController;
        }

        #endregion

        public void Logout()
        {
            ServiceManager.Instance.Stop();
            if (LoginManager.Instance.LoginUser != null && !string.IsNullOrEmpty(LoginManager.Instance.LoginUser.Token))
            {
                UFXBLLManager.Instance.LoginBLL.Logout();
            }
        }
    }
}
