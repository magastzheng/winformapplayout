using Config;
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
    public partial class ClosePositionForm : Forms.DefaultForm
    {
        private const string GridCloseId = "closeposition";
        private const string GridSecurityId = "closepositionsecurity";

        private GridConfig _gridConfig;

        public ClosePositionForm()
            :base()
        {
            InitializeComponent();
        }

        public ClosePositionForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
        }

        private bool Form_LoadControl(object sender, object data)
        {
            //set the monitorGridView
            TSDataGridViewHelper.AddColumns(this.closeGridView, _gridConfig.GetGid(GridCloseId));
            Dictionary<string, string> closeColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(ClosePositionItem));
            TSDataGridViewHelper.SetDataBinding(this.closeGridView, closeColDataMap);

            //set the securityGridView
            TSDataGridViewHelper.AddColumns(this.securityGridView, _gridConfig.GetGid(GridSecurityId));
            Dictionary<string, string> securityColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(ClosePositionSecurityItem));
            TSDataGridViewHelper.SetDataBinding(this.securityGridView, securityColDataMap);

            return true;
        }

        private bool Form_LoadData(object sender, object data)
        {
            return true;
        }
    }
}
