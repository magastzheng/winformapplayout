using BLL.TradeInstance;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
using Model.UI;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TradingSystem.Dialog;

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

            this.tsbModify.Click += new System.EventHandler(ToolStripButton_Modify_Click);
            this.tsbRefresh.Click += new System.EventHandler(ToolStripButton_Refresh_Click);
            this.tsbArchive.Click += new System.EventHandler(ToolStripButton_Archive_Click);

            this.gridView.MouseDoubleClick += new MouseDoubleClickHandler(GridView_MouseDoubleClick);
        }

        #region gridview event handler

        private void GridView_MouseDoubleClick(object sender, int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= _dataSource.Count)
                return;

            ModifyInstance(rowIndex);
        }

        #endregion

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

        private void ToolStripButton_Modify_Click(object sender, System.EventArgs e)
        {
            int currentRowIndex = this.gridView.GetCurrentRowIndex();
            if (currentRowIndex < 0 || currentRowIndex > _dataSource.Count - 1)
            {
                return;
            }

            ModifyInstance(currentRowIndex);
        }

        private void ToolStripButton_Refresh_Click(object sender, System.EventArgs e)
        {
            InternalLoadData();
        }

        private void ToolStripButton_Archive_Click(object sender, System.EventArgs e)
        {

        }

        #endregion

        #region private method

        private void ModifyInstance(int currentRowIndex)
        {
            var tradeInstance = _dataSource[currentRowIndex];
            TradeInstanceModifyDialog dialog = new TradeInstanceModifyDialog();
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            //dialog.OnLoadFormActived(json);
            //dialog.Visible = true;
            dialog.OnLoadControl(dialog, null);
            dialog.OnLoadData(dialog, tradeInstance);
            dialog.ShowDialog();
            if (dialog.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                var newTradeInstance = (TradeInstance)dialog.GetData();
                if (newTradeInstance != null)
                {
                    int result = _tradeInstanceBLL.UpdateTradeInstance(newTradeInstance);
                    if (result > 0)
                    {
                        //Success
                        tradeInstance.InstanceCode = newTradeInstance.InstanceCode;
                        tradeInstance.MonitorUnitId = newTradeInstance.MonitorUnitId;
                        tradeInstance.MonitorUnitName = newTradeInstance.MonitorUnitName;
                        tradeInstance.TemplateId = newTradeInstance.TemplateId;
                        tradeInstance.TemplateName = newTradeInstance.TemplateName;
                        tradeInstance.Notes = newTradeInstance.Notes;
                    }
                    else
                    {
                        //Failure
                    }
                }
                dialog.Dispose();
            }
            else
            {
                dialog.Dispose();
            }
        }

        #endregion
    }
}
