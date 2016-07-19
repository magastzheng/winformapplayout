using BLL.SecurityInfo;
using Config;
using Controls.Entity;
using Forms;
using Model.UI;
using System.Windows.Forms;

namespace TradingSystem.Dialog
{
    public partial class ChangePositionDialog : Forms.BaseFixedForm
    {
        private ClosePositionSecurityItem _inItem;
        public ChangePositionDialog()
        {
            InitializeComponent();

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);

            this.btnConfirm.Click += new System.EventHandler(Button_Confirm_Click);
            this.btnCancel.Click += new System.EventHandler(Button_Cancel_Click);
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

            _inItem = data as ClosePositionSecurityItem;

            LoadChangeOut(_inItem);


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

        #region button click

        private void Button_Cancel_Click(object sender, System.EventArgs e)
        {
            var outItem = GetOutItem();
            if (!ValidateOutItem(outItem))
            {
                MessageBox.Show("请确认换仓证券有效（数量，价格）", "错误", MessageBoxButtons.OK);
            }

            ClosePositionSecurityItem[] datas = new ClosePositionSecurityItem[2];
            datas[0] = outItem;
            datas[1] = _inItem;
            OnSave(this, datas);

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Button_Confirm_Click(object sender, System.EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
        }

        private ClosePositionSecurityItem GetOutItem()
        {
            var secuItem = this.acSecuOut.GetCurrentItem();

            ClosePositionSecurityItem outItem = new ClosePositionSecurityItem();
            outItem.SecuCode = secuItem.Id;
            outItem.SecuName = secuItem.Name;

            var findItem = SecurityInfoManager.Instance.Get(outItem.SecuCode);
            if (findItem != null)
            {
                outItem.SecuType = findItem.SecuType;
                outItem.ExchangeCode = findItem.ExchangeCode;
            }

            int temp;
            if (int.TryParse(this.tbOutAmount.Text, out temp))
            {
                outItem.EntrustAmount = temp;
            }

            double dtemp;
            if (double.TryParse(this.tbOutPrice.Text, out dtemp))
            {
                outItem.CommandPrice = dtemp;
            }

            outItem.InstanceId = _inItem.InstanceId;
            outItem.PortfolioId = _inItem.PortfolioId;
            outItem.PortfolioName = _inItem.PortfolioName;

            switch (_inItem.PositionType)
            {
                case PositionType.FuturesLong:
                    {
                        if (outItem.SecuType == Model.SecurityInfo.SecurityType.Futures)
                        {
                            outItem.PositionType = PositionType.FuturesShort;
                        }
                    }
                    break;
                case PositionType.FuturesShort:
                    {
                        if (outItem.SecuType == Model.SecurityInfo.SecurityType.Futures)
                        {
                            outItem.PositionType = PositionType.FuturesLong;
                        }
                    }
                    break;
                case PositionType.StockLong:
                    {
                        if (outItem.SecuType == Model.SecurityInfo.SecurityType.Stock)
                        {
                            outItem.PositionType = PositionType.StockShort;
                        }
                    }
                    break;
                case PositionType.StockShort:
                    {
                        if (outItem.SecuType == Model.SecurityInfo.SecurityType.Stock)
                        {
                            outItem.PositionType = PositionType.StockLong;
                        }
                    }
                    break;
                default:
                    break;
            }

            switch (_inItem.EDirection)
            {
                case Model.EnumType.EntrustDirection.BuySpot:
                    {
                        if (outItem.SecuType == Model.SecurityInfo.SecurityType.Stock)
                        {
                            outItem.EDirection = Model.EnumType.EntrustDirection.SellSpot;
                        }
                    }
                    break;
                case Model.EnumType.EntrustDirection.SellSpot:
                    {
                        if (outItem.SecuType == Model.SecurityInfo.SecurityType.Stock)
                        {
                            outItem.EDirection = Model.EnumType.EntrustDirection.BuySpot;
                        }
                    }
                    break;
                case Model.EnumType.EntrustDirection.BuyClose:
                    {
                        if (outItem.SecuType == Model.SecurityInfo.SecurityType.Futures)
                        {
                            outItem.EDirection = Model.EnumType.EntrustDirection.SellOpen;
                        }
                    }
                    break;
                case Model.EnumType.EntrustDirection.SellOpen:
                    {
                        if (outItem.SecuType == Model.SecurityInfo.SecurityType.Futures)
                        {
                            outItem.EDirection = Model.EnumType.EntrustDirection.BuyClose;
                        }
                    }
                    break;
                default:
                    break;
            }

            return outItem;
        }

        private bool ValidateOutItem(ClosePositionSecurityItem outItem)
        {
            if (string.IsNullOrEmpty(outItem.SecuCode))
            {
                return false;
            }

            if (outItem.EntrustAmount <= 0)
            {
                return false;
            }

            if (outItem.CommandPrice <= 0)
            {
                return false;
            }

            if (outItem.EDirection != Model.EnumType.EntrustDirection.BuyClose
                || outItem.EDirection != Model.EnumType.EntrustDirection.SellOpen
                || outItem.EDirection != Model.EnumType.EntrustDirection.BuySpot
                || outItem.EDirection != Model.EnumType.EntrustDirection.SellSpot)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
