using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controls.ButtonContainer
{
    public partial class ButtonContainer : UserControl
    {
        public event EventHandler ButtonClick;

        List<Button> _buttonList = new List<Button>();

        public ButtonContainer()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (ButtonClick != null)
            {
                ButtonClick(sender, e);
            }
        }

        public void AddButton(Button button)
        {
            _buttonList.Add(button);

            button.Click += new EventHandler(Button_Click);
            flPanel.Controls.Add(button);
        }

        //public void AddButton(Button button, bool rightAlign)
        //{
        //    _buttonList.Add(button);

        //    button.Click += new EventHandler(Button_Click);
        //    button.Anchor = AnchorStyles.Right;
        //    flPanel.Controls.Add(button);
        //}
    }
}
