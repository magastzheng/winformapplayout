using Model.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Forms;
using Model.config;
using Model.Constant;
using Util;
using Model.Dialog;
using BLL.TradeInstance;

namespace TradingSystem.Dialog
{
    public partial class OpenPositionDialog : Forms.BaseDialog
    {
        private const string msgValidDate = "opendialogvaliddate";
        private const string msgValidTime = "opendialogvalidtime";
        private const string msgInstanceCodeNoEmpty = "opendialoginstancecodenoempty";

        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();
        private OpenPositionItem _originOpenItem = null;

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
            _originOpenItem = data as OpenPositionItem;
            if (_originOpenItem == null)
                return false;

            //
            this.tbPortfolio.Text = _originOpenItem.PortfolioName;
            this.tbTemlate.Text = string.Format("{0}-{1}", _originOpenItem.TemplateId, _originOpenItem.TemplateName);
            this.tbFutures.Text = _originOpenItem.FuturesContract;
            this.tbCopies.Text = string.Format("{0}", _originOpenItem.Copies);
            this.tbCopies.Enabled = false;
            this.tbBias.Text = "0";

            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(now.Year, now.Month, now.Day, 9, 15, 0);
            DateTime endDate = new DateTime(now.Year, now.Month, now.Day, 15, 15, 0);
            this.tbStartDate.Text = DateFormat.Format(startDate, ConstVariable.DateFormat1);
            this.tbEndDate.Text = DateFormat.Format(endDate, ConstVariable.DateFormat1);
            this.tbStartTime.Text = DateFormat.Format(startDate, ConstVariable.TimeFormat1);
            this.tbEndTime.Text = DateFormat.Format(endDate, ConstVariable.TimeFormat1);

            //Initialize the instancecode
            var instances = _tradeInstanceBLL.GetAllInstance();
            var targetInstances = instances.Where(p => p.MonitorUnitId == _originOpenItem.MonitorId && p.TemplateId == _originOpenItem.TemplateId).ToList();
            
            ComboOption comboOption = new ComboOption
            {
                Items = new List<ComboOptionItem>()
            };

            if (targetInstances == null || targetInstances.Find(p => p.InstanceCode.Equals(_originOpenItem.InstanceCode)) == null)
            {
                ComboOptionItem currentItem = new ComboOptionItem
                {
                    Id = _originOpenItem.InstanceCode,
                    Name = _originOpenItem.InstanceCode
                };

                comboOption.Items.Add(currentItem);
            }
            
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
            }

            ComboBoxUtil.SetComboBox(this.cbInstanceCode, comboOption);
            ComboBoxUtil.SetComboBoxSelect(this.cbInstanceCode, _originOpenItem.InstanceCode);

            return true;
        }

        #endregion

        #region button click event handler

        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            if (!ValidateDate())
            {
                MessageDialog.Error(this, msgValidDate);
                return;
            }

            if (!ValidateTime())
            {
                MessageDialog.Error(this, msgValidTime);
                return;
            }

            var orderItem = GetNewItem();
            if (!ValidateInstanceCode(orderItem))
            {
                MessageDialog.Error(this, msgInstanceCodeNoEmpty);
                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private OrderConfirmItem GetNewItem()
        {
            var newOpenItem = new OrderConfirmItem
            {
                MonitorId = _originOpenItem.MonitorId,
                MonitorName = _originOpenItem.MonitorName,
                PortfolioId = _originOpenItem.PortfolioId,
                PortfolioName = _originOpenItem.PortfolioName,
                TemplateId = _originOpenItem.TemplateId,
                TemplateName = _originOpenItem.TemplateName,
                Copies = _originOpenItem.Copies,
                FuturesList = new List<string>() { _originOpenItem.FuturesContract },
            };

            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            DateTime startTime = DateTime.MinValue;
            DateTime endTime = DateTime.MinValue;

            DateTime dt;
            if (DateUtil.IsValidDate(this.tbStartDate.Text.Trim(), ConstVariable.DateFormat1, out dt))
            {
                startDate = dt;
            }

            if(DateUtil.IsValidDate(this.tbEndDate.Text.Trim(), ConstVariable.DateFormat1, out dt))
            {
                endDate = dt;
            }

            if(DateUtil.IsValidDate(this.tbStartTime.Text.Trim(), ConstVariable.TimeFormat1, out dt))
            {
                startTime = dt;
            }

            if(DateUtil.IsValidDate(this.tbEndTime.Text.Trim(), ConstVariable.TimeFormat1, out dt))
            {
                endTime = dt;
            }

            newOpenItem.StartDate = Convert.ToInt32(this.tbStartDate.Text.Trim());
            newOpenItem.EndDate = Convert.ToInt32(this.tbEndDate.Text.Trim());
            newOpenItem.StartTime = Convert.ToInt32(this.tbStartTime.Text.Trim());
            newOpenItem.EndTime = Convert.ToInt32(this.tbEndTime.Text.Trim());

            if (this.ckbInstanceCode.Checked)
            {
                var selectItem = (ComboOptionItem)this.cbInstanceCode.SelectedItem;
                if (selectItem != null)
                {
                    newOpenItem.InstanceCode = selectItem.Name;
                }
                else if (!string.IsNullOrEmpty(this.cbInstanceCode.Text))
                {
                    newOpenItem.InstanceCode = this.cbInstanceCode.Text.Trim();
                }
                else
                { 
                    //do nothing
                }
            }
            else
            {
                newOpenItem.InstanceCode = _originOpenItem.InstanceCode;
            }

            return newOpenItem;
        }

        public bool ValidateDate()
        {
            DateTime dt;

            int startDate = 0;
            int endDate = 0;

            if (DateUtil.IsValidDate(this.tbStartDate.Text.Trim(), ConstVariable.DateFormat1, out dt))
            {
                startDate = DateUtil.GetIntDate(dt);
            }

            if (DateUtil.IsValidDate(this.tbEndDate.Text.Trim(), ConstVariable.DateFormat1, out dt))
            {
                endDate = DateUtil.GetIntDate(dt);
            }

            if (startDate > 0 && endDate > 0 && startDate <= endDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateTime()
        {
            DateTime dt;

            int startTime = 0;
            int endTime = 0;

            if (DateUtil.IsValidDate(this.tbStartTime.Text.Trim(), ConstVariable.TimeFormat1, out dt))
            {
                startTime = DateUtil.GetIntTime(dt);
            }

            if (DateUtil.IsValidDate(this.tbEndTime.Text.Trim(), ConstVariable.TimeFormat1, out dt))
            {
                endTime = DateUtil.GetIntTime(dt);
            }

            if (startTime > 0 && endTime > 0 && startTime < endTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateInstanceCode(OrderConfirmItem item)
        {
            if (!string.IsNullOrEmpty(item.InstanceCode))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Get output data

        public override object GetData()
        {
            var orderItem = GetNewItem();
            return orderItem;
        }

        #endregion
    }
}
