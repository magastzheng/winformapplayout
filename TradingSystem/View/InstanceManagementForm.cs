using BLL.TradeInstance;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
using Model.UI;
using System.Collections.Generic;

namespace TradingSystem.View
{
    public partial class InstanceManagementForm : Forms.DefaultForm
    {
        private const string GridId = "instancemanagement";
        private GridConfig _gridConfig = null;

        private SortableBindingList<InstanceItem> _dataSource = new SortableBindingList<InstanceItem>(new List<InstanceItem>());

        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();

        public InstanceManagementForm() :
            base()
        {
            InitializeComponent();
        }

        public InstanceManagementForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);

            this.tsbRefresh.Click += new System.EventHandler(ToolStripButton_Refresh_Click);
        }

        #region load control

        private bool Form_LoadControl(object sender, object data)
        {
            TSDataGridViewHelper.AddColumns(this.gridView, _gridConfig.GetGid(GridId));
            Dictionary<string, string> colDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(InstanceItem));
            TSDataGridViewHelper.SetDataBinding(this.gridView, colDataMap);

            this.gridView.DataSource = _dataSource;

            return true;
        }

        #endregion

        #region load data

        private bool Form_LoadData(object sender, object data)
        {
            return InternalLoadData();
        }

        private bool InternalLoadData()
        {
            _dataSource.Clear();

            //TODO:
            var instItems = _tradeInstanceBLL.GetAllInstanceItem();
            foreach (var instItem in instItems)
            {
                _dataSource.Add(instItem);
            }

            return true;
        }

        #endregion

        #region tool strip button click event

        private void ToolStripButton_Refresh_Click(object sender, System.EventArgs e)
        {
            InternalLoadData();
        }

        #endregion
    }
}
