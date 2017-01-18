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
using BLL.Template;
using TradingSystem.Dialog;
using Model.strategy;
using BLL.TradeInstance;
using Model.SecurityInfo;
using Quote;
using BLL.Manager;

namespace TradingSystem.View
{
    public partial class HoldingTransferedForm : Forms.BaseForm
    {
        private const string msgNoSecuritySelected = "transferholdingnosecurityselected";
        private const string msgNoTransferAmount = "transferholdingnotransferamount";
        private const string msgNoEmptyDest = "transferholdingnoemptydest";
        private const string msgSuccess = "transferholdingsuccess";
        private const string msgInvalidAmount = "transferholdinginvalidamount";
        private const string msgCannotSameInstance = "transferholdingcannotsameinstance";

        private const string GridSourceId = "sourceportfolioholding";
        private const string GridDestId = "destinationportfolioholding";
        private GridConfig _gridConfig = null;

        private const string emptyPortfolioId = "emptyportfolio";
        private const int emptyInstanceId = -1;
        private const string emptyInstanceCode = "emptytradeinstance";

        private SortableBindingList<SourceHoldingItem> _srcDataSource = new SortableBindingList<SourceHoldingItem>(new List<SourceHoldingItem>());
        private SortableBindingList<DestinationHoldingItem> _destDataSource = new SortableBindingList<DestinationHoldingItem>(new List<DestinationHoldingItem>());

        private ProductBLL _productBLL = new ProductBLL();
        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();
        private TemplateBLL _templateBLl = new TemplateBLL();
        private TradeInstanceSecurityBLL _tradeInstanceSecuBLL = new TradeInstanceSecurityBLL();
        private AccountBLL _accountBLL = null;

        private List<SecurityItem> _secuList = new List<SecurityItem>();

        public HoldingTransferedForm()
            :base()
        {
            InitializeComponent();
        }

        public HoldingTransferedForm(GridConfig gridConfig, UFXBLLManager bLLManager)
            : this()
        {
            _gridConfig = gridConfig;
            _accountBLL = bLLManager.AccountBLL;

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);

            this.srcGridView.UpdateRelatedDataGridHandler += new UpdateRelatedDataGrid(GridView_Source_UpdateRelatedDataGridHandler);
            this.srcGridView.MouseDown += new MouseEventHandler(GridView_MouseDown);
            this.destGridView.MouseDown += new MouseEventHandler(GridView_MouseDown);

            this.cbOpertionType.SelectedIndexChanged += new EventHandler(ComboBox_OpertionType_SelectedIndexChanged);
            this.cbSrcFundCode.SelectedIndexChanged += new EventHandler(ComboBox_FundCode_SelectedIndexChanged);
            this.cbDestFundCode.SelectedIndexChanged += new EventHandler(ComboBox_FundCode_SelectedIndexChanged);
            this.cbSrcPortfolio.SelectedIndexChanged += new EventHandler(ComboBox_Portfolio_SelectedIndexChanged);
            this.cbDestPortfolio.SelectedIndexChanged += new EventHandler(ComboBox_Portfolio_SelectedIndexChanged);
            this.cbSrcTradeInst.SelectedIndexChanged += new EventHandler(ComboBox_TradeInst_SelectedIndexChanged);
            this.cbDestTradeInst.SelectedIndexChanged += new EventHandler(ComboBox_TradeInst_SelectedIndexChanged);

            //this.cbSrcTradeInst.DropDownClosed += new EventHandler(ComboBox_TradeInst_DropDownClosed);
            //this.cbDestTradeInst.DropDownClosed += new EventHandler(ComboBox_TradeInst_DropDownClosed);
            //this.cbSrcTradeInst.LostFocus += new EventHandler(ComboBox_TradeInst_LostFocus);
            //this.cbDestTradeInst.LostFocus += new EventHandler(ComboBox_TradeInst_LostFocus);

