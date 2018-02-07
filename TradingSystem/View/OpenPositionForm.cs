using Config;
using Controls.Entity;
using Controls.GridView;
using Model.SecurityInfo;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Quote;
using TradingSystem.Dialog;
using Model.EnumType;
using Model.Binding.BindingUtil;
using Model.Database;
using BLL.Template;
using Model.Constant;
using Model.Dialog;
using BLL.TradeInstance;
using BLL.TradeCommand;
using BLL.FuturesContractManager;
using BLL.Manager;
using Util;
using System.Text;
using Model.Quote;
using Forms;

namespace TradingSystem.View
{
    public partial class OpenPositionForm : Forms.DefaultForm
    {
        private const string msgSubmitFail = "opensubmitfail";

        private const string MonitorGridId = "openposition";
        private const string SecurityGridId = "openpositionsecurity";
        private const string BottomMenuId = "openposition";

        private const string msgCommandSuccess = "commandsuccess";
        private const string msgCommandFail = "commandfail";
        private const string msgCommandSuccessFail = "commandsuccessfail";

        private GridConfig _gridConfig;
        
        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();
        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private TemplateBLL _templateBLL = new TemplateBLL();
        private MonitorUnitBLL _monitorUnitBLL = new MonitorUnitBLL();
        //private FuturesContractBLL _futuresContractBLL = new FuturesContractBLL();

        private SortableBindingList<OpenPositionItem> _monitorDataSource = new SortableBindingList<OpenPositionItem>();
        private SortableBindingList<OpenPositionSecurityItem> _securityDataSource = new SortableBindingList<OpenPositionSecurityItem>();

        private WaitDialogWnd waitDialog = new WaitDialogWnd();

        public OpenPositionForm()
            :base()
        {
            InitializeComponent();
        }

        public OpenPositionForm(GridConfig gridConfig)
            :this()
        {
            _gridConfig = gridConfig;

            LoadControl += new FormLoadHandler(Form_LoadControl);
            LoadData += new FormLoadHandler(Form_LoadData);
            monitorGridView.UpdateRelatedDataGridHandler += new UpdateRelatedDataGrid(MonitorGridView_UpdateRelatedDataGrid);
            monitorGridView.NumericUpDownValueChanged += new NumericUpDownValueChanged(MonitorGridView_NumericUpDownValueChanged);

            securityGridView.MouseClick += new MouseEventHandler(SecurityGridView_MouseClick);

            btnBottomContainer.ButtonClick += new EventHandler(ButtonContainer_ButtonClick);

            secuContextMenu.ItemClicked += new ToolStripItemClickedEventHandler(SecurityContextMenu_ItemClicked);
        }

        #region monitor gridview

        private void MonitorGridView_NumericUpDownValueChanged(int newValue, int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= _monitorDataSource.Count)
                return;
            //该事件触发时，绑定数据还没有被同步
            OpenPositionItem monitorItem = _monitorDataSource[rowIndex];

            CalcEntrustAmount(monitorItem, newValue);

            securityGridView.Invalidate();
        }

        private void MonitorGridView_UpdateRelatedDataGrid(UpdateDirection direction, int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= _monitorDataSource.Count)
                return;

            OpenPositionItem monitorItem = _monitorDataSource[rowIndex];

