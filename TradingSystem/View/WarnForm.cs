using System;
using System.Windows.Forms;

namespace TradingSystem.View
{
    public partial class WarnForm : Form
    {
        private delegate void OnUpdateText(string text);
        public WarnForm()
        {
            InitializeComponent();

            //this.rtBoxMessage.Text = "登录失败";
        }

        #region

        public void UpdateText(string text)
        {
            //this.BeginInvoke(new OnUpdateText(UpdateRichTextBox), text);
            //UpdateRichTextBox(text);
            this.rtBoxMessage.Text = text;
            //if (IsHandleCreated)
            //{
            //    this.Invoke(new Action<string>(
            //            delegate(string message)
            //            {
            //                try
            //                {
            //                    UpdateRichTextBox(message);
            //                }
            //                catch (Exception e)
            //                {

            //                }
            //            }), text);
            //}
            //this.Invoke(new Action<string>());
        }

        private void UpdateRichTextBox(string message)
        {
            //this.rtBoxMessage.Text = message;
        }
        #endregion

        #region event handle
        private void ButtonConfirm_Click(object sender, System.EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion
    }
}
