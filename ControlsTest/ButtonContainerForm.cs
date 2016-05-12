using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlsTest
{
    public partial class ButtonContainerForm : Form
    {
        public ButtonContainerForm()
        {
            InitializeComponent();
            buttonContainer1.ButtonClick += new EventHandler(ButtonContainer_ButtonClick);

            for (int i = 0, count = 10; i < count; i++)
            {
                Button button = new Button();
                button.Name = string.Format("Button-{0}", i);
                button.Text = string.Format("Button-Text-{0}", i);

                if (i < 8)
                {
                    buttonContainer1.AddButton(button);
                }
                else
                {
                    //buttonContainer1.AddButton(button, true);
                }
            }

            //Button button = new Button();
            //button.Name = "test1";
            //button.Text = "Test1";
            //buttonContainer1.AddButton(button);

            //Button button2 = new Button();
            //button2.Name = "test2";
            //button2.Text = "Test2";
            //buttonContainer1.AddButton(button2);

        }

        private void ButtonContainer_ButtonClick(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                Console.WriteLine(button.Text);
            }
        }
    }
}
