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
        private const string msgInvalidSetting = "invalidsetting";

        private MonitorUnitBLL _monitorUnitBLL = new MonitorUnitBLL();
        private TemplateBLL _templateBLL = new TemplateBLL();
        private ProductBLL _productBLL = new ProductBLL();

        private InstanceItem _originTradeInstance = null;

        public TradeInstanceModifyDialog()
        {
            InitializeComponent();

            LoadControl += new FormLoadHandler(Form_LoadControl);
            LoadData += new FormLoadHandler(Form_LoadData);

            this.btnConfirm.Click += new EventHandler(Button_Confirm_Click);
            this.btnCancel.Click += new EventHandler(Button_Cancel_Click);

            //this.cbMonitorUnit.SelectedIndexChanged
        }

        #region LoadControl

        private bool Form_LoadControl(object sender, object data)
        {
            this.tbInstanceId.Enabled = false;
            this.cbFundCode.Enabled = false;
            this.cbAssetUnit.Enabled = false;
            this.cbPortfolio.Enabled = false;

            return true;
        }

        #endregion

        #region LoadData

        private bool Form_LoadData(object sender, object data)
        {
            if (data == null || !(data is InstanceItem))
                return false;

            _originTradeInstance = (InstanceItem)data;

            LoadTextBox(_originTradeInstance);
            LoadMonitorUnits(_originTradeInstance.MonitorUnitId);
            LoadTemplates(_originTradeInstance.TemplateId);
            LoadFund(_originTradeInstance.FundCode);
            LoadAssetUnit(_originTradeInstance.AssetUnitCode);
            LoadPortfolio(_originTradeInstance.PortfolioCode);

            return true;
        }

        #endregion

        #region GetData

        public override object GetData()
        {
            var newItem = GetItem();

            return newItem;
        }

        #endregion

        private void LoadTextBox(InstanceItem tradeInstance)
        {
            this.tbInstanceId.Text = string.Format("{0}", tradeInstance.InstanceId);
            this.tbInstanceCode.Text = tradeInstance.InstanceCode;

            this.tbNotes.Text = tradeInstance.Notes ?? string.Empty;
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
            if (!ValidateInput())
            {
                MessageDialog.Error(this, msgInvalidSetting);
                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion 

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(this.tbInstanceCode.Text))
            {
                return false;
            }

            var newItem = GetItem();
            if (newItem.MonitorUnitId <= 0)
            {
                return false;
            }

            if (newItem.TemplateId <= 0)
            {
                return false;
            }

            return true;
        }

        private TradeInstance GetItem()
        {
            TradeInstance tradeInstance = new TradeInstance
            {
                InstanceId = _originTradeInstance.InstanceId,
                AccountCode = _originTradeInstance.FundCode,
                AccountName = _originTradeInstance.FundName,
                AssetNo = _originTradeInstance.AssetUnitCode,
                AssetName = _originTradeInstance.AssetUnitName,
                PortfolioId = _originTradeInstance.PortfolioId,
                PortfolioCode = _originTradeInstance.PortfolioCode,
                PortfolioName = _originTradeInstance.PortfolioName,
                //Status = _originTradeInstance.Status,
            };

            //var selectItem = (ComboOptionItem)this.cbFundCode.SelectedItem;
            //if (selectItem != null)
            //{ 
            //    tradeInstance.AccountCode = selectItem.Id;
            //    tradeInstance.AccountName = selectItem.Name;
            //}

            //selectItem = (ComboOptionItem)this.cbAssetUnit.SelectedItem;
            //if (selectItem != null)
            //{
            //    tradeInstance.AssetNo = selectItem.Id;
            //    tradeInstance.AssetName = selectItem.Name;
            //}

            //selectItem = (ComboOptionItem)this.cbPortfolio.SelectedItem;
            //if (selectItem != null)
            //{
            //    tradeInstance.PortfolioCode = selectItem.Id;
            //    tradeInstance.PortfolioName = selectItem.Name;
            //}

            tradeInstance.InstanceCode = this.tbInstanceCode.Text.Trim();

            tradeInstance.Notes = this.tbNotes.Text.Trim();

            int temp = -1;
            var selectItem = (ComboOptionItem)this.cbTemplate.SelectedItem;
            if (selectItem != null)
            {
                tradeInstance.TemplateId = int.TryParse(selectItem.Id, out temp) ? temp : temp;
                tradeInstance.TemplateName = selectItem.Name;
            }

            selectItem = (ComboOptionItem)this.cbMonitorUnit.SelectedItem;
            if (selectItem != null)
            {
                tradeInstance.MonitorUnitId = int.TryParse(selectItem.Id, out temp) ? temp : temp;
                tradeInstance.MonitorUnitName = selectItem.Name;
            }

            return tradeInstance;
        }
    }
}
