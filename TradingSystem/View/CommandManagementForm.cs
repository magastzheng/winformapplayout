using BLL.Frontend;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TradingSystem.Dialog;

namespace TradingSystem.View
{
    public partial class CommandManagementForm : Forms.DefaultForm
    {
        private const string GridId = "commandmanagement";
        private GridConfig _gridConfig = null;

        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();

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
                //TODO: there is no item selected
                return;
            }

            if (selectedItems.Count > 1)
            {
                //TODO: only can select one item
                return;
            }

            var invalidItems = selectedItems.Where(p => p.ECommandStatus != Model.EnumType.CommandStatus.Effective
                && p.ECommandStatus != Model.EnumType.CommandStatus.Modified).ToList();
            if (invalidItems.Count > 0)
            {
                //TODO: there is invalid item
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
                //TODO: there is no item selected
                return;
            }

            var invalidItems = selectedItems.Where(p => p.ECommandStatus != Model.EnumType.CommandStatus.Effective
                && p.ECommandStatus != Model.EnumType.CommandStatus.Modified).ToList();
            if (invalidItems.Count > 0)
            {
                //TODO: there is invalid item.
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

                    _tradeCommandBLL.Update(cmdItem);
                }
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
                    PortfolioCode = item.PortfolioCode,
                    PortfolioName = item.PortfolioName,
                    TemplateId = item.TemplateId,
                    BearContract = item.BearContract,
                    FundCode = item.AccountCode,
                    FundName = item.AccountName,
                    Notes = item.Notes,
                };

                _dataSource.Add(cmdItem);
            }
        }

        #endregion
    }
}
