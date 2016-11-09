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

namespace TradingSystem.Dialog
{
    public partial class ModifyCommandDialog : Forms.BaseDialog
    {
        private const string GridId = "modifycommanddialog";
        GridConfig _gridConfig;

        private SortableBindingList<ModifySecurityItem> _dataSource = new SortableBindingList<ModifySecurityItem>(new List<ModifySecurityItem>());

        private TradeCommandSecurityBLL _tradeCommandSecurityBLL = new TradeCommandSecurityBLL();

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

        #region button click event handler

        private void Button_Click_Calc(object sender, EventArgs e)
        {
            //TODO: cancel by the last price
        }

        private void Button_Click_Confirm(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
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

            var cmdMngItem = data as CommandManagementItem;
            
            //Load the child top panel
            FillSummary(cmdMngItem);

            //load the bottom panel
            FillEdit(cmdMngItem);

            //load the grid view
            FillGridView(cmdMngItem);

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
            this.tbStartDate.Text = cmdMngItem.StartDate;
            this.tbEndDate.Text = cmdMngItem.EndDate;
            this.tbStartTime.Text = cmdMngItem.StartTime;
            this.tbEndTime.Text = cmdMngItem.EndTime;
            this.tbAdjProportion.Text = "100";
            //TODO: operation level
            this.tbNotes.Text = string.Empty;
        }

        private void FillGridView(CommandManagementItem cmdMngItem)
        {
            var securities = _tradeCommandSecurityBLL.GetTradeCommandSecurities(cmdMngItem.CommandId);
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

                _dataSource.Add(item);
            }

            Quote();
        }

        private void Quote()
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
                secuItem.OriginCommandMoney = secuItem.OriginCommandAmount * marketData.CurrentPrice;
                secuItem.NewCommandPrice = marketData.CurrentPrice;
                secuItem.NewCommandMoney = secuItem.NewCommandAmount * secuItem.NewCommandPrice;
                secuItem.ESuspendFlag = marketData.SuspendFlag;
                secuItem.ELimitUpDownFlag = QuotePriceHelper.GetLimitUpDownFlag(marketData.CurrentPrice, marketData.LowLimitPrice, marketData.HighLimitPrice);

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
    }
}
