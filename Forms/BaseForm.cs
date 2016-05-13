using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class BaseForm : Form, IFormActived, ISaveData, ILoadControl, ILoadData
    {
        public delegate void FormActiveHandler(string json);
        public delegate void FormLoadHandler(object sender, object data);

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

        public virtual void OnSave(object sender, object data)
        {
            if (SaveData != null)
            {
                SaveData(sender, data);
            }
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
