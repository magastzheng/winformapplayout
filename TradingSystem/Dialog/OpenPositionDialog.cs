using DBAccess;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Forms;
using Model.config;
using BLL.TradeCommand;

namespace TradingSystem.Dialog
{
    public partial class OpenPositionDialog : Forms.BaseFixedForm
    {
        //private TradingInstanceDAO _tradeinstdao = new TradingInstanceDAO();
        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();

        public OpenPositionDialog()
        {
            InitializeComponent();

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);

            this.ckbInstanceCode.CheckedChanged += new EventHandler(CheckBox_InstanceCode_CheckedChanged);
            this.btnCancel.Click += new EventHandler(Button_Cancel_Click);
            this.btnConfirm.Click += new EventHandler(Button_Confirm_Click);
        }

        private void CheckBox_InstanceCode_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == null || !(sender is CheckBox))
                return;
            CheckBox ckb = sender as CheckBox;
            if (ckb.Checked)
            {
                cbInstanceCode.Enabled = true;
            }
            else
            {
                cbInstanceCode.Enabled = false;
            }
        }

        #region load control

        private bool Form_LoadControl(object sender, object data)
        {
            if (!ckbInstanceCode.Checked)
            {
                cbInstanceCode.Enabled = false;
            }
            else
            {
                cbInstanceCode.Enabled = true;
            }

            return true;
        }

        #endregion

        #region load data

        private bool Form_LoadData(object sender, object data)
        {
            if (data == null)
                return false;
            if (!(data is OpenPositionItem))
                return false;
            var openItem = data as OpenPositionItem;

            //
            this.tbPortfolio.Text = openItem.PortfolioName;
            this.tbTemlate.Text = string.Format("{0}-{1}", openItem.TemplateId, openItem.TemplateName);
            this.tbFutures.Text = openItem.FuturesContract;
            this.tbCopies.Text = string.Format("{0}", openItem.Copies);
            this.tbCopies.Enabled = false;
            this.tbBias.Text = "0";

            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(now.Year, now.Month, now.Day, 9, 15, 0);
            DateTime endDate = new DateTime(now.Year, now.Month, now.Day, 15, 15, 0);
            this.tbStartDate.Text = startDate.ToString("yyyyMMdd");
            this.tbEndDate.Text = endDate.ToString("yyyyMMdd");
            this.tbStartTime.Text = startDate.ToString("hhmmss");
            this.tbEndTime.Text = endDate.ToString("hhmmss");

            //Initialize the instancecode
            var instances = _tradeInstanceBLL.GetAllInstance();
            var targetInstances = instances.Where(p => p.MonitorUnitId == openItem.MonitorId && p.TemplateId == openItem.TemplateId).ToList();
            
            ComboOption comboOption = new ComboOption
            {
                Items = new List<ComboOptionItem>()
            };

            ComboOptionItem currentItem = new ComboOptionItem
            {
                Id = openItem.InstanceCode,
                Name = openItem.InstanceCode
            };

            comboOption.Items.Add(currentItem);

            if (targetInstances != null && targetInstances.Count > 0)
            {
                foreach (var instance in targetInstances)
                {
                    ComboOptionItem item = new ComboOptionItem 
                    {
                        Id = instance.InstanceCode,
                        Name = instance.InstanceCode
                    };

                    comboOption.Items.Add(item);
                }

                comboOption.Selected = comboOption.Items[0].Id;
                ComboBoxUtil.SetComboBox(this.cbInstanceCode, comboOption);
            }
            return true;
        }

        #endregion

        #region button click event handler

        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion
    }
}
