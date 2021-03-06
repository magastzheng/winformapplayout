﻿using BLL.FuturesContractManager;
using BLL.Product;
using BLL.SecurityInfo;
using BLL.Template;
using Config;
using Controls.Entity;
using Forms;
using Model.config;
using Model.EnumType;
using Model.SecurityInfo;
using Model.UI;
using System;
using System.Collections.Generic;

namespace TradingSystem.Dialog
{
    public partial class MonitorUnitDialog : Forms.BaseDialog
    {
        private const string msgNoEmptyNameContract = "monitornoemptynameandcontract";

        //private FuturesContractBLL _futureBLL = new FuturesContractBLL();
        private ProductBLL _productBLL = new ProductBLL();
        private TemplateBLL _templateBLL = new TemplateBLL();

        public MonitorUnitDialog()
        {
            InitializeComponent();

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
            //this.SaveData += new FormLoadHandler(Form_SaveData);

            //TODO: set the datasource for dropdown list
        }

        #region loadcontrol
        private bool Form_LoadControl(object sender, object data)
        {
            //FuturesContracts
            acFuturesContracts.SetDropdownList(lbDropdown);
            List<SecurityItem> itemList = FuturesContractManager.Instance.GetAll(); //_futureBLL.GetAll();
            IList<AutoItem> dataSource = new List<AutoItem>();
            foreach (var fcItem in itemList)
            {
                AutoItem autoItem = new AutoItem 
                {
                    Id = fcItem.SecuCode,
                    Name = fcItem.SecuName
                };

                dataSource.Add(autoItem);
            }

            acFuturesContracts.AutoDataSource = dataSource;

            //accounttype
            cbAccountType.Enabled = false;
            var accoutTypes = ConfigManager.Instance.GetComboConfig().GetComboOption("accounttype");
            ComboBoxUtil.SetComboBox(this.cbAccountType, accoutTypes);

            //Portfolio combobox
            LoadPortfolio();

            //Load stock template combobox
            LoadStockTemplate();

            return true;
        }

        private void LoadPortfolio()
        {
            var portfolios = _productBLL.GetAll();
            if (portfolios == null || portfolios.Count == 0)
                return;

            ComboOption cbOption = new ComboOption
            {
                Items = new List<ComboOptionItem>()
            };

            foreach (var p in portfolios)
            {
                ComboOptionItem item = new ComboOptionItem
                {
                    Id = p.PortfolioId.ToString(),
                    Data = p,
                    Name = string.Format("{0} {1} ({2}-{3})", p.PortfolioId, p.PortfolioName, p.FundName, p.AssetName)
                };

                cbOption.Items.Add(item);
            }

            cbOption.Selected = cbOption.Items[0].Id;
            ComboBoxUtil.SetComboBox(this.cbPortfolioId, cbOption);
        }

        private void LoadStockTemplate()
        {
            var templates = _templateBLL.GetTemplates();
            if (templates == null || templates.Count == 0)
                return;

            ComboOption cbOption = new ComboOption
            {
                Items = new List<ComboOptionItem>()
            };

            foreach (var p in templates)
            {
                ComboOptionItem item = new ComboOptionItem
                {
                    Id = p.TemplateId.ToString(),
                    Data = p,
                    Name = string.Format("{0} {1}", p.TemplateId, p.TemplateName)
                };

                cbOption.Items.Add(item);
            }

            cbOption.Selected = cbOption.Items[0].Id;
            ComboBoxUtil.SetComboBox(this.cbStockTemplate, cbOption);
        }
        #endregion

        #region 
        private bool ValidateMonitorUnit(MonitorUnit monitorUnit)
        {
            if (string.IsNullOrEmpty(monitorUnit.MonitorUnitName))
            {
                return false;
            }

            if(monitorUnit.PortfolioId <= 0)
            {
                return false;
            }

            if (monitorUnit.StockTemplateId <= 0)
            {
                return false;
            }
            if (string.IsNullOrEmpty(monitorUnit.BearContract))
            {
                return false;
            }

            return true;
        }

        private MonitorUnit GetMonitorUnit()
        {
            MonitorUnit monitorUnit = new MonitorUnit();
            if (!string.IsNullOrEmpty(tbMonitorUnitId.Text) && tbMonitorUnitId.Text != "0")
            {
                int temp = 0;
                if (int.TryParse(tbMonitorUnitId.Text, out temp))
                {
                    monitorUnit.MonitorUnitId = temp;
                }
            }

            monitorUnit.MonitorUnitName = tbMonitorUnitName.Text;
            ComboOptionItem accountItem = (ComboOptionItem)cbAccountType.SelectedItem;
            if (accountItem != null && !string.IsNullOrEmpty(accountItem.Id))
            {
                int temp = 0;
                if (int.TryParse(accountItem.Id, out temp))
                {
                    monitorUnit.EAccountType = (MonitorUnitAccountType)temp;
                }
            }

            ComboOptionItem portItem = (ComboOptionItem)cbPortfolioId.SelectedItem;
            if (portItem != null)
            {
                if (!string.IsNullOrEmpty(portItem.Id))
                {
                    int temp = 0;
                    if (int.TryParse(portItem.Id, out temp))
                    {
                        monitorUnit.PortfolioId = temp;
                    }
                }

                if (portItem.Data != null && portItem.Data is Portfolio)
                {
                    monitorUnit.PortfolioName = (portItem.Data as Portfolio).PortfolioName;
                }
            }

            ComboOptionItem tempItem = (ComboOptionItem)cbStockTemplate.SelectedItem;
            if (tempItem != null)
            {
                if (!string.IsNullOrEmpty(tempItem.Id))
                {
                    int temp = 0;
                    if (int.TryParse(tempItem.Id, out temp))
                    {
                        monitorUnit.StockTemplateId = temp;
                    }
                }

                if (tempItem.Data != null && tempItem.Data is StockTemplate)
                {
                    monitorUnit.StockTemplateName = (tempItem.Data as StockTemplate).TemplateName;
                }
            }

            AutoItem futuresItem = acFuturesContracts.GetCurrentItem();
            if (futuresItem != null && !string.IsNullOrEmpty(futuresItem.Id))
            {
                monitorUnit.BearContract = futuresItem.Id;
            }

            return monitorUnit;
        }
        #endregion

        private bool Form_LoadData(object sender, object data)
        {
            if (data != null && data is MonitorUnit)
            {
                MonitorUnit monitorUnit = data as MonitorUnit;
                tbMonitorUnitId.Text = string.Format("{0}", monitorUnit.MonitorUnitId);
                tbMonitorUnitName.Text = monitorUnit.MonitorUnitName;
                //cbPortfolioId.SelectedValue
                ComboBoxUtil.SetComboBoxSelect(cbPortfolioId, monitorUnit.PortfolioId.ToString());
                ComboBoxUtil.SetComboBoxSelect(cbStockTemplate, monitorUnit.StockTemplateId.ToString());

                AutoItem autoItem = new AutoItem 
                {
                    Id = monitorUnit.BearContract,
                    Name = monitorUnit.BearContract
                };

                acFuturesContracts.SetCurrentItem(autoItem);
            }

            return true;
        }

        //private void Form_SaveData(object sender, object data)
        //{
            
        //}

        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            MonitorUnit monitorUnit = GetMonitorUnit();
            if (ValidateMonitorUnit(monitorUnit))
            {
                OnSave(this, monitorUnit);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                //TODO: make the error message
                //DialogResult = System.Windows.Forms.DialogResult.No;
                //MessageBox.Show(this, "请确保监控名称和空头合约不为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageDialog.Error(this, msgNoEmptyNameContract);
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
        }
    }
}
