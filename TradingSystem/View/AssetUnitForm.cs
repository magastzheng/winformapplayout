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

        private SortableBindingList<AssetUnit> _instDataSource = new SortableBindingList<AssetUnit>(new List<AssetUnit>());

        public AssetUnitForm():
            base()
        {
            InitializeComponent();
        }

        public AssetUnitForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
        }

        private bool Form_LoadControl(object sender, object data)
        {
            //set the monitorGridView
            TSDataGridViewHelper.AddColumns(this.gridView, _gridConfig.GetGid(GridId));
            Dictionary<string, string> colDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(ClosePositionItem));
            TSDataGridViewHelper.SetDataBinding(this.gridView, colDataMap);

            return true;
        }

        private bool Form_LoadData(object sender, object data)
        {
            return true;
        }
    }
}
