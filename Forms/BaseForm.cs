using System.Windows.Forms;

namespace Forms
{
    public partial class BaseForm : Form, IFormActived, IFormLeave, ISaveData, ILoadControl, ILoadData
    {
        public delegate void FormActiveHandler(string json);
        public delegate bool FormLeaveHandler(object sender, object data);
        public delegate bool FormLoadHandler(object sender, object data);
        public delegate bool FormSaveHandler(object sender, object data);

        public event FormLoadHandler LoadControl;
        public event FormLoadHandler LoadData;
        public event FormActiveHandler FormActived;
        public event FormLeaveHandler FormLeave;
        public event FormSaveHandler SaveData;

        public BaseForm()
        {
            InitializeComponent();
        }

        public virtual void OnFormActived(string json)
        {
            if (FormActived != null)
            { 
                FormActived(json);
            }

        }

        /// <summary>
        /// 切换窗体时调用，默认返回true。如果实现该事件，可以返回预期想要的结果。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool OnFormLeave(object sender, object data)
        {
            if (FormLeave != null)
            {
                return FormLeave(sender, data);
            }

            return true;
        }

        public virtual bool OnSave(object sender, object data)
        {
            if (SaveData != null)
            {
                return SaveData(sender, data);
            }

            return false;
        }

        public void OnLoadControl(object sender, object data)
        {
            if (LoadControl != null)
            {
                LoadControl(sender, data);
            }
        }

        public void OnLoadData(object sender, object data)
        {
            if (LoadData != null)
            {
                LoadData(sender, data);
            }
        }
    }
}
