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
using Model.Converter;
using Model.Database;
using BLL.Manager;

namespace TradingSystem.View
{
    public partial class StrategyTradingForm : Forms.BaseForm
    {
        //label id
        private const string msgContainCannotCancelSecurity = "tradecontaincannotcancelsecurity";
        private const string msgNoEntrustCancel = "tradenoentrustcancel";
        private const string msgCancelFail = "tradecancelfail";
        private const string msgEntrustPriceBeyondLimit = "tradeentrustpricebeyondlimit";
        private const string msgEntrustAmountBeyondTotal = "tradeentrustamountbeyondtotal";
        private const string msgNoEntrustSecurity = "tradenotentrustsecurity";
        private const string msgEntrustCommandSelect = "tradeentrustcommandselect";
        private const string msgShouldContainSecurity = "tradeshouldcontainsecurity";
        private const string msgEntrustSecuritySelect = "tradeentrustsecurityselect";
        private const string msgEntrustPricePrompt = "tradeentrustpriceprompt";
        private const string msgEntrustSecuritySuspend = "tradeentrustsecuritysuspend";
        private const string msgEntrustSecurityConfirm = "tradeentrustsecurityconfirm";
        private const string msgEntrustCommandFail = "tradeentrustcommandfail";
        private const string msgEntrustSecuritySuccessCount = "tradeentrustsecuritysuccesscount";
        private const string msgEntrustInvalidDate = "tradeentrustinvaliddate";
        private const string msgEntrustNoCommandSelect = "tradeentrustnocommandselect";

        private const string GridCmdTradingId = "cmdtrading";
        private const string GridEntrustFlowId = "entrustflow";
        private const string GridDealFlowId = "dealflow";
        private const string GridCmdSecurityId = "cmdsecurity";
        private const string GridBuySellId = "buysell";
        private const string EntrustPrice = "entrustprice";
        private const string ThisEntrustAmount = "thisentrustamount";
        private const string SuspensionFlag = "suspensionflag";

        private EntrustBLL _entrustBLL = new EntrustBLL();
        private WithdrawBLL _withdrawBLL = new WithdrawBLL();

        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private TradeCommandSecurityBLL _tradeCommandSecuBLL = new TradeCommandSecurityBLL();
        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();

        private SortableBindingList<TradeCommandItem> _cmdDataSource = new SortableBindingList<TradeCommandItem>(new List<TradeCommandItem>());
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

            //buy/sell copies
            this.cbCopies.CheckedChanged += new EventHandler(CheckBox_Copies_CheckedChanged);

            //Calculate
            this.btnCalc.Click += new EventHandler(Button_Calculate_Click);

            this.btnEntrust.Click += new EventHandler(Button_Entrust_Click);

            //select/unselect
            this.btnCmdSelect.Click += new EventHandler(Button_Command_Select_Click);
            this.btnCmdUnSelect.Click += new EventHandler(Button_Command_UnSelect_Click);

            this.btnSecuSelect.Click += new EventHandler(Button_Security_Select_Click);
            this.btnSecuUnSelect.Click += new EventHandler(Button_Security_UnSelect_Click);

            //entrust price setting
            this.cbSpotBuyPrice.SelectedIndexChanged += new EventHandler(ComboBox_PriceType_SelectedIndexChange);
            this.cbSpotSellPrice.SelectedIndexChanged += new EventHandler(ComboBox_PriceType_SelectedIndexChange);
            this.cbFuturesBuyPrice.SelectedIndexChanged += new EventHandler(ComboBox_PriceType_SelectedIndexChange);
            this.cbFuturesSellPrice.SelectedIndexChanged += new EventHandler(ComboBox_PriceType_SelectedIndexChange);

            //entrust flow view
            this.btnefSelect.Click += new EventHandler(ToolStripButton_EntrustFlow_Select);
            this.btnefUnSelect.Click += new EventHandler(ToolStripButton_EntrustFlow_UnSelect);
            this.btnefUndo.Click += new EventHandler(ToolStripButton_EntrustFlow_Undo);
            this.btnefCancelRedo.Click += new EventHandler(ToolStripButton_EntrustFlow_CancelRedo);

