using BLL.Template;
using Config;
using DBAccess;
using Forms;
using Model.config;
using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;

namespace TradingSystem.View
{
    public partial class TemplateDialog : Forms.BaseDialog
    {
        private BenchmarkBLL _benchmarkBLL = new BenchmarkBLL();
        public TemplateDialog()
        {
            InitializeComponent();

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
        }

        private bool Form_LoadControl(object sender, object data)
        {
            var weightType = ConfigManager.Instance.GetComboConfig().GetComboOption("weighttype");
            ComboBoxUtil.SetComboBox(this.cbWeightType, weightType);

            var replaceType = ConfigManager.Instance.GetComboConfig().GetComboOption("replacetype");
            ComboBoxUtil.SetComboBox(this.cbReplaceType, replaceType);

            var benchmarks = _benchmarkBLL.GetAll();
            ComboOption cbOption = new ComboOption
            {
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
            ComboBoxUtil.SetComboBox(this.cbBenchmark, cbOption);

            return true;
        }

        private bool Form_LoadData(object sender, object data)
        {
            if (sender == null || data == null)
                return false;
            if (!(data is StockTemplate))
                return false;

            StockTemplate stockTemplate = data as StockTemplate;
            if (stockTemplate != null)
            {
                FillData(stockTemplate);
            }

            return true;
        }

        //public override void OnFormActived(string json)
        //{
        //    base.OnFormActived(json);
        //}

        //private void Form_Load(object sender, EventArgs e)
        //{
            
        //}

        //private void Form_LoadActived(string json)
        //{
        //    if (!string.IsNullOrEmpty(json))
        //    {
        //        StockTemplate stockTemplate = JsonUtil.DeserializeObject<StockTemplate>(json);
        //        if (stockTemplate != null)
        //        {
        //            FillData(stockTemplate);
        //        }
        //    }
        //}

        private void FillData(StockTemplate stockTemplate)
        {
            this.tbTemplateNo.Text = stockTemplate.TemplateId.ToString();
            this.tbTemplateNo.Enabled = false;

            this.tbTemplateName.Text = stockTemplate.TemplateName;

            this.tbFutureCopies.Text = stockTemplate.FutureCopies.ToString();
            this.tbMarketCapOpt.Text = stockTemplate.MarketCapOpt.ToString();

            ComboBoxUtil.SetComboBoxSelect(this.cbBenchmark, stockTemplate.Benchmark);
            ComboBoxUtil.SetComboBoxSelect(this.cbWeightType, stockTemplate.EWeightType.ToString());
            ComboBoxUtil.SetComboBoxSelect(this.cbReplaceType, stockTemplate.EReplaceType.ToString());
        }

        private StockTemplate GetTemplate()
        {
            StockTemplate stockTemplate = new StockTemplate
            {
                EStatus = TemplateStatus.Normal,
            };

            if (!string.IsNullOrEmpty(this.tbTemplateNo.Text))
            {
                int tempNo = -1;
                if (int.TryParse(this.tbTemplateNo.Text, out tempNo))
                {
                    stockTemplate.TemplateId = tempNo;
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
                    stockTemplate.EWeightType = (WeightType)type;
                }
            }

            if (this.cbReplaceType.SelectedItem is ComboOptionItem)
            {
                ComboOptionItem item = this.cbReplaceType.SelectedItem as ComboOptionItem;
                int type = 0;
                if (int.TryParse(item.Id, out type))
                {
                    stockTemplate.EReplaceType = (ReplaceType)type;
                }
            }

            if (this.cbBenchmark.SelectedItem is ComboOptionItem)
            {
                ComboOptionItem item = this.cbBenchmark.SelectedItem as ComboOptionItem;
                stockTemplate.Benchmark = item.Id;
            }

            stockTemplate.UserId = LoginManager.Instance.GetUserId();
            
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
            StockTemplate stockTemplate = GetTemplate();
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
