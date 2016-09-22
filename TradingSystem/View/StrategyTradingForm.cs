using Config;
using Controls.Entity;
using Controls.GridView;
using Forms;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Model.SecurityInfo;
using Quote;
using Model.config;
using BLL.SecurityInfo;
using TradingSystem.Dialog;
using TradingSystem.TradeUtil;
using BLL.Entrust;
using Model.EnumType;
using Model.Binding.BindingUtil;
using BLL.UFX.impl;
using BLL.TradeCommand;
using BLL.UFX;
using Model.BLL;
using BLL.Frontend;
using BLL.EntrustCommand;
using BLL.Product;
using Calculation;

namespace TradingSystem.View
{
    public partial class StrategyTradingForm : Forms.BaseForm
    {
        private const string GridCmdTradingId = "cmdtrading";
        private const string GridEntrustFlowId = "entrustflow";
        private const string GridDealFlowId = "dealflow";
        private const string GridCmdSecurityId = "cmdsecurity";
        private const string GridBuySellId = "buysell";
        private const string EntrustPrice = "entrustprice";

        private EntrustBLL _entrustBLL = new EntrustBLL();
        private WithdrawBLL _withdrawBLL = new WithdrawBLL();
        private QueryBLL _queryBLL = new QueryBLL();

        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private TradeCommandSecurityBLL _tradeCommandSecuBLL = new TradeCommandSecurityBLL();
        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();
        private ProductBLL _productBLL = new ProductBLL();

        private SortableBindingList<TradingCommandItem> _cmdDataSource = new SortableBindingList<TradingCommandItem>(new List<TradingCommandItem>());
        private SortableBindingList<EntrustFlowItem> _efDataSource = new SortableBindingList<EntrustFlowItem>(new List<EntrustFlowItem>());
        private SortableBindingList<DealFlowItem> _dfDataSource = new SortableBindingList<DealFlowItem>(new List<DealFlowItem>());
        private SortableBindingList<EntrustItem> _eiDataSource = new SortableBindingList<EntrustItem>(new List<EntrustItem>());
        private SortableBindingList<CommandSecurityItem> _secuDataSource = new SortableBindingList<CommandSecurityItem>(new List<CommandSecurityItem>());

        GridConfig _gridConfig;
        public StrategyTradingForm()
            :base()
        {
            InitializeComponent();
        }

        public StrategyTradingForm(GridConfig gridConfig)
            :this()
        {
            _gridConfig = gridConfig;

            LoadControl += new FormLoadHandler(Form_LoadControl);
            LoadData += new FormLoadHandler(Form_LoadData);

            tabParentMain.SelectedIndexChanged += new EventHandler(TabControl_Parent_SelectedIndexChanged);
            tabChildSecurity.SelectedIndexChanged += new EventHandler(TabControl_Child_SelectedIndexChanged);

            //grid view event handler
            this.cmdGridView.UpdateRelatedDataGridHandler += new UpdateRelatedDataGrid(GridView_Command_UpdateRelatedDataGridHandler);
            this.bsGridView.UpdateRelatedDataGridHandler += new UpdateRelatedDataGrid(GridView_BuySell_UpdateRelatedDataGridHandler);
            this.securityGridView.CellEndEditHandler += new CellEndEditHandler(GridView_Security_CellEndEditHandler);

            //Refresh
            this.tsbRefresh.Click += new EventHandler(ToolStripButton_Command_Refresh);
            this.tsbefRefresh.Click += new EventHandler(ToolStripButton_EntrustFlow_Refresh);
            this.tsbdfRefresh.Click += new EventHandler(ToolStripButton_DealFlow_Refresh);

            //cancel
            this.tsbCancel.Click += new EventHandler(ToolStripButton_Command_Cancel);

            this.tsbCancelRedo.Click += new EventHandler(ToolStripButton_Command_CancelRedo);

            this.tsbCancelAdd.Click += new EventHandler(ToolStripButton_Command_CancelAdd);

            //Calculate
            this.btnCalc.Click += new EventHandler(Button_Calculate_Click);

            this.btnEntrust.Click += new EventHandler(Button_Entrust_Click);

            //select/unselect
            this.btnCmdSelect.Click += new EventHandler(Button_Command_Select_Click);
            this.btnCmdUnSelect.Click += new EventHandler(Button_Command_UnSelect_Click);

            this.btnSecuSelect.Click += new EventHandler(Button_Security_Select_Click);
            this.btnSecuUnSelect.Click += new EventHandler(Button_Security_UnSelect_Click);

            this.cbSpotBuyPrice.SelectedIndexChanged += new EventHandler(ComboBox_PriceType_SelectedIndexChange);
            this.cbSpotSellPrice.SelectedIndexChanged += new EventHandler(ComboBox_PriceType_SelectedIndexChange);
            this.cbFuturesBuyPrice.SelectedIndexChanged += new EventHandler(ComboBox_PriceType_SelectedIndexChange);
            this.cbFuturesSellPrice.SelectedIndexChanged += new EventHandler(ComboBox_PriceType_SelectedIndexChange);
        }

        #region Command panel cancel/cancelappend/canceladd click event

