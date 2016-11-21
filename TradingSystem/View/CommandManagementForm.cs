using BLL.Frontend;
using BLL.TradeInstance;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
using Model.Database;
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
        private GridConfig _gridConfig = null;

        private const string msgNoSelected = "tradecommandmodifynoselected";
        private const string msgCanEditOnlyOne = "tradecommandmodifycaneditonlyone";
        private const string msgInvalidSelected = "tradecommandmodifyinvalidselected";
        private const string msgModifySuccess = "tradecommandmodifysuccess";
        private const string msgModifyFailure = "tradecommandmodifyfailure";

        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();

        private SortableBindingList<CommandManagementItem> _dataSource = new SortableBindingList<CommandManagementItem>(new List<CommandManagementItem>());
        
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
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                object resData = dialog.GetData();
                if (resData != null && resData is List<ModifySecurityItem>)
                {
                    var secuItems = resData as List<ModifySecurityItem>;
                    //secuItems.ForEach(p => p.CommandId = selectedItem.CommandId);

                    //TODO: update the tradeinstance item
                    //if (UpdateItem(selectedItem, secuItems) > 0)
                    //{
                    //    MessageDialog.Info(this, msgModifySuccess);
                    //}
                    //else
                    //{
                    //    MessageDialog.Fail(this, msgModifyFailure);
                    //}
                }
            }
            else
            { 
                //do nothing
            }

            dialog.Dispose();
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
            TSDataGridViewHelper.AddColumns(this.gridView, _gridConfig.GetGid(GridId));
            Dictionary<string, string> colDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(CommandManagementItem));
            TSDataGridViewHelper.SetDataBinding(this.gridView, colDataMap);

            this.gridView.DataSource = _dataSource;

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
                };

                _dataSource.Add(cmdItem);
            }
        }

        #endregion


        #region modified command item

        private int UpdateItem(CommandManagementItem cmdMngItem, List<ModifySecurityItem> modifiedSecuItems)
        {
            TradeCommand cmdItem = new TradeCommand 
            {
                CommandId = cmdMngItem.CommandId,
                ECommandStatus = Model.EnumType.CommandStatus.Modified,
                ModifiedDate = DateTime.Now,
                Notes = cmdMngItem.Notes,
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
                TradingInstance tradeInstance = new TradingInstance 
                {
                    InstanceId = cmdMngItem.InstanceId,
                    InstanceCode = cmdMngItem.InstanceCode,
                };

                List<TradingInstanceSecurity> modifiedInstSecuItems = new List<TradingInstanceSecurity>();
                List<TradingInstanceSecurity> cancelInstSecuItems = new List<TradingInstanceSecurity>();

                result = _tradeInstanceBLL.Update(tradeInstance, modifiedInstSecuItems, cancelInstSecuItems);
            }

            return result;
        }

        #endregion
    }
}
