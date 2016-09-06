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
using BLL.SecurityInfo;
using BLL.TradeCommand;
using Model.EnumType;
using Model.Binding.BindingUtil;
using TradingSystem.TradeUtil;
using Model.Database;
using BLL.Template;
using BLL.Frontend;

namespace TradingSystem.View
{
    public partial class OpenPositionForm : Forms.DefaultForm
    {
        private const string MonitorGridId = "openposition";
        private const string SecurityGridId = "openpositionsecurity";
        private const string BottomMenuId = "openposition";

        private GridConfig _gridConfig;
        
        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();
        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private TemplateBLL _templateBLL = new TemplateBLL();
        private MonitorUnitBLL _monitorUnitBLL = new MonitorUnitBLL();

        private SortableBindingList<OpenPositionItem> _monitorDataSource;
        private SortableBindingList<OpenPositionSecurityItem> _securityDataSource;

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

            btnBottomContainer.ButtonClick += new EventHandler(ButtonContainer_ButtonClick);
        }

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
            //Load the data of open posoition
            List<OpenPositionItem> monitorList = _monitorUnitBLL.GetActive();
            _monitorDataSource = new SortableBindingList<OpenPositionItem>(monitorList);
            this.monitorGridView.DataSource = _monitorDataSource;

            //Load the data for each template
            List<OpenPositionSecurityItem> secuItems = new List<OpenPositionSecurityItem>();
            _securityDataSource = new SortableBindingList<OpenPositionSecurityItem>(secuItems);
            this.securityGridView.DataSource = _securityDataSource;
            
            if (monitorList.Count > 0)
            {
                List<int> selectIndex = TSDataGridViewHelper.GetSelectRowIndex(monitorGridView);
                if (selectIndex.Count > 0)
                {
                    List<OpenPositionItem> selectMonitors = new List<OpenPositionItem>();
                    foreach (var index in selectIndex)
                    {
                        selectMonitors.Add(_monitorDataSource[index]);
                    }

                    LoadSecurityData(selectMonitors);
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
            List<TemplateStock> stocks = _templateBLL.GetStocks(monitorItem.TemplateId);
            List<OpenPositionSecurityItem> secuItems = new List<OpenPositionSecurityItem>();

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
                    EntrustAmount = monitorItem.Copies * stock.Amount,
                    EDirection = EntrustDirection.BuySpot,
                    SecuType = SecurityType.Stock
                };

                _securityDataSource.Add(secuItem);
            }

