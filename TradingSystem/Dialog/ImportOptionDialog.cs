﻿using Config;
using System;
using System.Windows.Forms;

namespace TradingSystem.Dialog
{
    public enum ImportType
    { 
        Replace = 0,
        Append = 1,
    }

    public partial class ImportOptionDialog : Forms.BaseDialog
    {
        private ImportType _importType = ImportType.Replace;
        public ImportType ImportType { get { return _importType; } }

        public ImportOptionDialog()
        {
            InitializeComponent();

            this.Load += new EventHandler(Dialog_Load);
            this.LoadControl += new FormLoadHandler(Dialog_LoadControl);
        }

        private void Dialog_Load(object sender, EventArgs e)
        {
            OnLoadControl(null, null);
            OnLoadData(null, null);
        }

        private bool Dialog_LoadControl(object sender, object data)
        {
            string text = ConfigManager.Instance.GetLabelConfig().GetLabelText("importdialognotes");
            this.rtbImportDesc.Text = text;

            return true;
        }

        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            OnSave(this, _importType);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (!rb.Checked)
            {
                return;
            }

            switch (rb.Name)
            { 
                case "rbAppend":
                    _importType = ImportType.Append;
                    break;
                case "rbReplace":
                    _importType = ImportType.Replace;
                    break;
                default:
                    break;
            }
        }
    }
}
