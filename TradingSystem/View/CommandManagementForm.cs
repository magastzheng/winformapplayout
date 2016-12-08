using BLL.Frontend;
using BLL.Permission;
using BLL.TradeInstance;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
using Model.Constant;
using Model.Database;
using Model.EnumType.EnumTypeConverter;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingSystem.Dialog;

namespace TradingSystem.View
{
    public partial class CommandManagementForm : Forms.DefaultForm
    {
        private const string GridId = "commandmanagement";
        private const string GridSecurityId = "modifycommandsecurity";
        private const string GridEntrustId = "modifycommandentrust";
        private const string GridDealId = "modifycommanddeal";

        private GridConfig _gridConfig = null;

        private const string msgNoSelected = "tradecommandmodifynoselected";
        private const string msgCanEditOnlyOne = "tradecommandmodifycaneditonlyone";
        private const string msgInvalidSelected = "tradecommandmodifyinvalidselected";
        private const string msgModifySuccess = "tradecommandmodifysuccess";
        private const string msgModifyFailure = "tradecommandmodifyfailure";

        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();
        private UserBLL _userBLL = new UserBLL();

        private SortableBindingList<CommandManagementItem> _dataSource = new SortableBindingList<CommandManagementItem>(new List<CommandManagementItem>());
        private SortableBindingList<CommandManagementSecurityItem> _secuDataSource = new SortableBindingList<CommandManagementSecurityItem>(new List<CommandManagementSecurityItem>());
        private SortableBindingList<CommandManagementEntrustItem> _entrustDataSource = new SortableBindingList<CommandManagementEntrustItem>(new List<CommandManagementEntrustItem>());
        private SortableBindingList<CommandManagementDealItem> _dealDataSource = new SortableBindingList<CommandManagementDealItem>(new List<CommandManagementDealItem>());
        

        public CommandManagementForm()
            :base()
        {
            InitializeComponent();
        }

        public CommandManagementForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;


            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);

