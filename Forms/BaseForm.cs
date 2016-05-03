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
    public partial class BaseForm : Form, ILoadFormActived, ISaveFormData
    {
        public delegate void FormActiveHandler(string json);
        public delegate void FormDataSaveHandler(object sender, object data);

        public event FormActiveHandler LoadFormActived;
        public event FormDataSaveHandler SaveFormData;

        public BaseForm()
        {
            InitializeComponent();
        }

        public virtual void OnLoadFormActived(string json)
        {
            if (LoadFormActived != null)
            { 
                LoadFormActived(json);
            }

        }

        public virtual void OnSave(object sender, object data)
        {
            if (SaveFormData != null)
            {
                SaveFormData(sender, data);
            }
        }
    }
}
