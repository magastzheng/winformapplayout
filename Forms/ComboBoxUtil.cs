using Model.config;
using System.Windows.Forms;

namespace Forms
{
    public sealed class ComboBoxUtil
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

        public static void SetComboBoxSelect(ComboBox comboBox, string Id)
        {
            foreach (var item in comboBox.Items)
            {
                ComboOptionItem cbItem = (ComboOptionItem)item;
                if (cbItem.Id.Equals(Id))
                {
                    comboBox.SelectedItem = item;
                    break;
                }
            }
        }

        public static void ClearComboBox(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.ResetText();
        }
    }
}
