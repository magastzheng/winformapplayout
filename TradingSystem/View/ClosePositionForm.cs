using Config;
using Controls.Entity;
using Controls.GridView;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Model.SecurityInfo;
using Util;
using BLL.SecurityInfo;
using TradingSystem.Dialog;
using BLL.Template;
using Model.config;
using Model.EnumType;
using Model.Binding.BindingUtil;
using Quote;
using TradingSystem.TradeUtil;
using BLL.Entrust;
using System.Diagnostics;
using System.Text;
using Model.Database;
using BLL.Frontend;
using BLL.TradeInstance;

namespace TradingSystem.View
{
    public enum CloseDialogType
    {
        CloseAll = 1,
        ChangePosition = 2,
        AddSecurity = 3,
    }

    public partial class ClosePositionForm : Forms.DefaultForm
    {
        private const string GridCloseId = "closeposition";
        private const string GridSecurityId = "closepositionsecurity";
        private const string GridCloseCmdId = "closepositioncmd";

        private const string msgCopies = "closecopiesinput";
        private const string msgZeroAmount = "closecannotzeroentrustamount";
        private const string msgSubmitInvalid = "closesubmitinvalid";

        private GridConfig _gridConfig;

        //private FuturesContractBLL _futuresBLL = new FuturesContractBLL();
        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private TemplateBLL _templateBLL = new TemplateBLL();
        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();
        private TradeInstanceSecurityBLL _tradeInstanceSecuBLL = new TradeInstanceSecurityBLL();

        private SortableBindingList<ClosePositionItem> _instDataSource = new SortableBindingList<ClosePositionItem>(new List<ClosePositionItem>());
        private SortableBindingList<ClosePositionSecurityItem> _secuDataSource = new SortableBindingList<ClosePositionSecurityItem>(new List<ClosePositionSecurityItem>());
        private SortableBindingList<ClosePositionCmdItem> _cmdDataSource = new SortableBindingList<ClosePositionCmdItem>(new List<ClosePositionCmdItem>());
        private ComboOption _futuresOption = new ComboOption();

        private Dictionary<int, string> _instanceFuturesMap = new Dictionary<int, string>();

        private CloseDialogType _dialogType = CloseDialogType.CloseAll;

        public ClosePositionForm()
            :base()
        {
            InitializeComponent();
        }

        public ClosePositionForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);

            this.closeGridView.UpdateRelatedDataGridHandler += new UpdateRelatedDataGrid(GridView_Close_UpdateRelatedDataGridHandler);
            this.cmdGridView.ComboBoxSelectionChangeCommitHandler += new ComboBoxSelectionChangeCommitHandler(GridView_Command_ComboBoxSelectionChangeCommit);

            this.cbCopies.CheckedChanged += new EventHandler(ComboBox_Copies_CheckedChanged);
            this.btnCalc.Click += new EventHandler(Button_Calc_Click);
            this.btnCloseAll.Click += new EventHandler(Button_CloseAll_Click);
            this.btnChgPosition.Click += new EventHandler(Button_ChgPosition_Click);
            this.btnSubmit.Click += new EventHandler(Button_Submit_Click);