        private void ToolStripButton_Command_Cancel(object sender, EventArgs e)
        {
            var selectCmdItems = _cmdDataSource.Where(p => p.Selection).ToList();
            if (selectCmdItems == null || selectCmdItems.Count() == 0)
            {
                return;
            }

            List<EntrustCommandItem> entrustedCmdItems = new List<EntrustCommandItem>();
            foreach (var cmdItem in selectCmdItems)
            {
                var oneEntrustedCmdItems = _withdrawBLL.GetEntrustedCmdItems(cmdItem);
                entrustedCmdItems.AddRange(oneEntrustedCmdItems);
            }

            if (entrustedCmdItems.Count == 0)
            {
                MessageBox.Show(this, "当期没有委托可以撤单或撤补！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var calcItems = new List<CancelSecurityItem>();
            foreach (var cmdItem in entrustedCmdItems)
            {
                var cancelSecuItems = _withdrawBLL.GetEntrustedSecuItems(cmdItem);
                if (cancelSecuItems == null)
                    continue;

                foreach (var cancelRedoItem in cancelSecuItems)
                {
                    calcItems.Add(cancelRedoItem);
                }
            }

            var form = new CancelEntrustDialog(_gridConfig);
            form.Owner = this;
            form.OnLoadControl(form, null);
            form.OnLoadData(form, calcItems);
            //form.SaveData += new FormLoadHandler(Dialog_CancelRedoDialog_SaveData);
            form.ShowDialog();

            //var selectCmdItems = _cmdDataSource.Where(p => p.Selection);
            //if (selectCmdItems != null && selectCmdItems.Count() > 0)
            //{
            //    foreach (var cmdItem in selectCmdItems)
            //    {
            //        GridView_Command_Cancel(cmdItem);
            //    }
            //}
        }

        private void GridView_Command_Cancel(TradingCommandItem cmdItem)
        {
            var cancelEntrustCmdItems = _withdrawBLL.CancelOne(cmdItem, new CallerCallback(CancelOneCallback));

            this.cmdGridView.Invalidate();
        }

        private void ToolStripButton_Command_CancelRedo(object sender, EventArgs e)
        {
            var selectCmdItems = _cmdDataSource.Where(p => p.Selection).ToList();
            if (selectCmdItems == null || selectCmdItems.Count() == 0)
            {
                return;
            }

            //List<TradingCommandItem> successCancelItems = new List<TradingCommandItem>();
            //List<EntrustCommandItem> successCancelEntrustCmdItems = new List<EntrustCommandItem>();
            //foreach(var cmdItem in selectCmdItems)
            //{
            //    var cancelEntrustCmdItems = _withdrawBLL.CancelOne(cmdItem, new CallerCallback(CancelOneCallback));
            //    if (cancelEntrustCmdItems.Count > 0)
            //    {
            //        successCancelItems.Add(cmdItem);
            //        successCancelEntrustCmdItems.AddRange(cancelEntrustCmdItems);
            //    }
            //}

            List<EntrustCommandItem> entrustedCmdItems = new List<EntrustCommandItem>();
            foreach (var cmdItem in selectCmdItems)
            {
                var oneEntrustedCmdItems = _withdrawBLL.GetEntrustedCmdItems(cmdItem);
                entrustedCmdItems.AddRange(oneEntrustedCmdItems);
            }

            if (entrustedCmdItems.Count == 0)
            {
                return;
            }

            var form = new CancelRedoDialog(_gridConfig);
            form.Owner = this;
            form.OnLoadControl(form, null);
            form.OnLoadData(form, entrustedCmdItems);
            form.SaveData += new FormLoadHandler(Dialog_CancelRedoDialog_SaveData);
            form.ShowDialog();
        }

        private bool Dialog_CancelRedoDialog_SaveData(object sender, object data)
        {
            return true;
        }

        private void ToolStripButton_Command_CancelAdd(object sender, EventArgs e)
        {
            var selectCmdItems = _cmdDataSource.Where(p => p.Selection);
            //撤销本次计算结果，仅影响到指令证券中的价格类型、本次委托数量、目标数量、指令份数
            if (selectCmdItems != null && selectCmdItems.Count() > 0)
            {
                foreach (var cmdItem in selectCmdItems)
                {
                    GridView_Command_CancelAdd(cmdItem);
                }
            }

            //Update the GridView
            this.securityGridView.Invalidate();
        }

        private void GridView_Command_CancelAdd(TradingCommandItem cmdItem)
        {
            //Reset some columns
            var secuItems = _secuDataSource.Where(p => p.CommandId == cmdItem.CommandId);
            if (secuItems != null && secuItems.Count() > 0)
            {
                int weightAmount = 0;
                foreach (var secuItem in secuItems)
                {
                    weightAmount = secuItem.CommandAmount / cmdItem.CommandNum;
                    secuItem.Selection = true;
                    secuItem.CommandCopies = cmdItem.CommandNum;
                    secuItem.TargetCopies = cmdItem.TargetNum;
                    secuItem.TargetAmount = secuItem.TargetCopies * weightAmount;
                    secuItem.WaitAmount = secuItem.TargetCopies * weightAmount;
                    secuItem.EPriceType = PriceType.None;
                    secuItem.EntrustPrice = 0.0f;
                    secuItem.ThisEntrustAmount = 0;
                }
            }
        }

        private int CancelOneCallback(CallerToken token, object data, UFXErrorResponse errorResponse)
        {
            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                return 1;
            }
            else
            {
                string msg = string.Format("撤销指令号 [{0}] 提价序号 [{1}] 失败！", token.CommandId, token.SubmitId);
                MessageBox.Show(this, msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
        }

        #endregion

        #region price type change

        private void ComboBox_PriceType_SelectedIndexChange(object sender, EventArgs e)
        {
            if (sender == null || !(sender is ComboBox))
                return;
            ComboBox cb = sender as ComboBox;

            PriceType priceType = PriceTypeHelper.GetPriceType(cb);
            EntrustDirection direction = EntrustDirection.BuySpot;
            SecurityType secuType = SecurityType.All;
            switch (cb.Name)
            {
                case "cbSpotBuyPrice":
                    { 
                        direction = EntrustDirection.BuySpot;
                        secuType = SecurityType.Stock;
                    }
                    break;
                case "cbSpotSellPrice":
                    {
                        direction = EntrustDirection.SellSpot;
                        secuType = SecurityType.Stock;
                    }
                    break;
                case "cbFuturesBuyPrice":
                    {
                        direction = EntrustDirection.BuyClose;
                        secuType = SecurityType.Futures;
                    }
                    break;
                case "cbFuturesSellPrice":
                    {
                        direction = EntrustDirection.SellOpen;
                        secuType = SecurityType.Futures;
                    }
                    break;
                default:
                    break;
            }

            var items = _secuDataSource.Where(p => p.SecuType == secuType && p.EDirection == direction).ToList();
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.EPriceType = priceType;
                }
            }

            this.securityGridView.Invalidate();
        }

        #endregion

        #region GridView UpdateRelatedDataGridHandler

        private void GridView_Command_UpdateRelatedDataGridHandler(UpdateDirection direction, int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= _cmdDataSource.Count)
                return;

            TradingCommandItem cmdItem = _cmdDataSource[rowIndex];

            switch (direction)
            {
                case UpdateDirection.Select:
                    {
                        GridView_Command_Select(cmdItem);
                    }
                    break;
                case UpdateDirection.UnSelect:
                    {
                        GridView_Command_UnSelect(cmdItem);
                    }
                    break;
                case UpdateDirection.Increase:
                    {
                        
                    }
                    break;
                case UpdateDirection.Decrease:
                    {
                        
                    }
                    break;
            }
        }