            var templateItem = _templateBLL.GetTemplate(monitorItem.TemplateId);
            if (templateItem != null)
            {
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

                int pos = _securityDataSource.Count - stocks.Count;
                _securityDataSource.Insert(pos, futureItem);
            }
        }

        private void CalcEntrustAmount(OpenPositionItem monitorItem, int copies)
        {
            var secuItems = _securityDataSource.Where(p => p.MonitorId == monitorItem.MonitorId);
            foreach (var secuItem in secuItems)
            {
                secuItem.EntrustAmount = copies * secuItem.WeightAmount;
            }
        }

        private void CalcEntrustAmount(OpenPositionItem monitorItem)
        {
            var secuItems = _securityDataSource.Where(p => p.MonitorId == monitorItem.MonitorId);
            foreach (var secuItem in secuItems)
            {
                secuItem.EntrustAmount = monitorItem.Copies * secuItem.WeightAmount;
            }
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
                        QueryQuote();

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

            QuoteCenter.Instance.Query(secuList);
            foreach (var secuItem in _securityDataSource)
            {
                var targetItem = secuList.Find(p => p.SecuCode.Equals(secuItem.SecuCode) && (p.SecuType == SecurityType.Stock || p.SecuType == SecurityType.Futures));
                var marketData = QuoteCenter.Instance.GetMarketData(targetItem);
                secuItem.LastPrice = marketData.CurrentPrice;
                secuItem.CommandMoney = secuItem.LastPrice * secuItem.EntrustAmount;
                secuItem.BuyAmount = marketData.BuyAmount;
                secuItem.SellAmount = marketData.SellAmount;
                secuItem.ESuspendFlag = marketData.SuspendFlag;
                secuItem.ELimitUpDownFlag = QuotePriceHelper.GetLimitUpDownFlag(marketData.CurrentPrice, marketData.LowLimitPrice, marketData.HighLimitPrice);
            }
        }

        private void GiveOrder()
        {
            List<int> selectedList = TSDataGridViewHelper.GetSelectRowIndex(this.monitorGridView);
            foreach (var index in selectedList)
            {
                var openItem = _monitorDataSource[index];
                var newOpenItem = GetSubmitItem(openItem);
                if (newOpenItem == null)
                {
                    string msg = string.Format("对监控单元[{0}]下达指令失败", openItem.MonitorName);
                    MessageBox.Show(this, msg, "失败", MessageBoxButtons.OK);
                    continue;
                }

                int instanceId = -1;
                string instanceCode = newOpenItem.InstanceCode;
                var instance = _tradeInstanceBLL.GetInstance(instanceCode);
                if (instance != null && !string.IsNullOrEmpty(instance.InstanceCode) && instance.InstanceCode.Equals(instanceCode))
                {
                    instanceId = instance.InstanceId;
                    instance.OperationCopies += newOpenItem.Copies;
                    var secuItems = _securityDataSource.Where(p => p.MonitorId == newOpenItem.MonitorId).ToList();
                    _tradeInstanceBLL.Update(instance, newOpenItem, secuItems);
                }
                else
                {
                    TradingInstance tradeInstance = new TradingInstance
                    {
                        InstanceCode = instanceCode,
                        MonitorUnitId = newOpenItem.MonitorId,
                        StockDirection = EntrustDirection.BuySpot,
                        FuturesContract = newOpenItem.FuturesContract,
                        FuturesDirection = EntrustDirection.SellOpen,
                        OperationCopies = newOpenItem.Copies,
                        StockPriceType = StockPriceType.NoLimit,
                        FuturesPriceType = FuturesPriceType.NoLimit,
                        Status = TradingInstanceStatus.Active,
                    };

                    tradeInstance.Owner = LoginManager.Instance.LoginUser.Operator;

                    var secuItems = _securityDataSource.Where(p => p.MonitorId == newOpenItem.MonitorId).ToList();

                    instanceId = _tradeInstanceBLL.Create(tradeInstance, newOpenItem, secuItems);
                }

                if (instanceId > 0)
                {
                    //success! Will send generate TradingCommand
                    TradeCommand cmdItem = new TradeCommand
                    {
                        InstanceId = instanceId,
                        ECommandType = CommandType.Arbitrage,
                        EExecuteType = ExecuteType.OpenPosition,
                        CommandNum = newOpenItem.Copies,
                        EStockDirection = EntrustDirection.BuySpot,
                        EFuturesDirection = EntrustDirection.SellOpen,
                        EEntrustStatus = EntrustStatus.NoExecuted,
                        EDealStatus = DealStatus.NoDeal,
                        ModifiedTimes = 1
                    };

                    var cmdSecuItems = GetSelectCommandSecurities(newOpenItem, -1);

                    int ret = _tradeCommandBLL.Submit(cmdItem, cmdSecuItems);

                    if (ret > 0)
                    {
                        //Success
                    }
                    else
                    {
                        //Some item fail
                    }
                }
                else
                {
                    //TODO: error message
                }
            }
        }

        public OpenPositionItem GetSubmitItem(OpenPositionItem openItem)
        {
            string instanceCode = string.Format("{0}-{1}-{2}", openItem.PortfolioId, openItem.TemplateId, DateTime.Now.ToString("yyyyMMdd"));

            openItem.InstanceCode = instanceCode;

            OpenPositionItem newOpenItem = null;

            //Open the dialog
            OpenPositionDialog dialog = new OpenPositionDialog();
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.OnLoadControl(dialog, null);
            dialog.OnLoadData(dialog, openItem);
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                newOpenItem = (OpenPositionItem)dialog.GetData();
                dialog.Dispose();
            }
            else
            {
                newOpenItem = null;
                dialog.Dispose();
            }

            return newOpenItem;
        }

        private List<TradeCommandSecurity> GetSelectCommandSecurities(OpenPositionItem openItem, int commandId)
        {
            List<TradeCommandSecurity> cmdSecuItems = new List<TradeCommandSecurity>();
            foreach (var item in _securityDataSource)
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
