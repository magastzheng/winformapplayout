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
    public partial class MonitorUnitForm : Forms.DefaultForm
    {
        private GridConfig _gridConfig = null;
        private const string GridId = "monitorunitmanager";
        private MonitorUnitDAO _dbdao = new MonitorUnitDAO();
        private SortableBindingList<MonitorUnit> _dataSource;

        public MonitorUnitForm() : base()
        {
            InitializeComponent();
        }

        public MonitorUnitForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;
            
            this.Load += new EventHandler(Form_Load);
            this.LoadFormActived += new FormActiveHandler(Form_LoadFormActived);
        }

        private void Form_LoadFormActived(string json)
        {
            //Load data here
            List<MonitorUnit> monitorUnits = _dbdao.Get(-1);
            _dataSource = new SortableBindingList<MonitorUnit>(monitorUnits);
            dataGridView.DataSource = _dataSource;
            
        }

        private void Form_Load(object sender, EventArgs e)
        {
            TSDataGridViewHelper.AddColumns(this.dataGridView, _gridConfig.GetGid(GridId));
            Dictionary<string, string> colDataMap = new Dictionary<string, string>();
            colDataMap.Add("monitorunitid", "MonitorUnitId");
            colDataMap.Add("monitorunitname", "MonitorUnitName");
            colDataMap.Add("accounttype", "AccountType");
            colDataMap.Add("portfolioid", "PortfolioId");
            colDataMap.Add("portfolioname", "PortfolioName");
            colDataMap.Add("bearcontract", "BearContract");
            colDataMap.Add("stocktempid", "StockTemplateId");
            colDataMap.Add("stocktempname", "StockTemplateName");
            TSDataGridViewHelper.SetDataBinding(this.dataGridView, colDataMap);
        }
    }
}
