using DBAccess;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Forms;
using Model.config;
using BLL.TradeCommand;
using Model.Constant;
using Util;
using Model.Dialog;

namespace TradingSystem.Dialog
{
    public partial class OpenPositionDialog : Forms.BaseDialog
    {
        //private TradingInstanceDAO _tradeinstdao = new TradingInstanceDAO();
        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();
        private OpenPositionItem _originOpenItem = null;
        //private OpenPositionItem _newOpenItem = null;

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
                MessageBox.Show(this, "开始日期和结束日期为yyyyMMdd格式，且结束日期不能早于开始日期！", "错误", MessageBoxButtons.OK);
                return;
            }

            if (!ValidateTime())
            {
                MessageBox.Show(this, "开始时间和结束时间为HHmmss格式，且结束时间不能早于开始时间！", "错误", MessageBoxButtons.OK);
                return;
            }

            var orderItem = GetNewItem();
            if (!ValidateInstanceCode(orderItem))
            {
                MessageBox.Show(this, "交易实例编号不能为空！", "错误", MessageBoxButtons.OK);
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
                FuturesContract = _originOpenItem.FuturesContract,
            };

            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            DateTime startTime = DateTime.MinValue;
            DateTime endTime = DateTime.MinValue;

            DateTime dt;
            if (DateUtil.IsValidDate(this.tbStartDate.Text.Trim(), ConstVariable.DateFormat1, out dt))
            {
                //newOpenItem.StartDate = DateFormat.Format(dt, ConstVariable.DateFormat1);
                startDate = dt;
            }

            if(DateUtil.IsValidDate(this.tbEndDate.Text.Trim(), ConstVariable.DateFormat1, out dt))
            {
                //newOpenItem.EndDate = DateFormat.Format(dt, ConstVariable.DateFormat1);
                endDate = dt;
            }

            if(DateUtil.IsValidDate(this.tbStartTime.Text.Trim(), ConstVariable.TimeFormat1, out dt))
            {
                //newOpenItem.StartTime = DateFormat.Format(dt, ConstVariable.TimeFormat1);
                startTime = dt;
            }

            if(DateUtil.IsValidDate(this.tbEndTime.Text.Trim(), ConstVariable.TimeFormat1, out dt))
            {
                //newOpenItem.EndTime = DateFormat.Format(dt, ConstVariable.TimeFormat1);
                endTime = dt;
            }

            DateTime now = DateTime.Now;
            if (startDate > DateTime.MinValue && startTime > DateTime.MinValue)
            {
                newOpenItem.StartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour, startTime.Minute, startTime.Second);
            }
            else
            {
                newOpenItem.StartDate = new DateTime(now.Year, now.Month, now.Day, 9, 15, 0); ;
            }

            if (endDate > DateTime.MinValue && endTime > DateTime.MinValue)
            {
                newOpenItem.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hour, endTime.Minute, endTime.Second);
            }
            else
            {
                newOpenItem.EndDate = new DateTime(now.Year, now.Month, now.Day, 15, 15, 0);
            }

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

            if (startDate > 0 && endDate > 0 && startDate < endDate)
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
