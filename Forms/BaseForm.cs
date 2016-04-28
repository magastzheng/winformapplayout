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
    public partial class BaseForm : Form, ILoadFormActived
    {
        public delegate void FormActiveHandler(string json);

        public event FormActiveHandler LoadFormActived;

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
    }
}
