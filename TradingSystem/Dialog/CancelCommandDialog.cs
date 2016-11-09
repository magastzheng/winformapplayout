using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TradingSystem.Dialog
{
    public partial class CancelCommandDialog : Forms.BaseDialog
    {
        public CancelCommandDialog()
        {
            InitializeComponent();

            this.btnConfirm.Click += new EventHandler(Button_Click_Confirm);
            this.btnCancel.Click += new EventHandler(Button_Click_Cancel);
        }

        #region click event handler

        private void Button_Click_Confirm(object sender, EventArgs e)
        {
            //TODO: validate the length beyond the limitation.
            //if (this.tbCause.Text.Length > 50)
            //{  
            //}

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Button_Click_Cancel(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion

        public override object GetData()
        {
            return this.tbCause.Text;
        }
    }
}
