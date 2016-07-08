using System.Windows.Forms;

namespace Forms
{
    public partial class BaseForm : Form, IFormActived, ISaveData, ILoadControl, ILoadData
    {
        public delegate void FormActiveHandler(string json);
        public delegate bool FormLoadHandler(object sender, object data);

        public event FormLoadHandler LoadControl;
        public event FormLoadHandler LoadData;
        public event FormActiveHandler FormActived;
        public event FormLoadHandler SaveData;

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
