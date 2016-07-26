using BLL;
using BLL.UFX.impl;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
using Model.strategy;
using Model.UI;
using System.Collections.Generic;
using System.Threading;

namespace TradingSystem.View
{
    public partial class PortfolioForm : Forms.BaseForm
    {
        private const string GridId = "portfoliomaintain";
        private GridConfig _gridConfig = null;
        private LoginBLL _loginBLL = null;

        private ManualResetEvent _waitEvent = new ManualResetEvent(false);

        private SortableBindingList<Portfolio> _dataSource = new SortableBindingList<Portfolio>(new List<Portfolio>());

        public PortfolioForm():
            base()
        {
            InitializeComponent();
        }

        public PortfolioForm(GridConfig gridConfig, BLLManager bLLManager)
            : this()
        {
            _gridConfig = gridConfig;
            _loginBLL = bLLManager.LoginBLL;

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
        }

        private bool Form_LoadControl(object sender, object data)
        {
            //set the monitorGridView
            TSDataGridViewHelper.AddColumns(this.gridView, _gridConfig.GetGid(GridId));
            Dictionary<string, string> colDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(Portfolio));
            TSDataGridViewHelper.SetDataBinding(this.gridView, colDataMap);

            this.gridView.DataSource = _dataSource;

            return true;
        }

        private bool Form_LoadData(object sender, object data)
        {
            _dataSource.Clear();

            _loginBLL.QueryPortfolio(new DataHandlerCallback(ParseData));

            _waitEvent.WaitOne(5000);

            var portfolios = LoginManager.Instance.Portfolios;
            foreach (var p in portfolios)
            {
                Portfolio portfolio = new Portfolio 
                {
                    FundCode = p.AccountCode,
                    AssetNo = p.AssetNo,
                    PortfolioNo = p.CombiNo,
                    PortfolioName = p.CombiName,
                    CapitalAccount = p.CapitalAccount,
                };

                var fund = LoginManager.Instance.Accounts.Find(o => o.AccountCode.Equals(p.AccountCode));
                if (fund != null)
                {
                    portfolio.FundName = fund.AccountName;
                    int temp = -1;
                    if (int.TryParse(fund.AccountType, out temp))
                    {
                        portfolio.AccountType = temp;
                    }
                }

                var asset = LoginManager.Instance.Assets.Find(o => o.AssetNo.Equals(p.AssetNo));
                if (asset != null)
                {
                    portfolio.AssetName = asset.AssetName;
                    portfolio.CapitalAccount = asset.CapitalAccount;
                }

                _dataSource.Add(portfolio);
            }

            return true;
        }

        private int ParseData(DataParser parser)
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

            _waitEvent.Set();

            return 1;
        }
    }
}
