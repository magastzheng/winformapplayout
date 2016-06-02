using Config;
using Controls.Entity;
using Controls.GridView;
using DBAccess;
using Forms;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Model.SecurityInfo;
using Quote;

namespace TradingSystem.View
{
    public partial class StrategyTradingForm2 : Forms.BaseForm
    {
        private const string GridCmdTradingId = "cmdtrading";
        private const string GridEntrustFlowId = "entrustflow";
        private const string GridDealFlowId = "dealflow";
        private const string GridCmdSecurityId = "cmdsecurity";
        private const string GridBuySellId = "buysell";

        //private TradingInstanceDAO _tradeInstdao = new TradingInstanceDAO();
        private TradingCommandDAO _tradecmddao = new TradingCommandDAO();
        private TradingCommandSecurityDAO _tradecmdsecudao = new TradingCommandSecurityDAO();
        private SecurityInfoDAO _secudbdao = new SecurityInfoDAO();

        private SortableBindingList<TradingCommandItem> _cmdDataSource = new SortableBindingList<TradingCommandItem>(new List<TradingCommandItem>());
        private SortableBindingList<EntrustFlowItem> _efDataSource = new SortableBindingList<EntrustFlowItem>(new List<EntrustFlowItem>());
        private SortableBindingList<DealFlowItem> _dfDataSource = new SortableBindingList<DealFlowItem>(new List<DealFlowItem>());
        private SortableBindingList<EntrustItem> _eiDataSource = new SortableBindingList<EntrustItem>(new List<EntrustItem>());
        private SortableBindingList<CommandSecurityItem> _secuDataSource = new SortableBindingList<CommandSecurityItem>(new List<CommandSecurityItem>());

        private List<SecurityItem> _securityInfoList = new List<SecurityItem>();

        GridConfig _gridConfig;
        public StrategyTradingForm2()
            :base()
        {
            InitializeComponent();
        }

        public StrategyTradingForm2(GridConfig gridConfig)
            :this()
        {
            _gridConfig = gridConfig;

            LoadControl += new FormLoadHandler(Form_LoadControl);
            LoadData += new FormLoadHandler(Form_LoadData);

            tabParentMain.SelectedIndexChanged += new EventHandler(TabControl_Parent_SelectedIndexChanged);
            tabChildSecurity.SelectedIndexChanged += new EventHandler(TabControl_Child_SelectedIndexChanged);

            tbCopies.KeyPress += new KeyPressEventHandler(TextBox_Copies_KeyPress);

            this.cmdGridView.UpdateRelatedDataGridHandler += new UpdateRelatedDataGrid(GridView_Command_UpdateRelatedDataGridHandler);
            this.bsGridView.UpdateRelatedDataGridHandler += new UpdateRelatedDataGrid(GridView_BuySell_UpdateRelatedDataGridHandler);

            //Refresh
            this.tsbRefresh.Click += new EventHandler(ToolStripButton_Command_Refresh);
            this.tsbefRefresh.Click += new EventHandler(ToolStripButton_EntrustFlow_Refresh);
            this.tsbdfRefresh.Click += new EventHandler(ToolStripButton_DealFlow_Refresh);

            //Calculate
            this.btnCalc.Click += new EventHandler(Button_Calculate_Click);

            this.btnEntrust.Click += new EventHandler(Button_Entrust_Click);
        }

        private void GridView_Command_UpdateRelatedDataGridHandler(UpdateDirection direction, int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= _cmdDataSource.Count)
                return;

            TradingCommandItem cmdItem = _cmdDataSource[rowIndex];

            switch (direction)
            {
                case UpdateDirection.Select:
                    {
                        //Add into two gridview: CommandSecurity and entrust
                        var secuItems = _secuDataSource.Where(p => p.CommandId == cmdItem.CommandId);
                        if(secuItems == null || secuItems.Count() == 0)
                        {
                            secuItems = _tradecmdsecudao.Get(cmdItem.CommandId);
                            if (secuItems != null)
                            {
                                foreach (var secuItem in secuItems)
                                {
                                    secuItem.Selection = true;
                                    secuItem.CommandCopies = cmdItem.CommandNum;
                                    _secuDataSource.Add(secuItem);
                                }
                            }
                        }

                        //Add into buy/sell grid view
                        var entrustItems = _eiDataSource.Where(p => p.CommandNo == cmdItem.CommandId);
                        if (entrustItems == null || entrustItems.Count() == 0)
                        {
                            var entrustItem = new EntrustItem
                            {
                                CommandNo = cmdItem.CommandId,
                                Selection = true
                            };

                            _eiDataSource.Add(entrustItem);
                        }
                    }
                    break;
                case UpdateDirection.UnSelect:
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

        #region form loading

        private bool Form_LoadControl(object sender, object data)
        {
            //Load Command Trading
            TSDataGridViewHelper.AddColumns(this.cmdGridView, _gridConfig.GetGid(GridCmdTradingId));
            Dictionary<string, string> cmdColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(TradingCommandItem));
            TSDataGridViewHelper.SetDataBinding(this.cmdGridView, cmdColDataMap);           

            //Load EntrustFlow gridview
            TSDataGridViewHelper.AddColumns(this.efGridView, _gridConfig.GetGid(GridEntrustFlowId));
            Dictionary<string, string> efColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(EntrustFlowItem));
            TSDataGridViewHelper.SetDataBinding(this.efGridView, efColDataMap);

            //Load DealFlow gridview
            TSDataGridViewHelper.AddColumns(this.dfGridView, _gridConfig.GetGid(GridDealFlowId));
            Dictionary<string, string> dfColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(DealFlowItem));
            TSDataGridViewHelper.SetDataBinding(this.dfGridView, dfColDataMap);

