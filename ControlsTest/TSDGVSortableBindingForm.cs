﻿using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlsTest
{
    public partial class TSDGVSortableBindingForm : Form
    {
        private SortableBindingList<TemplateStock> _tempStocks = null;
        public TSDGVSortableBindingForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form_Load);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            GridConfig gridConfig = ConfigManager.Instance.GetGridConfig();
            TSGrid hsGrid = gridConfig.GetGid("templatestock");
            TSDataGridViewHelper.AddColumns(this.tsDataGridView1, hsGrid);
            Dictionary<string, string> colFieldMap = GridViewBindingHelper.GetPropertyBinding(typeof(TemplateStock));
            TSDataGridViewHelper.SetDataBinding(this.tsDataGridView1, colFieldMap);
            var stocks = GenerateData(hsGrid);
            _tempStocks = new SortableBindingList<TemplateStock>(stocks);
            this.tsDataGridView1.DataSource = _tempStocks;
            
        }

        private List<TemplateStock> GenerateData(TSGrid hsGrid)
        {
            List<TemplateStock> tempStocks = new List<TemplateStock>();
            TemplateStock item1 = new TemplateStock 
            {
                TemplateNo = 1,
                SecuCode = "000001",
                SecuName = "中国平安",
                Amount = 10200,
                Exchange = "深圳交易所",
                MarketCap = 652121.24,
                MarketCapWeight = 20.25,
                SettingWeight = 20.0
            };
            tempStocks.Add(item1);

            TemplateStock item2 = new TemplateStock
            {
                TemplateNo = 1,
                SecuCode = "000002",
                SecuName = "万科A",
                Amount = 7500,
                Exchange = "深圳交易所",
                MarketCap = 252102.74,
                MarketCapWeight = 12.25,
                SettingWeight = 13.0
            };
            tempStocks.Add(item2);

            TemplateStock item3 = new TemplateStock
            {
                TemplateNo = 1,
                SecuCode = "600519",
                SecuName = "贵州茅台",
                Amount = 12100,
                Exchange = "上海交易所",
                MarketCap = 4200125.02,
                MarketCapWeight = 30.31,
                SettingWeight = 30.0
            };
            tempStocks.Add(item3);

            return tempStocks;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TemplateStock item = new TemplateStock
            {
                TemplateNo = 1,
                SecuCode = "600207",
                SecuName = "安彩高科",
                Amount = 12100,
                Exchange = "上海交易所",
                MarketCap = 4200125.02,
                MarketCapWeight = 30.31,
                SettingWeight = 30.0
            };

            _tempStocks.Add(item);

            List<int> selectIndex = TSDataGridViewHelper.GetSelectRowIndex(this.tsDataGridView1);
            Console.WriteLine(selectIndex);

        }
    }
}
