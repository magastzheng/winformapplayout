using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
using Model.Dialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Util;

namespace TradingSystem.Dialog
{
    public partial class OpenMultiPositionDialog : Forms.BaseDialog
    {
        private const string GridId = "openpositioninstance";

        private const string msgInvalidDate = "opendialogvaliddate";
        private const string msgInvalidTime = "opendialogvalidtime";

        private const string StartDate = "startdate";
        private const string EndDate = "enddate";
        private const string StartTime = "starttime";
        private const string EndTime = "endtime";

        private GridConfig _gridConfig;

        private SortableBindingList<OrderConfirmItem> _dataSource = new SortableBindingList<OrderConfirmItem>(new List<OrderConfirmItem>());


        public OpenMultiPositionDialog()
            :base()
        {
            InitializeComponent();
        }

        public OpenMultiPositionDialog(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;

            LoadControl += new FormLoadHandler(Form_LoadControl);
            LoadData += new FormLoadHandler(Form_LoadData);

            this.btnConfirm.Click += new EventHandler(Button_Confirm_Click);
            this.btnCancel.Click += new EventHandler(Button_Cancel_Click);
        }

        #region load control

        private bool Form_LoadControl(object sender, object data)
        {
            //Load Command Trading
            TSDataGridViewHelper.AddColumns(this.gridView, _gridConfig.GetGid(GridId));
            Dictionary<string, string> gridColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(OrderConfirmItem));
            TSDataGridViewHelper.SetDataBinding(this.gridView, gridColDataMap);
            this.gridView.DataSource = _dataSource;

            return true;
        }

        #endregion

        #region load data

        private bool Form_LoadData(object sender, object data)
        {
            if (data == null)
            {
                return false;
            }

            if (!(data is List<OrderConfirmItem>))
            {
                return false;
            }

            var ds = data as List<OrderConfirmItem>;
            ds.ForEach(p => _dataSource.Add(p));

            return true;
        }

        #endregion


        #region button click event handler

        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            if (!ValidateDate())
            {
                MessageDialog.Error(this, msgInvalidDate);

                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion

        #region GetData

        public override object GetData()
        {
            var dataList = new List<OrderConfirmItem>();
            foreach (var item in _dataSource)
            {
                dataList.Add(item);
            }

            return dataList;
        }

        #endregion

        private bool ValidateDate()
        {
            foreach (var item in _dataSource)
            {
                if (!DateUtil.IsValidDate(item.StartDate))
                {
                    return false;
                }

                if (!DateUtil.IsValidDate(item.EndDate))
                {
                    return false;
                }

                if (!DateUtil.IsValidTime(item.StartTime))
                {
                    return false;
                }

                if (!DateUtil.IsValidTime(item.EndTime))
                {
                    return false;
                }

                if (item.StartDate > item.EndDate)
                {
                    return false;
                }

                if (item.StartDate == item.EndDate && item.StartTime >= item.EndTime)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
