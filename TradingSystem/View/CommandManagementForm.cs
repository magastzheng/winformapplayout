using BLL.Frontend;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
using Model.UI;
using System.Collections.Generic;

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
        }

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
            _dataSource.Clear();

            LoadTradeCommand();

            return true;
        }

        private void LoadTradeCommand()
        {
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
