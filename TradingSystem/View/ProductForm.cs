using BLL;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.strategy;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TradingSystem.View
{
    public partial class ProductForm : Forms.DefaultForm
    {
        private const string GridId = "fundmanagement";
        private GridConfig _gridConfig = null;
        private LoginBLL _loginBLL = null;

        private ManualResetEvent _waitEvent = new ManualResetEvent(false);

        private SortableBindingList<Fund> _dataSource = new SortableBindingList<Fund>(new List<Fund>());

        public ProductForm()
            :base()
        {
            InitializeComponent();
        }

        public ProductForm(GridConfig gridConfig, BLLManager bLLManager)
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
            Dictionary<string, string> colDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(Fund));
            TSDataGridViewHelper.SetDataBinding(this.gridView, colDataMap);

            this.gridView.DataSource = _dataSource;

            return true;
        }

        private bool Form_LoadData(object sender, object data)
        {
            _dataSource.Clear();

            var result = _loginBLL.QueryAccount(new DataHandlerCallback(ParseAccount));
            if (result != Model.ConnectionCode.Success)
            {
                return false;
            }

            _waitEvent.WaitOne(5000);

            var accounts = LoginManager.Instance.Accounts;
            foreach (var account in accounts)
            {
                Fund fund = new Fund 
                {
                    FundCode = account.AccountCode,
                    FundName = account.AccountName,
                };

                int temp = -1;
                if (int.TryParse(account.AccountType, out temp))
                {
                    fund.AccountType = temp;
                }

                _dataSource.Add(fund);
            }

            return true;
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

            _waitEvent.Set();
        }
    }
}
