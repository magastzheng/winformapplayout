using Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingSystem.Dialog
{
    public sealed class MessageDialog
    {
        public static DialogResult Error(Form form, string msgId)
        { 
            return Show(form, msgId, "errortitle", MessageBoxButtons.OK, MessageBoxIcon.Error); 
        }

        public static DialogResult Error(Form form, string msgId, MessageBoxButtons msgBoxButtons)
        {
            return Show(form, msgId, "errortitle", msgBoxButtons, MessageBoxIcon.Error);
        }

        public static DialogResult Fail(Form form, string msgId)
        {
            return Show(form, msgId, "failtitle", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Warn(Form form, string msgId)
        {
            return Show(form, msgId, "warntitle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult Warn(Form form, string msgId, MessageBoxButtons msgBoxButtons)
        {
            return Show(form, msgId, "warntitle", msgBoxButtons, MessageBoxIcon.Warning);
        }

        public static DialogResult Info(Form form, string msgId)
        {
            return Show(form, msgId, "prompttitle", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Info(Form form, string msgId, MessageBoxButtons msgBoxButtons)
        {
            return Show(form, msgId, "prompttitle", msgBoxButtons, MessageBoxIcon.Information);
        }

        private static DialogResult Show(Form form, string msgId, string titleId, MessageBoxButtons msgBoxButtons, MessageBoxIcon msgBoxIcon)
        {
            string title = ConfigManager.Instance.GetLabelConfig().GetLabelText(titleId);
            string text = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgId);
            if (string.IsNullOrEmpty(text))
            {
                text = titleId;
            }

            return MessageBox.Show(form, text, title, msgBoxButtons, msgBoxIcon); 
        }
    }
}