            //deal flow view
        }

        #region EntrustFlow view click event handler

        private void ToolStripButton_EntrustFlow_Select(object sender, EventArgs e)
        {
            foreach (var efItem in _efDataSource)
            {
                efItem.Selection = true;
            }

            this.efGridView.Invalidate();
        }

        private void ToolStripButton_EntrustFlow_UnSelect(object sender, EventArgs e)
        {
            foreach (var efItem in _efDataSource)
            {
                efItem.Selection = false;
            }

            this.efGridView.Invalidate();
        }

        private void ToolStripButton_EntrustFlow_CancelRedo(object sender, EventArgs e)
        {
            //TODO:
            var selectItems = _efDataSource.Where(p => p.Selection).ToList();
            var canCancelItems = selectItems.Where(p => p.EEntrustState == Model.UFX.UFXEntrustState.NoReport
                    || p.EEntrustState == Model.UFX.UFXEntrustState.WaitReport
                    || p.EEntrustState == Model.UFX.UFXEntrustState.Reporting
                    || p.EEntrustState == Model.UFX.UFXEntrustState.Reported
                    || p.EEntrustState == Model.UFX.UFXEntrustState.PartDone
                ).ToList();
            if (selectItems.Count != canCancelItems.Count)
            {
                MessageDialog.Warn(this, msgContainCannotCancelSecurity);
                return;
            }

            var cancelRedoItems = new List<CancelRedoItem>();
            foreach (var canCancelItem in canCancelItems)
            {
                CancelRedoItem calcItem = new CancelRedoItem
                {
                    SubmitId = canCancelItem.SubmitId,
                    CommandId = canCancelItem.CommandNo,
                    SecuCode = canCancelItem.SecuCode,
                    SecuType = canCancelItem.SecuType,
                    SecuName = canCancelItem.SecuName,
                    EntrustNo = canCancelItem.EntrustNo,
                    EntrustBatchNo = canCancelItem.EntrustBatchNo,
                    FirstDealDate = canCancelItem.DFirstDealDate,
                    EntrustDate = canCancelItem.DEntrustDate,
                    EDirection = canCancelItem.EDirection,
                    EOriginPriceType = canCancelItem.EEntrustPriceType,
                    ExchangeCode = UFXTypeConverter.GetMarketCode(canCancelItem.EMarketCode),
                    FundName = canCancelItem.FundName,
                    PortfolioName = canCancelItem.PortfolioName,
                    ReportAmount = canCancelItem.EntrustAmount,
                    ReportPrice = canCancelItem.EntrustPrice,
                    ReportNo = canCancelItem.DeclareNo,
                    DealAmount = canCancelItem.DealAmount,
                    DealTimes = canCancelItem.DealTimes,
                    DealMoney = canCancelItem.DealMoney,
                    EntrustAmount = canCancelItem.EntrustAmount - canCancelItem.DealAmount,
                    LeftAmount = canCancelItem.EntrustAmount - canCancelItem.DealAmount,
                };

                cancelRedoItems.Add(calcItem);
            }

            if (cancelRedoItems.Count == 0)
            {
                MessageDialog.Info(this, msgNoEntrustCancel);
                return;
            }

            //把可以撤销的证券传入撤补对话框
            var dialog = new CancelRedoDialog(_gridConfig);
            dialog.Owner = this;
            dialog.OnLoadControl(dialog, null);
            dialog.OnLoadData(dialog, cancelRedoItems);
            dialog.SaveData += new FormLoadHandler(Dialog_CancelRedoDialog_SaveData);
            dialog.ShowDialog();
        }