        private void GridView_BuySell_UpdateRelatedDataGridHandler(UpdateDirection direction, int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= _eiDataSource.Count)
                return;

            EntrustItem eiItem = _eiDataSource[rowIndex];
            switch (direction)
            {
                case UpdateDirection.Select:
                    {
                    }
                    break;
                case UpdateDirection.UnSelect:
                    {
                    }
                    break;
                case UpdateDirection.Increase:
                    { 
                        
                    }
                    break;
                case UpdateDirection.Decrease:
                    break;
                default:
                    break;
            }
        }

        private void GridView_Command_Select(TradingCommandItem cmdItem)
        {
            var spotBuyPrice = PriceTypeHelper.GetPriceType(this.cbSpotBuyPrice);
            var spotSellPrice = PriceTypeHelper.GetPriceType(this.cbSpotSellPrice);
            var futuBuyPrice = PriceTypeHelper.GetPriceType(this.cbFuturesBuyPrice);
            var futuSellPrice = PriceTypeHelper.GetPriceType(this.cbFuturesSellPrice);

            //Add into two gridview: CommandSecurity and entrust
            var secuItems = _secuDataSource.Where(p => p.CommandId == cmdItem.CommandId);
            if (secuItems == null || secuItems.Count() == 0)
            {
                secuItems = _tradeCommandSecuBLL.GetCommandSecurityItems(cmdItem);
               
                secuItems.Where(p => p.EDirection == EntrustDirection.BuySpot)
                    .ToList()
                    .ForEach(o => o.EPriceType = spotBuyPrice);
                secuItems.Where(p => p.EDirection == EntrustDirection.SellSpot)
                    .ToList()
                    .ForEach(o => o.EPriceType = spotSellPrice);
                secuItems.Where(p => p.EDirection == EntrustDirection.SellOpen)
                    .ToList()
                    .ForEach(o => o.EPriceType = futuSellPrice);
                secuItems.Where(p => p.EDirection == EntrustDirection.BuyClose)
                    .ToList()
                    .ForEach(o => o.EPriceType = futuBuyPrice);

                secuItems.ToList()
                    .ForEach(p => _secuDataSource.Add(p));
            }

            //询价
            QueryQuote(spotBuyPrice, spotSellPrice, futuBuyPrice, futuSellPrice);

            //Add into buy/sell grid view
            var entrustItems = _eiDataSource.Where(p => p.CommandNo == cmdItem.CommandId);
            if (entrustItems == null || entrustItems.Count() == 0)
            {
                var entrustItem = new EntrustItem
                {
                    CommandNo = cmdItem.CommandId,
                };

                _eiDataSource.Add(entrustItem);
            }
        }

        private void GridView_Command_UnSelect(TradingCommandItem cmdItem)
        {
            //Remove from Security GridView
            var secuItems = _secuDataSource.Where(p => p.CommandId == cmdItem.CommandId).ToList();
            if (secuItems != null && secuItems.Count() > 0)
            {
                foreach (var secuItem in secuItems)
                {
                    _secuDataSource.Remove(secuItem);
                }
            }

            //Remove from two GridView:
            var entrustItems = _eiDataSource.Where(p => p.CommandNo == cmdItem.CommandId).ToList();
            if (entrustItems != null && entrustItems.Count() > 0)
            {
                foreach (var item in entrustItems)
                {
                    _eiDataSource.Remove(item);
                }
            }
        }

