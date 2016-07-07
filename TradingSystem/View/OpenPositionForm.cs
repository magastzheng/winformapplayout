using Config;
using Controls.Entity;
using Controls.GridView;
using DBAccess;
using Model.SecurityInfo;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Quote;
using TradingSystem.Dialog;
using TradingSystem.TradeUtil;
using Model.Data;
using BLL.SecurityInfo;
using BLL.Entrust;
using BLL.TradeCommand;

namespace TradingSystem.View
{
    public partial class OpenPositionForm : Forms.DefaultForm
    {
        private const string MonitorGridId = "openposition";
        private const string SecurityGridId = "openpositionsecurity";
        private const string BottomMenuId = "openposition";

        private GridConfig _gridConfig;
        private MonitorUnitDAO _monitordbdao = new MonitorUnitDAO();
        private TemplateStockDAO _stockdbdao = new TemplateStockDAO();
        private StockTemplateDAO _tempdbdao = new StockTemplateDAO();
        private TradingInstanceDAO _tradeinstdao = new TradingInstanceDAO();
        private TradingCommandDAO _tradecmddao = new TradingCommandDAO();

        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();
        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();


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
            Dictionary<string, string> monitorColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(OpenPositionItem));
            TSDataGridViewHelper.SetDataBinding(this.monitorGridView, monitorColDataMap);           

            //set the securityGridView
            TSDataGridViewHelper.AddColumns(this.securityGridView, _gridConfig.GetGid(SecurityGridId));
            Dictionary<string, string> securityColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(OpenPositionSecurityItem));
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
            List<OpenPositionItem> monitorList = _monitordbdao.GetActive();
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
            List<TemplateStock> stocks = _stockdbdao.Get(monitorItem.TemplateId);
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
                    DirectionType = Model.Data.EntrustDirection.BuySpot,
                    SecuType = SecurityType.Stock
                };

                _securityDataSource.Add(secuItem);
            }

            var templateItems = _tempdbdao.Get(monitorItem.TemplateId);
            if (templateItems != null && templateItems.Count == 1)
            {
                var templateItem = templateItems[0];
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
                    DirectionType = Model.Data.EntrustDirection.SellOpen,
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
                        List<SecurityItem> secuList = new List<SecurityItem>();
                        foreach (var secuItem in _securityDataSource)
                        {
                            var findItem = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                            if (findItem != null)
                            {
                                var addedItem = secuList.Find(p => p.SecuCode.Equals(findItem.SecuCode) && p.SecuType == findItem.SecuType);
                                if (addedItem == null)
                                {
                                    secuList.Add(findItem);
                                }
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
                        }

                        this.securityGridView.Invalidate();    
                    }
                    break;
                case "GiveOrder":
                    {
                        List<int> selectedList = TSDataGridViewHelper.GetSelectRowIndex(this.monitorGridView);
                        foreach (var index in selectedList)
                        {
                            var openItem = _monitorDataSource[index];

                            //Open the dialog
                            OpenPositionDialog dialog = new OpenPositionDialog();
                            dialog.Owner = this;
                            dialog.StartPosition = FormStartPosition.CenterParent;
                            //dialog.OnLoadFormActived(json);
                            //dialog.Visible = true;
                            dialog.OnLoadControl(dialog, null);
                            dialog.OnLoadData(dialog, openItem);
                            //dialog.SaveData += new FormLoadHandler(Dialog_SaveData);
                            dialog.ShowDialog();

                            if (dialog.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
                                dialog.Dispose();
                            }
                            else
                            {
                                dialog.Dispose();
                            }

                            string instanceCode = string.Format("{0}-{1}-{2}", openItem.PortfolioId, openItem.TemplateId, DateTime.Now.ToString("yyyyMMdd"));

                            int instanceId = -1;
                            var instance = _tradeinstdao.Get(instanceCode);
                            if (instance != null && !string.IsNullOrEmpty(instance.InstanceCode) && instance.InstanceCode.Equals(instanceCode))
                            {
                                instanceId = instance.InstanceId;
                                instance.OperationCopies += openItem.Copies;
                                _tradeInstanceBLL.Update(instance);
                            }
                            else
                            {
                                TradingInstance tradeInstance = new TradingInstance
                                {
                                    InstanceCode = instanceCode,
                                    MonitorUnitId = openItem.MonitorId,
                                    StockDirection = Model.Data.EntrustDirection.BuySpot,
                                    FuturesContract = openItem.FuturesContract,
                                    FuturesDirection = Model.Data.EntrustDirection.SellOpen,
                                    OperationCopies = openItem.Copies,
                                    StockPriceType = Model.Data.StockPriceType.NoLimit,
                                    FuturesPriceType = Model.Data.FuturesPriceType.NoLimit,
                                    Status = 1,
                                    Owner = "111111"
                                };

                                var secuItems = _securityDataSource.Where(p => p.MonitorId == openItem.MonitorId).ToList();

                                instanceId = _tradeInstanceBLL.Create(tradeInstance, openItem, secuItems);
                            }

                            if (instanceId > 0)
                            {
                                //success! Will send generate TradingCommand
                                TradingCommandItem cmdItem = new TradingCommandItem 
                                {
                                    InstanceId = instanceId,
                                    ECommandType = Model.UI.CommandType.Arbitrage,
                                    EExecuteType = ExecuteType.OpenPosition,
                                    CommandNum = openItem.Copies,
                                    EStockDirection = Model.Data.EntrustDirection.BuySpot,
                                    EFuturesDirection = Model.Data.EntrustDirection.SellOpen,
                                    EEntrustStatus = EntrustStatus.NoExecuted,
                                    EDealStatus = DealStatus.NoDeal,
                                    ModifiedTimes = 1
                                };

                                var cmdSecuItems = GetSelectCommandSecurities(openItem, -1);

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
                    break;
                default:
                    break;
            }
        }

        private List<CommandSecurityItem> GetSelectCommandSecurities(OpenPositionItem openItem, int commandId)
        {
            List<CommandSecurityItem> cmdSecuItems = new List<CommandSecurityItem>();
            foreach (var item in _securityDataSource)
            {
                if (item.Selection && item.MonitorId == openItem.MonitorId)
                {
                    CommandSecurityItem secuItem = new CommandSecurityItem 
                    {
                        CommandId = commandId,
                        SecuCode = item.SecuCode,
                        SecuType = item.SecuType,
                        WeightAmount = item.WeightAmount,
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
