using System;
using System.Windows.Forms;

namespace TradingSystem.View
{
    public enum ImportType
    { 
        Replace = 0,
        Append = 1,
    }

    public partial class ImportOptionDialog : Forms.BaseFixedForm
    {
        private ImportType _importType = ImportType.Replace;
        public ImportType ImportType { get { return _importType; } }

        public ImportOptionDialog()
        {
            InitializeComponent();
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
