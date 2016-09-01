﻿using BLL;
using BLL.Product;
using BLL.UFX;
using BLL.UFX.impl;
using Config;
using Model;
using Model.strategy;
using Service;
using System.Threading;
using TradingSystem.View;

namespace TradingSystem.Controller
{
    public class LoginController
    {
        private LoginForm _loginForm;
        private T2SDKWrap _t2SDKWrap;
        private ProductBLL _productBLL;
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

            this._loginForm = loginForm;
            this._loginForm.LoginController = this;
        }

        public int Login(string userName, string password)
        {

            User user = new User
            {
                Operator = userName,
                Password = password
            };

            int retCode = (int)BLLManager.Instance.LoginBLL.Login(user);
            if (retCode == (int)ConnectionCode.Success)
            {
                LoginSuccess();
            }

            return retCode;
        }

        private void LoginSuccess()
        {
            BLLManager.Instance.AccountBLL.QueryAccount();
            BLLManager.Instance.AccountBLL.QueryAssetUnit();
            BLLManager.Instance.AccountBLL.QueryPortfolio();
            BLLManager.Instance.AccountBLL.QueryHolder();

            _productBLL.Create(LoginManager.Instance.Accounts, LoginManager.Instance.Assets, LoginManager.Instance.Portfolios);

            BLLManager.Instance.Subscriber.Subscribe(LoginManager.Instance.LoginUser);
            ServiceManager.Instance.Start();
            var gridConfig = ConfigManager.Instance.GetGridConfig();
            MainForm mainForm = new MainForm(gridConfig, this._t2SDKWrap);
            MainController mainController = new MainController(mainForm, this._t2SDKWrap);
            Program._s_mainfrmController = mainController;
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