            //Load Security gridview
            TSDataGridViewHelper.AddColumns(this.securityGridView, _gridConfig.GetGid(GridCmdSecurityId));
            Dictionary<string, string> secuColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(CommandSecurityItem));
            TSDataGridViewHelper.SetDataBinding(this.securityGridView, secuColDataMap);

            //Load Entrust gridview
            TSDataGridViewHelper.AddColumns(this.bsGridView, _gridConfig.GetGid(GridBuySellId));
            Dictionary<string, string> bsColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(EntrustItem));
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
            var spotBuy = ConfigManager.Instance.GetComboConfig().GetComboOption("spotbuy");
            ComboBoxUtil.SetComboBox(this.cbSpotBuyPrice, spotBuy);

            var spotSell = ConfigManager.Instance.GetComboConfig().GetComboOption("spotsell");
            ComboBoxUtil.SetComboBox(this.cbSpotSellPrice, spotSell);

            var futureBuy = ConfigManager.Instance.GetComboConfig().GetComboOption("futurebuy");
            ComboBoxUtil.SetComboBox(this.cbFuturesBuyPrice, futureBuy);

            var futureSell = ConfigManager.Instance.GetComboConfig().GetComboOption("futuresell");
            ComboBoxUtil.SetComboBox(this.cbFuturesSellPrice, futureSell);
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
            Clear();
            
            //Load data here
            
            //Load the securityinfo
            this._securityInfoList = _secudbdao.Get(SecurityType.All);

            var tradingcmds = _tradecmddao.Get(-1);
            if (tradingcmds != null)
            {
                foreach (var cmdItem in tradingcmds)
                {
                    _cmdDataSource.Add(cmdItem);
                }
            }

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

        #endregion

        #region control event handler
        
        private void TextBox_Copies_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region toolstrip click event handler

        private void ToolStripButton_DealFlow_Refresh(object sender, EventArgs e)
        {
            //TODO: load the dealflow security
        }

        private void ToolStripButton_EntrustFlow_Refresh(object sender, EventArgs e)
        {
            //TODO: load the entrustflow security
        }

        private void ToolStripButton_Command_Refresh(object sender, EventArgs e)
        {
            Form_LoadData(this, null);
        }

        #endregion

        #region buy/sell button click event

        private void Button_Calculate_Click(object sender, EventArgs e)
        {
            //TODO:
            if (!ValidateCopies())
            {
                MessageBox.Show(this, "请输入委托份数", "警告", MessageBoxButtons.OK);
                return;
            }

            //update each command item
            foreach (var eiItem in _eiDataSource)
            {
                var selCmdItems = _cmdDataSource.Where(p => p.CommandId == eiItem.CommandNo).ToList();
                if (selCmdItems != null && selCmdItems.Count == 1)
                {
                    selCmdItems[0].TargetNum = eiItem.Copies;

                    _secuDataSource.Where(p => p.CommandId == eiItem.CommandNo)
                        .ToList()
                        .ForEach(p => {
                            p.TargetCopies = eiItem.Copies;
                            p.TargetAmount = p.TargetCopies * p.WeightAmount;
                            p.ThisEntrustAmount = p.TargetCopies * p.WeightAmount;
                            p.WaitAmount = p.TargetCopies * p.WeightAmount;
                        });
                }
            }

            //query the price and set it
            List<SecurityItem> secuList = new List<SecurityItem>();
            var uniqueSecuItems = _secuDataSource.GroupBy(p => p.SecuCode).Select(p => p.First());
            foreach (var secuItem in uniqueSecuItems)
            {
                var findItem = _securityInfoList.Find(p => p.SecuCode.Equals(secuItem.SecuCode) && (p.SecuType == SecurityType.Stock || p.SecuType == SecurityType.Futures));
                secuList.Add(findItem);
                //if (findItem != null)
                //{
                //    var addedItem = secuList.Find(p => p.SecuCode.Equals(findItem.SecuCode) && p.SecuType == findItem.SecuType);
                //    if (addedItem == null)
                //    {
                //        secuList.Add(findItem);
                //    }
                //}
            }

            QuoteCenter.Instance.Query(secuList);
            foreach (var secuItem in _secuDataSource)
            {
                var targetItem = secuList.Find(p => p.SecuCode.Equals(secuItem.SecuCode) && (p.SecuType == SecurityType.Stock || p.SecuType == SecurityType.Futures));
                var marketData = QuoteCenter.Instance.GetMarketData(targetItem);
                secuItem.EntrustedPrice = marketData.CurrentPrice;
                secuItem.LimitUpPrice = marketData.HighLimitPrice;
                secuItem.LimitDownPrice = marketData.LowLimitPrice;
                secuItem.SuspensionFlag = marketData.SuspendFlag.ToString();
            }

            //refresh UI
            this.cmdGridView.Invalidate();
            this.bsGridView.Invalidate();
            this.securityGridView.Invalidate();
        }

        private void Button_Entrust_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private bool ValidateCopies()
        {
            bool ret = true;

            int copies = 0;
            if(!string.IsNullOrEmpty(tbCopies.Text))
            {
                int temp = 0;
                if (int.TryParse(tbCopies.Text, out temp))
                {
                    copies = temp;
                }
            }

            if (copies > 0)
            {
                foreach (var eiItem in _eiDataSource)
                {
                    eiItem.Copies = copies;
                }
            }

            var selItems = _eiDataSource.Where(p => p.Selection && p.Copies == 0).ToList();
            if (selItems != null && selItems.Count > 0)
            {
                ret = false;
            }

            return ret;
        }
        #endregion
    }
}
