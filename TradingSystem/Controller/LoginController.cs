using BLL;
using Config;
using Model;
using Model.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.View;

namespace TradingSystem.Controller
{
    public class LoginController
    {
        private LoginForm _loginForm;
        private T2SDKWrap _t2SDKWrap;
        private LoginBLL2 _loginBLL;
        
        public LoginForm LoginForm
        {
            get { return _loginForm; }
        }

        public T2SDKWrap T2SDKWrap
        {
            get { return _t2SDKWrap; }
        }

        public LoginController(LoginForm loginForm, T2SDKWrap t2SDKWrap)
        {
            this._t2SDKWrap = t2SDKWrap;
            this._loginBLL = new LoginBLL2(t2SDKWrap);

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

            int retCode = (int)_loginBLL.Login(user);
            if (retCode == (int)ConnectionCode.Success)
            {
                _loginBLL.QueryAccount(new DataHandlerCallback(ParseAccount));
                _loginBLL.QueryAssetUnit();
                //_loginBLL.QueryPortfolio();
                //_loginBLL.QueryHolder();
                //_loginBLL.QueryTrading();
            }

            return retCode;
        }

        private void ParseAccount(DataParser parser)
        {
            for (int i = 1, count = parser.DataSets.Count; i < count; i++)
            {
                var dataSet = parser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    AccountItem acc = new AccountItem();
                    acc.AccountCode = dataRow.Columns["account_code"].GetStr();
                    acc.AccountName = dataRow.Columns["account_name"].GetStr();
                    acc.AccountType = dataRow.Columns["account_type"].GetStr();

                    LoginManager.Instance.AddAccount(acc);
                }
                break;
            }
        }
    }
}
