using BLL.Template;
using Config;
using Controls.Entity;
using Controls.GridView;
using DBAccess;
using Model.Binding.BindingUtil;
using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TradingSystem.Dialog;

namespace TradingSystem.View
{
    enum MonitorType 
    { 
        None = -1,
        New = 1,
        Modify = 2,
    }

    public partial class MonitorUnitForm : Forms.DefaultForm
    {
        //label
        private const string msgDeleteConfirm = "monitordeleteconfirm";
        private const string msgSelectSetting = "monitorselectsetting";
        private const string msgDeleteSelect = "monitordeleteselect";

        //grid view id
        private const string GridId = "monitorunitmanager";
        private const string BottomMenuId = "monitorunit";
        private const string ConfirmCancelId = "confirmcancel";

        private GridConfig _gridConfig = null;

        private MonitorUnitBLL _monitorUnitBLL = new MonitorUnitBLL();
        private SortableBindingList<MonitorUnit> _dataSource;
        MonitorType _monitorType = MonitorType.None;

        public MonitorUnitForm() : base()
        {
            InitializeComponent();
        }

        public MonitorUnitForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;
            
            //this.Load += new EventHandler(Form_Load);
            //this.FormActived += new FormActiveHandler(Form_LoadFormActived);
            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
            this.buttonContainer.ButtonClick += new EventHandler(ButtonContainer_ButtonClick);
            this.confirmCancelContainer.ButtonClick += new EventHandler(ButtonContainer_ButtonClick);
        }

        #region load controls

        private bool Form_LoadControl(object sender, object data)
        {
            TSDataGridViewHelper.AddColumns(this.dataGridView, _gridConfig.GetGid(GridId));
            Dictionary<string, string> colDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(MonitorUnit));
            TSDataGridViewHelper.SetDataBinding(this.dataGridView, colDataMap);

            LoadBottomButton();

