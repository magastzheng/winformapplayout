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
            //
            //this.tbTemplateNo.Text
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
    }
}
