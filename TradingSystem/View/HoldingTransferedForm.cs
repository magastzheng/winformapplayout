using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
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
    public partial class HoldingTransferedForm : Forms.BaseForm
    {
        private const string GridSourceId = "sourceportfolioholding";
        private const string GridDestId = "destinationportfolioholding";
        private GridConfig _gridConfig = null;

        private SortableBindingList<SourceHoldingItem> _srcDataSource = new SortableBindingList<SourceHoldingItem>(new List<SourceHoldingItem>());
        private SortableBindingList<DestinationHoldingItem> _destDataSource = new SortableBindingList<DestinationHoldingItem>(new List<DestinationHoldingItem>());

        public HoldingTransferedForm()
            :base()
        {
            InitializeComponent();
        }

        public HoldingTransferedForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
        }

        private bool Form_LoadControl(object sender, object data)
        {
            TSDataGridViewHelper.AddColumns(this.srcGridView, _gridConfig.GetGid(GridSourceId));
            Dictionary<string, string> colDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(SourceHoldingItem));
            TSDataGridViewHelper.SetDataBinding(this.srcGridView, colDataMap);

            this.srcGridView.DataSource = _srcDataSource;

            TSDataGridViewHelper.AddColumns(this.destGridView, _gridConfig.GetGid(GridDestId));
            colDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(DestinationHoldingItem));
            TSDataGridViewHelper.SetDataBinding(this.destGridView, colDataMap);

            this.destGridView.DataSource = _destDataSource;

            return true;
        }

        private bool Form_LoadData(object sender, object data)
        {
            //TODO:

            return true;
        }
    }
}
