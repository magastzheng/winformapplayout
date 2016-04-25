using Config;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingSystem.View
{
    public partial class CancelRedoForm : Form
    {
        public CancelRedoForm(GridConfig gridConfig)
        {
            _gridConfig = gridConfig;

            InitializeComponent();
        }

        private void CancelRedoForm_Load(object sender, System.EventArgs e)
        {
            InitializeCombobox();
        }

        #region combobox
        private void InitializeCombobox()
        {
            var spotBuy = ConfigManager.Instance.GetComboConfig().GetComboOption("spotbuy");
            SetComboBox(this.cbSpotBuyPrice, spotBuy);

            var spotSell = ConfigManager.Instance.GetComboConfig().GetComboOption("spotsell");
            SetComboBox(this.cbSpotSellPrice, spotSell);

            var futureBuy = ConfigManager.Instance.GetComboConfig().GetComboOption("futurebuy");
            SetComboBox(this.cbFuturesBuyPrice, futureBuy);

            var futureSell = ConfigManager.Instance.GetComboConfig().GetComboOption("futuresell");
            SetComboBox(this.cbFuturesSellPrice, futureSell);

            var shPrice = ConfigManager.Instance.GetComboConfig().GetComboOption("shanghaiexchangepricetype");
            SetComboBox(this.cbSHExchangePrice, shPrice);

            var szPrice = ConfigManager.Instance.GetComboConfig().GetComboOption("shenzhenexchangepricetype");
            SetComboBox(this.cbSZExchangePrice, szPrice);

        }

        private void SetComboBox(ComboBox comboBox, ComboOption comboOption)
        {
            foreach (var item in comboOption.Items)
            {
                comboBox.Items.Add(item);
            }

            comboBox.SelectedIndex = 0;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null)
                return;

            ComboOptionItem selectedItem = comboBox.SelectedItem as ComboOptionItem;
            if (selectedItem == null)
                return;

            Console.WriteLine("Selected: " + comboBox.Name + " " + selectedItem.Id);
            switch (comboBox.Name)
            {
                case "cbSpotBuyPrice":

                    break;
                case "cbSpotSellPrice":
                    break;
                case "cbFuturesBuyPrice":
                    break;
                case "cbFuturesSellPrice":
                    break;
                case "cbSHExchangePrice":
                    break;
                case "cbSZExchangePrice":
                    break;
            }
            if (sender is ComboBox)
            {
                Console.WriteLine("Event: ", sender.ToString());
            }
        }
        #endregion

        #region button click

        private void ButtonUnSelectAll_Click(object sender, System.EventArgs e)
        {
            this.dataGridViewECA.SelectAll(false);
        }

        private void ButtonSelectAll_Click(object sender, System.EventArgs e)
        {
            this.dataGridViewECA.SelectAll(true);
        }

        private void ButtonConfirm_Click(object sender, System.EventArgs e)
        { 
            
        }

        private void ButtonCancel_Click(object sender, System.EventArgs e)
        {

        }
        #endregion
    }
}