        private void ToolStripButton_EntrustFlow_Undo(object sender, EventArgs e)
        {
            //TODO:
            //only select the no-deal
            var selectItems = _efDataSource.Where(p => p.Selection).ToList();
            var canCancelItems = selectItems.Where(p => p.EEntrustState == Model.UFX.UFXEntrustState.NoReport
                    || p.EEntrustState == Model.UFX.UFXEntrustState.WaitReport
                    || p.EEntrustState == Model.UFX.UFXEntrustState.Reporting
                    || p.EEntrustState == Model.UFX.UFXEntrustState.Reported
                    || p.EEntrustState == Model.UFX.UFXEntrustState.PartDone
                ).ToList();

            if (selectItems.Count != canCancelItems.Count)
            {
                MessageDialog.Warn(this, msgContainCannotCancelSecurity);
                return;
            }

            var calcItems = new List<CancelSecurityItem>();
            foreach (var canCancelItem in canCancelItems)
            {
                CancelSecurityItem calcItem = new CancelSecurityItem 
                {
                    SubmitId = canCancelItem.SubmitId,
                    CommandId = canCancelItem.CommandNo,
                    SecuCode = canCancelItem.SecuCode,
                    SecuName = canCancelItem.SecuName,
                    EntrustNo = canCancelItem.EntrustNo,
                    EntrustBatchNo = canCancelItem.EntrustBatchNo,
                    FirstDealDate = canCancelItem.DFirstDealDate,
                    EntrustDate = canCancelItem.DEntrustDate,
                    EDirection = canCancelItem.EDirection,
                    ExchangeCode = UFXTypeConverter.GetMarketCode(canCancelItem.EMarketCode),
                    FundName = canCancelItem.FundName,
                    PortfolioName = canCancelItem.PortfolioName,
                    ReportAmount = canCancelItem.EntrustAmount,
                    ReportPrice = canCancelItem.EntrustPrice,
                    ReportNo = canCancelItem.DeclareNo,
                    DealAmount = canCancelItem.DealAmount,
                    DealTimes = canCancelItem.DealTimes,
                    DealMoney = canCancelItem.DealMoney,
                    LeftAmount = canCancelItem.EntrustAmount - canCancelItem.DealAmount,
                    //ECommandPrice = canCancelItem.EntrustPrice,                    
                };

                calcItems.Add(calcItem);
            }

            if (calcItems.Count == 0)
            {
                MessageDialog.Info(this, msgNoEntrustCancel);
                return;
            }

            var form = new CancelEntrustDialog(_gridConfig);
            form.Owner = this;
            form.OnLoadControl(form, null);
            form.OnLoadData(form, calcItems);
            form.ShowDialog();
        }

        #endregion

        #region Command panel cancel/cancelappend/canceladd click event

