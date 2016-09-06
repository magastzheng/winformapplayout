using BLL;
using BLL.UFX.impl;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
using Model.EnumType;
using Model.strategy;
using Model.UI;
using System.Collections.Generic;
using System.Threading;

namespace TradingSystem.View
{
    public partial class AssetUnitForm : Forms.DefaultForm
    {
        private const string GridId = "assetunitmanagement";
        private GridConfig _gridConfig = null;
        private AccountBLL _accountBLL = null;

        private SortableBindingList<AssetUnit> _dataSource = new SortableBindingList<AssetUnit>(new List<AssetUnit>());

        public AssetUnitForm():
            base()
        {
            InitializeComponent();
        }

        public AssetUnitForm(GridConfig gridConfig, BLLManager bLLManager)
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
            Dictionary<string, string> colDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(AssetUnit));
            TSDataGridViewHelper.SetDataBinding(this.gridView, colDataMap);

            this.gridView.DataSource = _dataSource;

            return true;
        }

        private bool Form_LoadData(object sender, object data)
        {
            _dataSource.Clear();

            var result = _accountBLL.QueryAssetUnit();
            if (result != Model.ConnectionCode.Success)
            {
                return false;
            }

            var accounts = LoginManager.Instance.Assets;
            foreach (var account in accounts)
            {
                AssetUnit asset = new AssetUnit
                {
                    FundCode = account.AccountCode,
                    CapitalAccount = account.CapitalAccount,
                    AssetNo = account.AssetNo,
                    AssetName = account.AssetName,
                };

                var fund = LoginManager.Instance.Accounts.Find(o => o.AccountCode.Equals(asset.FundCode));
                if (fund != null)
                {
                    asset.FundName = fund.AccountName;
                    asset.EAccountType = fund.AccountType;
                }

                _dataSource.Add(asset);
            }

            return true;
        }
    }
}
