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
    public partial class MonitorUnitForm : Forms.DefaultForm
    {
        private GridConfig _gridConfig = null;
        private const string GridId = "monitorunitmanager";
        private const string BottomMenuId = "monitorunit";
        private const string ConfirmCancelId = "confirmcancel";
        private MonitorUnitDAO _dbdao = new MonitorUnitDAO();
        private SortableBindingList<MonitorUnit> _dataSource;

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
            List<MonitorUnit> monitorUnits = _dbdao.Get(-1);
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
                        break;
                    case "UnSelect":
                        break;
                    case "Add":
                        {
                            MonitorUnitDialog dialog = new MonitorUnitDialog();
                            dialog.Owner = this;
                            dialog.StartPosition = FormStartPosition.CenterParent;
                            //dialog.OnLoadFormActived(json);
                            //dialog.Visible = true;
                            dialog.OnLoadControl(dialog, null);
                            dialog.OnLoadData(dialog, null);
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
                        break;
                    case "Modify":
                        break;
                    case "Confirm":
                        break;
                    case "Cancel":
                        break;
                }
            }
        }

        #endregion
    }
}
