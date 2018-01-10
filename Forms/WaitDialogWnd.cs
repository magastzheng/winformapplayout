using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public class WaitDialogWnd
    {
        WaitDailog loadingDialog;
        Thread loadThread;

        public void Show()
        {
            loadThread = new Thread(new ThreadStart(LoadingProcessEx));
            loadThread.Start();
        }

        public void Show(Form parent)
        {
            loadThread = new Thread(new ParameterizedThreadStart(LoadingProcessEx));
            loadThread.Start(parent);
        }

        public void Close()
        {
            if (loadingDialog != null)
            {
                loadingDialog.BeginInvoke(new System.Threading.ThreadStart(loadingDialog.CloseLoadingForm));
                loadingDialog = null;
                loadThread = null;
            }
        }

        private void LoadingProcessEx()
        {
            loadingDialog = new WaitDailog();
            loadingDialog.ShowDialog();
        }

        private void LoadingProcessEx(object parent)
        {
            Form parentForm = parent as Form;
            loadingDialog = new WaitDailog(parentForm);
            loadingDialog.ShowDialog();
        }
    }
}
