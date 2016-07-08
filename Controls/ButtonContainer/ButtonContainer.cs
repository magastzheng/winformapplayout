using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Controls.Entity;

namespace Controls.ButtonContainer
{
    public partial class ButtonContainer : UserControl
    {
        public event EventHandler ButtonClick;

        List<Button> _buttonList = new List<Button>();

        public ButtonContainer()
        {
            InitializeComponent();
            this.flPanel.FlowDirection = FlowDirection.BottomUp;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (ButtonClick != null)
            {
                ButtonClick(sender, e);
            }
        }

        public void AddButtonGroup(ButtonGroup buttonGroup)
        {
            switch (buttonGroup.Direction)
            { 
                case LayoutDirection.RightToLeft:
                    this.flPanel.FlowDirection = FlowDirection.RightToLeft;
                    break;
                case LayoutDirection.LeftToRight:
                    this.flPanel.FlowDirection = FlowDirection.LeftToRight;
                    break;
            }

            foreach (ButtonItem buttonItem in buttonGroup.Buttons)
            {
                if (buttonItem.Visible)
                {
                    Button button = new Button();
                    button.Name = buttonItem.Name;
                    button.Text = buttonItem.Text;

                    AddButton(button);
                }
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
