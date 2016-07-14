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

namespace TradingSystem.View
{
    public partial class ClosePositionForm : Forms.DefaultForm
    {
        private const string GridCloseId = "closeposition";
        private const string GridSecurityId = "closepositionsecurity";
        private const string GridCloseCmdId = "closepositioncmd";

        private GridConfig _gridConfig;
        private TradingInstanceDAO _tradeinstdao = new TradingInstanceDAO();
        private TradingInstanceSecurityDAO _tradeinstsecudao = new TradingInstanceSecurityDAO();
        private FuturesContractDAO _fcdbdao = new FuturesContractDAO();

        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private TemplateBLL _templateBLL = new TemplateBLL();

        private SortableBindingList<ClosePositionItem> _instDataSource = new SortableBindingList<ClosePositionItem>(new List<ClosePositionItem>());
        private SortableBindingList<ClosePositionSecurityItem> _secuDataSource = new SortableBindingList<ClosePositionSecurityItem>(new List<ClosePositionSecurityItem>());
        private SortableBindingList<ClosePositionCmdItem> _cmdDataSource = new SortableBindingList<ClosePositionCmdItem>(new List<ClosePositionCmdItem>());

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
                    }
                    break;
                default:
                    break;
            }
        }

        private void LoadSecurity(ClosePositionItem closeItem)
        {
            var secuItems = _tradeinstsecudao.Get(closeItem.InstanceId);
            var tempstockitems = _templateBLL.GetTemplate(closeItem.TemplateId);
            if (secuItems == null || secuItems.Count == 0)
            {
                return;
            }

            foreach (var secuItem in secuItems)
            {
                ClosePositionSecurityItem closeSecuItem = new ClosePositionSecurityItem
                {
                    Selection = true,
                    InstanceId = secuItem.InstanceId,
                    SecuCode = secuItem.SecuCode,
                    SecuType = secuItem.SecuType,
                    PositionType = secuItem.PositionType,

                    HoldingAmount = secuItem.PositionAmount,
                    AvailableAmount = secuItem.AvailableAmount,

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
        }

        private void LoadCloseCommand(ClosePositionItem closeItem)
        {
            ClosePositionCmdItem cmdItem = new ClosePositionCmdItem 
            {
                InstanceId = closeItem.InstanceId,
                InstanceCode = closeItem.InstanceCode,
                Selection = true,
                SpotTemplate = closeItem.TemplateId.ToString(),
                MonitorName = closeItem.MonitorName,
                TradeDirection = ((int)EntrustDirection.Buy).ToString()
            };

            if (_secuDataSource != null && _secuDataSource.Count > 0)
            {
                var futuresItem = _secuDataSource.First(p => p.InstanceId == closeItem.InstanceId && p.SecuType == Model.SecurityInfo.SecurityType.Futures);
                if (futuresItem != null)
                {
                    cmdItem.FuturesContract = futuresItem.SecuCode;
                }
            }

            _cmdDataSource.Add(cmdItem);
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

            LoadCommandComboBoxOption();

            return true;
        }

        private void LoadCommandComboBoxOption()
        {
            var tradeDirectionOption = ConfigManager.Instance.GetComboConfig().GetComboOption("tradedirection");
            TSDataGridViewHelper.SetDataBinding(this.cmdGridView, "tradedirection", tradeDirectionOption);

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
                        Name = string.Format("{0} {1}",temp.TemplateId, temp.TemplateName)
                    };

                    tempOption.Items.Add(option);
                }

                TSDataGridViewHelper.SetDataBinding(this.cmdGridView, "spottemplate", tempOption);
            }

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

        #endregion

        #region load data

        private bool Form_LoadData(object sender, object data)
        {
            _instDataSource.Clear();
            _secuDataSource.Clear();
            _cmdDataSource.Clear();
            
            var tradeInstances = _tradeinstdao.GetCombine(-1);
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
                };

                _instDataSource.Add(closeItem);
            }

            return true;
        }

        #endregion

        #region Button Click Event

        private void Button_Calc_Click(object sender, EventArgs e)
        {
            var cmdItems = _cmdDataSource.Where(p => p.Selection).ToList();
            if (!ValidateCopies(cmdItems))
            {
                MessageBox.Show(this, "请输入有效的操作份数！", "错误", MessageBoxButtons.OK);
                return;
            }

            foreach (var cmdItem in cmdItems)
            {
                CalculateInstance(cmdItem);
            }

            this.cmdGridView.Invalidate();
            this.securityGridView.Invalidate();
        }

        private void Button_CloseAll_Click(object sender, EventArgs e)
        {
            var cmdItems = _cmdDataSource.Where(p => p.Selection).ToList();
            
            foreach (var cmdItem in cmdItems)
            {
                //cmdItem.TradeDirection = ((int)EntrustDirection.Sell).ToString();
                CloseAll(cmdItem);

                var closeItem = _instDataSource.First(p => p.InstanceId == cmdItem.InstanceId);
                var closeSecuItems = _secuDataSource.Where(p => p.InstanceId == cmdItem.InstanceId).ToList();

                int result = _tradeCommandBLL.SubmitCloseAll(closeItem, closeSecuItems);
                if (result < 0)
                { 
                    //TODO: fail to submit
                }
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

            ChangePositionDialog dialog = new ChangePositionDialog();
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            //dialog.OnLoadFormActived(json);
            //dialog.Visible = true;
            dialog.OnLoadControl(dialog, null);
            dialog.OnLoadData(dialog, secuItem);
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
        }

        private void Button_Submit_Click(object sender, EventArgs e)
        {
            var cmdItems = _cmdDataSource.Where(p => p.Selection).ToList();
            foreach (var cmdItem in cmdItems)
            {
                var tdcmdItem = GetTradeCommandItem(cmdItem);
                var closeItem = _instDataSource.First(p => p.InstanceId.Equals(tdcmdItem.InstanceId));
                var selectedItems = _secuDataSource.Where(p => p.Selection && p.InstanceId.Equals(tdcmdItem.InstanceId)).ToList();
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
            
            var instance = _instDataSource.Single(p => p.InstanceId == cmdItem.InstanceId);
            if (instance == null)
            {
                return;
            }

            StockTemplate template = _templateBLL.GetTemplate(instance.TemplateId);
         
            if (template == null)
            {
                return;
            }

            //instance.TemplateId
            var tempstockitems = _templateBLL.GetStocks(instance.TemplateId);
            var secuItems = _secuDataSource.Where(p => p.InstanceId == cmdItem.InstanceId);
            if (secuItems == null)
            {
                return;
            }

            //TODO: handle those 
            foreach (var secuItem in secuItems)
            {
                if (!secuItem.Selection)
                {
                    secuItem.EntrustAmount = 0;
                    //TODO: set the EntrustDirection as empty
                    continue;
                }

                var tempsecuItem = tempstockitems.Find(p => p.SecuCode.Equals(secuItem.SecuCode));

                int weightAmount = 0;
                if (tempsecuItem != null)
                {
                    weightAmount = tempsecuItem.Amount;
                }

                switch (direction)
                {
                    case EntrustDirection.Buy:
                        {
                            if (secuItem.SecuType == SecurityType.Stock)
                            {
                                secuItem.EntrustDirection = (int)EntrustDirection.BuySpot;
                                secuItem.EntrustAmount = weightAmount * copies;
                            }
                            else if (secuItem.SecuType == SecurityType.Futures)
                            {
                                secuItem.EntrustDirection = (int)EntrustDirection.SellOpen;
                                secuItem.EntrustAmount = template.FutureCopies * copies;
                            }
                            else
                            {
                                //Nothing to do
                            }
                        }
                        break;
                    case EntrustDirection.Sell:
                        {
                            if (secuItem.SecuType == SecurityType.Stock)
                            {
                                secuItem.EntrustDirection = (int)EntrustDirection.SellSpot;

                                if (secuItem.AvailableAmount >= weightAmount * copies)
                                {
                                    secuItem.EntrustAmount = weightAmount * copies;
                                }
                                else
                                {
                                    secuItem.EntrustAmount = secuItem.AvailableAmount;
                                }
                            }
                            else if (secuItem.SecuType == SecurityType.Futures)
                            {
                                secuItem.EntrustDirection = (int)EntrustDirection.BuyClose;
                                if (secuItem.AvailableAmount >= template.FutureCopies * copies)
                                {
                                    secuItem.EntrustAmount = template.FutureCopies * copies;
                                }
                                else
                                {
                                    secuItem.EntrustAmount = secuItem.AvailableAmount;
                                }
                            }
                            else
                            {
                                //Nothing to do
                            }
                        }
                        break;
                    case EntrustDirection.AdjustedToBuySell:
                        {
                            if (secuItem.SecuType == SecurityType.Stock)
                            {
                                if (weightAmount == 0)
                                {
                                    secuItem.EntrustDirection = (int)EntrustDirection.SellSpot;
                                    secuItem.EntrustAmount = secuItem.AvailableAmount;
                                }
                                else
                                {
                                    int rest = secuItem.HoldingAmount - weightAmount * copies;
                                    if (rest > 0)
                                    {
                                        secuItem.EntrustDirection = (int)EntrustDirection.SellSpot;
                                        secuItem.EntrustAmount = rest;
                                    }
                                    else
                                    {
                                        secuItem.EntrustDirection = (int)EntrustDirection.BuySpot;
                                        secuItem.EntrustAmount = 0 - rest;
                                    }
                                }
                            }
                            else if (secuItem.SecuType == SecurityType.Futures)
                            {
                                int rest = secuItem.HoldingAmount - template.FutureCopies * copies;
                                if (rest > 0)
                                {
                                    secuItem.EntrustDirection = (int)EntrustDirection.BuyClose;
                                    secuItem.EntrustAmount = rest;
                                }
                                else
                                {
                                    secuItem.EntrustDirection = (int)EntrustDirection.SellSpot;
                                    secuItem.EntrustAmount = 0 - rest;
                                }
                            }
                            else
                            {
                                //Nothing to do
                            }
                        }
                        break;
                    default:
                        break;
                }
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
                switch (secuItem.SecuType)
                {
                    case SecurityType.Stock:
                        {
                            secuItem.EntrustDirection = (int)EntrustDirection.SellSpot;
                            secuItem.EntrustAmount = secuItem.AvailableAmount;
                        }
                        break;
                    case SecurityType.Futures:
                        {
                            secuItem.EntrustDirection = (int)EntrustDirection.BuyClose;
                            secuItem.EntrustAmount = secuItem.AvailableAmount;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region

        private TradingCommandItem GetTradeCommandItem(ClosePositionCmdItem closeCmdItem)
        {
            TradingCommandItem tdcmdItem = new TradingCommandItem
            {
                InstanceId = closeCmdItem.InstanceId,
                ECommandType = CommandType.Arbitrage,
                //EExecuteType = ExecuteType.OpenPosition,
                CommandNum = closeCmdItem.Copies,
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

            return tdcmdItem;
        }

        #endregion
    }
}
