using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controls.ContextMenu
{
    public partial class ContextMenu : ContextMenuStrip
    {
        public ContextMenu()
        {
            InitializeComponent();
        }

        public void ShowMenu(Control control, Point position)
        {
            int width = this.Width;
            int height = this.Height;
            int right = control.Right;
            int bottom = control.Bottom;

            int x = position.X;
            int y = position.Y;

            if (x + width > right)
            {
                x = right - width;
            }

            if (y + height > bottom)
            {
                y = bottom - height;
            }

            Point p = new Point(x, y);
            Show(control, p);
        }
    }
}
