using BLL.TradeCommand;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
using Model.Constant;
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
using BLL.SecurityInfo;
using Quote;
using TradingSystem.TradeUtil;
using BLL.Frontend;
using BLL.TradeInstance;
using Model.Database;
using Util;

namespace TradingSystem.Dialog
{
    public partial class ModifyCommandDialog : Forms.BaseDialog
    {
        private const string msgModifyFailure = "tradecommandmodifyfailure";

        private const string GridId = "modifycommanddialog";
        GridConfig _gridConfig;

        private SortableBindingList<ModifySecurityItem> _dataSource = new SortableBindingList<ModifySecurityItem>(new List<ModifySecurityItem>());

        private TradeCommandSecurityBLL _tradeCommandSecurityBLL = new TradeCommandSecurityBLL();
        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();
        private TradeInstanceSecurityBLL _tradeInstanceSecurityBLL = new TradeInstanceSecurityBLL();

        private CommandManagementItem _cmdMgnItem = null;
        public ModifyCommandDialog()
            :base()
        {
            InitializeComponent();

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
            this.btnCalc.Click += new EventHandler(Button_Click_Calc);
            this.btnConfirm.Click += new EventHandler(Button_Click_Confirm);
            this.btnCancel.Click += new EventHandler(Button_Click_Cancel);
        }

        public ModifyCommandDialog(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;
        }


        #region get data

        public override object GetData()
        {
            return GetModifiedItems();
        }

        private List<ModifySecurityItem> GetModifiedItems()
        {
            return _dataSource.ToList();
        }
        #endregion

        #region button click event handler

        private void Button_Click_Calc(object sender, EventArgs e)
        {
            //TODO: cancel by the last price
            QueryQuote();

            this.gridView.Invalidate();
        }

        private void Button_Click_Confirm(object sender, EventArgs e)
        {
            if (!ValidateDate())
            { 
                DialogResult = System.Windows.Forms.DialogResult.No;
                return;
            }

            if (!ValidateTime())
            {
                DialogResult = System.Windows.Forms.DialogResult.No;
                return;
            }

            int intStartDate = 0;
            int intEndDate = 0;
            int intStartTime = 0;
            int intEndTime = 0;
            int temp = 0;
            if (int.TryParse(this.tbStartDate.Text.Trim(), out temp))
            {
                intStartDate = temp;
            }

            if (int.TryParse(this.tbEndDate.Text.Trim(), out temp))
            {
                intEndDate = temp;
            }

            if (int.TryParse(this.tbStartTime.Text.Trim(), out temp))
            {
                intStartTime = temp;
            }

            if (int.TryParse(this.tbEndTime.Text.Trim(), out temp))
            {
                intEndTime = temp;
            }

            DateTime startDate = DateUtil.GetDateTimeFromInt(intStartDate, intStartTime);
            DateTime endDate = DateUtil.GetDateTimeFromInt(intEndDate, intEndTime);

            string notes = this.tbNotes.Text.Trim();
            int result = UpdateItem(_cmdMgnItem, GetModifiedItems(), startDate, endDate, notes);
            if (result > 0)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageDialog.Fail(this, msgModifyFailure);
            }
        }

        private void Button_Click_Cancel(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion

        #region LoadControl

        private bool Form_LoadControl(object sender, object data)
        {
            TSDataGridViewHelper.AddColumns(this.gridView, _gridConfig.GetGid(GridId));
            Dictionary<string, string> colDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(ModifySecurityItem));
            TSDataGridViewHelper.SetDataBinding(this.gridView, colDataMap);

            this.gridView.DataSource = _dataSource;

            LoadEntrustDirectionOption();

            return true;
        }

        private void LoadEntrustDirectionOption()
        {
            var entrustDirectionOption = ConfigManager.Instance.GetComboConfig().GetComboOption("entrustdirection");
            TSDataGridViewHelper.SetDataBinding(this.gridView, "entrustdirection", entrustDirectionOption);
        }

        #endregion

        #region LoadData

        private bool Form_LoadData(object sender, object data)
        {
            if (data == null || !(data is CommandManagementItem))
                return false;

            _cmdMgnItem = data as CommandManagementItem;
            
            //Load the child top panel
            FillSummary(_cmdMgnItem);

            //load the bottom panel
            FillEdit(_cmdMgnItem);

            //load the grid view
            FillGridView(_cmdMgnItem);

            return true;
        }

        private void FillSummary(CommandManagementItem cmdMngItem)
        {
            if (cmdMngItem == null)
                return;

            this.tbCommandId.Text = string.Format("{0}",cmdMngItem.CommandId);
            this.tbFundName.Text = string.Format("{0}--{1}", cmdMngItem.FundCode, cmdMngItem.FundName);
            this.tbPortfolioName.Text = string.Format("{0}--{1}", cmdMngItem.PortfolioCode, cmdMngItem.PortfolioName);
            this.tbCommandType.Text = string.Empty;
            this.tbArbType.Text = string.Empty;
            this.tbExecuteStage.Text = string.Empty;
            this.tbInstNo.Text = string.Format("{0}", cmdMngItem.InstanceId);
            this.tbInstCode.Text = string.Format("{0}", cmdMngItem.InstanceCode);
            this.tbSubmitDate.Text = DateFormat.Format(cmdMngItem.DDate, ConstVariable.DateFormat);
            this.tbSubmitTime.Text = DateFormat.Format(cmdMngItem.DDate, ConstVariable.TimeFormat);
        }