        private void ToolStripButton_Command_Cancel(object sender, EventArgs e)
        {
            var selectCmdItems = _cmdDataSource.Where(p => p.Selection).ToList();
            if (selectCmdItems == null || selectCmdItems.Count() == 0)
            {
                MessageDialog.Warn(this, msgNoEntrustCancel);
                return;
            }

            List<EntrustCommand> entrustedCmdItems = new List<EntrustCommand>();
            foreach (var cmdItem in selectCmdItems)
            {
                var oneEntrustedCmdItems = _withdrawBLL.GetEntrustedCmdItems(cmdItem);
                entrustedCmdItems.AddRange(oneEntrustedCmdItems);
            }

            if (entrustedCmdItems.Count == 0)
            {
                MessageDialog.Info(this, msgNoEntrustCancel);
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

            if (calcItems.Count == 0)
            {
                MessageDialog.Info(this, msgNoEntrustCancel);
                return;
            }

            var form = new CancelEntrustDialog(_gridConfig);
            form.Owner = this;
            form.OnLoadControl(form, null);
            form.OnLoadData(form, calcItems);
            form.ShowDialog();
        }

        private void ToolStripButton_Command_CancelRedo(object sender, EventArgs e)
        {
            var selectCmdItems = _cmdDataSource.Where(p => p.Selection).ToList();
            if (selectCmdItems == null || selectCmdItems.Count() == 0)
            {
                MessageDialog.Warn(this, msgNoEntrustCancel);
                return;
            }

            //获取选中的所有可以撤销的证券
            List<EntrustCommand> entrustedCmdItems = new List<EntrustCommand>();
            foreach (var cmdItem in selectCmdItems)
            {
                var oneEntrustedCmdItems = _withdrawBLL.GetEntrustedCmdItems(cmdItem);
                entrustedCmdItems.AddRange(oneEntrustedCmdItems);
            }

            if (entrustedCmdItems.Count == 0)
            {
                MessageDialog.Warn(this, msgNoEntrustCancel);
                return;
            }

            var cancelRedoItems = new List<CancelRedoItem>();
            foreach (var entrustedCmdItem in entrustedCmdItems)
            {
                var cancelSecuItems = _withdrawBLL.GetEntrustedSecuItems(entrustedCmdItem);
                cancelRedoItems.AddRange(cancelSecuItems);
            }

            if (cancelRedoItems.Count == 0)
            {
                MessageDialog.Warn(this, msgNoEntrustCancel);
                return;
            }

            //把可以撤销的证券传入撤补对话框
            var dialog = new CancelRedoDialog(_gridConfig);
            dialog.Owner = this;
            dialog.OnLoadControl(dialog, null);
            dialog.OnLoadData(dialog, cancelRedoItems);
            dialog.SaveData += new FormLoadHandler(Dialog_CancelRedoDialog_SaveData);
            dialog.ShowDialog();
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

        private void GridView_Command_CancelAdd(TradeCommandItem cmdItem)
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
                string format = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgCancelFail);
                string msg = string.Format(format, token.CommandId, token.SubmitId);
                MessageDialog.Warn(this, msg);
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

            TradeCommandItem cmdItem = _cmdDataSource[rowIndex];

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
                default:
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

        private void GridView_Command_Select(TradeCommandItem cmdItem)
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

        private void GridView_Command_UnSelect(TradeCommandItem cmdItem)
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
            var secuItem = _secuDataSource[rowIndex];
            switch(columnName)
            {
                case EntrustPrice:
                    {
                        if (secuItem.EntrustPrice < secuItem.LimitDownPrice || secuItem.EntrustPrice > secuItem.LimitUpPrice)
                        {
                            MessageDialog.Warn(this, msgEntrustPriceBeyondLimit);

                            var findItem = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                            var marketData = QuoteCenter2.Instance.GetMarketData(findItem);
                            secuItem.EntrustPrice = QuotePriceHelper.GetPrice(PriceType.Last, marketData);
                        }
                        else
                        {
                            secuItem.EPriceType = PriceType.Assign;
                        }

                        this.securityGridView.InvalidateCell(columnIndex - 1, rowIndex);
                        this.securityGridView.InvalidateCell(columnIndex, rowIndex);
                    }
                    break;
                case ThisEntrustAmount:
                    {
                        if (secuItem.ThisEntrustAmount + secuItem.EntrustedAmount > secuItem.CommandAmount)
                        {
                            MessageDialog.Warn(this, msgEntrustAmountBeyondTotal);

                            secuItem.ThisEntrustAmount = secuItem.CommandAmount - secuItem.EntrustedAmount;
                            this.securityGridView.InvalidateCell(columnIndex, rowIndex);
                        }
                    }
                    break;
                default:
                    break;
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
            Dictionary<string, string> cmdColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(TradeCommandItem));
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
            //LoadEntrustControl();

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
            var setting = SettingManager.Instance.Get();
            var spotPrices = ConfigManager.Instance.GetComboConfig().GetComboOption("spotprice");
            ComboBoxUtil.SetComboBox(this.cbSpotBuyPrice, spotPrices, setting.EntrustSetting.BuySpotPrice.ToString());

            var spotSellPrices = new ComboOption 
            {
                Name = spotPrices.Name,
                Selected = spotPrices.Selected,
                Items = spotPrices.Items.OrderBy(p => p.Order2).ToList()
            };
            ComboBoxUtil.SetComboBox(this.cbSpotSellPrice, spotSellPrices, setting.EntrustSetting.SellSpotPrice.ToString());

            var futurePrice = ConfigManager.Instance.GetComboConfig().GetComboOption("futureprice");
            ComboBoxUtil.SetComboBox(this.cbFuturesBuyPrice, futurePrice, setting.EntrustSetting.BuyFutuPrice.ToString());

            var futureSellPrices = new ComboOption
            {
                Name = futurePrice.Name,
                Selected = futurePrice.Selected,
                Items = futurePrice.Items.OrderBy(p => p.Order2).ToList()
            };

            ComboBoxUtil.SetComboBox(this.cbFuturesSellPrice, futureSellPrices, setting.EntrustSetting.SellFutuPrice.ToString());
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

                LoadDataEntrustFlow();
            }
            else if (selectTabName == this.tpDealFlow.Name)
            {
                this.tpCmdDealFlow.Controls.Clear();
                this.dfMainPanel.Controls.Clear();
                this.dfMainPanel.Controls.Add(this.dfGridView);

                LoadDataDealFlow();
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

                LoadDataEntrustFlow();
            }
            else if (selectTabName == this.tpCmdDealFlow.Name)
            {
                this.dfMainPanel.Controls.Clear();
                this.tpCmdDealFlow.Controls.Clear();
                this.tpCmdDealFlow.Controls.Add(this.dfGridView);

                LoadDataDealFlow();
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
            //Load combobox. Move here to fix the global setting change, then the buy/sell setting update, too. 
            LoadEntrustControl();

            //Load data here
            LoadDataTradeCommand();

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

            var tradingcmds = _tradeCommandBLL.GetTradeCommandItems();
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
                    var secuInfo = SecurityInfoManager.Instance.Get(p.SecuCode, p.ExchangeCode);
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
                    MessageDialog.Error(this, errorResponse.ErrorMessage);
                }));

                return -1;
            }

            this.BeginInvoke(new Action(()=>
            {
                var entrustItems = _entrustSecurityBLL.GetAllCombine();
                efItems.ForEach(p => {
                    var secuInfo = SecurityInfoManager.Instance.Get(p.SecuCode, p.ExchangeCode);
                    if (secuInfo != null)
                    {
                        p.SecuName = secuInfo.SecuName;
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

        private void CheckBox_Copies_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == null || !(sender is CheckBox))
                return;
            CheckBox cb = sender as CheckBox;
            if (cb.Checked)
            {
                this.nudCopies.Enabled = true;
            }
            else
            {
                this.nudCopies.Enabled = false;
            }
        }

        private void Button_Calculate_Click(object sender, EventArgs e)
        {
            //如果份数框不为0，将所有的份数均设为输入框中的值
            AssignCopies();
            
            if (_secuDataSource.Count == 0)
            {
                MessageDialog.Warn(this, msgNoEntrustSecurity, MessageBoxButtons.OK);
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
            //Check the entrust security items of entrust item. Make sure there is security item selected
            var selectedEntrustItems = _eiDataSource.ToList();
            if (selectedEntrustItems == null || selectedEntrustItems.Count == 0)
            {
                MessageDialog.Warn(this, msgEntrustCommandSelect, MessageBoxButtons.OK);
                return;
            }

            DateTime now = DateTime.Now;
            foreach (var eiItem in selectedEntrustItems)
            {
                var cmdItem = _cmdDataSource.ToList().Find(p => p.CommandId == eiItem.CommandNo);
                if (cmdItem == null)
                {
                    MessageDialog.Warn(this, msgEntrustNoCommandSelect, MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    if (cmdItem.DStartDate > now || cmdItem.DEndDate < now || cmdItem.DStartDate > cmdItem.DEndDate)
                    {
                        MessageDialog.Warn(this, msgEntrustInvalidDate, MessageBoxButtons.OK);
                        return;
                    }
                }

                var thisSecuItems = _secuDataSource.Where(p => p.Selection && p.CommandId == eiItem.CommandNo).ToList();
                int count = thisSecuItems.Count;
                if (count == 0)
                {
                    MessageDialog.Warn(this, msgShouldContainSecurity, MessageBoxButtons.OK);
                    return;
                }

                //选中的非正常交易证券
                count = thisSecuItems.Count(p => p.ESuspendFlag != Model.Quote.SuspendFlag.NoSuspension);
                if (count > 0)
                {
                    MessageDialog.Warn(this, msgEntrustSecuritySuspend, MessageBoxButtons.OK);
                    return;
                }
            }

            if (!ValidateEntrust())
            {
                return;
            }

            var selectedSecuItems = _secuDataSource.Where(p => p.Selection).ToList();
            string confirmFormat = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgEntrustSecurityConfirm);
            string confirmMsg = string.Format(confirmFormat, selectedSecuItems.Count);
            if(MessageDialog.Warn(this, confirmMsg, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            //submit each entrust item and each security in the entrustitem
            //TODO：choose the strategy(cancel all if failure appear)
            List<int> submitIds = new List<int>();
            int successCount = 0;
            foreach (var eiItem in selectedEntrustItems)
            {
                EntrustCommand eciItem = new EntrustCommand
                {
                    CommandId = eiItem.CommandNo,
                    Copies = eiItem.Copies,
                };

                var entrustSecuItems = GetEntrustSecurityItems(-1, eiItem.CommandNo);
                var bllResponse = _entrustBLL.SubmitOne(eciItem, entrustSecuItems, null);

                if (BLLResponse.Success(bllResponse))
                {
                    //success to submit into database
                    submitIds.Add(eciItem.SubmitId);

                    //TODO: update the targetnum in UI
                    successCount += entrustSecuItems.Count;

                    UpdateEntrustedAmount(entrustSecuItems);
                }
                else
                { 
                    //Fail to submit into database
                    string errFormat = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgEntrustCommandFail);
                    string msg = string.Format(errFormat, eiItem.CommandNo, bllResponse.Message);
                    if(MessageDialog.Error(this, msg, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        break;
                    }
                }
            }

            string msgFormat = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgEntrustSecuritySuccessCount);
            string successMsg = string.Format(msgFormat, successCount);
            MessageDialog.Info(this, successMsg);
            //update the UI
            GridView_Security_ResetPriceType(selectedSecuItems);
            nudCopies.Value = 1;
            this.cmdGridView.Invalidate();
            this.bsGridView.Invalidate();
            this.securityGridView.Invalidate();
        }

        private void UpdateEntrustedAmount(List<EntrustSecurity> entrustedSecuItems)
        {
            foreach (var entrustedSecuItem in entrustedSecuItems)
            {
                var findItem = _secuDataSource.ToList().Find(p => p.CommandId == entrustedSecuItem.CommandId && p.SecuCode.Equals(entrustedSecuItem.SecuCode));
                if (findItem != null)
                {
                    findItem.EntrustedAmount += entrustedSecuItem.EntrustAmount;
                }
            }
        }

        /// <summary>
        /// 计算目标份数、目标数量、本次委托数量、待补足数量
        /// 新的目标份数 = 原来目标份数(计算前的) + 追加份数
        /// 目标数量 = 新的目标份数 * 份数权重
        /// 本次委托数量 = 目标数量 - 已委托数量
        /// 待补足数量 = 目标数量 - 已委托数量
        /// </summary>
        /// <param name="eiItem">操作的指令与其份数</param>
        /// <param name="spotBuyPrice">现货买入价格类型</param>
        /// <param name="spotSellPrice">现货卖出价格类型</param>
        /// <param name="futureBuyPrice">股指期货买入价格类型</param>
        /// <param name="futureSellPrice">股指期货卖出价格类型</param>
        private void CalculateOne(EntrustItem eiItem, PriceType spotBuyPrice, PriceType spotSellPrice, PriceType futureBuyPrice, PriceType futureSellPrice)
        {
            var selCmdItem = _cmdDataSource.Single(p => p.CommandId == eiItem.CommandNo);
            if (selCmdItem == null || selCmdItem.CommandNum <= 0)
            {
                return;
            }

            //检查委托数量
            var noEntrustNum = selCmdItem.CommandAmount - selCmdItem.EntrustedAmount;
            if (noEntrustNum <= 0)
            {
                return;
            }

            int thisCopies = eiItem.Copies;
            int oldTargetNum = selCmdItem.TargetNum;
            int newTargetNum = oldTargetNum + eiItem.Copies;
            if (newTargetNum > selCmdItem.CommandNum)
            {
                newTargetNum = selCmdItem.CommandNum;
            }

            //TODO:确认指令数量、目标数量、待补足数量之间的关系
            var secuItems = _secuDataSource.Where(p => p.CommandId == eiItem.CommandNo).ToList();
            foreach (var secuItem in secuItems)
            {
                CalculateSecurity(secuItem, selCmdItem.CommandNum, newTargetNum, thisCopies, spotBuyPrice, spotSellPrice, futureBuyPrice, futureSellPrice);
            }
        }

        /// <summary>
        /// 设置目标数量、本次委托数量、待补足数量、委托价格
        /// 目标数量 = 新目标份数 * 单位数量
        /// 
        /// 本次委托数量：
        /// 1. 如果操作份数为零，本次委托数量补足至当前目标份数，即(目标数量 - 已委托数量)
        /// 2. 如果操作份数大于零，本次委托数量为 max(操作份数 * 单位数量, 指令数量 - 已委托数量)
        /// 
        /// 待补足数量 = 目标数量 - 本次委托数量
        ///
        /// </summary>
        /// <param name="secuItem">委托的证券</param>
        /// <param name="commandNum">指令份数</param>
        /// <param name="newTargetNum">新的目标份数</param>
        /// <param name="thisCopies">本次操作份数</param>
        /// <param name="spotBuyPrice">现货买入价格类型</param>
        /// <param name="spotSellPrice">现货卖出价格类型</param>
        /// <param name="futureBuyPrice">股指期货买入价格类型</param>
        /// <param name="futureSellPrice">股指期货卖出价格类型</param>
        private void CalculateSecurity(CommandSecurityItem secuItem, int commandNum, int newTargetNum, int thisCopies, PriceType spotBuyPrice, PriceType spotSellPrice, PriceType futureBuyPrice, PriceType futureSellPrice)
        {
            //份数权重
            int weightAmount = secuItem.CommandAmount / commandNum;
            //目标数量
            int targetAmount = 0;
            //本次委托数量
            int thisEntrustAmount = 0;
            //待补足数量
            int waitAmount = 0;

            secuItem.TargetCopies = newTargetNum;
            targetAmount = newTargetNum * weightAmount;

            if (thisCopies == 0)
            {
                thisEntrustAmount = targetAmount - secuItem.EntrustedAmount;
            }
            else
            {
                thisEntrustAmount = thisCopies * weightAmount;
            }

            if (thisEntrustAmount < 0)
            {
                thisEntrustAmount = 0;
            }

            waitAmount = targetAmount - secuItem.EntrustedAmount;
            if (waitAmount < 0)
            {
                waitAmount = 0;
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
            foreach (var secuItem in _secuDataSource)
            {
                var targetItem = secuList.Find(p => p.SecuCode.Equals(secuItem.SecuCode) && (p.SecuType == SecurityType.Stock || p.SecuType == SecurityType.Futures));
                var marketData = QuoteCenter2.Instance.GetMarketData(targetItem);

                secuItem.LimitUpPrice = marketData.HighLimitPrice;
                secuItem.LimitDownPrice = marketData.LowLimitPrice;
                secuItem.ESuspendFlag = marketData.SuspendFlag;
                secuItem.ELimitUpDownFlag = marketData.LimitUpDownFlag;

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

        private List<EntrustSecurity> GetEntrustSecurityItems(int submitId, int commandId)
        {
            List<EntrustSecurity> entrustSecuItems = new List<EntrustSecurity>();

            var secuItems = _secuDataSource.Where(p => p.Selection && p.CommandId == commandId).ToList();
            if (secuItems == null || secuItems.Count == 0)
            {
                return entrustSecuItems;
            }

            var setting = SettingManager.Instance.Get();
            foreach (var secuItem in secuItems)
            {
                //var priceType = PriceTypeHelper.GetPriceType(secuItem.PriceType);
                var priceType = secuItem.EPriceType;
                EntrustSecurity entrustSecurityItem = new EntrustSecurity
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
                    exchangeCode = SecurityItemHelper.GetExchangeCode(secuItem.SecuCode, secuItem.SecuType);
                }

                //只有选择市价时设置市价委托方式
                if (priceType == PriceType.Market && entrustSecurityItem.SecuType == SecurityType.Stock)
                {
                    if (exchangeCode.Equals(Exchange.SHSE))
                    {
                        entrustSecurityItem.EntrustPriceType = setting.EntrustSetting.SseEntrustPriceType;
                    }
                    else if (exchangeCode.Equals(Exchange.SZSE))
                    {
                        entrustSecurityItem.EntrustPriceType = setting.EntrustSetting.SzseEntrustPriceType;
                    }
                }
                else
                {
                    entrustSecurityItem.EntrustPriceType = EntrustPriceType.FixedPrice;
                }

                entrustSecuItems.Add(entrustSecurityItem);
            }

            return entrustSecuItems;
        }

        private void AssignCopies()
        {
            if (this.cbCopies.Checked)
            {
                int copies = (int)nudCopies.Value;
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
                MessageDialog.Warn(this, msgEntrustSecuritySelect);
                return false;
            }

            foreach (var entrustSecuItem in entrustSecuItems)
            {
                if (entrustSecuItem.ThisEntrustAmount + entrustSecuItem.EntrustedAmount > entrustSecuItem.CommandAmount)
                {
                    MessageDialog.Warn(this, msgEntrustAmountBeyondTotal);

                    var findIndex = _secuDataSource.ToList().FindIndex(p => p.SecuCode == entrustSecuItem.SecuCode
                        && p.CommandId == entrustSecuItem.CommandId);
                    if (findIndex >= 0)
                    {
                        securityGridView.SetFocus(findIndex, ThisEntrustAmount);
                    }

                    return false;
                }

                if (entrustSecuItem.ESuspendFlag != Model.Quote.SuspendFlag.NoSuspension)
                {
                    MessageDialog.Warn(this, msgEntrustPricePrompt);

                    var findIndex = _secuDataSource.ToList().FindIndex(p => p.SecuCode == entrustSecuItem.SecuCode
                        && p.CommandId == entrustSecuItem.CommandId);
                    if (findIndex >= 0)
                    {
                        securityGridView.SetFocus(findIndex, SuspensionFlag);
                    }

                    return false;
                }
                else if (entrustSecuItem.ThisEntrustAmount <= 0)
                {
                    MessageDialog.Warn(this, msgEntrustPricePrompt);

                    var findIndex = _secuDataSource.ToList().FindIndex(p => p.SecuCode == entrustSecuItem.SecuCode
                        && p.CommandId == entrustSecuItem.CommandId);
                    if (findIndex >= 0)
                    {
                        securityGridView.SetFocus(findIndex, ThisEntrustAmount);
                    }

                    return false;
                }
                else if(FloatUtil.IsZero(entrustSecuItem.EntrustPrice)
                    || entrustSecuItem.EntrustPrice < entrustSecuItem.LimitDownPrice
                    || entrustSecuItem.EntrustPrice > entrustSecuItem.LimitUpPrice
                   )
                {
                    MessageDialog.Warn(this, msgEntrustPricePrompt);

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