            this.tsbRefresh.Click += new System.EventHandler(ToolStripButton_Click_Refresh);
            this.tsbModify.Click += new System.EventHandler(ToolStripButton_Click_Modify);
            this.tsbCancel.Click += new System.EventHandler(ToolStripButton_Click_Cancel);
        }

        #region ToolStripButton click event handler

        private void ToolStripButton_Click_Refresh(object sender, System.EventArgs e)
        {
            LoadTradeCommand();
        }

        private void ToolStripButton_Click_Modify(object sender, System.EventArgs e)
        {
            var selectedItems = _dataSource.Where(p => p.Selection).ToList();
            if (selectedItems.Count == 0)
            {
                MessageDialog.Warn(this, msgNoSelected);
                return;
            }

            if (selectedItems.Count > 1)
            {
                MessageDialog.Warn(this, msgCanEditOnlyOne);
                return;
            }

            var invalidItems = selectedItems.Where(p => p.ECommandStatus != Model.EnumType.CommandStatus.Effective
                && p.ECommandStatus != Model.EnumType.CommandStatus.Modified).ToList();
            if (invalidItems.Count > 0)
            {
                MessageDialog.Warn(this, msgInvalidSelected);
                return;
            }

            var selectedItem = selectedItems.First();
            ModifyCommandDialog dialog = new ModifyCommandDialog(_gridConfig);
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.OnLoadControl(dialog, null);
            dialog.OnLoadData(dialog, selectedItem);
            var dialogResult = dialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                //object resData = dialog.GetData();
                //if (resData != null && resData is List<ModifySecurityItem>)
                //{
                //    var secuItems = resData as List<ModifySecurityItem>;
                    
                //    //TODO: update the tradeinstance item
                //    if (UpdateItem(selectedItem, secuItems) > 0)
                //    {
                        MessageDialog.Info(this, msgModifySuccess);
                //    }
                //    else
                //    {
                //        MessageDialog.Fail(this, msgModifyFailure);
                //    }
                //}

                        dialog.Dispose();
            }
            else if(dialogResult == System.Windows.Forms.DialogResult.No)
            { 
                //do nothing
                MessageDialog.Fail(this, msgModifyFailure);

                dialog.Dispose();
            }

            
        }

        private void ToolStripButton_Click_Cancel(object sender, System.EventArgs e)
        {
            var selectedItems = _dataSource.Where(p => p.Selection).ToList();
            if (selectedItems.Count == 0)
            {
                MessageDialog.Warn(this, msgNoSelected);
                return;
            }

            var invalidItems = selectedItems.Where(p => p.ECommandStatus != Model.EnumType.CommandStatus.Effective
                && p.ECommandStatus != Model.EnumType.CommandStatus.Modified).ToList();
            if (invalidItems.Count > 0)
            {
                MessageDialog.Warn(this, msgInvalidSelected);
                return;
            }

            CancelCommandDialog dialog = new CancelCommandDialog();
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.OnLoadControl(dialog, null);
            dialog.OnLoadData(dialog, null);
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string notes = (string)dialog.GetData();
                StringBuilder sbSuccess = new StringBuilder();
                StringBuilder sbFailure = new StringBuilder();

                sbSuccess.Append("撤销成功指令：");
                sbFailure.Append("撤销失败指令：");
                List<int> lsSuccess = new List<int>();
                List<int> lsFailure = new List<int>();
                foreach (var selectedItem in selectedItems)
                {
                    Model.Database.TradeCommand cmdItem = new Model.Database.TradeCommand
                    {
                        CommandId = selectedItem.CommandId,
                        ECommandStatus = Model.EnumType.CommandStatus.Canceled,
                        ModifiedDate = DateTime.Now,
                        DStartDate = selectedItem.DStartDate,
                        DEndDate = selectedItem.DEndDate,
                        Notes = notes,
                    };

                    //call the UFX interface to withdraw the entrust securities

                    //TODO: update the security in tradinginstance table
                    if (_tradeCommandBLL.Update(cmdItem) > 0)
                    {
                        lsSuccess.Add(cmdItem.CommandId);
                    }
                    else
                    {
                        lsFailure.Add(cmdItem.CommandId);
                    }
                }

                string msg = string.Empty;
                if (lsSuccess.Count > 0)
                { 
                    sbSuccess.Append(string.Join(",", lsSuccess));
                    msg = sbSuccess.ToString();
                }

                if (lsFailure.Count > 0)
                {
                    sbFailure.Append(string.Join(",", lsFailure));

                    msg += sbFailure.ToString();
                }

                MessageDialog.Info(this, msg);
            }
            else
            { 
                //do nothing
            }

            //dialog.Close();
            dialog.Dispose();
        }

        #endregion

        #region LoadControl

        private bool Form_LoadControl(object sender, object data)
        {
            //交易指令表
            TSDataGridViewHelper.AddColumns(this.gridView, _gridConfig.GetGid(GridId));
            Dictionary<string, string> colDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(CommandManagementItem));
            TSDataGridViewHelper.SetDataBinding(this.gridView, colDataMap);

            //交易证券表
            TSDataGridViewHelper.AddColumns(this.secuGridView, _gridConfig.GetGid(GridSecurityId));
            colDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(CommandManagementSecurityItem));
            TSDataGridViewHelper.SetDataBinding(this.secuGridView, colDataMap);

            //委托证券表
            TSDataGridViewHelper.AddColumns(this.entrustGridView, _gridConfig.GetGid(GridEntrustId));
            colDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(CommandManagementEntrustItem));
            TSDataGridViewHelper.SetDataBinding(this.entrustGridView, colDataMap);

            //成交证券表
            TSDataGridViewHelper.AddColumns(this.dealGridView, _gridConfig.GetGid(GridDealId));
            colDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(CommandManagementDealItem));
            TSDataGridViewHelper.SetDataBinding(this.dealGridView, colDataMap);

            this.gridView.DataSource = _dataSource;
            this.secuGridView.DataSource = _secuDataSource;
            this.entrustGridView.DataSource = _entrustDataSource;
            this.dealGridView.DataSource = _dealDataSource;

            return true;
        }

        #endregion

        #region LoadData

        private bool Form_LoadData(object sender, object data)
        {
            LoadTradeCommand();

            return true;
        }

        private void LoadTradeCommand()
        {
            _dataSource.Clear();

            var tradeCommandItems = _tradeCommandBLL.GetAll();
            foreach(var item in tradeCommandItems)
            {
                CommandManagementItem cmdItem = new CommandManagementItem
                {
                    DDate = item.CreatedDate,
                    CommandId = item.CommandId,
                    ECommandStatus = item.ECommandStatus,
                    ArbitrageCopies = item.CommandNum,
                    DStartDate = item.DStartDate,
                    DEndDate = item.DEndDate,
                    EExecutype = item.EExecuteType,
                    EDealStatus = item.EDealStatus,
                    EEntrustStatus = item.EEntrustStatus,
                    CommandModifiedTimes = item.ModifiedTimes,
                    //DDispatchDate = item.d
                    InstanceId = item.InstanceId,
                    InstanceCode = item.InstanceCode,
                    PortfolioId = item.PortfolioId,
                    PortfolioCode = item.PortfolioCode,
                    PortfolioName = item.PortfolioName,
                    TemplateId = item.TemplateId,
                    TemplateName = item.TemplateName,
                    BearContract = item.BearContract,
                    FundCode = item.AccountCode,
                    FundName = item.AccountName,
                    Notes = item.Notes,
                    ModifiedCause = item.ModifiedCause,
                    CancelCause = item.CancelCause,
                };

                _dataSource.Add(cmdItem);
            }

            if (tradeCommandItems.Count > 0)
            {
                LoadCommandSummary(_dataSource[0]);

                //TODO: load the security/entrust/deal information
            }
        }

        private void LoadCommandSummary(CommandManagementItem tradeCommand)
        {
            //var user = _userBLL.GetById(tradeCommand.SubmitPerson);
            this.tbCommandId.Text = string.Format("{0}", tradeCommand.CommandId);
            this.tbFundName.Text = tradeCommand.FundDisplay;
            this.tbPortName.Text = tradeCommand.PortfolioDisplay;// string.Format("{0}--{1}", tradeCommand.PortfolioCode, tradeCommand.PortfolioName);
            this.tbSecuName.Text = "N/A";
            this.tbPriceMode.Text = "N/A";
            this.tbCommandPrice.Text = "N/A";
            //this.tbCommandAmount.Text = string.Format("{0}", tradeCommand.CommandNum);
            //TODO:get the deal amount
            this.tbDealAmount.Text = "0";
            this.tbAveragePrice.Text = "N/A";
            this.tbSubmitDate.Text = tradeCommand.CommandSubmitDate;
            this.tbSubmitTime.Text = tradeCommand.CommandSubmitTime;
            this.tbStartDate.Text = tradeCommand.StartDate;
            this.tbStartTime.Text = tradeCommand.StartTime;
            this.tbEndDate.Text = tradeCommand.EndDate;
            this.tbEndTime.Text = tradeCommand.EndTime;
            this.tbCommandStatus.Text = tradeCommand.CommandStatus;
            this.tbEntrustStatus.Text = tradeCommand.EntrustExecuteStatus;// CommandStatusHelper.GetEntrustName(tradeCommand.EEntrustStatus);
            this.tbDealStatus.Text = CommandStatusHelper.GetDealName(tradeCommand.EDealStatus);

            this.tbSubmitPerson.Text = tradeCommand.CommandSubmitPerson;
            this.tbModifyPerson.Text = tradeCommand.ModifyOperator;
            this.tbCancelPerson.Text = tradeCommand.ModifyOperator;

            this.tbModifyTime.Text = DateFormat.Format(tradeCommand.DModifiedDate, ConstVariable.TimeFormat);
            this.tbCancelTime.Text = DateFormat.Format(tradeCommand.DCancelDate, ConstVariable.TimeFormat);
            this.tbNotes.Text = tradeCommand.Notes;
            this.tbModifyCause.Text = tradeCommand.ModifiedCause;
            this.tbCancelCause.Text = tradeCommand.CancelCause;
        }

        #endregion


        //#region modified command item

        //private int UpdateItem(CommandManagementItem cmdMngItem, List<ModifySecurityItem> modifiedSecuItems)
        //{
        //    TradeCommand cmdItem = new TradeCommand 
        //    {
        //        CommandId = cmdMngItem.CommandId,
        //        ECommandStatus = Model.EnumType.CommandStatus.Modified,
        //        ModifiedDate = DateTime.Now,
        //        Notes = cmdMngItem.Notes,
        //    };

        //    List<TradeCommandSecurity> tradeModifiedSecuItems = new List<TradeCommandSecurity>();
        //    List<TradeCommandSecurity> tradeCancelSecuItems = new List<TradeCommandSecurity>();
        //    var selectedModifiedSecuItems = modifiedSecuItems.Where(p => p.Selection).ToList();
        //    foreach (var secuItem in selectedModifiedSecuItems)
        //    {
        //        TradeCommandSecurity tradeSecuItem = new TradeCommandSecurity 
        //        {
        //            CommandId = cmdItem.CommandId,
        //            SecuCode = secuItem.SecuCode,
        //            SecuType = secuItem.SecuType,
        //            EDirection = secuItem.EDirection,
        //            CommandAmount = secuItem.NewCommandAmount,
        //            CommandPrice = secuItem.NewCommandPrice,
        //        };

        //        if (secuItem.Selection)
        //        {
        //            tradeModifiedSecuItems.Add(tradeSecuItem);       
        //        }
        //        else
        //        {
        //            tradeCancelSecuItems.Add(tradeSecuItem);
        //        }
        //    }

        //    int result = _tradeCommandBLL.Update(cmdItem, tradeModifiedSecuItems, tradeCancelSecuItems);
        //    if (result > 0)
        //    {
        //        //TODO: add more parameters
        //        TradingInstance tradeInstance = new TradingInstance 
        //        {
        //            InstanceId = cmdMngItem.InstanceId,
        //            InstanceCode = cmdMngItem.InstanceCode,
        //        };

        //        List<TradingInstanceSecurity> modifiedInstSecuItems = new List<TradingInstanceSecurity>();
        //        List<TradingInstanceSecurity> cancelInstSecuItems = new List<TradingInstanceSecurity>();
        //        foreach (var secuItem in selectedModifiedSecuItems)
        //        {
        //            int modifiedAmount = secuItem.NewCommandAmount - secuItem.OriginCommandAmount;

        //            TradingInstanceSecurity tradeInstSecuItem = new TradingInstanceSecurity 
        //            {
        //                SecuCode = secuItem.SecuCode,
        //                SecuType = secuItem.SecuType,
        //                InstructionPreBuy = 0,
        //                InstructionPreSell = 0,
        //            };

        //            //TODO::::::how to handle the case???
        //            switch (secuItem.EDirection)
        //            {
        //                case Model.EnumType.EntrustDirection.BuySpot:
        //                    {
        //                        tradeInstSecuItem.InstructionPreBuy = modifiedAmount;
        //                    }
        //                    break;
        //                case Model.EnumType.EntrustDirection.SellSpot:
        //                    {
        //                        tradeInstSecuItem.InstructionPreSell = modifiedAmount;
        //                    }
        //                    break;
        //                case Model.EnumType.EntrustDirection.SellOpen:
        //                    {
        //                        tradeInstSecuItem.InstructionPreSell = modifiedAmount;
        //                    }
        //                    break;
        //                case Model.EnumType.EntrustDirection.BuyClose:
        //                    {
        //                        tradeInstSecuItem.InstructionPreBuy = modifiedAmount;
        //                    }
        //                    break;
        //            }

        //            if (secuItem.Selection)
        //            {
        //                modifiedInstSecuItems.Add(tradeInstSecuItem);
        //            }
        //            else
        //            {
        //                cancelInstSecuItems.Add(tradeInstSecuItem);
        //            }
        //        }

        //        result = _tradeInstanceBLL.Update(tradeInstance, modifiedInstSecuItems, cancelInstSecuItems);
        //    }

        //    return result;
        //}

        //#endregion
    }
}
