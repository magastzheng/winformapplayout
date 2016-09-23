using BLL;
using BLL.Product;
using BLL.UFX.impl;
using Config;
using Controls.Entity;
using Controls.GridView;
using Forms;
using Model.Binding.BindingUtil;
using Model.config;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using BLL.TradeCommand;
using BLL.Template;
using BLL.Entrust;

namespace TradingSystem.View
{
    public partial class HoldingTransferedForm : Forms.BaseForm
    {
        private const string GridSourceId = "sourceportfolioholding";
        private const string GridDestId = "destinationportfolioholding";
        private GridConfig _gridConfig = null;

        private SortableBindingList<SourceHoldingItem> _srcDataSource = new SortableBindingList<SourceHoldingItem>(new List<SourceHoldingItem>());
        private SortableBindingList<DestinationHoldingItem> _destDataSource = new SortableBindingList<DestinationHoldingItem>(new List<DestinationHoldingItem>());

        private ProductBLL _productBLL = new ProductBLL();
        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();
        private TemplateBLL _templateBLl = new TemplateBLL();
        private TradeInstanceSecurityBLL _tradeInstanceSecuBLL = new TradeInstanceSecurityBLL();
        private AccountBLL _accountBLL = null;

        public HoldingTransferedForm()
            :base()
        {
            InitializeComponent();
        }

        public HoldingTransferedForm(GridConfig gridConfig, BLLManager bLLManager)
            : this()
        {
            _gridConfig = gridConfig;
            _accountBLL = bLLManager.AccountBLL;

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);

            this.cbOpertionType.SelectedIndexChanged += new EventHandler(ComboBox_OpertionType_SelectedIndexChanged);
            this.cbSrcFundCode.SelectedIndexChanged += new EventHandler(ComboBox_FundCode_SelectedIndexChanged);
            this.cbDestFundCode.SelectedIndexChanged += new EventHandler(ComboBox_FundCode_SelectedIndexChanged);
            this.cbSrcPortfolio.SelectedIndexChanged += new EventHandler(ComboBox_Portfolio_SelectedIndexChanged);
            this.cbDestPortfolio.SelectedIndexChanged += new EventHandler(ComboBox_Portfolio_SelectedIndexChanged);
            this.cbSrcTradeInst.SelectedIndexChanged += new EventHandler(ComboBox_TradeInst_SelectedIndexChanged);
            this.cbDestTradeInst.SelectedIndexChanged += new EventHandler(ComboBox_TradeInst_SelectedIndexChanged);
        }

        #region combobox selected index changed event handler

        private void ComboBox_OpertionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDestFundCodeState();

            this.cbSrcFundCode.ResetText();
            ComboBoxUtil.ClearComboBox(this.cbSrcPortfolio);
            ComboBoxUtil.ClearComboBox(this.cbSrcTradeInst);

