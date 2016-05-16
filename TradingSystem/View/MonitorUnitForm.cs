using Config;
using Controls.Entity;
using Controls.GridView;
using DBAccess;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
        private GridConfig _gridConfig = null;
        private const string GridId = "monitorunitmanager";
        private const string BottomMenuId = "monitorunit";
        private const string ConfirmCancelId = "confirmcancel";
        private MonitorUnitDAO _dbdao = new MonitorUnitDAO();
        //List<MonitorUnit> _monitorUnits;
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
            
            this.Load += new EventHandler(Form_Load);
            this.FormActived += new FormActiveHandler(Form_LoadFormActived);
            this.buttonContainer.ButtonClick += new EventHandler(ButtonContainer_ButtonClick);
            this.confirmCancelContainer.ButtonClick += new EventHandler(ButtonContainer_ButtonClick);
            LoadBottomButton();
        }

        #region load 

        private void Form_LoadFormActived(string json)
        {
            //Load data here
            var monitorUnits = _dbdao.Get(-1);
            _dataSource = new SortableBindingList<MonitorUnit>(monitorUnits);
            dataGridView.DataSource = _dataSource;
            
        }

        private void Form_Load(object sender, EventArgs e)
        {
            TSDataGridViewHelper.AddColumns(this.dataGridView, _gridConfig.GetGid(GridId));
            Dictionary<string, string> colDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(MonitorUnit));
            TSDataGridViewHelper.SetDataBinding(this.dataGridView, colDataMap);
        }

        private void LoadBottomButton()
        {
            ButtonGroup bottomButtonGroup = ConfigManager.Instance.GetButtonConfig().GetButtonGroup(BottomMenuId);
            buttonContainer.AddButtonGroup(bottomButtonGroup);

            ButtonGroup confirmCancelGroup = ConfigManager.Instance.GetButtonConfig().GetButtonGroup(ConfirmCancelId);
            confirmCancelContainer.AddButtonGroup(confirmCancelGroup);
        }

        #endregion

        #region button event

        private void ButtonContainer_ButtonClick(object sender, EventArgs e)
        {
            if (sender is Button)
            {
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
                            dialog.SaveData += new FormLoadHandler(Dialog_SaveData);
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
                            if (selectIndex != null && selectIndex.Count > 0)
                            {
                                for (int i = selectIndex.Count - 1; i >= 0; i--)
                                {
                                    MonitorUnit monitorUnit = _dataSource[i];
                                    int ret = _dbdao.Delete(monitorUnit.MonitorUnitId);
                                    if (ret > 0)
                                    {
                                        _dataSource.RemoveAt(i);
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
                            dialog.SaveData += new FormLoadHandler(Dialog_SaveData);
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
                            var monitorUnits = _dbdao.Get(-1);
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
                        break;
                    case "Cancel":
                        break;
                }
            }
        }

        private void Dialog_SaveData(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new Exception("Fail to get the setting from dialog");
            }

            if (data is MonitorUnit)
            {
                MonitorUnit monitorUnit = data as MonitorUnit;
                switch (_monitorType)
                {
                    case MonitorType.New:
                        {
                            int newid = _dbdao.Create(monitorUnit);
                            if (newid > 0)
                            {
                                _dataSource.Add(monitorUnit);
                            }
                        }
                        break;
                    case MonitorType.Modify:
                        {
                            int newid = _dbdao.Update(monitorUnit);
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
                
            }
        }

        #endregion
    }
}
