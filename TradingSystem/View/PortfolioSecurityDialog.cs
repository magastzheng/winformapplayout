using Controls.Entity;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TradingSystem.View
{
    public enum DialogType
    { 
        New = 0,
        Modify = 1,
    }

    public partial class PortfolioSecurityDialog : Forms.BaseFixedForm
    {
        private int _templateId = -1;
        public DialogType DialogType
        {
            set;
            get;
        }

        public PortfolioSecurityDialog()
        {
            InitializeComponent();

            this.btnConfirm.Click += new EventHandler(Button_Confirm_Click);
            this.btnCancel.Click += new EventHandler(Button_Cancel_Click);

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
        }

        #region button click event handler

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
        }
        
        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            TemplateStock stock = GetDialogData();
            if (Validate(stock))
            {
                OnSave(this, stock);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                //TODO: make the error message
                //DialogResult = System.Windows.Forms.DialogResult.No;
                MessageBox.Show(this, "证券代码不能为空", "错误", MessageBoxButtons.OK);
            }
        }

        private bool Validate(TemplateStock stock)
        {
            if (string.IsNullOrEmpty(stock.SecuCode))
            {
                return false;
            }

            return true;
        }
        #endregion

        private bool Form_LoadData(object sender, object data)
        {
            if (data != null && data is TemplateStock)
            {
                TemplateStock stock = data as TemplateStock;
                _templateId = stock.TemplateNo;
                InitControlData(stock);
            }

            return true;
        }

        private bool Form_LoadControl(object sender, object data)
        {
            if (data is DialogType)
            {
                DialogType = (DialogType)data;
                if (DialogType == View.DialogType.Modify)
                {
                    this.acSecurity.Enabled = false;
                }

                this.StartPosition = FormStartPosition.CenterParent;
            }

            acSecurity.SetDropdownList(this.lbDropdown);
            IList<AutoItem> dataSource = new List<AutoItem>();
            //foreach (var fcItem in itemList)
            //{
            //    AutoItem autoItem = new AutoItem
            //    {
            //        Id = fcItem.Code,
            //        Name = fcItem.Code
            //    };

            //    dataSource.Add(autoItem);
            //}
            acSecurity.AutoDataSource = dataSource;

            return true;
        }

        private void InitControlData(TemplateStock stock)
        {
            AutoItem autoItem = new AutoItem 
            {
                Id = stock.SecuCode,
                Name = stock.SecuName
            };
            acSecurity.SetCurrentItem(autoItem);

            tbAmount.Text = string.Format("{0}", stock.Amount);
            tbSettingWeight.Text = string.Format("{0}", stock.SettingWeight);
        }

        private TemplateStock GetDialogData()
        {
            TemplateStock stock = new TemplateStock
            {
                TemplateNo = _templateId
            };

            var autoItem = acSecurity.GetCurrentItem();
            stock.SecuCode = autoItem.Id;
            stock.SecuName = autoItem.Name;

            if (rdbAmount.Checked && !string.IsNullOrEmpty(tbAmount.Text))
            {
                int temp = -1;
                if (int.TryParse(tbAmount.Text, out temp))
                {
                    if (temp > 0)
                    {
                        temp = ((int)Math.Round((double)temp / 100)) * 100;
                    }
                    else
                    {
                        temp = 0;
                    }

                    stock.Amount = temp;
                }
            }

            if (rdbPercent.Checked && !string.IsNullOrEmpty(tbSettingWeight.Text))
            {
                double temp = 0.0f;
                if (double.TryParse(tbSettingWeight.Text, out temp))
                {
                    stock.SettingWeight = temp;
                }
            }

            return stock;
        }
    }
}
