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

namespace TradingSystem.Dialog
{
    public partial class CancelEntrustDialog : Forms.BaseDialog
    {
        private const string GridId = "entrustcancel";

        //private EntrustBLL _entrustBLL = new EntrustBLL();
        private WithdrawBLL _withdrawBLL = new WithdrawBLL();

        private SortableBindingList<CancelSecurityItem> _secuDataSource = new SortableBindingList<CancelSecurityItem>(new List<CancelSecurityItem>());
        //private List<EntrustCommandItem> _entrustCommandItems = new List<EntrustCommandItem>();

        GridConfig _gridConfig;

        public CancelEntrustDialog()
            :base()
        {
            InitializeComponent();
        }

        public CancelEntrustDialog(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;

            LoadControl += new FormLoadHandler(Form_LoadControl);
            LoadData += new FormLoadHandler(Form_LoadData);

            this.btnSelectAll.Click += new EventHandler(Button_SelectAll_Click);
            this.btnUnSelectAll.Click += new EventHandler(Button_UnSelectAll_Click);
            this.btnConfirm.Click += new EventHandler(Button_Confirm_Click);
            this.btnCancel.Click += new EventHandler(Button_Cancel_Click);
        }

        #region button click event handler

        private void Button_SelectAll_Click(object sender, EventArgs e)
        {
            _secuDataSource.Where(p => !p.Selection).ToList().ForEach(p => p.Selection = true);

            this.secuGridView.Invalidate();
        }

        private void Button_UnSelectAll_Click(object sender, EventArgs e)
        {
            _secuDataSource.Where(p => p.Selection).ToList().ForEach(p => p.Selection = false);

            this.secuGridView.Invalidate();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            string outMsg = string.Empty;
            var selectedItems = _secuDataSource.Where(p => p.Selection).ToList();
            if (selectedItems.Count == 0)
            {
                string msg = "没有勾选的证券";
                MessageBox.Show(this, msg, "警告", MessageBoxButtons.OK);
                return;
            }

            var failedCancelItems = new List<CancelSecurityItem>();
            var submitIds = _secuDataSource.ToList().Select(p => p.SubmitId).Distinct().ToList();
            foreach (var submitId in submitIds)
            {
                var submitCalcItems = _secuDataSource.Where(p => p.SubmitId == submitId).ToList();
                var commandIds = submitCalcItems.Select(p => p.CommandId).Distinct().ToList();
                if (commandIds != null && commandIds.Count > 0)
                {
                    foreach (var commandId in commandIds)
                    {
                        var calcSrcItems = submitCalcItems.Where(p => p.CommandId == commandId).ToList();
                        var calcResItems = _withdrawBLL.CancelSecuItem(submitId, commandId, calcSrcItems, null);
                        if (calcResItems.Count != calcSrcItems.Count)
                        {
                            var failItems = calcSrcItems.ToList().Except(calcResItems).ToList();
                            failedCancelItems.AddRange(failItems);
                        }
                    }
                }
            }

            if (failedCancelItems.Count > 0)
            {
                var failedSecuCode = failedCancelItems.Select(p => p.SecuCode).ToList();
                string msg = string.Join("|", failedSecuCode);
                msg = string.Format("部分证券撤单失败: {0}", msg);

                MessageBox.Show(this, msg, "警告", MessageBoxButtons.OK);
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            else
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }

        }

        #endregion

        #region load control

        private bool Form_LoadControl(object sender, object data)
        {
            //Load Command Trading
            TSDataGridViewHelper.AddColumns(this.secuGridView, _gridConfig.GetGid(GridId));
            Dictionary<string, string> gridColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(CancelSecurityItem));
            TSDataGridViewHelper.SetDataBinding(this.secuGridView, gridColDataMap);
            this.secuGridView.DataSource = _secuDataSource;


            return true;
        }

        #endregion

        #region loaddata

        private bool Form_LoadData(object sender, object data)
        {
            if (data == null)
            {
                return false;
            }

            if (!(data is List<CancelSecurityItem>))
            {
                return false;
            }

            _secuDataSource.Clear();

            var calcItems = data as List<CancelSecurityItem>;
            foreach (var calcItem in calcItems)
            {
                _secuDataSource.Add(calcItem);
            }

            return true;
        }

        #endregion
    }
}
