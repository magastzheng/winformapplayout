using BLL.TradeCommand;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            this.tbSubmitDate.Text = cmdMngItem.CommandSubmitDate;
            this.tbSubmitTime.Text = cmdMngItem.CommandSubmitTime;
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
                    Fund = cmdMngItem.FundName,
                    Portfolio = cmdMngItem.PortfolioDisplay,
                    OriginCommandAmount = security.CommandAmount,
                    EDirection = security.EDirection,
                    OriginCommandPrice = security.CommandPrice,
                    EntrustDirection = string.Format("{0}", (int)security.EDirection),
                    //EPriceType = security.
                };

                _dataSource.Add(item);
            }
        }

        #endregion
    }
}
