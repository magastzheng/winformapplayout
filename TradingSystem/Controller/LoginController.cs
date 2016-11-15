using BLL;
using BLL.Permission;
using BLL.Product;
using BLL.UFX;
using BLL.UFX.impl;
using Config;
using Model;
using Model.strategy;
using ServiceInterface;
using System.Threading;
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

        public int Login(string userName, string password)
        {
            LoginUser user = new LoginUser
            {
                Operator = userName,
                Password = password
            };

            int retCode = (int)BLLManager.Instance.LoginBLL.Login(user);
            if (retCode == (int)ConnectionCode.Success)
            {
                retCode = (int)LoginSuccess();
            }

            return retCode;
        }

        private ConnectionCode LoginSuccess()
        {
            //get or create the user into the database.
            var user = _userBLL.GetUser(LoginManager.Instance.LoginUser.Operator);
            if (user != null && user.Id > 0)
            {
                LoginManager.Instance.LoginUser.LocalUser = user;
            }

            //fetch the Fund, AssetUnit, Portfolio, Holder by UFX.
            BLLManager.Instance.AccountBLL.QueryAccount();
            BLLManager.Instance.AccountBLL.QueryAssetUnit();
            BLLManager.Instance.AccountBLL.QueryPortfolio();
            BLLManager.Instance.AccountBLL.QueryHolder();

            //sync the fund, AssetUnit, Portfolio into database.
            _productBLL.Create(LoginManager.Instance.Accounts, LoginManager.Instance.Assets, LoginManager.Instance.Portfolios);

            //Subscribe the message from UFX.
            var subRet = BLLManager.Instance.Subscriber.Subscribe(LoginManager.Instance.LoginUser);
            if (subRet != ConnectionCode.SuccessSubscribe)
            {
                return subRet;
            }

            ServiceManager.Instance.Init();

            //TODO: register the notify/callback method
            //Add another form the show the message???

            //ServiceManager.Instance.
            ServiceManager.Instance.Start();
            if (ServiceManager.Instance.Wait())
            {
                //TODO: waiting for the services. 
                var gridConfig = ConfigManager.Instance.GetGridConfig();
                MainForm mainForm = new MainForm(gridConfig, this._t2SDKWrap);
                MainController mainController = new MainController(mainForm, this._t2SDKWrap);
                Program._s_mainfrmController = mainController;

                return ConnectionCode.Success;
            }
            else
            {
                return ConnectionCode.ErrorFailStartService;
            }
        }

        public void Logout()
        {
            ServiceManager.Instance.Stop();
            if (LoginManager.Instance.LoginUser != null && !string.IsNullOrEmpty(LoginManager.Instance.LoginUser.Token))
            {
                BLLManager.Instance.LoginBLL.Logout();
            }
        }
    }
}
