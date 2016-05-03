using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public sealed class FormUtil
    {
        public static void SetComboBox(ComboBox comboBox, ComboOption comboOption)
        {
            foreach (var item in comboOption.Items)
            {
                comboBox.Items.Add(item);
                if (item.Id == comboOption.Selected)
                {
                    comboBox.SelectedItem = item;
                }
            }
        }
    }
}
