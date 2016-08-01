using Config;
using Controls.Entity;
using Controls.GridView;
using DBAccess;
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
using BLL.TradeCommand;
using Model.config;
using Model.EnumType;
using Model.Binding.BindingUtil;
using Quote;
using TradingSystem.TradeUtil;
using BLL.Entrust;
using System.Diagnostics;
using System.Text;

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

        private GridConfig _gridConfig;
        private FuturesContractDAO _fcdbdao = new FuturesContractDAO();

        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private TemplateBLL _templateBLL = new TemplateBLL();
        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();
        private TradeInstanceSecurityBLL _tradeInstanceSecuBLL = new TradeInstanceSecurityBLL();

        private SortableBindingList<ClosePositionItem> _instDataSource = new SortableBindingList<ClosePositionItem>(new List<ClosePositionItem>());
        private SortableBindingList<ClosePositionSecurityItem> _secuDataSource = new SortableBindingList<ClosePositionSecurityItem>(new List<ClosePositionSecurityItem>());
        private SortableBindingList<ClosePositionCmdItem> _cmdDataSource = new SortableBindingList<ClosePositionCmdItem>(new List<ClosePositionCmdItem>());

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
            this.btnCalc.Click += new EventHandler(Button_Calc_Click);
            this.btnCloseAll.Click += new EventHandler(Button_CloseAll_Click);
            this.btnChgPosition.Click += new EventHandler(Button_ChgPosition_Click);
            this.btnSubmit.Click += new EventHandler(Button_Submit_Click);
        }

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
                        LoadSecurity(closeItem);
                        LoadCloseCommand(closeItem);
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
            var secuItems = _tradeInstanceSecuBLL.Get(closeItem.InstanceId);
            var tempstockitems = _templateBLL.GetTemplate(closeItem.TemplateId);
            if (secuItems == null || secuItems.Count == 0)
            {
                return;
            }

            foreach (var secuItem in secuItems)
            {
                AddSecurity(secuItem, closeItem);
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
                TradeDirection = ((int)EntrustDirection.Buy).ToString(),
            };

            if (_secuDataSource != null && _secuDataSource.Count > 0)
            {
                var futuresItem = _secuDataSource.ToList()
                                    .Find(p => p.InstanceId == closeItem.InstanceId && p.SecuType == Model.SecurityInfo.SecurityType.Futures);
                if (futuresItem != null)
                {
                    cmdItem.FuturesContract = futuresItem.SecuCode;
                    _cmdDataSource.Add(cmdItem);
                }
            }
        }

        #endregion

        #region load control
        private bool Form_LoadControl(object sender, object data)
        {
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
            var futures = _fcdbdao.Get("");
            if (futures != null && futures.Count > 0)
            {
                ComboOption futuresOption = new ComboOption
                {
                    Items = new List<ComboOptionItem>(),
                };

                foreach (var future in futures)
                {
                    ComboOptionItem option = new ComboOptionItem
                    {
                        Id = future.Code,
                        Name = future.Code
                    };

                    futuresOption.Items.Add(option);
                }

                TSDataGridViewHelper.SetDataBinding(this.cmdGridView, "futurescontract", futuresOption);
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
                };

                _instDataSource.Add(closeItem);
            }

            LoadHolding();

            return true;
        }

        private void LoadHolding()
        {
            //UFXQueryHoldingBLL holdingBLL = new UFXQueryHoldingBLL();
            UFXQueryMultipleHoldingBLL holdingBLL = new UFXQueryMultipleHoldingBLL();
            holdingBLL.Query();

            Debug.WriteLine("Only test for the holding!");
        }
        #endregion

        #region Button Click Event

        private void Button_Calc_Click(object sender, EventArgs e)
        {
            //_execType = ExecuteType.ClosePosition;
            //var cmdItems = _cmdDataSource.Where(p => p.Selection).ToList();
            var cmdItems = _cmdDataSource.ToList();
            if (!ValidateCopies(cmdItems))
            {
                MessageBox.Show(this, "请输入有效的操作份数！", "错误", MessageBoxButtons.OK);
                return;
            }

            foreach (var cmdItem in cmdItems)
            {
                CalculateInstance(cmdItem);
            }

            //询价
            QueryQuote();

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
                MessageBox.Show(this, "委托数量不为0，不能换仓！", "错误", MessageBoxButtons.OK);
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

            QuoteCenter.Instance.Query(secuList);
            foreach (var secuItem in _secuDataSource)
            {
                var targetItem = secuList.Find(p => p.SecuCode.Equals(secuItem.SecuCode) && (p.SecuType == SecurityType.Stock || p.SecuType == SecurityType.Futures));
                var marketData = QuoteCenter.Instance.GetMarketData(targetItem);
                secuItem.LastPrice = marketData.CurrentPrice;
                secuItem.CommandMoney = secuItem.LastPrice * secuItem.EntrustAmount;
                //secuItem.ESuspendFlag = marketData.SuspendFlag;
                secuItem.ELimitUpDownFlag = QuotePriceHelper.GetLimitUpDownFlag(marketData.CurrentPrice, marketData.LowLimitPrice, marketData.HighLimitPrice);
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
                string msg = string.Format("证券未勾选或勾选证券均未设置委托数量, 交易实例为: {0}", outMsg);
                MessageBox.Show(this, msg, "警告", MessageBoxButtons.OK);
                return;
            }

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
                                    p.EDirection = EntrustDirection.SellSpot;
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

            //TODO: handle those 
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
                    selectedItems = selectedItems.Where(p => p.EntrustAmount == 0).ToList();
                    if (selectedItems.Count > 0)
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
                closeSecuItem.PositionType = PositionType.StockLong;
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


        #endregion

        #region

        private TradingCommandItem GetTradeCommandItem(ClosePositionCmdItem closeCmdItem)
        {
            TradingCommandItem tdcmdItem = new TradingCommandItem
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
