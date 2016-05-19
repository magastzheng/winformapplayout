using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlsTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Application.Run(new AutoCompleteForm());
            //Application.Run(new TSDataGridViewForm());
            //Application.Run(new TSDataGridVieweFormWithoutBinding());
            Application.Run(new TSDGVSortableBindingForm());
            //Application.Run(new TreeViewForm());
            //Application.Run(new ButtonContainerForm());
        }
    }
}
