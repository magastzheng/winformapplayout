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
        public static string TitleError = "errortitle";
        public static string TitleFail = "failtitle";
        public static string TitleWarn = "warntitle";
        public static string TitlePrompt = "prompttitle";

        public static DialogResult Error(Form form, string msgId)
        {
            return Show(form, TitleError, msgId, MessageBoxButtons.OK, MessageBoxIcon.Error); 
        }

        public static DialogResult Error(Form form, string msgId, MessageBoxButtons msgBoxButtons)
        {
            return Show(form, TitleError, msgId, msgBoxButtons, MessageBoxIcon.Error);
        }

        public static DialogResult Fail(Form form, string msgId)
        {
            return Show(form, TitleFail, msgId, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Warn(Form form, string msgId)
        {
            return Show(form, TitleWarn, msgId, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult Warn(Form form, string msgId, MessageBoxButtons msgBoxButtons)
        {
            return Show(form, TitleWarn, msgId, msgBoxButtons, MessageBoxIcon.Warning);
        }

        public static DialogResult Info(Form form, string msgId)
        {
            return Show(form, TitlePrompt, msgId, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Info(Form form, string msgId, MessageBoxButtons msgBoxButtons)
        {
            return Show(form, TitlePrompt, msgId, msgBoxButtons, MessageBoxIcon.Information);
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
