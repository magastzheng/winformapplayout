using Config;
using Controls.Entity;
using Controls.GridView;
using DBAccess;
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
    public partial class OpenPositionForm : Forms.DefaultForm
    {
        private const string MonitorGridId = "openposition";
        private const string SecurityGridId = "openpositionsecurity";

        private GridConfig _gridConfig;
        private MonitorUnitDAO _monitordbdao = new MonitorUnitDAO();
        private TemplateStockDAO _stockdbdao = new TemplateStockDAO();
        private SortableBindingList<OpenPositionItem> _monitorDataSource;
        private SortableBindingList<OpenPositionSecurityItem> _securityDataSource;

        public OpenPositionForm()
            :base()
        {
            InitializeComponent();
        }

        public OpenPositionForm(GridConfig gridConfig)
            :this()
        {
            _gridConfig = gridConfig;

            LoadControl += new FormLoadHandler(Form_LoadControl);
            LoadData += new FormLoadHandler(Form_LoadData);
        }

        private void Form_LoadControl(object sender, object data)
        {
            //set the monitorGridView
            TSDataGridViewHelper.AddColumns(this.monitorGridView, _gridConfig.GetGid(MonitorGridId));
            Dictionary<string, string> monitorColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(OpenPositionItem));
            TSDataGridViewHelper.SetDataBinding(this.monitorGridView, monitorColDataMap);           

            //set the securityGridView
            TSDataGridViewHelper.AddColumns(this.securityGridView, _gridConfig.GetGid(SecurityGridId));
            Dictionary<string, string> securityColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(OpenPositionSecurityItem));
            TSDataGridViewHelper.SetDataBinding(this.securityGridView, securityColDataMap);
        }

        private void Form_LoadData(object sender, object data)
        {
            //Load the data of open posoition
            List<OpenPositionItem> monitorList = _monitordbdao.GetActive();
            _monitorDataSource = new SortableBindingList<OpenPositionItem>(monitorList);
            this.monitorGridView.DataSource = _monitorDataSource;

            //Load the data for each template
            if (monitorList.Count > 0)
            { 
                
            }
        }
    }
}