            //button click
            this.btnTransfer.Click += new EventHandler(Button_Transfer_Click);
            this.btnRefresh.Click += new EventHandler(Button_Refresh_Click);
            this.btnCalc.Click += new EventHandler(Button_Calc_Click);
        }

        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (!ValidateTradingInstance(this.cbSrcTradeInst, this.cbDestTradeInst))
            {
                MessageDialog.Warn(this, "目标交易实例不能与源交易实例相同！");
                SetFocus();
            }
        }

        private void GridView_Source_UpdateRelatedDataGridHandler(UpdateDirection direction, int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= _srcDataSource.Count)
                return;

            SourceHoldingItem selectedItem = _srcDataSource[rowIndex];

            switch (direction)
            {
                case UpdateDirection.Select:
                    {
                        GridView_Source_Select(selectedItem);
                    }
                    break;
                case UpdateDirection.UnSelect:
                    {
                        GridView_Source_UnSelect(selectedItem);
                    }
                    break;
                default:
                    break;
            }

            this.srcGridView.Invalidate();
        }

        private void GridView_Source_Select(SourceHoldingItem selectedItem)
        {
            selectedItem.PriceType = "lastprice";
            selectedItem.TransferedAmount = selectedItem.AvailableTransferedAmount;

            //TODO: update the price
            var targetItem = _secuList.Find(p => p.SecuCode.Equals(selectedItem.SecuCode));
            if (targetItem == null)
            {
                targetItem = SecurityInfoManager.Instance.Get(selectedItem.SecuCode, selectedItem.SecuType);
            }

            if (targetItem != null)
            {
                var marketData = QuoteCenter2.Instance.GetMarketData(targetItem);
                if (marketData != null)
                {
                    selectedItem.TransferedPrice = marketData.CurrentPrice;
                }
            }
        }

        private void GridView_Source_UnSelect(SourceHoldingItem selectedItem)
        {
            selectedItem.PriceType = "";
            selectedItem.TransferedAmount = 0;
            selectedItem.TransferedPrice = 0;
        }

        private void QueryQuote()
        {
            //query the price and set it
            
            var uniqueSecuItems = _srcDataSource.GroupBy(p => p.SecuCode).Select(o => o.First());
            foreach (var secuItem in uniqueSecuItems)
            {
                if (_secuList.Find(p => p.SecuCode.Equals(secuItem.SecuCode)) == null)
                {
                    var findItem = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                    if (findItem != null)
                    {
                        _secuList.Add(findItem);
                    }
                }
            }

            //更新行情相关数据
            //List<PriceType> priceTypes = new List<PriceType>() { PriceType.Last };
            //QuoteCenter2.Instance.Query(_secuList, priceTypes);
        }

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

            LoadTradeDirectionOption();

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

        private void LoadTradeDirectionOption()
        {
            var transferPriceType = ConfigManager.Instance.GetComboConfig().GetComboOption("transferpricetype");
            TSDataGridViewHelper.SetDataBinding(this.srcGridView, "pricetype", transferPriceType);
        }

        #endregion

        #region load data

        private bool Form_LoadData(object sender, object data)
        {
            //clear old data
            ClearData();

            //load template
            LoadTemplate();

            //TODO:
            LoadProductData();

            //UFXQueryHoldingBLL holdingBLL = new UFXQueryHoldingBLL();
            //holdingBLL.Query(null);

            return true;
        }

        private void ClearData()
        {
            ComboBoxUtil.ClearComboBox(this.cbTemplate);
            ComboBoxUtil.ClearComboBox(this.cbSrcFundCode);
            ComboBoxUtil.ClearComboBox(this.cbSrcPortfolio);
            ComboBoxUtil.ClearComboBox(this.cbSrcTradeInst);
            ComboBoxUtil.ClearComboBox(this.cbDestFundCode);
            ComboBoxUtil.ClearComboBox(this.cbDestPortfolio);
            ComboBoxUtil.ClearComboBox(this.cbDestTradeInst);
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
                Items = optionList
            };

            if (optionList.Count > 0)
            {
                accoutOption.Name = optionList[0].Name;
                accoutOption.Selected = optionList[0].Id;
            }

            return accoutOption;
        }

        private ComboOption LoadPortfolio(string fundCode)
        {
            var optionList = new List<ComboOptionItem>();

            PortfolioItem emptyPortfolio = new PortfolioItem 
            {
                AccountCode = fundCode,
                CombiNo = emptyPortfolioId,
                CombiName = string.Empty,
            };
            ComboOptionItem emptyItem = new ComboOptionItem
            {
                Id = emptyPortfolio.CombiNo,
                Name = "",
                Code = "",
                Data = emptyPortfolio
            };

            optionList.Add(emptyItem);

            var portfolioOption = new ComboOption
            {
                Items = optionList
            };

            if(optionList.Count > 0)
            {
                portfolioOption.Name = optionList[0].Name;
                portfolioOption.Selected = optionList[0].Id;
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

            TradeInstance emptyInstance = new TradeInstance 
            {
                InstanceId = emptyInstanceId,
                InstanceCode = emptyInstanceCode,
            };

            ComboOptionItem emptyItem = new ComboOptionItem
            {
                Id = emptyInstance.InstanceId.ToString(),
                Name = "",
                Code = "",
                Data = emptyInstance,
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
                Items = optionList
            };

            if(optionList.Count > 0)
            {
                tradeInstOption.Name = optionList[0].Name;
                tradeInstOption.Selected = optionList[0].Id;
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
                Items = optionList
            };

            if(optionList.Count > 0)
            {
                templateOption.Name = optionList[0].Name;
                templateOption.Selected = optionList[0].Id;
            };

            ComboBoxUtil.SetComboBox(this.cbTemplate, templateOption);

            return true;
        }

        #endregion


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

                        //TODO: load the gridview
                        _srcDataSource.Clear();
                    }
                    break;
                case "cbDestPortfolio":
                    {
                        ComboBoxUtil.ClearComboBox(this.cbDestTradeInst);

                        ComboBoxUtil.SetComboBox(this.cbDestTradeInst, tradeInstOption);

                        //TODO:load the gridview
                        _destDataSource.Clear();
                    }
                    break;
                default:
                    break;
            }
        }

        private void ComboBox_TradeInst_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("SelectedIndexChanged: " + e.ToString());

            ComboBox cb = sender as ComboBox;
            if (cb == null)
                return;

            var tradeInst = (ComboOptionItem)cb.SelectedItem;
            //if(string.IsNullOrEmpty(tradeInst.Id) || tradeInst.Id.StartsWith("empty"))
            //    return;
            if (!(tradeInst.Data is TradeInstance))
                return;
            

            //TODO: cannot select the same TradingInstance
            var instance = tradeInst.Data as TradeInstance;
            var securities = _tradeInstanceSecuBLL.Get(instance.InstanceId);
            switch (cb.Name)
            {
                case "cbSrcTradeInst":
                    {
                        _srcDataSource.Clear();
                        FillSrcGridView(_srcDataSource, securities, instance);

                        //Quote the price
                        QueryQuote();
                    }
                    break;
                case "cbDestTradeInst":
                    {
                        _destDataSource.Clear();
                        FillDestGridView(_destDataSource, securities, instance);
                    }
                    break;
                default:
                    break;
            }
        }

        private bool ValidateTradingInstance(ComboBox cbSrc, ComboBox cbDest)
        {
            var srcTradeInst = (ComboOptionItem)this.cbSrcTradeInst.SelectedItem;
            var destTradeInst = (ComboOptionItem)this.cbDestTradeInst.SelectedItem;

            if (srcTradeInst != null && destTradeInst != null)
            {
                if (srcTradeInst.Id.Equals(destTradeInst.Id))
                {
                    return false;
                }
            }

            return true;
        }

        private void SetFocus()
        {
            this.cbDestTradeInst.Focus();
        }

        private void FillSrcGridView(SortableBindingList<SourceHoldingItem> dataSource, List<TradeInstanceSecurity> secuItems, TradeInstance tradeInstance)
        {
            foreach (var secuItem in secuItems)
            {
                SourceHoldingItem srcItem = new SourceHoldingItem
                {
                    SecuCode = secuItem.SecuCode,
                    CurrentAmount = secuItem.PositionAmount,
                    AvailableTransferedAmount = secuItem.PositionAmount,
                    SecuType = secuItem.SecuType,
                    PositionType = secuItem.PositionType,
                    PortfolioCode = tradeInstance.PortfolioCode,
                    PortfolioName = tradeInstance.PortfolioName
                };

                var findItem = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                if (findItem != null)
                {
                    srcItem.SecuName = findItem.SecuName;
                    srcItem.ExchangeCode = findItem.ExchangeCode;
                }
                dataSource.Add(srcItem);
            }
        }

        private void FillDestGridView(SortableBindingList<DestinationHoldingItem> dataSource, List<TradeInstanceSecurity> secuItems, TradeInstance tradeInstance)
        {
            foreach (var secuItem in secuItems)
            {
                DestinationHoldingItem destItem = new DestinationHoldingItem
                {
                    SecuCode = secuItem.SecuCode,
                    SecuType = secuItem.SecuType,
                    CurrentAmount = secuItem.PositionAmount,
                    PositionType = secuItem.PositionType,
                    PortfolioCode = tradeInstance.PortfolioCode,
                    PortfolioName = tradeInstance.PortfolioName
                };

                var findItem = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                if (findItem != null)
                {
                    destItem.SecuName = findItem.SecuName;
                    destItem.ExchangeCode = findItem.ExchangeCode;
                }

                dataSource.Add(destItem);
            }
        }

        #endregion

        #region button click event

        private void Button_Calc_Click(object sender, EventArgs e)
        {
            var selectedItem = this.cbTemplate.SelectedItem as ComboOptionItem;
            if (selectedItem == null)
            {
                return;
            }

            var template = selectedItem.Data as StockTemplate;
            if (template == null)
            {
                return;
            }

            int copies = (int)this.nudCopies.Value;
            if (copies <= 0)
            {
                return;
            }

            var stocks = _templateBLl.GetStocks(template.TemplateId);
            foreach (var srcItem in _srcDataSource)
            {
                var findItem = stocks.Find(p => p.SecuCode.Equals(srcItem.SecuCode));
                if (findItem != null)
                {
                    srcItem.Seletion = true;
                    srcItem.PriceType = "lastprice";

                    int amount = copies * findItem.Amount;
                    srcItem.CalcAmount = amount;

                    if (srcItem.AvailableTransferedAmount > amount)
                    {
                        srcItem.TransferedAmount = amount;
                    }
                    else
                    {
                        srcItem.TransferedAmount = srcItem.AvailableTransferedAmount;
                    }
                }
                else
                {
                    srcItem.CalcAmount = 0;
                }
            }
        }

        private void Button_Refresh_Click(object sender, EventArgs e)
        {
            //all selection items have the TransferAmount=Available, CalcAmount=0
            _srcDataSource.ToList()
                .Where(p => p.Seletion)
                .ToList()
                .ForEach(o => 
                { 
                    o.CalcAmount = 0;
                    o.TransferedAmount = o.AvailableTransferedAmount;
                });

            this.srcGridView.Invalidate();
        }

        private void Button_Transfer_Click(object sender, EventArgs e)
        {
            //get the source portfolio, instance
            AccountItem srcAccount = null;
            var srcSelectFund = cbSrcFundCode.SelectedItem as ComboOptionItem;
            if (srcSelectFund != null)
            {
                srcAccount = srcSelectFund.Data as AccountItem;
            }

            PortfolioItem srcPortfolio = null;
            var srcSelectPortfolio = cbSrcPortfolio.SelectedItem as ComboOptionItem;
            if (srcSelectPortfolio != null)
            {
                srcPortfolio = srcSelectPortfolio.Data as PortfolioItem;
            }

            TradeInstance srcTradingInstance = null;
            var srcSelectInstance = cbSrcTradeInst.SelectedItem as ComboOptionItem;
            if (srcSelectInstance != null)
            {
                srcTradingInstance = srcSelectInstance.Data as TradeInstance;
            }

            //get the target portfolio, instance
            AccountItem destAccount = null;
            var destSelectFund = cbDestFundCode.SelectedItem as ComboOptionItem;
            if (destSelectFund != null)
            {
                destAccount = destSelectFund.Data as AccountItem;
            }
            PortfolioItem destPortfolio = null;
            var destSelectPortfolio = cbDestPortfolio.SelectedItem as ComboOptionItem;
            if (destSelectPortfolio != null)
            {
                destPortfolio = destSelectPortfolio.Data as PortfolioItem;
            }

            TradeInstance destTradingInstance = null;
            var destSelectInstance = cbDestTradeInst.SelectedItem as ComboOptionItem;
            if (destSelectInstance != null)
            {
                destTradingInstance = destSelectInstance.Data as TradeInstance;
            }

            if (!ValidatePortfolio(srcPortfolio) || !ValidatePortfolio(destPortfolio))
            {
                MessageDialog.Warn(this, msgNoEmptyDest);
                return;
            }

            if (!ValidateTradingInstance(srcTradingInstance) || !ValidateTradingInstance(destTradingInstance))
            {
                MessageDialog.Warn(this, msgNoEmptyDest);
                return;
            }

            var selectedItems = _srcDataSource.Where(p => p.Seletion).ToList();
            if (selectedItems.Count == 0)
            {
                MessageDialog.Warn(this, msgNoSecuritySelected);
                return;
            }

            var selectedAmountItems = selectedItems.Where(p => p.TransferedAmount <= 0).ToList();
            if (selectedAmountItems.Count > 0)
            {
                MessageDialog.Warn(this, msgNoTransferAmount);
                return;
            }

            var invalidItems = selectedItems.Where(p => p.TransferedAmount > p.AvailableTransferedAmount || p.TransferedAmount > p.CurrentAmount).ToList();
            if (invalidItems.Count > 0)
            { 
                MessageDialog.Warn(this, msgInvalidAmount);
                return;
            }

            if (srcTradingInstance != null && destTradingInstance != null)
            {
                int ret = _tradeInstanceSecuBLL.Transfer(destTradingInstance, srcTradingInstance, selectedItems);
                if (ret > 0)
                {
                    MessageDialog.Info(this, msgSuccess);
                    //TODO: update the gridview security in both source and destination
                    AdjustSecurity(selectedItems);

                    this.srcGridView.Invalidate();
                    this.destGridView.Invalidate();
                }
            }
        }


        private bool ValidatePortfolio(PortfolioItem portfolio)
        {
            if (portfolio == null || portfolio.CombiNo.Equals(emptyInstanceId))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidateTradingInstance(TradeInstance instance)
        {
            if (instance == null || instance.InstanceId == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private void AdjustSecurity(List<SourceHoldingItem> selectedItems)
        {
            foreach (var selectItem in selectedItems)
            {
                var transferedAmount = selectItem.TransferedAmount;
                var srcItem = _srcDataSource.ToList()
                    .Find(p => p.SecuCode.Equals(selectItem.SecuCode) && p.SecuType == selectItem.SecuType);
                if (srcItem != null)
                {
                    srcItem.AvailableTransferedAmount = srcItem.AvailableTransferedAmount - transferedAmount;
                    srcItem.CurrentAmount = srcItem.CurrentAmount - transferedAmount;
                    if (srcItem.CurrentAmount == 0)
                    {
                        _srcDataSource.Remove(srcItem);
                    }
                    else
                    {
                        srcItem.Seletion = false;
                        srcItem.TransferedAmount = 0;
                        srcItem.PriceType = string.Empty;
                    }
                }

                var destItem = _destDataSource.ToList()
                    .Find(p => p.SecuCode.Equals(selectItem.SecuCode) && p.SecuType == selectItem.SecuType);
                if (destItem != null)
                {
                    destItem.CurrentAmount = destItem.CurrentAmount + transferedAmount;
                }
                else
                {
                    destItem = new DestinationHoldingItem
                    {
                        SecuCode = selectItem.SecuCode,
                        SecuType = selectItem.SecuType,
                        PositionType = selectItem.PositionType,
                        PortfolioCode = selectItem.PortfolioCode,
                        PortfolioName = selectItem.PortfolioName,
                        SecuName = selectItem.SecuName,
                        ExchangeCode = selectItem.ExchangeCode,
                        CurrentAmount = transferedAmount,
                    };

                    _destDataSource.Add(destItem);
                }
            }
        }

        #endregion
    }
}