        private void GridView_Security_CellEndEditHandler(int rowIndex, int columnIndex, string columnName)
        {
            if(rowIndex < 0 || rowIndex >= _secuDataSource.Count)
                return;

            //TODO: while the EntrustPrice is changed, record it.
            //Console.WriteLine(columnName);
            if (columnName.CompareTo(EntrustPrice) == 0)
            {
                var secuItem = _secuDataSource[rowIndex];
                if (secuItem.EntrustPrice < secuItem.LimitDownPrice || secuItem.EntrustPrice > secuItem.LimitUpPrice)
                {
                    MessageBox.Show(this, "委托价格必须在跌停价和涨停价之间！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    var findItem = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                    var marketData = QuoteCenter.Instance.GetMarketData(findItem);
                    secuItem.EntrustPrice = QuotePriceHelper.GetPrice(PriceType.Last, marketData);
                }
                else
                {
                    secuItem.EPriceType = PriceType.Assign;
                }

                this.securityGridView.InvalidateCell(columnIndex - 1, rowIndex);
                this.securityGridView.InvalidateCell(columnIndex, rowIndex);
            }
        }

        private void GridView_Security_ResetPriceType(List<CommandSecurityItem> secuItems)
        {
            var spotBuyPrice = PriceTypeHelper.GetPriceType(this.cbSpotBuyPrice);
            var spotSellPrice = PriceTypeHelper.GetPriceType(this.cbSpotSellPrice);
            var futuBuyPrice = PriceTypeHelper.GetPriceType(this.cbFuturesBuyPrice);
            var futuSellPrice = PriceTypeHelper.GetPriceType(this.cbFuturesSellPrice);

            if (secuItems != null)
            {
                secuItems.Where(p => p.EDirection == EntrustDirection.BuySpot)
                    .ToList()
                    .ForEach(o => o.EPriceType = spotBuyPrice);
                secuItems.Where(p => p.EDirection == EntrustDirection.SellSpot)
                    .ToList()
                    .ForEach(o => o.EPriceType = spotSellPrice);
                secuItems.Where(p => p.EDirection == EntrustDirection.SellOpen)
                    .ToList()
                    .ForEach(o => o.EPriceType = futuSellPrice);
                secuItems.Where(p => p.EDirection == EntrustDirection.BuyClose)
                    .ToList()
                    .ForEach(o => o.EPriceType = futuBuyPrice);
            }
        }

        #endregion

        #region form loading

        private bool Form_LoadControl(object sender, object data)
        {
            //Load Command Trading
            TSDataGridViewHelper.AddColumns(this.cmdGridView, _gridConfig.GetGid(GridCmdTradingId));
            Dictionary<string, string> cmdColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(TradingCommandItem));
            TSDataGridViewHelper.SetDataBinding(this.cmdGridView, cmdColDataMap);           

            //Load EntrustFlow gridview
            TSDataGridViewHelper.AddColumns(this.efGridView, _gridConfig.GetGid(GridEntrustFlowId));
            Dictionary<string, string> efColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(EntrustFlowItem));
            TSDataGridViewHelper.SetDataBinding(this.efGridView, efColDataMap);