            this.btnSelectAll.Click += new EventHandler(Button_SelectAll_Click);
            this.btnUnSelectAll.Click += new EventHandler(Button_UnSelectAll_Click);
        }

        private void GridView_Command_ComboBoxSelectionChangeCommit(ComboBox comboBox, object selectedItem, int columnIndex, int rowIndex)
        {
            if (selectedItem == null || !(selectedItem is ComboOptionItem))
            {
                return;
            }

            if (rowIndex < 0 || rowIndex >= _cmdDataSource.Count)
            {
                return;
            }

            var cbItem = selectedItem as ComboOptionItem;
            EntrustDirection direction = EntrustDirectionUtil.GetEntrustDirection(cbItem.Id);

            var cmdItem = _cmdDataSource[rowIndex];
            var closeItem = _instDataSource.ToList().Find(p => p.InstanceId == cmdItem.InstanceId);
            if (closeItem != null)
            {
                for (int i = _secuDataSource.Count - 1; i >= 0; i--)
                {
                    if (_secuDataSource[i].InstanceId == cmdItem.InstanceId)
                    {
                        _secuDataSource.RemoveAt(i);
                    }
                }

                LoadSecurity(closeItem, direction);
            }
        }

        #region select/unselect

        private void Button_SelectAll_Click(object sender, EventArgs e)
        {
            _secuDataSource.ToList()
                .ForEach(p => p.Selection = true);

            this.securityGridView.Invalidate();
        }

        private void Button_UnSelectAll_Click(object sender, EventArgs e)
        {
            _secuDataSource.ToList()
                .ForEach(p => p.Selection = false);

            this.securityGridView.Invalidate();
        }

        #endregion

        #region GridView UpdateRelated

        private void GridView_Close_UpdateRelatedDataGridHandler(UpdateDirection direction, int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= _instDataSource.Count)
                return;

            ClosePositionItem closeItem = _instDataSource[rowIndex];
            switch (direction)
            {
                case UpdateDirection.Select:
                    {
                        LoadCloseCommand(closeItem);
                        LoadSecurity(closeItem);
                    }
                    break;
                case UpdateDirection.UnSelect:
                    { 
                        //Remove the unselected security items
                        var secuItems = _secuDataSource.Where(p => p.InstanceId == closeItem.InstanceId).ToList();
                        if (secuItems != null && secuItems.Count() > 0)
                        {
                            foreach (var secuItem in secuItems)
                            {
                                _secuDataSource.Remove(secuItem);
                            }
                        }

                        var cmdItems = _cmdDataSource.Where(p => p.InstanceId == closeItem.InstanceId).ToList();
                        if (cmdItems != null && cmdItems.Count > 0)
                        {
                            foreach (var cmdItem in cmdItems)
                            {
                                _cmdDataSource.Remove(cmdItem);
                            }
                        }

                        if (_instanceFuturesMap.ContainsKey(closeItem.InstanceId))
                        {
                            _instanceFuturesMap.Remove(closeItem.InstanceId);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void LoadSecurity(ClosePositionItem closeItem)
        {
            var closeCmdItem = _cmdDataSource.ToList().Find(p => p.InstanceId == closeItem.InstanceId);
            if (closeCmdItem == null)
                return;

            EntrustDirection direction = EntrustDirectionUtil.GetEntrustDirection(closeCmdItem.TradeDirection);
            LoadSecurity(closeItem, direction);
        }

        private void LoadSecurity(ClosePositionItem closeItem, EntrustDirection direction)
        {
            var secuItems = _tradeInstanceSecuBLL.Get(closeItem.InstanceId);

            if (secuItems != null)
            {
                if (direction == EntrustDirection.Sell)
                {
                    secuItems = secuItems.Where(p => p.PositionAmount > 0 || p.InstructionPreBuy > 0 || p.InstructionPreSell > 0).ToList();
                }

                foreach (var secuItem in secuItems)
                {
                    AddSecurity(secuItem, closeItem);
                }
            }

            if (direction != EntrustDirection.Sell)
            {
                var tempstockitems = _templateBLL.GetStocks(closeItem.TemplateId);
                if (tempstockitems != null)
                {
                    var noIncludedStocks = from p in tempstockitems
                                           where secuItems.Find(o => o.SecuCode.Equals(p.SecuCode)) == null
                                           select p;

                    var noIncludedList = noIncludedStocks.ToList();
                    //AddSecurity();
                    foreach (var stockItem in noIncludedList)
                    {
                        AddSecurity(stockItem, closeItem);
                    }
                }
            }
        }

        private void LoadCloseCommand(ClosePositionItem closeItem)
        {
            ClosePositionCmdItem cmdItem = new ClosePositionCmdItem 
            {
                InstanceId = closeItem.InstanceId,
                InstanceCode = closeItem.InstanceCode,
                SpotTemplate = closeItem.TemplateId.ToString(),
                MonitorName = closeItem.MonitorName,
                FuturesContract = closeItem.FuturesContract,
                TradeDirection = ((int)EntrustDirection.Sell).ToString(),
                Copies = 1,
            };

            //if (_secuDataSource != null && _secuDataSource.Count > 0)
            //{
            //    var futuresItem = _secuDataSource.ToList()
            //                        .Find(p => p.InstanceId == closeItem.InstanceId && p.SecuType == Model.SecurityInfo.SecurityType.Futures);
            //    if (futuresItem != null)
            //    {
            //        cmdItem.FuturesContract = futuresItem.SecuCode;
            //    }
            //}

            _cmdDataSource.Add(cmdItem);
        }

        #endregion

        #region load control
        private bool Form_LoadControl(object sender, object data)
        {
            //set the default copies setting
            SetCopiesState(this.cbCopies);

            //set the monitorGridView
            TSDataGridViewHelper.AddColumns(this.closeGridView, _gridConfig.GetGid(GridCloseId));
            Dictionary<string, string> closeColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(ClosePositionItem));
            TSDataGridViewHelper.SetDataBinding(this.closeGridView, closeColDataMap);

            //set the securityGridView
            TSDataGridViewHelper.AddColumns(this.securityGridView, _gridConfig.GetGid(GridSecurityId));
            Dictionary<string, string> securityColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(ClosePositionSecurityItem));
            TSDataGridViewHelper.SetDataBinding(this.securityGridView, securityColDataMap);

            //set the command gridview
            TSDataGridViewHelper.AddColumns(this.cmdGridView, _gridConfig.GetGid(GridCloseCmdId));
            Dictionary<string, string> cmdColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(ClosePositionCmdItem));
            TSDataGridViewHelper.SetDataBinding(this.cmdGridView, cmdColDataMap);

            this.closeGridView.DataSource = _instDataSource;
            this.securityGridView.DataSource = _secuDataSource;
            this.cmdGridView.DataSource = _cmdDataSource;

            LoadTradeDirectionOption();
            LoadFuturesContractOption();

            return true;
        }

        private void LoadTradeDirectionOption()
        {
            var tradeDirectionOption = ConfigManager.Instance.GetComboConfig().GetComboOption("tradedirection");
            TSDataGridViewHelper.SetDataBinding(this.cmdGridView, "tradedirection", tradeDirectionOption);
        }

        private void LoadFuturesContractOption()
        {
            var futures = FuturesContractManager.Instance.GetAll();
            if (futures != null && futures.Count > 0)
            {
                var items = new List<ComboOptionItem>();
                foreach (var future in futures)
                {
                    ComboOptionItem option = new ComboOptionItem
                    {
                        Id = future.SecuCode,
                        Name = future.SecuName
                    };

                    items.Add(option);
                }

                _futuresOption.Items = items;
                TSDataGridViewHelper.SetDataBinding(this.cmdGridView, "futurescontract", _futuresOption);
            }
        }

        private void LoadTemplateOption()
        {
            //binding the template
            var templates = _templateBLL.GetTemplates();
            if (templates != null && templates.Count > 0)
            {
                ComboOption tempOption = new ComboOption
                {
                    Items = new List<ComboOptionItem>(),
                };

                foreach (var temp in templates)
                {
                    ComboOptionItem option = new ComboOptionItem
                    {
                        Id = temp.TemplateId.ToString(),
                        Name = string.Format("{0} {1}", temp.TemplateId, temp.TemplateName)
                    };

                    tempOption.Items.Add(option);
                }

                TSDataGridViewHelper.SetDataBinding(this.cmdGridView, "spottemplate", tempOption);
            }
        }

        #endregion

        #region load data

        private bool Form_LoadData(object sender, object data)
        {
            _instDataSource.Clear();
            _secuDataSource.Clear();
            _cmdDataSource.Clear();
            _instanceFuturesMap.Clear();

            LoadTemplateOption();

            var tradeInstances = _tradeInstanceBLL.GetAllInstance();
            if (tradeInstances == null || tradeInstances.Count == 0)
            {
                return false;
            }

            foreach (var instance in tradeInstances)
            {
                ClosePositionItem closeItem = new ClosePositionItem
                {
                    InstanceId = instance.InstanceId,
                    InstanceCode = instance.InstanceCode,
                    MonitorId = instance.MonitorUnitId,
                    MonitorName = instance.MonitorUnitName,
                    TemplateId = instance.TemplateId,
                    PortfolioId = instance.PortfolioId,
                    PortfolioName = instance.PortfolioName,
                    FuturesContract = instance.FuturesContract,
                    PortfolioCode = instance.PortfolioCode,
                    TemplateName = instance.TemplateName,
                };

                AddFuturesContract(closeItem.FuturesContract);

                _instDataSource.Add(closeItem);
            }

            //LoadHolding();

            return true;
        }

        private void LoadHolding()
        {
            //UFXQueryHoldingBLL holdingBLL = new UFXQueryHoldingBLL();
            //UFXQueryMultipleHoldingBLL holdingBLL = new UFXQueryMultipleHoldingBLL();
            //holdingBLL.Query();

            //Debug.WriteLine("Only test for the holding!");
        }

        private void AddFuturesContract(string futuresCode)
        {
            if (_futuresOption == null || _futuresOption.Items == null)
            {
                return;
            }

            var findItem = _futuresOption.Items.Find(p => p.Id.Equals(futuresCode));
            if (findItem == null)
            {
                ComboOptionItem option = new ComboOptionItem
                {
                    Id = futuresCode,
                    Name = futuresCode,
                };

                _futuresOption.Items.Add(option);
            }
        }
        #endregion

        #region Button Click Event

        private void ComboBox_Copies_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == null || !(sender is CheckBox))
                return;
            CheckBox cb = sender as CheckBox;
            SetCopiesState(cb);
        }

        private void SetCopiesState(CheckBox cb)
        {
            if (cb.Checked)
            {
                this.nudCopies.Enabled = true;
            }
            else
            {
                this.nudCopies.Enabled = false;
            }
        }

        private void Button_Calc_Click(object sender, EventArgs e)
        {
            //_execType = ExecuteType.ClosePosition;
            //var cmdItems = _cmdDataSource.Where(p => p.Selection).ToList();
            var cmdItems = _cmdDataSource.ToList();
            if (!ValidateCopies(cmdItems))
            {
                MessageDialog.Error(this, msgCopies);
                return;
            }

            foreach (var cmdItem in cmdItems)
            {
                CalculateInstance(cmdItem);
            }

            //询价
            QueryQuote();

            this.closeGridView.Invalidate();
            this.cmdGridView.Invalidate();
            this.securityGridView.Invalidate();
        }

        private void Button_CloseAll_Click(object sender, EventArgs e)
        {
            _dialogType = CloseDialogType.CloseAll;
            var cmdItems = _cmdDataSource.ToList();
            
            foreach (var cmdItem in cmdItems)
            {
                //cmdItem.TradeDirection = ((int)EntrustDirection.Sell).ToString();
                CloseAll(cmdItem);

                //var closeItem = _instDataSource.First(p => p.InstanceId == cmdItem.InstanceId);
                //var closeSecuItems = _secuDataSource.Where(p => p.InstanceId == cmdItem.InstanceId).ToList();

                //int result = _tradeCommandBLL.SubmitCloseAll(closeItem, closeSecuItems);
                //if (result < 0)
                //{ 
                //    //TODO: fail to submit
                //}
            }

            QueryQuote();

            this.cmdGridView.Invalidate();
            this.securityGridView.Invalidate();
        }

        private void Button_ChgPosition_Click(object sender, EventArgs e)
        {
            int rowIndex = this.securityGridView.GetCurrentRowIndex();
            if (rowIndex < 0)
            {
                return;
            }

            var secuItem = this._secuDataSource[rowIndex];
            if (secuItem == null)
            {
                return;
            }

            if (secuItem.EntrustAmount > 0)
            {
                MessageDialog.Error(this, msgZeroAmount);
                return;
            }

            ChangePositionDialog dialog = new ChangePositionDialog();
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            //dialog.OnLoadFormActived(json);
            //dialog.Visible = true;
            dialog.OnLoadControl(dialog, null);
            dialog.OnLoadData(dialog, secuItem);
            dialog.SaveData += new FormLoadHandler(Dialog_SaveData);
            dialog.ShowDialog();

            if (dialog.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                _dialogType = CloseDialogType.ChangePosition;
                dialog.Dispose();

                this.securityGridView.Invalidate();
            }
            else
            {
                dialog.Dispose();
            }
        }

        private void QueryQuote()
        {
            var uniqueSecuItems = _secuDataSource.GroupBy(p => p.SecuCode).Select(p => p.First());

            List<SecurityItem> secuList = new List<SecurityItem>();
            foreach (var secuItem in uniqueSecuItems)
            {
                var findItem = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                if (findItem != null)
                {
                    secuList.Add(findItem);
                }
            }

            //QuoteCenter.Instance.Query(secuList);
            foreach (var secuItem in _secuDataSource)
            {
                var targetItem = secuList.Find(p => p.SecuCode.Equals(secuItem.SecuCode) && (p.SecuType == SecurityType.Stock || p.SecuType == SecurityType.Futures));
                var marketData = QuoteCenter2.Instance.GetMarketData(targetItem);
                secuItem.LastPrice = marketData.CurrentPrice;
               
                //secuItem.ESuspendFlag = marketData.SuspendFlag;
                secuItem.ELimitUpDownFlag = QuotePriceHelper.GetLimitUpDownFlag(marketData.CurrentPrice, marketData.LowLimitPrice, marketData.HighLimitPrice);

                if (secuItem.SecuType == SecurityType.Stock)
                {
                    secuItem.CommandMoney = secuItem.LastPrice * secuItem.EntrustAmount;
                    switch (secuItem.EDirection)
                    {
                        case EntrustDirection.BuySpot:
                            { 
                                secuItem.TargetMktCap = secuItem.LastPrice * (secuItem.HoldingAmount + secuItem.EntrustAmount);
                            }
                            break;
                        case EntrustDirection.SellSpot:
                            {
                                secuItem.TargetMktCap = secuItem.LastPrice * (secuItem.HoldingAmount - secuItem.EntrustAmount);
                            }
                            break;
                        default:
                            break;
                    }
                }
                else if (secuItem.SecuType == SecurityType.Futures)
                {
                    secuItem.CommandMoney = FuturesContractManager.Instance.GetMoney(secuItem.SecuCode, secuItem.EntrustAmount, secuItem.LastPrice);
                    int totalAmount = 0;
                    switch (secuItem.EDirection)
                    {
                        case EntrustDirection.SellOpen:
                            {
                                totalAmount = secuItem.HoldingAmount + secuItem.EntrustAmount;
                            }
                            break;
                        case EntrustDirection.BuyClose:
                            {
                                totalAmount = secuItem.HoldingAmount - secuItem.EntrustAmount;
                            }
                            break;
                        default:
                            break;
                    }

                    secuItem.TargetMktCap = FuturesContractManager.Instance.GetMoney(secuItem.SecuCode, totalAmount, secuItem.LastPrice);
                }
                else
                {
                    string msg = string.Format("存在不支持的证券类型: {0}", secuItem.SecuCode);
                    throw new NotSupportedException(msg);
                }
            }

            var selectedItems = _instDataSource.Where(p => p.Selection).ToList();
            foreach (var instItem in selectedItems)
            {
                var futureItems = _secuDataSource.Where(p => p.InstanceId == instItem.InstanceId && p.SecuType == SecurityType.Futures).ToList();
                if (futureItems != null)
                {
                    instItem.FuturesMktCap = futureItems.Sum(p => p.HoldingAmount * p.LastPrice);
                }

                var secuItems = _secuDataSource.Where(p => p.InstanceId == instItem.InstanceId && p.SecuType == SecurityType.Stock).ToList();
                if (secuItems != null)
                {
                    instItem.StockMktCap = secuItems.Sum(p => p.HoldingAmount * p.LastPrice);
                }
            }
        }

        /// <summary>
        /// Callback when click Confirm button
        /// </summary>
        /// <param name="sender">The dialog instance.</param>
        /// <param name="data">An array of ClosePositionSecurityItem with 2 elements. The first one is new item and the second
        /// one is old item.
        /// </param>
        /// <returns></returns>
        private bool Dialog_SaveData(object sender, object data)
        {
            if (data == null || !(data is ClosePositionSecurityItem[]))
                return false;
            var itemArr = data as ClosePositionSecurityItem[];
            if (itemArr.Length != 2)
                return false;

            var newItem = itemArr[0];
            var originItem = itemArr[1];

            var addItem = _secuDataSource.ToList().Find(p => p.SecuCode.Equals(newItem.SecuCode));
            if (addItem != null)
            {
                addItem.Selection = true;
                addItem.EntrustAmount = newItem.EntrustAmount;
                addItem.EDirection = newItem.EDirection;
            }
            else
            {
                newItem.Selection = true;
                _secuDataSource.Add(newItem);
            }

            var oldItem = _secuDataSource.ToList().Find(p => p.SecuCode.Equals(originItem.SecuCode));
            if (oldItem != null)
            {
                oldItem.EntrustAmount = originItem.EntrustAmount;
                oldItem.EDirection = originItem.EDirection;

                if (!oldItem.Selection)
                {
                    oldItem.Selection = true;
                }
            }

            return true;
        }

        private void Button_Submit_Click(object sender, EventArgs e)
        {
            var cmdItems = _cmdDataSource.ToList();

            string outMsg;
            if (!ValidateEntrustSecurities(cmdItems, out outMsg))
            {
                string format = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgSubmitInvalid);
                string msg = string.Format(format, outMsg);
                MessageDialog.Warn(this, msg);
                return;
            }

            if (!GetConfirmItems(cmdItems))
            {
                return;
            }

            //TODO: show the success message and failure message
            foreach (var cmdItem in cmdItems)
            {
                var tdcmdItem = GetTradeCommandItem(cmdItem);
                var futureItems = _secuDataSource.Where(p => p.Selection && p.InstanceId == cmdItem.InstanceId && p.SecuType == SecurityType.Futures).ToList();
                if (futureItems != null && futureItems.Count > 0)
                {
                    var minFutuAmount = futureItems.Select(p => p.EntrustAmount).Min();
                    //TODO:settingweight
                    if (minFutuAmount > 0)
                    {
                        tdcmdItem.CommandNum = minFutuAmount;
                    }
                }
                else
                {
                    tdcmdItem.CommandNum = 1;
                }

                var closeItem = _instDataSource.ToList().Find(p => p.InstanceId.Equals(tdcmdItem.InstanceId));
                var selectedItems = _secuDataSource.Where(p => p.Selection && p.EntrustAmount > 0 && p.InstanceId.Equals(tdcmdItem.InstanceId)).ToList();
                var result = _tradeCommandBLL.SubmitClosePosition(tdcmdItem, closeItem, selectedItems);
                if (result > 0)
                {

                }
                else
                { 
                    //TODO:
                }
            }

        }

        private bool ValidateCopies(List<ClosePositionCmdItem> closeCmdItems)
        {
            if (this.cbCopies.Checked)
            {
                int copies = (int)nudCopies.Value;
                if (copies <= 0)
                {
                    foreach (var cmdItem in closeCmdItems)
                    {
                        if (cmdItem.Copies <= 0)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    foreach (var cmdItem in closeCmdItems)
                    {
                        cmdItem.Copies = copies;
                    }
                }
            }

            return true;
        }

        private void CalculateInstance(ClosePositionCmdItem cmdItem)
        {
            int copies = cmdItem.Copies;
            EntrustDirection direction = EntrustDirectionUtil.GetEntrustDirection(cmdItem.TradeDirection);
            
            var closeItem = _instDataSource.Single(p => p.InstanceId == cmdItem.InstanceId);
            if (closeItem == null)
            {
                return;
            }

            StockTemplate template = _templateBLL.GetTemplate(closeItem.TemplateId);
         
            if (template == null)
            {
                return;
            }

            var secuItems = _secuDataSource.Where(p => p.InstanceId == cmdItem.InstanceId).ToList();
            if (secuItems == null)
            {
                return;
            }

            //可以在指令处改变期货合约，如果做出了改变，计算之后，结果会反映到证券列表中
            var oldFutureId = closeItem.FuturesContract;
            var newFutureId = cmdItem.FuturesContract;
            if (_instanceFuturesMap.ContainsKey(cmdItem.InstanceId))
            {
                oldFutureId = _instanceFuturesMap[cmdItem.InstanceId];
            }

            _instanceFuturesMap[closeItem.InstanceId] = newFutureId;

            //根据指令设置添加或删除期货合约
            if (!oldFutureId.Equals(closeItem.FuturesContract) && !oldFutureId.Equals(newFutureId))
            {
                var oldFuturesContract = secuItems.Find(p => p.SecuCode.Equals(oldFutureId) && p.SecuType == SecurityType.Futures);
                if (oldFuturesContract != null)
                {
                    //secuItems.ToList().Remove(oldFuturesContract);
                    _secuDataSource.Remove(oldFuturesContract);
                }

                AddSecurity(newFutureId, SecurityType.Futures, closeItem);
            }
            else if (!oldFutureId.Equals(newFutureId))
            {
                AddSecurity(newFutureId, SecurityType.Futures, closeItem);
            }

            //获取模板中的股票
            var tempstockitems = _templateBLL.GetStocks(closeItem.TemplateId);

            //TODO: handle each case
            //Buy: only buy those securities in template with price
            //Sell: only buy those securities in template with available holding
            //Adjusted: both buy and sell 
            secuItems = _secuDataSource.Where(p => p.InstanceId == cmdItem.InstanceId).ToList();
            var includedTemplates = from p in secuItems
                                    where tempstockitems.Find(o => o.SecuCode.Equals(p.SecuCode)) != null
                                    select p;

            var includedList = includedTemplates.ToList();

            //original futures contract
            //var futureContract = secuItems.Find(p => p.SecuCode.Equals(closeItem.FuturesContract));
            //if (futureContract != null)
            //{
            //    includedList.Add(futureContract);
            //}

            var newFutureContract = secuItems.Find(p => p.SecuCode.Equals(newFutureId));
            if (newFutureContract != null)
            {
                includedList.Add(newFutureContract);
            }

            //TODO: How to handle the futures
            var excludedList = secuItems.Except(includedList).ToList();

            switch (direction)
            {
                case EntrustDirection.Buy:
                    {
                        includedList.ForEach(p => {
                            var tempsecuItem = tempstockitems.Find(o => o.SecuCode.Equals(p.SecuCode));

                            int weightAmount = 0;
                            if (tempsecuItem != null)
                            {
                                weightAmount = tempsecuItem.Amount;
                            }

                            if (p.SecuType == SecurityType.Stock)
                            {
                                p.EDirection = EntrustDirection.BuySpot;
                                p.EntrustAmount = weightAmount * copies;
                            }
                            else if (p.SecuType == SecurityType.Futures)
                            {
                                p.EDirection = EntrustDirection.SellOpen;
                                p.EntrustAmount = template.FutureCopies * copies;
                            }
                            else
                            {
                                //Nothing to do
                            }
                        });

                        excludedList.ForEach(p => { 
                            p.Selection = false;
                            p.EntrustAmount = 0;
                            p.EDirection = EntrustDirection.None;
                        });
                    }
                    break;
                case EntrustDirection.Sell:
                    {
                        var zeroAmountList = includedList.Where(p => p.AvailableAmount == 0).ToList();
                        var notZeroAmountList = includedList.Except(zeroAmountList).ToList();

                        zeroAmountList.ForEach(p => {
                            p.Selection = false;
                            p.EntrustAmount = 0;
                            p.EDirection = EntrustDirection.None;
                        });

                        notZeroAmountList.ForEach(p =>
                        {
                            var tempsecuItem = tempstockitems.Find(o => o.SecuCode.Equals(p.SecuCode));
                            int weightAmount = 0;
                            if (tempsecuItem != null)
                            {
                                weightAmount = tempsecuItem.Amount;
                            }

                            if (p.SecuType == SecurityType.Stock)
                            {
                                p.EDirection = EntrustDirection.SellSpot;

                                if (p.AvailableAmount >= weightAmount * copies)
                                {
                                    p.EntrustAmount = weightAmount * copies;
                                }
                                else
                                {
                                    p.EntrustAmount = p.AvailableAmount;
                                }
                            }
                            else if (p.SecuType == SecurityType.Futures)
                            {
                                p.EDirection = EntrustDirection.BuyClose;
                                if (p.AvailableAmount >= template.FutureCopies * copies)
                                {
                                    p.EntrustAmount = template.FutureCopies * copies;
                                }
                                else
                                {
                                    p.EntrustAmount = p.AvailableAmount;
                                }
                            }
                            else
                            {
                                //Nothing to do
                            }
                        });

                        excludedList.ForEach(p =>
                        {
                            p.Selection = false;
                            p.EntrustAmount = 0;
                            p.EDirection = EntrustDirection.None;
                        });
                    }
                    break;
                case EntrustDirection.AdjustedToBuySell:
                    {
                        includedList.ForEach(p => {
                            var tempsecuItem = tempstockitems.Find(o => o.SecuCode.Equals(p.SecuCode));
                            int weightAmount = 0;
                            if (tempsecuItem != null)
                            {
                                weightAmount = tempsecuItem.Amount;
                            }

                            if (p.SecuType == SecurityType.Stock)
                            {
                                int rest = p.HoldingAmount - weightAmount * copies;
                                if (rest > 0)
                                {
                                    p.EDirection = EntrustDirection.SellSpot;
                                    p.EntrustAmount = rest;
                                }
                                else
                                {
                                    p.EDirection = EntrustDirection.BuySpot;
                                    p.EntrustAmount = 0 - rest;
                                }
                            }
                            else if (p.SecuType == SecurityType.Futures)
                            {
                                int rest = p.HoldingAmount - template.FutureCopies * copies;
                                if (rest > 0)
                                {
                                    p.EDirection = EntrustDirection.BuyClose;
                                    p.EntrustAmount = rest;
                                }
                                else
                                {
                                    p.EDirection = EntrustDirection.SellOpen;
                                    p.EntrustAmount = 0 - rest;
                                }
                            }
                            else
                            { 
                                
                            }

                        });

                        excludedList.ForEach(p =>
                        {
                            if (p.AvailableAmount > 0)
                            {
                                p.Selection = true;
                                p.EntrustAmount = p.AvailableAmount;
                                if (p.SecuType == SecurityType.Stock)
                                {
                                    p.EDirection = EntrustDirection.SellSpot;
                                }
                                else if (p.SecuType == SecurityType.Futures)
                                {
                                    p.EDirection = EntrustDirection.BuyClose;
                                }
                            }
                            else
                            {
                                p.Selection = false;
                                p.EntrustAmount = 0;
                                p.EDirection = EntrustDirection.None;
                            }
                        });
                    }
                    break;
            }
        }

        private void CloseAll(ClosePositionCmdItem cmdItem)
        {
            EntrustDirection direction = EntrustDirectionUtil.GetEntrustDirection(cmdItem.TradeDirection);
            var instance = _instDataSource.Single(p => p.InstanceId == cmdItem.InstanceId);
            if (instance == null)
            {
                return;
            }

            var secuItems = _secuDataSource.Where(p => p.InstanceId == cmdItem.InstanceId);
            if (secuItems == null)
            {
                return;
            }

            //设置委托数量和委托方向
            foreach (var secuItem in secuItems)
            {
                secuItem.EntrustAmount = secuItem.AvailableAmount;
                switch (secuItem.SecuType)
                {
                    case SecurityType.Stock:
                        {
                            secuItem.EDirection = EntrustDirection.SellSpot;
                        }
                        break;
                    case SecurityType.Futures:
                        {
                            secuItem.EDirection = EntrustDirection.BuyClose;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private bool ValidateEntrustSecurities(List<ClosePositionCmdItem> closeCmdItems, out string msg)
        {
            msg = string.Empty;
            List<ClosePositionCmdItem> invalidItems = new List<ClosePositionCmdItem>();

            foreach (var cmdItem in closeCmdItems)
            {
                var selectedItems = _secuDataSource.Where(p => p.Selection && p.InstanceId == cmdItem.InstanceId).ToList();
                if (selectedItems.Count > 0)
                {
                    //选中的委托数量不能为0
                    selectedItems = selectedItems.Where(p => p.EntrustAmount > 0).ToList();
                    if (selectedItems.Count == 0)
                    {
                        invalidItems.Add(cmdItem);
                    }
                }
                else
                {
                    invalidItems.Add(cmdItem);
                }
            }

            if (invalidItems.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                invalidItems.ForEach(p => {
                    sb.Append("|");
                    sb.Append(p.InstanceCode);
                });
                sb.Append("|");

                msg = sb.ToString();

                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region get confirm submitted items

        private bool GetConfirmItems(List<ClosePositionCmdItem> cmdItems)
        {
            List<ClosePositionInstanceItem> instItems = new List<ClosePositionInstanceItem>();
            foreach (var cmdItem in cmdItems)
            {
                ClosePositionInstanceItem instItem = new ClosePositionInstanceItem 
                {
                    InstanceCode = cmdItem.InstanceCode,
                    Copies = cmdItem.Copies,
                    FuturesList = new List<string>(),
                };

                var closeItem = _instDataSource.ToList().Find(p => p.InstanceId == cmdItem.InstanceId);
                if (closeItem != null)
                {
                    instItem.TemplateId = closeItem.TemplateId;
                    instItem.TemplateName = closeItem.TemplateName;
                    instItem.PortfolioCode = closeItem.PortfolioCode;
                    instItem.PortfolioName = closeItem.PortfolioName;
                }

                var instSecuItems = _secuDataSource.Where(p => p.Selection && p.InstanceId == cmdItem.InstanceId)
                    .ToList();

                var futures = instSecuItems.Where(p => p.SecuType == SecurityType.Futures)
                    .ToList();
                foreach (var future in futures)
                {
                    if (!instItem.FuturesList.Contains(future.SecuCode))
                    {
                        instItem.FuturesList.Add(future.SecuCode);
                    }
                }

                var spotBuyMktVal = instSecuItems.Where(p => p.SecuType == SecurityType.Stock
                    && p.EDirection == EntrustDirection.BuySpot)
                    .Sum(o => o.CommandMoney);
                instItem.SpotBuyMktVal = spotBuyMktVal;

                var spotSellMktVal = instSecuItems.Where(p => p.SecuType == SecurityType.Stock
                    && p.EDirection == EntrustDirection.SellSpot)
                    .Sum(o => o.CommandMoney);
                instItem.SpotSellMktVal = spotSellMktVal;

                var futuBuyMktVal = instSecuItems.Where(p => p.SecuType == SecurityType.Futures
                    && p.EDirection == EntrustDirection.BuyClose)
                    .Sum(o => o.CommandMoney);
                instItem.FutuBuyMktVal = futuBuyMktVal;

                var futuSellMktVal = instSecuItems.Where(p => p.SecuType == SecurityType.Futures
                    && p.EDirection == EntrustDirection.SellOpen)
                    .Sum(o => o.CommandMoney);
                instItem.FutuSellMktVal = futuSellMktVal;

                instItems.Add(instItem);
            }

            bool ret = false;
            ClosePositionDialog dialog = new ClosePositionDialog(_gridConfig);
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.OnLoadControl(dialog, null);
            dialog.OnLoadData(dialog, instItems);
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dialog.Dispose();
                ret = true;
            }
            else
            {
                dialog.Dispose();
            }

            return ret;
        }

        #endregion

        #region add security

        private void AddSecurity(TradingInstanceSecurity secuItem, ClosePositionItem closeItem)
        {
            ClosePositionSecurityItem closeSecuItem = new ClosePositionSecurityItem
            {
                Selection = true,
                InstanceId = secuItem.InstanceId,
                SecuCode = secuItem.SecuCode,
                SecuType = secuItem.SecuType,
                PositionType = secuItem.PositionType,

                HoldingAmount = secuItem.PositionAmount,
                AvailableAmount = secuItem.PositionAmount - secuItem.BuyToday,

                PortfolioId = closeItem.PortfolioId,
                PortfolioName = closeItem.PortfolioName,
            };

            var secuInfo = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
            if (secuInfo != null)
            {
                closeSecuItem.SecuName = secuInfo.SecuName;
                closeSecuItem.ExchangeCode = secuInfo.ExchangeCode;
            }

            _secuDataSource.Add(closeSecuItem);
        }

        private void AddSecurity(string secuCode, SecurityType secuType, ClosePositionItem closeItem)
        {
            ClosePositionSecurityItem closeSecuItem = new ClosePositionSecurityItem
            {
                Selection = true,
                InstanceId = closeItem.InstanceId,
                SecuCode = secuCode,
                SecuType = secuType,
                HoldingAmount = 0,
                AvailableAmount = 0,
                PortfolioId = closeItem.PortfolioId,
                PortfolioName = closeItem.PortfolioName,
            };

            if (secuType == SecurityType.Stock)
            {
                closeSecuItem.PositionType = PositionType.SpotLong;
            }
            else if (secuType == SecurityType.Futures)
            {
                closeSecuItem.PositionType = PositionType.FuturesShort;
            }
            else
            {
                //do nothing
            }

            var secuInfo = SecurityInfoManager.Instance.Get(secuCode, secuType);
            if (secuInfo != null)
            {
                closeSecuItem.SecuName = secuInfo.SecuName;
                closeSecuItem.ExchangeCode = secuInfo.ExchangeCode;
            }

            _secuDataSource.Add(closeSecuItem);
        }

        private void AddSecurity(TemplateStock stock, ClosePositionItem closeItem)
        {
            ClosePositionSecurityItem closeSecuItem = new ClosePositionSecurityItem
            {
                Selection = true,
                InstanceId = closeItem.InstanceId,
                SecuCode = stock.SecuCode,
                SecuType = SecurityType.Stock,
                HoldingAmount = 0,
                AvailableAmount = 0,
                PortfolioId = closeItem.PortfolioId,
                PortfolioName = closeItem.PortfolioName,
            };

            var secuInfo = SecurityInfoManager.Instance.Get(closeSecuItem.SecuCode, closeSecuItem.SecuType);
            if (secuInfo != null)
            {
                closeSecuItem.SecuName = secuInfo.SecuName;
                closeSecuItem.SecuType = secuInfo.SecuType;
                closeSecuItem.ExchangeCode = secuInfo.ExchangeCode;
            }

            if (closeSecuItem.SecuType == SecurityType.Stock)
            {
                closeSecuItem.PositionType = PositionType.SpotLong;
            }
            else if (closeSecuItem.SecuType == SecurityType.Futures)
            {
                closeSecuItem.PositionType = PositionType.FuturesShort;
            }
            else
            {
                //do nothing
            }

            _secuDataSource.Add(closeSecuItem);
        }

        #endregion

        #region

        private TradeCommand GetTradeCommandItem(ClosePositionCmdItem closeCmdItem)
        {
            TradeCommand tdcmdItem = new TradeCommand
            {
                InstanceId = closeCmdItem.InstanceId,
                ECommandType = CommandType.Arbitrage,
                //CommandNum = closeCmdItem.Copies,
                //EStockDirection = Model.Data.EntrustDirection.BuySpot,
                //EFuturesDirection = Model.Data.EntrustDirection.SellOpen,
                EEntrustStatus = EntrustStatus.NoExecuted,
                EDealStatus = DealStatus.NoDeal,
                ModifiedTimes = 1
            };

            EntrustDirection direction = EntrustDirectionUtil.GetEntrustDirection(closeCmdItem.TradeDirection);
            switch (direction)
            {
                case EntrustDirection.Buy:
                    {
                        tdcmdItem.EExecuteType = ExecuteType.OpenPosition;
                        tdcmdItem.EStockDirection = EntrustDirection.BuySpot;
                        tdcmdItem.EFuturesDirection = EntrustDirection.SellOpen;
                    }
                    break;
                case EntrustDirection.Sell:
                    {
                        tdcmdItem.EExecuteType = ExecuteType.ClosePosition;
                        tdcmdItem.EStockDirection = EntrustDirection.SellSpot;
                        tdcmdItem.EFuturesDirection = EntrustDirection.BuyClose;
                    }
                    break;
                case EntrustDirection.AdjustedToBuySell:
                    {
                        tdcmdItem.EExecuteType = ExecuteType.AdjustPosition;
                        tdcmdItem.EStockDirection = EntrustDirection.BuySpot;
                        tdcmdItem.EFuturesDirection = EntrustDirection.SellOpen;
                    }
                    break;
                default:
                    break;
            }

            if (_dialogType == CloseDialogType.CloseAll)
            {
                tdcmdItem.EExecuteType = ExecuteType.ClosePosition;
            }
            else if (_dialogType == CloseDialogType.ChangePosition)
            {
                tdcmdItem.EExecuteType = ExecuteType.AdjustPosition;
            }

            return tdcmdItem;
        }

        #endregion
    }
}
