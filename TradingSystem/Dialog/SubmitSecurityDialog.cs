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

namespace TradingSystem.Dialog
{
    public partial class SubmitSecurityDialog : Forms.BaseDialog
    {
        private const string GridId = "entrustsecurity";

        private GridConfig _gridConfig;

        private SortableBindingList<EntrustSecurityItem> _dataSource = new SortableBindingList<EntrustSecurityItem>(new List<EntrustSecurityItem>());

        public SubmitSecurityDialog()
            :base()
        {
            InitializeComponent();
        }

        public SubmitSecurityDialog(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
        }

        private bool Form_LoadControl(object sender, object data)
        {
            //Load Command Trading
            TSDataGridViewHelper.AddColumns(this.gridView, _gridConfig.GetGid(GridId));
            Dictionary<string, string> gridColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(EntrustSecurityItem));
            TSDataGridViewHelper.SetDataBinding(this.gridView, gridColDataMap);
            this.gridView.DataSource = _dataSource;

            return true;
        }

        private bool Form_LoadData(object sender, object data)
        {
            if (data == null)
            {
                return false;
            }

            if (!(data is List<EntrustSecurityItem>))
            {
                return false;
            }

            var ds = data as List<EntrustSecurityItem>;
            ds.ForEach(p => _dataSource.Add(p));

            return true;
        }
    }
}