        private void FillEdit(CommandManagementItem cmdMngItem)
        {
            this.tbBasisPoint.Text = "0";
            this.tbTemplate.Text = string.Format("{0}--{1}", cmdMngItem.TemplateId, cmdMngItem.TemplateName);
            //TODO: submit person
            this.tbFutures.Text = cmdMngItem.BearContract;
            this.tbStartDate.Text = DateFormat.Format(cmdMngItem.DStartDate, ConstVariable.DateFormat1); 
            this.tbEndDate.Text = DateFormat.Format(cmdMngItem.DEndDate, ConstVariable.DateFormat1);
            this.tbStartTime.Text = DateFormat.Format(cmdMngItem.DStartDate, ConstVariable.TimeFormat1);
            this.tbEndTime.Text = DateFormat.Format(cmdMngItem.DEndDate, ConstVariable.TimeFormat1);
            this.tbAdjProportion.Text = "100";
            //TODO: operation level
            this.tbNotes.Text = string.Empty;
        }

        private void FillGridView(CommandManagementItem cmdMngItem)
        {
            //TODO: query the trading instance security to get the position amount and available amount
            var securities = _tradeCommandSecurityBLL.GetTradeCommandSecurities(cmdMngItem.CommandId);
            var instSecuItems = _tradeInstanceSecurityBLL.Get(cmdMngItem.InstanceId);
            foreach (var security in securities)
            {
                var item = new ModifySecurityItem 
                {
                    Selection = true,
                    SecuCode = security.SecuCode,
                    SecuType = security.SecuType,
                    Fund = cmdMngItem.FundName,
                    Portfolio = cmdMngItem.PortfolioDisplay,
                    OriginCommandAmount = security.CommandAmount,
                    EDirection = security.EDirection,
                    OriginCommandPrice = security.CommandPrice,
                    EntrustDirection = string.Format("{0}", (int)security.EDirection),
                    NewCommandAmount = security.CommandAmount,
                };

                if (instSecuItems != null)
                {
                    var findItem = instSecuItems.Find(p => p.SecuCode.Equals(item.SecuCode) && p.SecuType == item.SecuType);
                    if (findItem != null)
                    {
                        item.AvailableAmount = findItem.AvailableAmount;
                    }
                }

                _dataSource.Add(item);
            }

            QueryQuote();

            this.gridView.Invalidate();
        }

        private void QueryQuote()
        {
            //query the price and set it
            List<SecurityItem> secuList = new List<SecurityItem>();
            var uniqueSecuItems = _dataSource.GroupBy(p => p.SecuCode).Select(p => p.First());
            foreach (var secuItem in uniqueSecuItems)
            {
                var findItem = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                secuList.Add(findItem);
            }

            foreach (var secuItem in _dataSource)
            {
                var targetItem = secuList.Find(p => p.SecuCode.Equals(secuItem.SecuCode) && (p.SecuType == SecurityType.Stock || p.SecuType == SecurityType.Futures));
                var marketData = QuoteCenter2.Instance.GetMarketData(targetItem);
                //secuItem.EntrustPrice = QuotePriceHelper.GetPrice(priceType, marketData);
                secuItem.LastPrice = marketData.CurrentPrice;
                secuItem.OriginCommandMoney = secuItem.OriginCommandAmount * marketData.CurrentPrice;
                secuItem.NewCommandPrice = marketData.CurrentPrice;
                secuItem.NewCommandMoney = secuItem.NewCommandAmount * secuItem.NewCommandPrice;
                secuItem.ESuspendFlag = marketData.SuspendFlag;
                secuItem.ELimitUpDownFlag = marketData.LimitUpDownFlag;

                secuItem.SecuName = targetItem.SecuName;
                secuItem.ExchangeCode = targetItem.ExchangeCode;
                if (secuItem.SecuType == SecurityType.Stock)
                {
                    secuItem.PositionType = Model.EnumType.PositionType.SpotLong;
                }
                else if (secuItem.SecuType == SecurityType.Futures)
                {
                    secuItem.PositionType = Model.EnumType.PositionType.FuturesShort;
                }
                else
                { 
                    //do nothing
                }
            }
        }

        #endregion

        #region modified command item