            //Load DealFlow gridview
            TSDataGridViewHelper.AddColumns(this.dfGridView, _gridConfig.GetGid(GridDealFlowId));
            Dictionary<string, string> dfColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(DealFlowItem));
            TSDataGridViewHelper.SetDataBinding(this.dfGridView, dfColDataMap);

            //Load Security gridview
            TSDataGridViewHelper.AddColumns(this.securityGridView, _gridConfig.GetGid(GridCmdSecurityId));
            Dictionary<string, string> secuColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(CommandSecurityItem));
            TSDataGridViewHelper.SetDataBinding(this.securityGridView, secuColDataMap);

            //Load Entrust gridview
            TSDataGridViewHelper.AddColumns(this.bsGridView, _gridConfig.GetGid(GridBuySellId));
            Dictionary<string, string> bsColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(EntrustItem));
            TSDataGridViewHelper.SetDataBinding(this.bsGridView, bsColDataMap); 

            //Load combobox
            LoadEntrustControl();

            //Binding data
            this.cmdGridView.DataSource = _cmdDataSource;
            this.efGridView.DataSource = _efDataSource;
            this.dfGridView.DataSource = _dfDataSource;
            this.bsGridView.DataSource = _eiDataSource;
            this.securityGridView.DataSource = _secuDataSource;

            return true;
        }

        private void LoadEntrustControl()
        {
            var spotPrices = ConfigManager.Instance.GetComboConfig().GetComboOption("spotprice");
            ComboBoxUtil.SetComboBox(this.cbSpotBuyPrice, spotPrices);

            var spotSellPrices = new ComboOption 
            {
                Name = spotPrices.Name,
                Selected = spotPrices.Selected,
                Items = spotPrices.Items.OrderBy(p => p.Order2).ToList()
            };
            ComboBoxUtil.SetComboBox(this.cbSpotSellPrice, spotSellPrices);

            var futurePrice = ConfigManager.Instance.GetComboConfig().GetComboOption("futureprice");
            ComboBoxUtil.SetComboBox(this.cbFuturesBuyPrice, futurePrice);

            var futureSellPrices = new ComboOption
            {
                Name = futurePrice.Name,
                Selected = futurePrice.Selected,
                Items = futurePrice.Items.OrderBy(p => p.Order2).ToList()
            };

            ComboBoxUtil.SetComboBox(this.cbFuturesSellPrice, futureSellPrices);
        }

        #endregion

        #region tabcontrol index changed

        private void TabControl_Parent_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            if (tc == null)
                return;

            string selectTabName = tc.SelectedTab.Name;
            if (selectTabName == this.tpEntrustFlow.Name)
            {
                this.tpCmdEntrustFlow.Controls.Clear();
                this.efMainPanel.Controls.Clear();
                this.efMainPanel.Controls.Add(this.efGridView);
            }
            else if (selectTabName == this.tpDealFlow.Name)
            {
                this.tpCmdDealFlow.Controls.Clear();
                this.dfMainPanel.Controls.Clear();
                this.dfMainPanel.Controls.Add(this.dfGridView);
            }
            else if (selectTabName == tpCmdTrading.Name)
            {
                SwitchChildTabPage(this.tabChildSecurity.SelectedTab.Name);
            }
        }

        private void SwitchChildTabPage(string selectTabName)
        {
            if (selectTabName == this.tpCmdSecurity.Name)
            {
                //do nothing
            }
            else if (selectTabName == this.tpCmdEntrustFlow.Name)
            {
                this.efMainPanel.Controls.Clear();
                this.tpCmdEntrustFlow.Controls.Clear();
                this.tpCmdEntrustFlow.Controls.Add(this.efGridView);
            }
            else if (selectTabName == this.tpCmdDealFlow.Name)
            {
                this.dfMainPanel.Controls.Clear();
                this.tpCmdDealFlow.Controls.Clear();
                this.tpCmdDealFlow.Controls.Add(this.dfGridView);
            }
        }

        private void TabControl_Child_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            if (tc == null)
                return;

            string selectTabName = tc.SelectedTab.Name;
            SwitchChildTabPage(selectTabName);
        }

        #endregion

        #region Load data
        private bool Form_LoadData(object sender, object data)
        {
            //Clear();
            
            //Load data here
            LoadDataTradeCommand();
            //LoadDataEntrustFlow();
            //LoadDataDealFlow();

            return true;
        }

        private void Clear()
        {
            _cmdDataSource.Clear();
            _secuDataSource.Clear();
            _efDataSource.Clear();
            _eiDataSource.Clear();
            _dfDataSource.Clear();
        }

        private bool LoadDataTradeCommand()
        {
            _cmdDataSource.Clear();
            _secuDataSource.Clear();
            _eiDataSource.Clear();

            var tradingcmds = _tradeCommandBLL.GetTradeCommandAll();
            if (tradingcmds != null)
            {
                tradingcmds.ForEach( p => _cmdDataSource.Add(p));
            }

            return true;
        }

        private bool LoadDataEntrustFlow()
        {
            _efDataSource.Clear();

            UFXQueryEntrustBLL queryBll = new UFXQueryEntrustBLL();
            var test = queryBll.QueryToday(new CallerCallback(LoadDataEntrustFlow2));

            return true;
        }

        private bool LoadDataDealFlow()
        {
            _dfDataSource.Clear();

            UFXQueryDealBLL queryDealBll = new UFXQueryDealBLL();
            var test = queryDealBll.QueryToday(new CallerCallback(LoadDataDealFlow2));

            return true;
        }

        private int LoadDataDealFlow2(CallerToken token, object data, UFXErrorResponse errorResponse)
        {
            if (data == null || !(data is List<DealFlowItem>))
                return -1;
            var dfItems = data as List<DealFlowItem>;

            this.BeginInvoke(new Action(() =>
            {
                dfItems.ForEach(p => {
                    var secuInfo = SecurityInfoManager.Instance.Get(p.SecuCode);
                    if (secuInfo != null)
                    {
                        p.SecuName = secuInfo.SecuName;
                    }

                    var findFund = LoginManager.Instance.Accounts.Find(o => o.AccountCode.Equals(p.FundNo));
                    if (findFund != null)
                    {
                        p.FundName = findFund.AccountName;
                    }

                    var findPort = LoginManager.Instance.Portfolios.Find(o => o.CombiNo.Equals(p.PortfolioCode));
                    if (findPort != null)
                    {
                        p.PortfolioName = findPort.CombiName;
                    }

                    _dfDataSource.Add(p); 
                });

                this.efGridView.Invalidate();
            }), null);
            return 1;
        }

        private int LoadDataEntrustFlow2(CallerToken token, object data, UFXErrorResponse errorResponse)
        {
            if (data == null || !(data is List<EntrustFlowItem>))
                return -1;
            var efItems = data as List<EntrustFlowItem>;
            
            if(!T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                this.BeginInvoke(new Action(() =>
                {
                    MessageBox.Show(this, errorResponse.ErrorMessage, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));

                return -1;
            }

            this.BeginInvoke(new Action(()=>
            {
                var entrustItems = _entrustSecurityBLL.GetAllCombine();
                efItems.ForEach(p => {
                    var secuInfo = SecurityInfoManager.Instance.Get(p.SecuCode);
                    if (secuInfo != null)
                    {
                        p.SecuName = secuInfo.SecuName;
                        //p.Market = SecurityItemHelper.GetExchange(secuInfo.ExchangeCode);
                    }

                    var entrustItem = entrustItems.Find(o => o.RequestId == p.RequestId);
                    if (entrustItem != null)
                    {
                        p.CommandNo = entrustItem.CommandId;
                        p.FundName = entrustItem.AccountName;
                        p.PortfolioName = entrustItem.PortfolioName;
                        p.InstanceId = entrustItem.InstanceId;
                        p.InstanceNo = entrustItem.InstanceCode;
                        
                    }

                    _efDataSource.Add(p); 
                });

                this.efGridView.Invalidate();
            }), null);
            return 1;
        }

        #endregion

        #region toolstrip refresh click event handler

        private void ToolStripButton_DealFlow_Refresh(object sender, EventArgs e)
        {
            //TODO: load the dealflow security
            LoadDataDealFlow();
        }

        private void ToolStripButton_EntrustFlow_Refresh(object sender, EventArgs e)
        {
            //TODO: load the entrustflow security
            LoadDataEntrustFlow();
        }

        private void ToolStripButton_Command_Refresh(object sender, EventArgs e)
        {
            Form_LoadData(this, null);
        }

        #endregion

        #region TradingCommand select/unselect click

        private void Button_Command_Select_Click(object sender, EventArgs e)
        {
            foreach (var cmdItem in _cmdDataSource)
            {
                cmdItem.Selection = true;

                GridView_Command_Select(cmdItem);
            }

            ////Get the price type
            //PriceType spotBuyPrice = PriceTypeHelper.GetPriceType(this.cbSpotBuyPrice);
            //PriceType spotSellPrice = PriceTypeHelper.GetPriceType(this.cbSpotSellPrice);
            //PriceType futureBuyPrice = PriceTypeHelper.GetPriceType(this.cbFuturesBuyPrice);
            //PriceType futureSellPrice = PriceTypeHelper.GetPriceType(this.cbFuturesSellPrice);

            ////询价
            //QueryQuote(spotBuyPrice, spotSellPrice, futureBuyPrice, futureSellPrice);

            this.cmdGridView.InvalidateColumn(0);
            this.securityGridView.InvalidateColumn(0);
            this.bsGridView.InvalidateColumn(0);
        }

        private void Button_Command_UnSelect_Click(object sender, EventArgs e)
        {
            foreach (var cmdItem in _cmdDataSource)
            {
                cmdItem.Selection = false;

                GridView_Command_UnSelect(cmdItem);
            }

            this.cmdGridView.InvalidateColumn(0);
            this.securityGridView.InvalidateColumn(0);
            this.bsGridView.InvalidateColumn(0);
        }

        #endregion

        #region TradingCommand Security select/unselect

        private void Button_Security_Select_Click(object sender, EventArgs e)
        {
            foreach (var secuItem in _secuDataSource)
            {
                secuItem.Selection = true;
            }

            this.securityGridView.Invalidate();
        }

        private void Button_Security_UnSelect_Click(object sender, EventArgs e)
        {
            foreach (var secuItem in _secuDataSource)
            {
                secuItem.Selection = false;
            }

            this.securityGridView.Invalidate();
        }

        #endregion

        #region buy/sell button click event

        private void Button_Calculate_Click(object sender, EventArgs e)
        {
            //如果份数框不为0，将所有的份数均设为输入框中的值
            AssignCopies();
            
            if (!ValidateCopies())
            {
                MessageBox.Show(this, "委托份数非法，请输入正确的委托份数！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            if (_secuDataSource.Count == 0)
            {
                MessageBox.Show(this, "没有需要委托的证券！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            //Get the price type
            PriceType spotBuyPrice = PriceTypeHelper.GetPriceType(this.cbSpotBuyPrice);
            PriceType spotSellPrice = PriceTypeHelper.GetPriceType(this.cbSpotSellPrice);
            PriceType futureBuyPrice = PriceTypeHelper.GetPriceType(this.cbFuturesBuyPrice);
            PriceType futureSellPrice = PriceTypeHelper.GetPriceType(this.cbFuturesSellPrice);

            //update each command item
            foreach (var eiItem in _eiDataSource)
            {
                CalculateOne(eiItem, spotBuyPrice, spotSellPrice, futureBuyPrice, futureSellPrice);
            }

            //询价
            QueryQuote(spotBuyPrice, spotSellPrice, futureBuyPrice, futureSellPrice);

            //refresh UI
            this.cmdGridView.Invalidate();
            this.bsGridView.Invalidate();
            this.securityGridView.Invalidate();
        }

        private void Button_Entrust_Click(object sender, EventArgs e)
        {
            //TODO:
            //Check the entrust security items of entrust item. Make sure there is security item selected
            var selectedEntrustItems = _eiDataSource.ToList();
            if (selectedEntrustItems == null || selectedEntrustItems.Count == 0)
            {
                //TODO: show message
                MessageBox.Show(this, "请选择要委托的指令！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            foreach (var eiItem in selectedEntrustItems)
            {
                int count = _secuDataSource.Count(p => p.Selection && p.CommandId == eiItem.CommandNo);
                if (count == 0)
                {
                    MessageBox.Show(this, "请确保委托指令中包含有效证券！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
            }

            if (!ValidateEntrust())
            {
                MessageBox.Show(this, "请设置委托的证券数量和价格，证券数量需为正值，价格不能为零且需在最高最低价之间，证券可以正常交易！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            var selectedSecuItems = _secuDataSource.Where(p => p.Selection).ToList();
            string confirmMsg = string.Format("共有[{0}]只证券\n确定开始委托吗？", selectedSecuItems.Count);
            if (MessageBox.Show(this, confirmMsg, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            //submit each entrust item and each security in the entrustitem
            List<int> submitIds = new List<int>();
            int successCount = 0;
            foreach (var eiItem in selectedEntrustItems)
            {
                var cmdItem = _cmdDataSource.Single(p => p.CommandId == eiItem.CommandNo);
                if (cmdItem == null)
                {
                    //TODO: there is no CommandItem
                    continue;
                }

                //cmdItem.TargetNum += eiItem.Copies;
                int targetNum = cmdItem.TargetNum + eiItem.Copies;
                if (targetNum > cmdItem.CommandNum)
                { 
                    //TODO: there are no so many securities
                    continue;
                }

                EntrustCommandItem eciItem = new EntrustCommandItem
                {
                    CommandId = eiItem.CommandNo,
                    Copies = eiItem.Copies,
                };

                var entrustSecuItems = GetEntrustSecurityItems(-1, eiItem.CommandNo);
                var bllResponse = _entrustBLL.SubmitOne(eciItem, entrustSecuItems);

                if (BLLResponse.Success(bllResponse))
                {
                    //success to submit into database
                    submitIds.Add(eciItem.SubmitId);

                    //TODO: update the targetnum in UI
                    successCount += entrustSecuItems.Count;
                }
                else
                { 
                    //Fail to submit into database
                    string msg = string.Format("委托指令[{0}]失败: {1}", eiItem.CommandNo, bllResponse.Message);
                    MessageBox.Show(this, msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }

            string successMsg = string.Format("共有[{0}]只证券委托全部成功执行！", successCount);
            MessageBox.Show(this, successMsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            //update the UI
            GridView_Security_ResetPriceType(selectedSecuItems);
            nudCopies.Value = 1;
            this.cmdGridView.Invalidate();
            this.bsGridView.Invalidate();
            this.securityGridView.Invalidate();
        }

        private void CalculateOne(EntrustItem eiItem, PriceType spotBuyPrice, PriceType spotSellPrice, PriceType futureBuyPrice, PriceType futureSellPrice)
        {
            var selCmdItem = _cmdDataSource.Single(p => p.CommandId == eiItem.CommandNo);
            if (selCmdItem == null)
            {
                return;
            }

            //检查委托数量
            var noEntrustNum = selCmdItem.CommandAmount - selCmdItem.EntrustedAmount;
            if (noEntrustNum <= 0)
            {
                return;
            }

            //selCmdItems[0].TargetNum = eiItem.Copies;
            int thisCopies = eiItem.Copies;
            int targetNum = selCmdItem.TargetNum + eiItem.Copies;
            if (targetNum > selCmdItem.CommandNum)
            {
                targetNum = selCmdItem.CommandNum;
                thisCopies = targetNum - selCmdItem.TargetNum;
            }

            if (thisCopies <= 0)
            {
                thisCopies = 0;
            }

            var secuItems = _secuDataSource.Where(p => p.CommandId == eiItem.CommandNo).ToList();
            int weightAmount = 0;
            foreach (var secuItem in secuItems)
            {
                secuItem.TargetCopies = targetNum;

                int targetAmount = 0;
                int thisEntrustAmount = 0;
                int waitAmount = 0;

                if (thisCopies > 0)
                {
                    //算出每一份的数量
                    weightAmount = secuItem.CommandAmount / selCmdItem.CommandNum;
                    if (weightAmount > 0)
                    {
                        targetAmount = targetNum * weightAmount;
                        thisEntrustAmount = thisCopies * weightAmount;
                        waitAmount = targetNum * weightAmount;
                    }
                    else
                    {
                        targetAmount = secuItem.TargetCopies;
                        thisEntrustAmount = thisCopies;
                        waitAmount = secuItem.TargetCopies;
                    }
                }
                else
                {
                    targetAmount = secuItem.CommandAmount - secuItem.EntrustedAmount;
                    thisEntrustAmount = secuItem.CommandAmount - secuItem.EntrustedAmount;
                    waitAmount = thisEntrustAmount;
                }

                if (secuItem.EDirection == EntrustDirection.BuySpot && secuItem.SecuType == SecurityType.Stock)
                {
                    if (thisEntrustAmount % 100 != 0)
                    {
                        thisEntrustAmount = (int)Math.Ceiling(thisEntrustAmount / 100.0) * 100;
                        targetAmount = secuItem.TargetAmount + thisEntrustAmount;
                        waitAmount = secuItem.WaitAmount + thisEntrustAmount;
                    }
                }

                if (thisEntrustAmount + secuItem.EntrustedAmount <= secuItem.CommandAmount)
                {
                    secuItem.ThisEntrustAmount = thisEntrustAmount;
                }
                else
                {
                    secuItem.ThisEntrustAmount = secuItem.CommandAmount - secuItem.EntrustedAmount;
                }

                if (waitAmount <= secuItem.CommandAmount)
                {
                    secuItem.WaitAmount = waitAmount;
                }
                else
                {
                    secuItem.WaitAmount = secuItem.CommandAmount;
                }

                if (targetAmount <= secuItem.CommandAmount)
                {
                    secuItem.TargetAmount = targetAmount;
                }
                else
                {
                    secuItem.TargetAmount = secuItem.CommandAmount;
                }

                if (secuItem.EPriceType != PriceType.Assign)
                {
                    switch (secuItem.EDirection)
                    {
                        case EntrustDirection.BuySpot:
                            {
                                if (secuItem.SecuType == SecurityType.Stock)
                                {
                                    secuItem.EPriceType = spotBuyPrice;
                                }
                            }
                            break;
                        case EntrustDirection.SellSpot:
                            {
                                if (secuItem.SecuType == SecurityType.Stock)
                                {
                                    secuItem.EPriceType = spotSellPrice;
                                }
                            }
                            break;
                        case EntrustDirection.SellOpen:
                            {
                                if (secuItem.SecuType == SecurityType.Futures)
                                {
                                    secuItem.EPriceType = futureSellPrice;
                                }
                            }
                            break;
                        case EntrustDirection.BuyClose:
                            {
                                if (secuItem.SecuType == SecurityType.Futures)
                                {
                                    secuItem.EPriceType = futureBuyPrice;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void QueryQuote(PriceType spotBuyPrice, PriceType spotSellPrice, PriceType futureBuyPrice, PriceType futureSellPrice)
        {
            //query the price and set it
            List<SecurityItem> secuList = new List<SecurityItem>();
            var uniqueSecuItems = _secuDataSource.GroupBy(p => p.SecuCode).Select(p => p.First());
            foreach (var secuItem in uniqueSecuItems)
            {
                var findItem = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                if (findItem != null)
                {
                    secuList.Add(findItem);
                }
            }

            //更新行情相关数据
            List<PriceType> priceTypes = new List<PriceType>() { spotBuyPrice, spotSellPrice, futureBuyPrice, futureSellPrice };
            QuoteCenter.Instance.Query(secuList, priceTypes);
            foreach (var secuItem in _secuDataSource)
            {
                var targetItem = secuList.Find(p => p.SecuCode.Equals(secuItem.SecuCode) && (p.SecuType == SecurityType.Stock || p.SecuType == SecurityType.Futures));
                var marketData = QuoteCenter.Instance.GetMarketData(targetItem);

                secuItem.LimitUpPrice = marketData.HighLimitPrice;
                secuItem.LimitDownPrice = marketData.LowLimitPrice;
                secuItem.ESuspendFlag = marketData.SuspendFlag;

                if (secuItem.EPriceType != PriceType.Assign)
                {
                    switch (secuItem.SecuType)
                    {
                        case SecurityType.Stock:
                            {

                                if (secuItem.EDirection == EntrustDirection.BuySpot)
                                {
                                    secuItem.EntrustPrice = QuotePriceHelper.GetPrice(spotBuyPrice, marketData);
                                }
                                else if (secuItem.EDirection == EntrustDirection.SellSpot)
                                {
                                    secuItem.EntrustPrice = QuotePriceHelper.GetPrice(spotSellPrice, marketData);
                                }
                            }
                            break;
                        case SecurityType.Futures:
                            {
                                if (secuItem.EDirection == EntrustDirection.SellOpen)
                                {
                                    secuItem.EntrustPrice = QuotePriceHelper.GetPrice(futureSellPrice, marketData);
                                }
                                else if (secuItem.EDirection == EntrustDirection.BuyClose)
                                {
                                    secuItem.EntrustPrice = QuotePriceHelper.GetPrice(futureBuyPrice, marketData);
                                }
                            }
                            break;
                    }
                }
            }
        }

        private List<EntrustSecurityItem> GetEntrustSecurityItems(int submitId, int commandId)
        {
            List<EntrustSecurityItem> entrustSecuItems = new List<EntrustSecurityItem>();

            var secuItems = _secuDataSource.Where(p => p.Selection && p.CommandId == commandId).ToList();
            if (secuItems == null || secuItems.Count == 0)
            {
                return entrustSecuItems;
            }

            foreach (var secuItem in secuItems)
            {
                var priceType = PriceTypeHelper.GetPriceType(secuItem.PriceType);
                EntrustSecurityItem entrustSecurityItem = new EntrustSecurityItem
                {
                    SubmitId = submitId,
                    CommandId = commandId,
                    SecuCode = secuItem.SecuCode,
                    SecuType = secuItem.SecuType,
                    EntrustAmount = secuItem.ThisEntrustAmount,
                    EntrustPrice = secuItem.EntrustPrice,
                    EntrustDirection = secuItem.EDirection,
                    EntrustStatus = EntrustStatus.SubmitToDB,
                    PriceType = priceType,
                    EntrustDate = DateTime.Now,
                };

                var secuInfo = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                string exchangeCode = string.Empty;
                if (secuInfo != null)
                {
                    exchangeCode = secuInfo.ExchangeCode;
                }
                else
                {
                    exchangeCode = SecurityInfoHelper.GetExchangeCode(secuItem.SecuCode);
                }

                entrustSecurityItem.EntrustPriceType = EntrustPriceType.FixedPrice;
                //if (exchangeCode.Equals(Exchange.SHSE))
                //{
                //    entrustSecurityItem.EntrustPriceType = EntrustPriceType.FifthIsLeftOffSH;
                //}
                //else if (exchangeCode.Equals(Exchange.SZSE))
                //{
                //    entrustSecurityItem.EntrustPriceType = EntrustPriceType.FifthIsLeftOffSZ;
                //}
                //else if (exchangeCode.Equals(Exchange.CFFEX))
                //{
                //    entrustSecurityItem.EntrustPriceType = EntrustPriceType.FifthIsLeftOffCFX;
                //}
                //else
                //{ 
                //    //Do nothing
                //}
                

                entrustSecuItems.Add(entrustSecurityItem);
            }

            return entrustSecuItems;
        }

        private void AssignCopies()
        {
            int copies = (int)nudCopies.Value;
            if (copies > 0)
            {
                _eiDataSource.ToList()
                    .ForEach(o => o.Copies = copies);
            }
        }

        private bool ValidateCopies()
        {
            foreach (var eiItem in _eiDataSource)
            {
                var cmdItem = _cmdDataSource.Single(p => p.Selection && p.CommandId == eiItem.CommandNo);
                if (cmdItem == null)
                {
                    return false;
                }
            }

            var selItems = _eiDataSource.Where(p => p.Copies == 0).ToList();
            if (selItems != null && selItems.Count > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateEntrust()
        {
            var entrustSecuItems = _secuDataSource.Where(p => p.Selection).ToList();
            if (entrustSecuItems == null || entrustSecuItems.Count == 0)
            {
                //TODO: show message
                MessageBox.Show(this, "请选择要委托的证券！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            foreach (var entrustSecuItem in entrustSecuItems)
            {
                if (entrustSecuItem.ThisEntrustAmount <= 0
                    || FloatUtil.IsZero(entrustSecuItem.EntrustPrice)
                    || entrustSecuItem.EntrustPrice < entrustSecuItem.LimitDownPrice
                    || entrustSecuItem.EntrustPrice > entrustSecuItem.LimitUpPrice
                    || entrustSecuItem.ESuspendFlag != Model.Quote.SuspendFlag.NoSuspension
                   )
                {
                    var findIndex = _secuDataSource.ToList().FindIndex(p => p.SecuCode == entrustSecuItem.SecuCode
                        && p.CommandId == entrustSecuItem.CommandId);
                    if (findIndex >= 0)
                    {
                        securityGridView.SetFocus(findIndex, EntrustPrice);
                    }

                    return false;
                }
            }

            return true;
        }


        #endregion
    }
}
