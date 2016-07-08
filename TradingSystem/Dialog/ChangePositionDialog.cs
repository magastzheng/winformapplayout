using Config;
using Controls.Entity;
using Forms;
using Model.UI;

namespace TradingSystem.Dialog
{
    public partial class ChangePositionDialog : Forms.BaseFixedForm
    {
        public ChangePositionDialog()
        {
            InitializeComponent();

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
        }

        #region load control

        private bool Form_LoadControl(object sender, object data)
        {
            this.acSecuOut.SetDropdownList(this.lbSecuOut);
            this.acSecuIn.SetDropdownList(this.lbSecuIn);

            var longShortTypes = ConfigManager.Instance.GetComboConfig().GetComboOption("longshort");
            ComboBoxUtil.SetComboBox(this.cbLongShort, longShortTypes);

            var buySellTypes = ConfigManager.Instance.GetComboConfig().GetComboOption("tradedirection");
            ComboBoxUtil.SetComboBox(this.cbOutDirection, buySellTypes);

            ComboBoxUtil.SetComboBox(this.cbInDirection, buySellTypes);

            return true;
        }

        #endregion

        #region load data

        private bool Form_LoadData(object sender, object data)
        {
            if (data == null)
                return false;
            if (!(data is ClosePositionSecurityItem))
                return false;

            ClosePositionSecurityItem closeSecuItem = data as ClosePositionSecurityItem;

            LoadChangeOut(closeSecuItem);


            return true;
        }

        private void LoadChangeOut(ClosePositionSecurityItem closeSecuItem)
        {
            AutoItem autoItem = new AutoItem
            {
                Id = closeSecuItem.SecuCode,
                Name = closeSecuItem.SecuName
            };
            
            this.acSecuOut.SetCurrentItem(autoItem);

            string longShort = string.Format("{0}", closeSecuItem.PositionType);
            ComboBoxUtil.SetComboBoxSelect(this.cbOutDirection, longShort);

            string outDirection = string.Format("{0}", closeSecuItem.EntrustDirection);
            ComboBoxUtil.SetComboBoxSelect(this.cbOutDirection, outDirection);

            this.tbOutAmount.Text = string.Format("{0}", closeSecuItem.AvailableAmount);
        }

        #endregion
    }
}
