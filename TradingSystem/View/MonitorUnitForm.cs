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
            this.buttonContainer.ButtonClick += new EventHandler(ButtonContainer_ButtonClick);
            LoadBottomButton();
        }

        private void ButtonContainer_ButtonClick(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
            }
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

        private void LoadBottomButton()
        {
            Dictionary<string, string> buttonMap = new Dictionary<string, string>();
            buttonMap.Add("SelectAll", "全选");
            buttonMap.Add("UnSelectAll", "反选");
            buttonMap.Add("Add", "增加");
            buttonMap.Add("Remove", "删除");
            buttonMap.Add("Modify", "修改");
            buttonMap.Add("Refresh", "刷新");
            buttonMap.Add("BatchAdd", "批量添加");

            foreach (var kv in buttonMap)
            {
                Button button = new Button();
                button.Name = kv.Key;
                button.Text = kv.Value;

                buttonContainer.AddButton(button);
            }
        }
    }
}
