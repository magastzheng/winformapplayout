using BLL;
using Config;
using hundsun.t2sdk;
using TradingSystem.Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            //if (retCode == (int)ConnectionCode.Success)
            //{
                var gridConfig = ConfigManager.Instance.GetGridConfig();
                TradingSystem.View.TradingCommandForm mainForm = new TradingSystem.View.TradingCommandForm(gridConfig);
                MainController mainController = new MainController(mainForm, _loginController.T2SDKWrap);
                Program._s_mainfrmController = mainController;

                this._isExit = false;
                this.Close();
            //}
            //else
            //{
            //    WarnForm warnForm = new WarnForm();
            //    warnForm.Owner = this;
            //    //warnForm.Show();
            //    warnForm.ShowDialog();
            //    warnForm.UpdateText(ConfigManager.Instance.GetLabelConfig().GetErrorMessage(retCode));
            //}
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
