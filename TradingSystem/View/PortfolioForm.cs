using BLL;
using BLL.UFX.impl;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
using Model.UI;
using System.Collections.Generic;

namespace TradingSystem.View
{
    public partial class PortfolioForm : Forms.BaseForm
    {
        private const string GridId = "portfoliomaintain";
        private GridConfig _gridConfig = null;
        private AccountBLL _accountBLL = null;

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
            _accountBLL = bLLManager.AccountBLL;

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

            _accountBLL.QueryPortfolio();

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
                    portfolio.EAccountType = fund.AccountType;
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
    }
}
