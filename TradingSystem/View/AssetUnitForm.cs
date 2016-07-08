using BLL;
using BLL.UFX.impl;
using Config;
using Controls.Entity;
using Controls.GridView;
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
        private LoginBLL _loginBLL = null;

        private ManualResetEvent _waitEvent = new ManualResetEvent(false);

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
            _loginBLL = bLLManager.LoginBLL;

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
        }

        private bool Form_LoadControl(object sender, object data)
        {
            //set the monitorGridView
            TSDataGridViewHelper.AddColumns(this.gridView, _gridConfig.GetGid(GridId));
            Dictionary<string, string> colDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(AssetUnit));
            TSDataGridViewHelper.SetDataBinding(this.gridView, colDataMap);

            this.gridView.DataSource = _dataSource;

            return true;
        }

        private bool Form_LoadData(object sender, object data)
        {
            _dataSource.Clear();

            var result = _loginBLL.QueryAssetUnit(new DataHandlerCallback(ParseData));
            if (result != Model.ConnectionCode.Success)
            {
                return false;
            }

            _waitEvent.WaitOne(5000);

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
                    int temp = -1;
                    if (int.TryParse(fund.AccountType, out temp))
                    {
                        asset.AccountType = temp;
                    }
                }

                _dataSource.Add(asset);
            }

            return true;
        }

        private void ParseData(DataParser parser)
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

            _waitEvent.Set();
        }
    }
}