        private int UpdateItem(CommandManagementItem cmdMngItem, List<ModifySecurityItem> modifiedSecuItems, DateTime startDate, DateTime endDate, string notes)
        {
            //var oldInstance = _tradeInstanceBLL.GetInstance(cmdMngItem.InstanceId);
            //if (oldInstance == null || oldInstance.InstanceId != cmdMngItem.InstanceId)
            //{
            //    return -1;
            //}

            //TODO: add the StartDate, EndDate
            TradeCommand cmdItem = new TradeCommand
            {
                CommandId = cmdMngItem.CommandId,
                ECommandStatus = Model.EnumType.CommandStatus.Modified,
                DStartDate = startDate,
                DEndDate = endDate,
                ModifiedDate = DateTime.Now,
                Notes = !string.IsNullOrEmpty(notes)?notes: cmdMngItem.Notes,
            };

            List<TradeCommandSecurity> tradeModifiedSecuItems = new List<TradeCommandSecurity>();
            List<TradeCommandSecurity> tradeCancelSecuItems = new List<TradeCommandSecurity>();
            var selectedModifiedSecuItems = modifiedSecuItems.Where(p => p.Selection).ToList();
            foreach (var secuItem in selectedModifiedSecuItems)
            {
                TradeCommandSecurity tradeSecuItem = new TradeCommandSecurity
                {
                    CommandId = cmdItem.CommandId,
                    SecuCode = secuItem.SecuCode,
                    SecuType = secuItem.SecuType,
                    EDirection = secuItem.EDirection,
                    CommandAmount = secuItem.NewCommandAmount,
                    CommandPrice = secuItem.NewCommandPrice,
                };

                if (secuItem.Selection)
                {
                    tradeModifiedSecuItems.Add(tradeSecuItem);
                }
                else
                {
                    tradeCancelSecuItems.Add(tradeSecuItem);
                }
            }

            int result = _tradeCommandBLL.Update(cmdItem, tradeModifiedSecuItems, tradeCancelSecuItems);
            if (result > 0)
            {
                //TODO: add more parameters
                TradeInstance tradeInstance = new TradeInstance
                {
                    InstanceId = cmdMngItem.InstanceId,
                    InstanceCode = cmdMngItem.InstanceCode,
                    FuturesContract = cmdMngItem.BearContract,
                    //MonitorUnitId = oldInstance.MonitorUnitId,
                    //StockDirection = oldInstance.StockDirection,
                    //FuturesDirection = oldInstance.FuturesDirection,
                    //FuturesPriceType = oldInstance.FuturesPriceType,
                };

                List<TradeInstanceSecurity> modifiedInstSecuItems = new List<TradeInstanceSecurity>();
                List<TradeInstanceSecurity> cancelInstSecuItems = new List<TradeInstanceSecurity>();
                foreach (var secuItem in selectedModifiedSecuItems)
                {
                    int modifiedAmount = secuItem.NewCommandAmount - secuItem.OriginCommandAmount;

                    TradeInstanceSecurity tradeInstSecuItem = new TradeInstanceSecurity
                    {
                        SecuCode = secuItem.SecuCode,
                        SecuType = secuItem.SecuType,
                        InstructionPreBuy = 0,
                        InstructionPreSell = 0,
                    };

                    //TODO::::::how to handle the case???
                    switch (secuItem.EDirection)
                    {
                        case Model.EnumType.EntrustDirection.BuySpot:
                            {
                                tradeInstSecuItem.InstructionPreBuy = modifiedAmount;
                            }
                            break;
                        case Model.EnumType.EntrustDirection.SellSpot:
                            {
                                tradeInstSecuItem.InstructionPreSell = modifiedAmount;
                            }
                            break;
                        case Model.EnumType.EntrustDirection.SellOpen:
                            {
                                tradeInstSecuItem.InstructionPreSell = modifiedAmount;
                            }
                            break;
                        case Model.EnumType.EntrustDirection.BuyClose:
                            {
                                tradeInstSecuItem.InstructionPreBuy = modifiedAmount;
                            }
                            break;
                    }

                    if (secuItem.Selection)
                    {
                        modifiedInstSecuItems.Add(tradeInstSecuItem);
                    }
                    else
                    {
                        cancelInstSecuItems.Add(tradeInstSecuItem);
                    }
                }

                result = _tradeInstanceBLL.Update(tradeInstance, modifiedInstSecuItems, cancelInstSecuItems);
            }

            return result;
        }

        #endregion

        #region validate the input date and time

        public bool ValidateDate()
        {
            int intStartDate = 0;
            int intEndDate = 0;

            int temp = 0;
            if(int.TryParse(this.tbStartDate.Text.Trim(), out temp))
            {
                intStartDate = temp;
            }

            if (int.TryParse(this.tbEndDate.Text.Trim(), out temp))
            {
                intEndDate = temp;
            }

            if (intStartDate > 0 
                && intEndDate > 0 
                && intStartDate <= intEndDate
                && DateUtil.IsValidDate(intStartDate)
                && DateUtil.IsValidDate(intEndDate)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateTime()
        {
            int startTime = 0;
            int endTime = 0;
            int temp = 0;

            if (int.TryParse(this.tbStartTime.Text.Trim(), out temp))
            {
                startTime = temp;
            }

            if (int.TryParse(this.tbEndTime.Text.Trim(), out temp))
            {
                endTime = temp;
            }

            if (startTime >= 0 
                && endTime >= 0 
                && startTime < endTime
                && DateUtil.IsValidTime(startTime)
                && DateUtil.IsValidTime(endTime)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
