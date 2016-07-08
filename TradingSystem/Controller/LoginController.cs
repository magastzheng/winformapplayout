using BLL;
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
        private CountdownEvent _cdEvent = new CountdownEvent(4);
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
            BLLManager.Instance.LoginBLL.QueryAccount(new DataHandlerCallback(ParseAccount));
            BLLManager.Instance.LoginBLL.QueryAssetUnit(new DataHandlerCallback(ParseAssetUnit));
            BLLManager.Instance.LoginBLL.QueryPortfolio(new DataHandlerCallback(ParsePortfolio));
            BLLManager.Instance.LoginBLL.QueryHolder(new DataHandlerCallback(ParseHolder));

            _cdEvent.Wait();
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

            _cdEvent.Signal();
        }

        private void ParseAssetUnit(DataParser parser)
        {
            for (int i = 1, count = parser.DataSets.Count; i < count; i++)
            {
                var dataSet = parser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    AssetItem asset = new AssetItem();
                    asset.CapitalAccount = dataRow.Columns["capital_account"].GetStr();
                    asset.AccountCode = dataRow.Columns["account_code"].GetStr();
                    asset.AssetNo = dataRow.Columns["asset_no"].GetStr();
                    asset.AssetName = dataRow.Columns["asset_name"].GetStr();

                    LoginManager.Instance.AddAsset(asset);
                }
                break;
            }

            _cdEvent.Signal();
        }

        private void ParsePortfolio(DataParser parser)
        {
            for (int i = 1, count = parser.DataSets.Count; i < count; i++)
            {
                var dataSet = parser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    PortfolioItem p = new PortfolioItem();
                    p.AccountCode = dataRow.Columns["account_code"].GetStr();
                    p.AssetNo = dataRow.Columns["asset_no"].GetStr();
                    p.CombiNo = dataRow.Columns["combi_no"].GetStr();
                    p.CombiName = dataRow.Columns["combi_name"].GetStr();
                    p.CapitalAccount = dataRow.Columns["capital_account"].GetStr();
                    p.MarketNoList = dataRow.Columns["market_no_list"].GetStr();
                    p.FutuInvestType = dataRow.Columns["futu_invest_type"].GetStr();
                    p.EntrustDirectionList = dataRow.Columns["entrust_direction_list"].GetStr();

                    LoginManager.Instance.AddPortfolio(p);
                }
                break;
            }

            _cdEvent.Signal();
        }

        private void ParseHolder(DataParser parser)
        {
            for (int i = 1, count = parser.DataSets.Count; i < count; i++)
            {
                var dataSet = parser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    HolderItem p = new HolderItem();
                    p.AccountCode = dataRow.Columns["account_code"].GetStr();
                    p.AssetNo = dataRow.Columns["asset_no"].GetStr();
                    p.CombiNo = dataRow.Columns["combi_no"].GetStr();
                    p.StockHolderId = dataRow.Columns["stockholder_id"].GetStr();
                    p.MarketNo = dataRow.Columns["market_no"].GetStr();

                    LoginManager.Instance.AddHolder(p);
                }
                break;
            }

            _cdEvent.Signal();
        }
    }
}