            return true;
        }

        private void LoadBottomButton()
        {
            ButtonGroup bottomButtonGroup = ConfigManager.Instance.GetButtonConfig().GetButtonGroup(BottomMenuId);
            buttonContainer.AddButtonGroup(bottomButtonGroup);

            ButtonGroup confirmCancelGroup = ConfigManager.Instance.GetButtonConfig().GetButtonGroup(ConfirmCancelId);
            confirmCancelContainer.AddButtonGroup(confirmCancelGroup);
        }

        #endregion

        #region load data

        private bool Form_LoadData(object sender, object data)
        {
            //Load data here
            var monitorUnits = _monitorUnitBLL.GetAll();
            _dataSource = new SortableBindingList<MonitorUnit>(monitorUnits);
            dataGridView.DataSource = _dataSource;

            return true;
        }

        #endregion

        #region button event

        private void ButtonContainer_ButtonClick(object sender, EventArgs e)
        {
            if (!(sender is Button))
            {
                return;
            }

            Button button = sender as Button;
            switch (button.Name)
            {
                case "SelectAll":
                    {
                        dataGridView.SelectAll(true);
                    }
                    break;
                case "UnSelect":
                    {
                        dataGridView.SelectAll(false);
                    }
                    break;
                case "Add":
                    {
                        _monitorType = MonitorType.New;
                        MonitorUnitDialog dialog = new MonitorUnitDialog();
                        dialog.Owner = this;
                        dialog.StartPosition = FormStartPosition.CenterParent;
                        //dialog.OnLoadFormActived(json);
                        //dialog.Visible = true;
                        dialog.OnLoadControl(dialog, null);
                        dialog.OnLoadData(dialog, null);
                        dialog.SaveData += new FormSaveHandler(Dialog_SaveData);
                        dialog.ShowDialog();

                        if (dialog.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            dialog.Dispose();
                        }
                        else
                        {
                            dialog.Dispose();
                        }
                    }
                    break;
                case "Delete":
                    {
                        List<int> selectIndex = TSDataGridViewHelper.GetSelectRowIndex(dataGridView);
                        if (selectIndex == null && selectIndex.Count == 0)
                        {
                            MessageDialog.Warn(this, msgDeleteSelect);
                            return;
                        }

                        string format = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgDeleteConfirm);
                        string msg = string.Format(format, selectIndex.Count);
                        if (MessageDialog.Warn(this, msg, MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                        {
                            return;
                        }

                        for (int i = selectIndex.Count - 1; i >= 0; i--)
                        {
                            int rowIndex = selectIndex[i];
                            if (rowIndex >= 0 && rowIndex < _dataSource.Count)
                            {
                                MonitorUnit monitorUnit = _dataSource[rowIndex];
                                int ret = _monitorUnitBLL.Delete(monitorUnit.MonitorUnitId);
                                if (ret > 0)
                                {
                                    _dataSource.RemoveAt(rowIndex);
                                }
                            }
                        }
                    }
                    break;
                case "Modify":
                    {
                        _monitorType = MonitorType.Modify;
                        if (dataGridView.CurrentRow == null)
                        {
                            return;
                        }

                        int index = dataGridView.CurrentRow.Index;
                        if (index < 0 || index > _dataSource.Count)
                        {
                            return;
                        }

                        MonitorUnit monitorUnit = _dataSource[index];

                        MonitorUnitDialog dialog = new MonitorUnitDialog();
                        dialog.Owner = this;
                        dialog.StartPosition = FormStartPosition.CenterParent;
                        //dialog.OnLoadFormActived(json);
                        //dialog.Visible = true;
                        dialog.OnLoadControl(dialog, null);
                        dialog.OnLoadData(dialog, monitorUnit);
                        dialog.SaveData += new FormSaveHandler(Dialog_SaveData);
                        dialog.ShowDialog();

                        if (dialog.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            dialog.Close();
                            dialog.Dispose();
                        }
                        else
                        {
                            dialog.Close();
                            dialog.Dispose();
                        }
                    }
                    break;
                case "Refresh":
                    {
                        _dataSource.Clear();
                        var monitorUnits = _monitorUnitBLL.GetAll();
                        if (monitorUnits != null)
                        {
                            foreach (var item in monitorUnits)
                            {
                                _dataSource.Add(item);
                            }
                        }
                    }
                    break;
                case "Confirm":
                    {
                        List<int> selectIndex = TSDataGridViewHelper.GetSelectRowIndex(dataGridView);
                        if (selectIndex == null && selectIndex.Count == 0)
                        {
                            MessageDialog.Warn(this, msgSelectSetting);
                            return;
                        }

                        for (int rowIndex = 0; rowIndex < _dataSource.Count; rowIndex++)
                        {
                            MonitorUnit monitorUnit = _dataSource[rowIndex];
                            if (selectIndex.Contains(rowIndex))
                            {

                                int ret = _monitorUnitBLL.Active(monitorUnit.MonitorUnitId, MonitorUnitStatus.Active);
                            }
                            else
                            {
                                int ret = _monitorUnitBLL.Active(monitorUnit.MonitorUnitId, MonitorUnitStatus.Inactive);
                            }
                        }
                    }
                    break;
                case "Cancel":
                    break;
            }
        }

        private bool Dialog_SaveData(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new Exception("Fail to get the setting from dialog");
            }

            if (!(data is MonitorUnit))
            {
                return false;
            }

            MonitorUnit monitorUnit = data as MonitorUnit;
            switch (_monitorType)
            {
                case MonitorType.New:
                    {
                        int newid = _monitorUnitBLL.Create(monitorUnit);
                        if (newid > 0)
                        {
                            monitorUnit.MonitorUnitId = newid;
                            _dataSource.Add(monitorUnit);
                        }
                    }
                    break;
                case MonitorType.Modify:
                    {
                        int newid = _monitorUnitBLL.Update(monitorUnit);
                        if (newid > 0)
                        {
                            for (int i = 0, count = _dataSource.Count; i < count; i++)
                            {
                                if (_dataSource[i].MonitorUnitId == newid)
                                {
                                    _dataSource[i] = monitorUnit;
                                    break;
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            return true;
        }

        #endregion
    }
}