            switch (direction)
            {
                case UpdateDirection.Select:
                    {
                        LoadSecurityData(monitorItem);
                    }
                    break;
                case UpdateDirection.UnSelect:
                    {
                        RemoveSecurityData(monitorItem);
                    }
                    break;
                case UpdateDirection.Increase:
                    {
                        CalcEntrustAmount(monitorItem);

                        //monitorGridView.Invalidate();
                        securityGridView.Invalidate();
                    }
                    break;
                case UpdateDirection.Decrease:
                    {
                        CalcEntrustAmount(monitorItem);
                        //monitorGridView.Invalidate();
                        securityGridView.Invalidate();
                    }
                    break;
            }
        }

        #endregion

        #region security gridview

        //right-click popup menu
        private void SecurityGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.secuContextMenu.Show(this.securityGridView, e.Location);
            }
        }

        //click the popup menu item
        private void SecurityContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "selectAllToolStripMenuItem":
                    {
                        _securityDataSource.ToList().ForEach(p => p.Selection = true);
                    }
                    break;
                case "unSelectToolStripMenuItem":
                    {
                        _securityDataSource.ToList().ForEach(p => p.Selection = false);
                    }
                    break;
                case "cancelSelectToolStripMenuItem":
                    {
                        _securityDataSource.Where(p => p.Selection).ToList().ForEach(p => p.Selection = false);
                    }
                    break;
                case "cancelStopToolStripMenuItem":
                    {
                        _securityDataSource.Where(p => p.ESuspendFlag != SuspendFlag.NoSuspension).ToList().ForEach(p => p.Selection = false);
                    }
                    break;
                case "cancelLimitUpToolStripMenuItem":
                    {
                        _securityDataSource.Where(p => p.ELimitUpDownFlag == LimitUpDownFlag.LimitUp).ToList().ForEach(p => p.Selection = false);
                    }
                    break;
                case "cancelLimitDownToolStripMenuItem":
                    {
                        _securityDataSource.Where(p => p.ELimitUpDownFlag == LimitUpDownFlag.LimitDown).ToList().ForEach(p => p.Selection = false);
                    }
                    break;
                default:
                    break;
            }

            this.securityGridView.Invalidate();
        }

        #endregion

        #region load control
        private bool Form_LoadControl(object sender, object data)
        {
            //set the monitorGridView
            TSDataGridViewHelper.AddColumns(this.monitorGridView, _gridConfig.GetGid(MonitorGridId));
            Dictionary<string, string> monitorColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(OpenPositionItem));
            TSDataGridViewHelper.SetDataBinding(this.monitorGridView, monitorColDataMap);           

            //set the securityGridView
            TSDataGridViewHelper.AddColumns(this.securityGridView, _gridConfig.GetGid(SecurityGridId));
            Dictionary<string, string> securityColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(OpenPositionSecurityItem));
            TSDataGridViewHelper.SetDataBinding(this.securityGridView, securityColDataMap);

            //Load bottom button
            LoadBottomButton();

            this.monitorGridView.DataSource = _monitorDataSource;
            this.securityGridView.DataSource = _securityDataSource;

            return true;
        }

        private void LoadBottomButton()
        {
            ButtonGroup bottomButtonGroup = ConfigManager.Instance.GetButtonConfig().GetButtonGroup(BottomMenuId);
            btnBottomContainer.AddButtonGroup(bottomButtonGroup);
        }
        #endregion
        private bool Form_LoadData(object sender, object data)
        {
            _monitorDataSource.Clear();
            _securityDataSource.Clear();

            //Load the data of open posoition
            List<OpenPositionItem> monitorList = _monitorUnitBLL.GetOpenItems();
            monitorList.ForEach(p => _monitorDataSource.Add(p));

            //Load the data for each template
            if (monitorList.Count > 0)
            {
                var selectedItems = _monitorDataSource.Where(p => p.Selection).ToList();
                if (selectedItems.Count > 0)
                {
                    LoadSecurityData(selectedItems);
                }
            }

            return true;
        }

        private void LoadSecurityData(List<OpenPositionItem> monitorItems)
        {
            foreach (var mitem in monitorItems)
            {
                LoadSecurityData(mitem);
            }
        }

        public void LoadSecurityData(OpenPositionItem monitorItem)
        {
            int bmkCopies = 1;
            var templateItem = _templateBLL.GetTemplate(monitorItem.TemplateId);
            if (templateItem != null)
            {
                bmkCopies = templateItem.FutureCopies;
                //Load the future
                OpenPositionSecurityItem futureItem = new OpenPositionSecurityItem
                {
                    Selection = true,
                    MonitorId = monitorItem.MonitorId,
                    MonitorName = monitorItem.MonitorName,
                    SecuCode = monitorItem.FuturesContract,
                    SecuName = monitorItem.FuturesContract,
                    WeightAmount = templateItem.FutureCopies,
                    EntrustAmount = monitorItem.Copies * templateItem.FutureCopies,
                    EDirection = EntrustDirection.SellOpen,
                    SecuType = SecurityType.Futures
                };

                _securityDataSource.Add(futureItem);
            }

            List<TemplateStock> stocks = _templateBLL.GetStocks(monitorItem.TemplateId);
            List<OpenPositionSecurityItem> secuItems = new List<OpenPositionSecurityItem>();

            int actualCopies = bmkCopies * monitorItem.Copies;
            foreach (var stock in stocks)
            {
                OpenPositionSecurityItem secuItem = new OpenPositionSecurityItem
                {
                    Selection = true,
                    MonitorId = monitorItem.MonitorId,
                    MonitorName = monitorItem.MonitorName,
                    SecuCode = stock.SecuCode,
                    SecuName = stock.SecuName,
                    WeightAmount = stock.Amount,
                    EntrustAmount = actualCopies * stock.Amount,
                    EDirection = EntrustDirection.BuySpot,
                    SecuType = SecurityType.Stock
                };

                _securityDataSource.Add(secuItem);
            }
        }

        private void CalcEntrustAmount(OpenPositionItem monitorItem, int opCopies)
        {
            //获得模板中设置的基准份数
            var templateItem = _templateBLL.GetTemplate(monitorItem.TemplateId);
            int bmkCopies = 0;
            if (templateItem != null)
            {
                bmkCopies = templateItem.FutureCopies;
            }
            else
            {
                bmkCopies = 1;
            }

            //真正的份数有模板设置份数和操作份数相乘得到
            int actualCopies = bmkCopies * opCopies;

            var secuItems = _securityDataSource.Where(p => p.MonitorId == monitorItem.MonitorId);
            foreach (var secuItem in secuItems)
            {
                if (secuItem.SecuType == SecurityType.Stock)
                {
                    secuItem.EntrustAmount = actualCopies * secuItem.WeightAmount;
                }
                else if(secuItem.SecuType == SecurityType.Futures)
                {
                    secuItem.EntrustAmount = opCopies * secuItem.WeightAmount;
                }
            }
        }

        private void CalcEntrustAmount(OpenPositionItem monitorItem)
        {
            CalcEntrustAmount(monitorItem, monitorItem.Copies);
        }

        public void RemoveSecurityData(OpenPositionItem monitorItem)
        {
            for (int i = _securityDataSource.Count - 1; i >= 0; i--)
            {
                var secuItem = _securityDataSource[i];
                if (secuItem.MonitorId == monitorItem.MonitorId)
                {
                    _securityDataSource.RemoveAt(i);
                }
            }
        }

        #region button click in ButtonContainer

        private void ButtonContainer_ButtonClick(object sender, EventArgs e)
        {
            if (!(sender is Button))
            {
                return;
            }

            Button button = sender as Button;
            switch (button.Name)
            {
                case "Refresh":
                    {
                        try
                        {
                            waitDialog.Show(this);
                            QueryQuote();
                        }
                        catch (Exception ex)
                        {
                            MessageDialog.Error(this, ex.Message);
                        }
                        finally
                        {
                            waitDialog.Close();
                        }
                        this.monitorGridView.Invalidate();
                        this.securityGridView.Invalidate();    
                    }
                    break;
                case "GiveOrder":
                    {
                        GiveOrder();
                    }
                    break;
                default:
                    break;
            }
        }

        private void QueryQuote()
        {
            var uniqueSecuItems = _securityDataSource.GroupBy(p => p.SecuCode).Select(p => p.First());
            List<SecurityItem> secuList = new List<SecurityItem>();
            foreach (var secuItem in uniqueSecuItems)
            {
                var findItem = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                if (findItem != null)
                {
                    secuList.Add(findItem);
                }
            }

            //Add the index
            foreach (var openItem in _monitorDataSource)
            {
                var findItem = SecurityInfoManager.Instance.Get(openItem.BenchmarkId, SecurityType.Index);
                if (findItem != null)
                {
                    secuList.Add(findItem);
                }
            }

            //QuoteCenter.Instance.Query(secuList);
            foreach (var secuItem in _securityDataSource)
            {
                var targetItem = secuList.Find(p => p.SecuCode.Equals(secuItem.SecuCode) && (p.SecuType == SecurityType.Stock || p.SecuType == SecurityType.Futures));
                var marketData = QuoteCenter.Instance.GetMarketData(targetItem);
                secuItem.LastPrice = marketData.CurrentPrice;
                secuItem.BuyAmount = marketData.BuyAmount;
                secuItem.SellAmount = marketData.SellAmount;
                secuItem.ESuspendFlag = marketData.SuspendFlag;
                secuItem.ELimitUpDownFlag = marketData.LimitUpDownFlag; 
                
                if (secuItem.SecuType == SecurityType.Stock)
                {
                    secuItem.CommandMoney = secuItem.LastPrice * secuItem.EntrustAmount;
                }
                else if (secuItem.SecuType == SecurityType.Futures)
                {
                    secuItem.CommandMoney = FuturesContractManager.Instance.GetMoney(secuItem.SecuCode, secuItem.EntrustAmount, secuItem.LastPrice);
                }
                else
                {
                    string msg = string.Format("存在不支持的证券类型: {0}", secuItem.SecuCode);
                    throw new NotSupportedException(msg);
                }
            }

            var selectedOpenItems = _monitorDataSource.Where(p => p.Selection).ToList();
            foreach (var openItem in selectedOpenItems)
            {
                //calc the basis
                double benchmarkPrice = GetPrice(secuList, openItem.BenchmarkId, SecurityType.Index);
                double futurePrice = GetPrice(secuList, openItem.FuturesContract, SecurityType.Futures);
                openItem.Basis = futurePrice - benchmarkPrice;

                //future total capital
                var futureItem = _securityDataSource.ToList().Find(p => p.MonitorId == openItem.MonitorId && p.SecuType == SecurityType.Futures);
                if(futureItem != null)
                {
                    openItem.FuturesMktCap = futureItem.CommandMoney;
                }

                //spot total capital
                var stockItems = _securityDataSource.Where(p => p.MonitorId == openItem.MonitorId && p.SecuType == SecurityType.Stock).ToList();
                if (stockItems.Count > 0)
                {
                    openItem.StockMktCap = stockItems.Sum(p => p.CommandMoney);
                    openItem.StockNumbers = stockItems.Count;
                }

                openItem.Risk = openItem.StockMktCap - openItem.FuturesMktCap;

                var suspensionItems = stockItems.Where(p => p.ESuspendFlag == Model.Quote.SuspendFlag.Suspend1Day
                    || p.ESuspendFlag == Model.Quote.SuspendFlag.Suspend1Hour
                    || p.ESuspendFlag == Model.Quote.SuspendFlag.Suspend2Hour
                    || p.ESuspendFlag == Model.Quote.SuspendFlag.SuspendAfternoon
                    || p.ESuspendFlag == Model.Quote.SuspendFlag.SuspendHalfDay
                    || p.ESuspendFlag == Model.Quote.SuspendFlag.SuspendHalfHour
                    || p.ESuspendFlag == Model.Quote.SuspendFlag.SuspendTemp
                    ).ToList();
                if (suspensionItems != null)
                {
                    openItem.SuspensionNumbers = suspensionItems.Count;
                }

                var limitUpItems = stockItems.Where(p => p.ELimitUpDownFlag == Model.Quote.LimitUpDownFlag.LimitUp).ToList();
                if (limitUpItems != null)
                {
                    openItem.LimitUpNumbers = limitUpItems.Count;
                }

                var limitDownItems = stockItems.Where(p => p.ELimitUpDownFlag == Model.Quote.LimitUpDownFlag.LimitDown).ToList();
                if (limitDownItems != null)
                {
                    openItem.LimitDownNumbers = limitDownItems.Count;
                }
            }
        }

        private double GetPrice(List<SecurityItem> secuList, string secuCode, SecurityType secuType)
        {
            double price = 0.0;
            var targetItem = secuList.Find(p => p.SecuCode.Equals(secuCode) && p.SecuType == secuType);
            if (targetItem != null)
            {
                var marketData = QuoteCenter.Instance.GetMarketData(targetItem);
                price = marketData.CurrentPrice;
            }

            return price;
        }

        private void GiveOrder()
        {
            List<OrderConfirmItem> orderItemList = new List<OrderConfirmItem>();
            var selectedItems = _monitorDataSource.Where(p => p.Selection).ToList();
            if (selectedItems.Count == 1)
            {
                var openItem = selectedItems[0];
                var orderItem = GetSubmitItem(openItem);
                if (orderItem == null)
                {
                    string format = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgSubmitFail);
                    string msg = string.Format(format, openItem.MonitorName);
                    MessageDialog.Fail(this, msg);
                    return;
                }
                else
                {
                    orderItemList.Add(orderItem);
                }
            }
            else if(selectedItems.Count > 1)
            {
                DateTime startDate = DateUtil.OpenDate;
                DateTime endDate = DateUtil.CloseDate;
                var orderItems = new List<OrderConfirmItem>();
                foreach (var openItem in selectedItems)
                {
                    OrderConfirmItem orderItem = new OrderConfirmItem 
                    {
                        MonitorId = openItem.MonitorId,
                        MonitorName = openItem.MonitorName,
                        PortfolioId = openItem.PortfolioId,
                        PortfolioName = openItem.PortfolioName,
                        PortfolioCode = openItem.PortfolioCode,
                        TemplateId = openItem.TemplateId,
                        TemplateName = openItem.TemplateName,
                        InstanceCode = string.Format("{0}-{1}-{2}", openItem.PortfolioId, openItem.TemplateId, DateFormat.Format(DateTime.Now, ConstVariable.DateFormat1)),
                        Copies = openItem.Copies,
                        FuturesList = new List<string>() { openItem.FuturesContract },
                        StartDate = DateUtil.GetIntDate(startDate),
                        EndDate = DateUtil.GetIntDate(endDate),
                        StartTime = DateUtil.GetIntTime(startDate),
                        EndTime = DateUtil.GetIntTime(endDate),
                    };

                    orderItems.Add(orderItem);
                }

                OpenMultiPositionDialog dialog = new OpenMultiPositionDialog(_gridConfig);
                dialog.Owner = this;
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.OnLoadControl(dialog, null);
                dialog.OnLoadData(dialog, orderItems);
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    orderItemList = (List<OrderConfirmItem>)dialog.GetData();
                    dialog.Dispose();
                }
                else
                {
                    dialog.Dispose();
                }
            }

            if (orderItemList.Count == 0)
            {
                return;
            }

            List<OrderConfirmItem> successItems = new List<OrderConfirmItem>();
            List<OrderConfirmItem> failItems = new List<OrderConfirmItem>();
            foreach (var orderItem in orderItemList)
            {
                var newOpenItem = GetOpenPositionItem(orderItem);
                var selectedSecuItems = _securityDataSource.Where(p => p.Selection && p.MonitorId == newOpenItem.MonitorId).ToList();
                DateTime startDate = DateUtil.GetDateTimeFromInt(orderItem.StartDate, orderItem.StartTime);
                DateTime endDate = DateUtil.GetDateTimeFromInt(orderItem.EndDate, orderItem.EndTime);
                startDate = DateUtil.GetStartDate(startDate);
                endDate = DateUtil.GetEndDate(endDate, startDate);

                int ret = _tradeCommandBLL.SubmitOpenPosition(newOpenItem, selectedSecuItems, startDate, endDate);

                if (ret > 0)
                {
                    successItems.Add(orderItem);
                }
                else
                {
                    failItems.Add(orderItem);
                }
            }

            if (successItems.Count == orderItemList.Count)
            {
                MessageDialog.Info(this, msgCommandSuccess);
            }
            else if (failItems.Count == orderItemList.Count)
            {
                MessageDialog.Fail(this, msgCommandFail);
            }
            else
            {
                StringBuilder sbSuccess = new StringBuilder();
                StringBuilder sbFail = new StringBuilder();
                successItems.ForEach(p => sbSuccess.AppendFormat("{0}|", p.MonitorId));

                failItems.ForEach(p => sbFail.AppendFormat("{0}|", p.MonitorId));

                string format = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgCommandSuccessFail);
                string msg = string.Format(format, sbSuccess.ToString().TrimEnd('|'), sbFail.ToString().TrimEnd('|'));

                MessageDialog.Warn(this, msg);
            }
        }

        public OrderConfirmItem GetSubmitItem(OpenPositionItem openItem)
        {
            string instanceCode = string.Format("{0}-{1}-{2}", openItem.PortfolioId, openItem.TemplateId, DateFormat.Format(DateTime.Now, ConstVariable.DateFormat1));
            openItem.InstanceCode = instanceCode;

            OrderConfirmItem orderItem = null;
            //Open the dialog
            OpenPositionDialog dialog = new OpenPositionDialog();
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.OnLoadControl(dialog, null);
            dialog.OnLoadData(dialog, openItem);
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                orderItem = (OrderConfirmItem)dialog.GetData();
                dialog.Dispose();
            }
            else
            {
                dialog.Dispose();
            }

            return orderItem;
        }

        private OpenPositionItem GetOpenPositionItem(OrderConfirmItem orderItem)
        {
            var newOpenItem = new OpenPositionItem
            {
                MonitorId = orderItem.MonitorId,
                MonitorName = orderItem.MonitorName,
                PortfolioId = orderItem.PortfolioId,
                PortfolioName = orderItem.PortfolioName,
                TemplateId = orderItem.TemplateId,
                TemplateName = orderItem.TemplateName,
                Copies = orderItem.Copies,
                FuturesContract = orderItem.FuturesContract,
                InstanceCode = orderItem.InstanceCode,
                Notes = orderItem.Notes??string.Empty,
            };

            return newOpenItem;
        }

        private List<TradeCommandSecurity> GetSelectCommandSecurities(OpenPositionItem openItem, int commandId, List<OpenPositionSecurityItem> selectedSecuItems)
        {
            List<TradeCommandSecurity> cmdSecuItems = new List<TradeCommandSecurity>();
            foreach (var item in selectedSecuItems)
            {
                if (item.Selection && item.MonitorId == openItem.MonitorId)
                {
                    TradeCommandSecurity secuItem = new TradeCommandSecurity 
                    {
                        CommandId = commandId,
                        SecuCode = item.SecuCode,
                        SecuType = item.SecuType,
                        CommandAmount = item.EntrustAmount,
                        CommandPrice = item.CommandPrice,
                        EntrustStatus = EntrustStatus.NoExecuted
                    };

                    if(secuItem.SecuType == SecurityType.Stock)
                    {
                        secuItem.EDirection = EntrustDirection.BuySpot;
                    }
                    else
                    {
                        secuItem.EDirection = EntrustDirection.SellOpen;
                    }
                       
                    cmdSecuItems.Add(secuItem);
                }
            }

            return cmdSecuItems;
        }
        #endregion
    }
}