            this.cbDestFundCode.ResetText();
            ComboBoxUtil.ClearComboBox(this.cbDestPortfolio);
            ComboBoxUtil.ClearComboBox(this.cbDestTradeInst);
        }

        private void ComboBox_FundCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb == null)
                return;
            
            var fundItem = (ComboOptionItem)cb.SelectedItem;
            var portfolioOption = LoadPortfolio(fundItem.Id);

            switch (cb.Name)
            {
                case "cbSrcFundCode":
                    {
                        ComboBoxUtil.ClearComboBox(this.cbSrcPortfolio);
                        ComboBoxUtil.ClearComboBox(this.cbSrcTradeInst);

                        ComboBoxUtil.SetComboBox(this.cbSrcPortfolio, portfolioOption);

                        if (IsSecurityTransfer())
                        {
                            ComboBoxUtil.ClearComboBox(this.cbDestPortfolio);
                            ComboBoxUtil.ClearComboBox(this.cbDestTradeInst);

                            ComboBoxUtil.SetComboBoxSelect(this.cbDestFundCode, fundItem.Id);
                            ComboBoxUtil.SetComboBox(this.cbDestPortfolio, portfolioOption);
                        }
                    }
                    break;
                case "cbDestFundCode":
                    {
                        ComboBoxUtil.ClearComboBox(this.cbDestPortfolio);
                        ComboBoxUtil.ClearComboBox(this.cbDestTradeInst);

                        ComboBoxUtil.SetComboBox(this.cbDestPortfolio, portfolioOption);
                    }
                    break;
                default:
                    break;
            }
        }

        private void ComboBox_Portfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb == null)
                return;

            var portfolio = (ComboOptionItem)cb.SelectedItem;
            var tradeInstOption = LoadTradeInstance(portfolio.Id);

            switch (cb.Name)
            {
                case "cbSrcPortfolio":
                    {
                        ComboBoxUtil.ClearComboBox(this.cbSrcTradeInst);

                        ComboBoxUtil.SetComboBox(this.cbSrcTradeInst, tradeInstOption);
                    }
                    break;
                case "cbDestPortfolio":
                    {
                        ComboBoxUtil.ClearComboBox(this.cbDestTradeInst);

                        ComboBoxUtil.SetComboBox(this.cbDestTradeInst, tradeInstOption);
                    }
                    break;
                default:
                    break;
            }
        }

        private void ComboBox_TradeInst_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb == null)
                return;

            var tradeInst = (ComboOptionItem)cb.SelectedItem;
            if(string.IsNullOrEmpty(tradeInst.Id) || tradeInst.Id.StartsWith("empty"))
                return;

            var securities = _tradeInstanceSecuBLL.Get(((TradingInstance)tradeInst.Data).InstanceId);
            switch (cb.Name)
            {
                case "cbSrcTradeInst":
                    {
                        _srcDataSource.Clear();

                        foreach (var secuItem in securities)
                        {
                            SourceHoldingItem srcItem = new SourceHoldingItem 
                            {
                                SecuCode = secuItem.SecuCode,
                                CurrentAmount = secuItem.PositionAmount,
                                SecuType = secuItem.SecuType,
                                PositionType = secuItem.PositionType,
                            };

                            _srcDataSource.Add(srcItem);
                        }
                    }
                    break;
                case "cbDestTradeInst":
                    {
                        _destDataSource.Clear();

                        foreach (var secuItem in securities)
                        {
                            DestinationHoldingItem destItem = new DestinationHoldingItem 
                            {
                                SecuCode = secuItem.SecuCode,
                                SecuType = secuItem.SecuType,
                                CurrentAmount = secuItem.PositionAmount,
                                PositionType = secuItem.PositionType,
                            };

                            _destDataSource.Add(destItem);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        
        #endregion

        #region load control

        private bool Form_LoadControl(object sender, object data)
        {
            TSDataGridViewHelper.AddColumns(this.srcGridView, _gridConfig.GetGid(GridSourceId));
            Dictionary<string, string> colDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(SourceHoldingItem));
            TSDataGridViewHelper.SetDataBinding(this.srcGridView, colDataMap);

            this.srcGridView.DataSource = _srcDataSource;

            TSDataGridViewHelper.AddColumns(this.destGridView, _gridConfig.GetGid(GridDestId));
            colDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(DestinationHoldingItem));
            TSDataGridViewHelper.SetDataBinding(this.destGridView, colDataMap);

            this.destGridView.DataSource = _destDataSource;

            //Load child control
            LoadProductControl();

            return true;
        }

        private void LoadProductControl()
        {
            var operationType = ConfigManager.Instance.GetComboConfig().GetComboOption("operationtype");
            ComboBoxUtil.SetComboBox(this.cbOpertionType, operationType);
        }

        private bool IsSecurityTransfer()
        {
            return ((ComboOptionItem)this.cbOpertionType.SelectedItem).Id.Equals("1");
        }

        private void SetDestFundCodeState()
        {
            if (IsSecurityTransfer())
            {
                this.cbDestFundCode.Enabled = false;
            }
            else
            {
                this.cbDestFundCode.Enabled = true;
            }
        }
        #endregion

        #region load data

        private bool Form_LoadData(object sender, object data)
        {
            //load template
            LoadTemplate();

            //TODO:
            LoadProductData();

            UFXQueryHoldingBLL holdingBLL = new UFXQueryHoldingBLL();
            holdingBLL.Query(null);

            return true;
        }

        private bool LoadProductData()
        {
            var fundOption = LoadFund();
            ComboBoxUtil.SetComboBox(this.cbSrcFundCode, fundOption);
            ComboBoxUtil.SetComboBox(this.cbDestFundCode, fundOption);

            return true;
        }


        private ComboOption LoadFund()
        {
            var accounts = LoginManager.Instance.Accounts;
            if (accounts == null || accounts.Count == 0)
            {
                var result = _accountBLL.QueryAccount();
                if (result == Model.ConnectionCode.Success)
                {
                    accounts = LoginManager.Instance.Accounts;
                }
            }

            var optionList = new List<ComboOptionItem>();
            foreach (var account in accounts)
            {
                ComboOptionItem item = new ComboOptionItem 
                {
                    Id = account.AccountCode,
                    Name = account.AccountName,
                    Code = account.AccountCode,
                    Data = account
                };

                optionList.Add(item);
            }

            var accoutOption = new ComboOption
            {
                Name = optionList[0].Name,
                Selected = optionList[0].Id,
                Items = optionList
            };

            return accoutOption;
        }

        private ComboOption LoadPortfolio(string fundCode)
        {
            var optionList = new List<ComboOptionItem>();

            ComboOptionItem emptyItem = new ComboOptionItem
            {
                Id = "emptyportfolio",
                Name = "",
                Code = "",
                Data = "emptyportfolio"
            };

            optionList.Add(emptyItem);

            var portfolioOption = new ComboOption
            {
                Name = optionList[0].Name,
                Selected = optionList[0].Id,
                Items = optionList
            };

            var portfolios = LoginManager.Instance.Portfolios;
            if (portfolios == null || portfolios.Count == 0)
            {
                var result = _accountBLL.QueryPortfolio();
                if (result != Model.ConnectionCode.Success)
                {
                    return portfolioOption;
                }
                else
                {
                    portfolios = LoginManager.Instance.Portfolios;
                }
            }

            var validPortfolios = portfolios.Where(p => p.AccountCode.Equals(fundCode));

            foreach (var portfolio in validPortfolios)
            {
                ComboOptionItem item = new ComboOptionItem
                {
                    Id = portfolio.CombiNo,
                    Name = portfolio.CombiName,
                    Code = portfolio.CombiNo,
                    Data = portfolio
                };

                optionList.Add(item);
            }

            return portfolioOption;
        }

        private ComboOption LoadTradeInstance(string portfolioCode)
        {
            var tradeInstances = _tradeInstanceBLL.GetPortfolioInstance(portfolioCode);
            var optionList = new List<ComboOptionItem>();

            ComboOptionItem emptyItem = new ComboOptionItem
            {
                Id = "emptytradeinstance",
                Name = "",
                Code = "",
                Data = "emptytradeinstance"
            };

            optionList.Add(emptyItem);

            foreach (var tradeInstance in tradeInstances)
            {
                ComboOptionItem item = new ComboOptionItem
                {
                    Id = tradeInstance.InstanceId.ToString(),
                    Name = tradeInstance.InstanceCode,
                    Code = tradeInstance.InstanceId.ToString(),
                    Data = tradeInstance,
                };

                optionList.Add(item);
            }

            var tradeInstOption = new ComboOption
            {
                Name = optionList[0].Name,
                Selected = optionList[0].Id,
                Items = optionList
            };

            return tradeInstOption;
        }

        private bool LoadTemplate()
        {
            var templates = _templateBLl.GetTemplates();

            var optionList = new List<ComboOptionItem>();

            foreach (var template in templates)
            {
                ComboOptionItem item = new ComboOptionItem
                {
                    Id = template.TemplateId.ToString(),
                    Name = template.TemplateName,
                    Code = template.TemplateId.ToString(),
                    Data = template,
                };

                optionList.Add(item);
            }

            var templateOption = new ComboOption
            {
                Name = optionList[0].Name,
                Selected = optionList[0].Id,
                Items = optionList
            };

            ComboBoxUtil.SetComboBox(this.cbTemplate, templateOption);

            return true;
        }

        #endregion
    }
}
