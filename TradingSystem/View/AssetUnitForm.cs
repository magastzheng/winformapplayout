using BLL;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TradingSystem.View
{
    public partial class AssetUnitForm : Forms.DefaultForm
    {
        private const string GridId = "assetunitmanagement";
        private GridConfig _gridConfig = null;
        private LoginBLL _loginBLL = null;

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
            return true;
        }
    }
}
