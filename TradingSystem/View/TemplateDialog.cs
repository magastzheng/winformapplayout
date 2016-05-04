using Config;
using DBAccess;
using Forms;
using Model;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Util;

namespace TradingSystem.View
{
    public partial class TemplateDialog : Forms.BaseFixedForm
    {
        private StockTemplateDAO _dbdao = new StockTemplateDAO();
        public TemplateDialog()
        {
            InitializeComponent();
        }

        public override void OnLoadFormActived(string json)
        {
            base.OnLoadFormActived(json);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            var weightType = ConfigManager.Instance.GetComboConfig().GetComboOption("weighttype");
            FormUtil.SetComboBox(this.cbWeightType, weightType);

            var replaceType = ConfigManager.Instance.GetComboConfig().GetComboOption("replacetype");
            FormUtil.SetComboBox(this.cbReplaceType, replaceType);

            var benchmarks = _dbdao.GetBenchmark();
            ComboOption cbOption = new ComboOption { 
                Items = new List<ComboOptionItem>()
            };
            foreach (var benchmark in benchmarks)
            {
                ComboOptionItem item = new ComboOptionItem 
                {
                    Id = benchmark.BenchmarkId,
                    Name = benchmark.BenchmarkName
                };

                cbOption.Items.Add(item);
            }

            cbOption.Selected = benchmarks[0].BenchmarkId;
            FormUtil.SetComboBox(this.cbBenchmark, cbOption);
        }

        private void Form_LoadActived(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                StockTemplate stockTemplate = JsonUtil.DeserializeObject<StockTemplate>(json);
                if (stockTemplate != null)
                {
                    FillData(stockTemplate);
                }
            }
        }

        private void FillData(StockTemplate stockTemplate)
        {
            this.tbTemplateNo.Text = stockTemplate.TemplateNo.ToString();
            this.tbTemplateNo.Enabled = false;

            this.tbTemplateName.Text = stockTemplate.TemplateName;

            this.tbFutureCopies.Text = stockTemplate.FutureCopies.ToString();
            this.tbMarketCapOpt.Text = stockTemplate.MarketCapOpt.ToString();

            this.cbBenchmark.SelectedValue = stockTemplate.Benchmark.ToString();
            this.cbWeightType.SelectedValue = stockTemplate.WeightType.ToString();
            this.cbReplaceType.SelectedValue = stockTemplate.ReplaceType.ToString();
        }

        private StockTemplate GetData()
        {
            StockTemplate stockTemplate = new StockTemplate();
            if (!string.IsNullOrEmpty(this.tbTemplateNo.Text))
            {
                int tempNo = -1;
                if (int.TryParse(this.tbTemplateNo.Text, out tempNo))
                {
                    stockTemplate.TemplateNo = tempNo;
                }
            }

            stockTemplate.TemplateName = this.tbTemplateName.Text;

            int temp = 0;
            if(int.TryParse(this.tbFutureCopies.Text, out temp))
            {
                stockTemplate.FutureCopies = temp;
            }
            
            double dTemp = 0.0f;
            if(double.TryParse(this.tbMarketCapOpt.Text, out dTemp))
            {
                stockTemplate.MarketCapOpt = dTemp;
            }

            if (this.cbWeightType.SelectedItem is ComboOptionItem)
            {
                ComboOptionItem item = this.cbWeightType.SelectedItem as ComboOptionItem;
                int type = 0;
                if (int.TryParse(item.Id, out type))
                {
                    stockTemplate.WeightType = type;
                }
            }

            if (this.cbReplaceType.SelectedItem is ComboOptionItem)
            {
                ComboOptionItem item = this.cbReplaceType.SelectedItem as ComboOptionItem;
                int type = 0;
                if (int.TryParse(item.Id, out type))
                {
                    stockTemplate.ReplaceType = type;
                }
            }

            if (this.cbBenchmark.SelectedItem is ComboOptionItem)
            {
                ComboOptionItem item = this.cbBenchmark.SelectedItem as ComboOptionItem;
                stockTemplate.Benchmark = item.Id;
            }

            return stockTemplate;
        }

        private bool CheckInputValue(StockTemplate stockTemplate)
        {
            if (string.IsNullOrEmpty(stockTemplate.TemplateName))
            {
                return false;
            }

            if (stockTemplate.FutureCopies < 1)
            {
                return false;
            }

            if (stockTemplate.MarketCapOpt < 1)
            {
                return false;
            }

            if (string.IsNullOrEmpty(stockTemplate.Benchmark))
            {
                return false;
            }

            return true;
        }

        #region Button click event handler
        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            StockTemplate stockTemplate = GetData();
            if (CheckInputValue(stockTemplate))
            {
                OnSave(this, stockTemplate);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                DialogResult = System.Windows.Forms.DialogResult.No;
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        #endregion
    }
}
