using TradingSystem.Controller;
using System;
using System.Windows.Forms;
using Model;
using Config;

namespace TradingSystem.View
{
    public partial class LoginForm : Form
    {
        private LoginController _loginController;
        public LoginController LoginController
        {
            set { _loginController = value; }
        }
        private bool _isExit = true;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void ButtonCancel_Click(object sender, System.EventArgs e)
        {
            this.Exit();
        }

        private void ButtonLogin_Click(object sender, System.EventArgs e)
        {
            Login();
        }

        private void LoginForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            this.Exit();
        }

        public void Login()
        {
            string userName = this.cmbOperatorNo.Text;
            string password = this.tbOperatorPwd.Text;
            Console.WriteLine("user: " + userName + " " + "Password: " + password);

            var retCode = _loginController.Login(userName, password);
            if (retCode == (int)ConnectionCode.Success)
            {
                this._isExit = false;
                this.Close();
            }
            else
            {
                WarnForm warnForm = new WarnForm();
                warnForm.Owner = this;
                warnForm.UpdateText(ConfigManager.Instance.GetLabelConfig().GetErrorMessage(retCode));
                warnForm.ShowDialog();
            }
        }

        public void Exit()
        {
            if (this._isExit)
            {
                Application.Exit();
            }
        }
    }
}
