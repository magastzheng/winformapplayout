using Config;
using Controls.Entity;
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

namespace TradingSystem.Dialog
{
    public partial class MonitorUnitDialog : Forms.BaseFixedForm
    {
        private FuturesContractDAO _fcdbdao = new FuturesContractDAO();
        private PortfolioDAO _portdbdao = new PortfolioDAO();
        private StockTemplateDAO _tempdbdao = new StockTemplateDAO();

        public MonitorUnitDialog()
        {
            InitializeComponent();

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
            this.SaveData += new FormLoadHandler(Form_SaveData);

            
            //TODO: set the datasource for dropdown list

            
        }

        #region loadcontrol
        private void Form_LoadControl(object sender, object data)
        {
            //FuturesContracts
            acFuturesContracts.SetDropdownList(lbDropdown);
            List<FuturesContract> itemList = _fcdbdao.Get("");
            IList<AutoItem> dataSource = new List<AutoItem>();
            foreach (var fcItem in itemList)
            {
                AutoItem autoItem = new AutoItem 
                {
                    Id = fcItem.Code,
                    Name = fcItem.Code
                };

                dataSource.Add(autoItem);
            }

            acFuturesContracts.AutoDataSource = dataSource;

            //accounttype
            cbAccountType.Enabled = false;
            var accoutTypes = ConfigManager.Instance.GetComboConfig().GetComboOption("accounttype");
            FormUtil.SetComboBox(this.cbAccountType, accoutTypes);

            //Portfolio combobox
            LoadPortfolio();

            //Load stock template combobox
            LoadStockTemplate();
        }

        private void LoadPortfolio()
        {
            var portfolios = _portdbdao.GetPortfolio("");
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
                    Name = string.Format("{0} {1} ({2}-{3})", p.PortfolioId, p.PortfolioName, p.FundId, p.AssetUnitId)
                };

                cbOption.Items.Add(item);
            }

            cbOption.Selected = cbOption.Items[0].Id;
            FormUtil.SetComboBox(this.cbPortfolioId, cbOption);
        }

        private void LoadStockTemplate()
        {
            var templates = _tempdbdao.GetTemplate(-1);
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
                    Id = p.TemplateNo.ToString(),
                    Name = string.Format("{0} {1}", p.TemplateNo, p.TemplateName)
                };

                cbOption.Items.Add(item);
            }

            cbOption.Selected = cbOption.Items[0].Id;
            FormUtil.SetComboBox(this.cbStockTemplate, cbOption);
        }
        #endregion

        private void Form_LoadData(object sender, object data)
        {

        }

        private void Form_SaveData(object sender, object data)
        {
            
        }

        private void Button_Confirm_Click(object sender, EventArgs e)
        {

        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {

        }
    }
}
