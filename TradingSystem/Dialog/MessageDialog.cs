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
        public static string CaptionError = "captionerror";
        public static string CaptionFail = "captionfail";
        public static string CaptionWarn = "captionwarn";
        public static string CaptionPrompt = "captionprompt";

        public static DialogResult Error(Form form, string msgId)
        {
            return Show(form, msgId, CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error); 
        }

        public static DialogResult Error(Form form, string msgId, MessageBoxButtons msgBoxButtons)
        {
            return Show(form, msgId, CaptionError, msgBoxButtons, MessageBoxIcon.Error);
        }

        public static DialogResult Fail(Form form, string msgId)
        {
            return Show(form, msgId, CaptionFail, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Warn(Form form, string msgId)
        {
            return Show(form, msgId, CaptionWarn, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult Warn(Form form, string msgId, MessageBoxButtons msgBoxButtons)
        {
            return Show(form, msgId, CaptionWarn, msgBoxButtons, MessageBoxIcon.Warning);
        }

        public static DialogResult Info(Form form, string msgId)
        {
            return Show(form, msgId, CaptionPrompt, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Info(Form form, string msgId, MessageBoxButtons msgBoxButtons)
        {
            return Show(form, msgId, CaptionPrompt, msgBoxButtons, MessageBoxIcon.Information);
        }

        private static DialogResult Show(Form form, string msgId, string CaptionId, MessageBoxButtons msgBoxButtons, MessageBoxIcon msgBoxIcon)
        {
            string caption = ConfigManager.Instance.GetLabelConfig().GetLabelText(CaptionId);
            string text = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgId);
            if (string.IsNullOrEmpty(text))
            {
                text = msgId;
            }

            return MessageBox.Show(form, text, caption, msgBoxButtons, msgBoxIcon); 
        }
    }
}
