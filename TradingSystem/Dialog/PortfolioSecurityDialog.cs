using BLL.Manager;
using BLL.SecurityInfo;
using Calculation;
using Controls.Entity;
using Model.SecurityInfo;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TradingSystem.Dialog
{
    public enum DialogType
    { 
        New = 0,
        Modify = 1,
    }

    public partial class PortfolioSecurityDialog : Forms.BaseDialog
    {
        private const string msgSecuCodeNoEmpty = "securitydialogcodenoempty";
        private const string msgInputInvalid = "securitydialoginputinvalid";
        private const string msgWeightError = "securitydialogweighterror";

        private int _templateId = -1;
        private IList<AutoItem> _dataSource = new List<AutoItem>();
        private List<SecurityItem> _securityInfoList = new List<SecurityItem>();

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

            this.rdbAmount.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
            this.rdbPercent.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);

            this.tbAmount.KeyPress += new KeyPressEventHandler(TextBox_KeyPress_Int);
            this.tbSettingWeight.KeyPress += new KeyPressEventHandler(TextBox_KeyPress_Float);
        }

        #region button click event handler

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
        }
        
        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

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
                MessageDialog.Error(this, msgSecuCodeNoEmpty);
            }
        }

        private bool ValidateInput()
        {
            //整型
            Regex re = new Regex(@"^\d+$");
            //浮点型
            Regex re2 = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            if (rdbAmount.Checked)
            {
                if(string.IsNullOrEmpty(tbAmount.Text))
                {
                    MessageDialog.Error(this, msgSecuCodeNoEmpty);
                    return false;
                }

                if (!re.IsMatch(tbAmount.Text))
                {
                    MessageDialog.Error(this, msgInputInvalid);
                    return false;
                }
            }

            if (rdbPercent.Checked)
            {
                if (string.IsNullOrEmpty(tbSettingWeight.Text))
                {
                    MessageDialog.Error(this, msgSecuCodeNoEmpty);
                    return false;
                }

                if (!re2.IsMatch(tbSettingWeight.Text))
                {
                    MessageDialog.Error(this, msgInputInvalid);
                    return false;
                }
            }

            return true;
        }

        private bool Validate(TemplateStock stock)
        {
            if (string.IsNullOrEmpty(stock.SecuCode))
            {
                MessageDialog.Error(this, msgSecuCodeNoEmpty);
                return false;
            }

            if (stock.SettingWeight < -0.00001 || stock.SettingWeight > 100.0001)
            {
                MessageDialog.Error(this, msgWeightError);
                return false;
            }

            return true;
        }
        #endregion

        #region RadioButton CheckedChanged

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton))
                return;
            RadioButton rdb = (RadioButton)sender;
            switch (rdb.Name)
            { 
                case "rdbAmount":
                    tbAmount.Enabled = rdb.Checked;
                    break;
                case "rdbPercent":
                    tbSettingWeight.Enabled = rdb.Checked;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region TextBox numeric

        private void TextBox_KeyPress_Int(object sender, KeyPressEventArgs e)
        {
            if (!(sender is TextBox))
                return;

            TextBox textBox = (TextBox)sender;

            if (System.Text.Encoding.Default.GetBytes(e.KeyChar.ToString()).Length == 2)
            {
                e.Handled = true;
                return;
            }
               
            if (Char.IsDigit(e.KeyChar))
            {
                // 判断输入的是否是数字 
                e.Handled = false;
            }
            else if (e.KeyChar == (Char)Keys.Back)
            {
                //退格键
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            return;
        }

        private void TextBox_KeyPress_Float(object sender, KeyPressEventArgs e)
        {
            if (!(sender is TextBox))
                return;

            TextBox textBox = (TextBox)sender;
            bool dotExisted = textBox.Text.Contains(".");

            if (System.Text.Encoding.Default.GetBytes(e.KeyChar.ToString()).Length == 2)
            {
                e.Handled = true;
                return;
            }
            // 判断输入的是否是数字    
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (!dotExisted && e.KeyChar == '.')
            { 
                //仅出现一次的小数点
                e.Handled = false;
            }
            else if (e.KeyChar == (Char)Keys.Back)
            {
                //退格键
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            return;
        }

        #endregion

        #region LoadData

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

        #endregion

        #region LoadControl

        private bool Form_LoadControl(object sender, object data)
        {
            if (data is DialogType)
            {
                DialogType = (DialogType)data;
                if (DialogType == DialogType.Modify)
                {
                    this.acSecurity.Enabled = false;
                }

                this.StartPosition = FormStartPosition.CenterParent;
            }

            acSecurity.SetDropdownList(this.lbDropdown);
            _securityInfoList = SecurityInfoManager.Instance.Get();
            foreach (var secuInfo in _securityInfoList)
            { 
                AutoItem autoItem = new AutoItem
                {
                    Id = secuInfo.SecuCode,
                    Name = secuInfo.SecuName,
                };

                _dataSource.Add(autoItem);
            }
            acSecurity.AutoDataSource = _dataSource;

            return true;
        }

        #endregion

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
                        temp = AmountRoundUtil.Round(temp);
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
