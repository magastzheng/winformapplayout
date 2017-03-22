using BLL.Product;
using BLL.Template;
using Config;
using Forms;
using Model.config;
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
    public partial class TradeInstanceModifyDialog : Forms.BaseDialog
    {
        private MonitorUnitBLL _monitorUnitBLL = new MonitorUnitBLL();
        private TemplateBLL _templateBLL = new TemplateBLL();
        private ProductBLL _productBLL = new ProductBLL();

        public TradeInstanceModifyDialog()
        {
            InitializeComponent();

            LoadControl += new FormLoadHandler(Form_LoadControl);
            LoadData += new FormLoadHandler(Form_LoadData);

            this.btnConfirm.Click += new EventHandler(Button_Confirm_Click);
            this.btnCancel.Click += new EventHandler(Button_Cancel_Click);
        }

        private bool Form_LoadControl(object sender, object data)
        {
            this.tbInstanceId.Enabled = false;
            this.cbFundCode.Enabled = false;
            this.cbAssetUnit.Enabled = false;
            this.cbPortfolio.Enabled = false;

            return true;
        }

        private bool Form_LoadData(object sender, object data)
        {
            if (data == null || !(data is TradeInstance))
                return false;

            TradeInstance tradeInstance = (TradeInstance)data;

            LoadTextBox(tradeInstance);
            LoadMonitorUnits(tradeInstance.MonitorUnitId);
            LoadTemplates(tradeInstance.TemplateId);
            LoadFund(tradeInstance.AccountCode);
            LoadAssetUnit(tradeInstance.AssetNo);
            LoadPortfolio(tradeInstance.PortfolioCode);

            return true;
        }

        private void LoadTextBox(TradeInstance tradeInstance)
        {
            this.tbInstanceId.Text = string.Format("{0}", tradeInstance.InstanceId);
            this.tbInstanceCode.Text = tradeInstance.InstanceCode;
        }

        private void LoadMonitorUnits(int monitorUnitId)
        {
            var monitorUnits = _monitorUnitBLL.GetAll();
            ComboOption comboOption = new ComboOption
            {
                Items = new List<ComboOptionItem>()
            };

            if (monitorUnits != null && monitorUnits.Count > 0)
            {
                foreach (var monitorUnit in monitorUnits)
                {
                    ComboOptionItem item = new ComboOptionItem
                    {
                        Id = string.Format("{0}", monitorUnit.MonitorUnitId),
                        Name = monitorUnit.MonitorUnitName
                    };

                    comboOption.Items.Add(item);
                }
            }

            ComboBoxUtil.SetComboBox(this.cbMonitorUnit, comboOption);
            ComboBoxUtil.SetComboBoxSelect(this.cbMonitorUnit, monitorUnitId.ToString());
        }

        private void LoadTemplates(int templateId)
        {
            var templates = _templateBLL.GetTemplates();
            ComboOption comboOption = new ComboOption
            {
                Items = new List<ComboOptionItem>()
            };

            if (templates != null && templates.Count > 0)
            {
                foreach (var template in templates)
                {
                    ComboOptionItem item = new ComboOptionItem
                    {
                        Id = string.Format("{0}", template.TemplateId),
                        Name = template.TemplateName
                    };

                    comboOption.Items.Add(item);
                }
            }

            ComboBoxUtil.SetComboBox(this.cbTemplate, comboOption);
            ComboBoxUtil.SetComboBoxSelect(this.cbTemplate, templateId.ToString());
        }

        private void LoadFund(string fundCode)
        {
            var accounts = LoginManager.Instance.Accounts;
            ComboOption comboOption = new ComboOption
            {
                Items = new List<ComboOptionItem>()
            };

            if (accounts != null && accounts.Count > 0)
            {
                foreach (var account in accounts)
                {
                    ComboOptionItem item = new ComboOptionItem
                    {
                        Id = string.Format("{0}", account.AccountCode),
                        Name = account.AccountName
                    };

                    comboOption.Items.Add(item);
                }
            }

            ComboBoxUtil.SetComboBox(this.cbFundCode, comboOption);
            ComboBoxUtil.SetComboBoxSelect(this.cbFundCode, fundCode);
        }

        private void LoadAssetUnit(string assetCode)
        {
            var assetUnits = LoginManager.Instance.Assets;
            ComboOption comboOption = new ComboOption
            {
                Items = new List<ComboOptionItem>()
            };

            if (assetUnits != null && assetUnits.Count > 0)
            {
                foreach (var assetUnit in assetUnits)
                {
                    ComboOptionItem item = new ComboOptionItem
                    {
                        Id = string.Format("{0}", assetUnit.AssetNo),
                        Name = assetUnit.AssetName
                    };

                    comboOption.Items.Add(item);
                }
            }

            ComboBoxUtil.SetComboBox(this.cbAssetUnit, comboOption);
            ComboBoxUtil.SetComboBoxSelect(this.cbAssetUnit, assetCode);
        }

        private void LoadPortfolio(string portfolioCode)
        {
            var portfolios = LoginManager.Instance.Portfolios;
            ComboOption comboOption = new ComboOption
            {
                Items = new List<ComboOptionItem>()
            };

            if (portfolios != null && portfolios.Count > 0)
            {
                foreach (var portfolio in portfolios)
                {
                    ComboOptionItem item = new ComboOptionItem
                    {
                        Id = string.Format("{0}", portfolio.CombiNo),
                        Name = portfolio.CombiName
                    };

                    comboOption.Items.Add(item);
                }
            }

            ComboBoxUtil.SetComboBox(this.cbPortfolio, comboOption);
            ComboBoxUtil.SetComboBoxSelect(this.cbPortfolio, portfolioCode);
        }

        #region button click event handler

        private void Button_Confirm_Click(object sender, EventArgs e)
        {

        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {

        }

        #endregion 
    }
}
