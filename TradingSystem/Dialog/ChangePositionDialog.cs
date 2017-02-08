using BLL.SecurityInfo;
using Config;
using Controls.Entity;
using Forms;
using Model.EnumType;
using Model.UI;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Quote;
using Model.SecurityInfo;
using System;
using Model.Quote;
using BLL.Manager;
using Calculation;

namespace TradingSystem.Dialog
{
    public partial class ChangePositionDialog : Forms.BaseDialog
    {
        private const string msgChgPositionConfirm = "chgpositionconfirm";

        private ClosePositionSecurityItem _originItem;
        private List<SecurityItem> _secuInfoList = null;

        public ChangePositionDialog()
        {
            InitializeComponent();

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);

            this.btnConfirm.Click += new System.EventHandler(Button_Confirm_Click);
            this.btnCancel.Click += new System.EventHandler(Button_Cancel_Click);

            this.acSecuIn.SelectChangedItem += new Controls.SelectChangedItemHandler(AutoComplete_SecuIn_SelectChangedItem);
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

            _secuInfoList = SecurityInfoManager.Instance.Get();
            if (_secuInfoList == null)
                return false;

            _originItem = data as ClosePositionSecurityItem;

            LoadAutoComplete(_originItem);
            LoadChangeOut(_originItem);

            return true;
        }

        private void LoadAutoComplete(ClosePositionSecurityItem closeSecuItem)
        {
            IList<AutoItem> dataSource = new List<AutoItem>();
            var matchItems = _secuInfoList.Where(p => p.SecuType == closeSecuItem.SecuType && !p.SecuCode.Equals(closeSecuItem.SecuCode)).ToList();
            foreach (var item in matchItems)
            {
                AutoItem autoItem = new AutoItem
                {
                    Id = item.SecuCode,
                    Name = item.SecuName,
                };

                dataSource.Add(autoItem);
            }

            this.acSecuOut.AutoDataSource = dataSource;
            this.acSecuIn.AutoDataSource = dataSource;
        }

        private void LoadChangeOut(ClosePositionSecurityItem closeSecuItem)
        {
            AutoItem autoItem = new AutoItem
            {
                Id = closeSecuItem.SecuCode,
                Name = closeSecuItem.SecuName
            };
            
            this.acSecuOut.SetCurrentItem(autoItem);

            string longShort = string.Format("{0}", (int)closeSecuItem.PositionType);
            ComboBoxUtil.SetComboBoxSelect(this.cbOutDirection, longShort);

            //股票设置调出为卖出，调入为买入
            //股指期货设置调出为买入平仓，调入为卖出开仓
            EntrustDirection eOutDirection;
            EntrustDirection eInDirection;
            if (closeSecuItem.SecuType == Model.SecurityInfo.SecurityType.Stock)
            {
                eOutDirection = EntrustDirection.Sell;
                eInDirection = EntrustDirection.Buy;
            }
            else if (closeSecuItem.SecuType == Model.SecurityInfo.SecurityType.Futures)
            {
                eOutDirection = EntrustDirection.Buy;
                eInDirection = EntrustDirection.Sell;
            }
            else
            {
                eOutDirection = EntrustDirection.Sell;
                eInDirection = EntrustDirection.Buy;
            }

            string outDirection = string.Format("{0}", (int)eOutDirection);
            ComboBoxUtil.SetComboBoxSelect(this.cbOutDirection, outDirection);

            this.tbOutAmount.Text = string.Format("{0}", closeSecuItem.AvailableAmount);

            //设置调入
            string inDirection = string.Format("{0}", (int)eInDirection);
            ComboBoxUtil.SetComboBoxSelect(this.cbInDirection, inDirection);
        }

        #endregion

        #region button click

        private void Button_Cancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
        }

        private void Button_Confirm_Click(object sender, System.EventArgs e)
        {
            var changeInItem = GetChangeInItem();
            var changeOutItem = GetChangeOutItem();
            if (!ValidateChangeItem(changeInItem) || !ValidateChangeItem(changeOutItem))
            {
                MessageDialog.Error(this, msgChgPositionConfirm);
                return;
            }

            ClosePositionSecurityItem[] datas = new ClosePositionSecurityItem[2];
            datas[0] = changeInItem;
            datas[1] = changeOutItem;
            if (OnSave(this, datas))
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            //else
            //{
            //    DialogResult = System.Windows.Forms.DialogResult.No;
            //}
        }

        private ClosePositionSecurityItem GetChangeOutItem()
        {
            ClosePositionSecurityItem changeOutItem = new ClosePositionSecurityItem 
            {
                SecuCode = _originItem.SecuCode,
                SecuName = _originItem.SecuName,
                SecuType = _originItem.SecuType,
                InstanceId = _originItem.InstanceId,
                PortfolioId = _originItem.PortfolioId,
                PortfolioName = _originItem.PortfolioName,
                ExchangeCode = _originItem.ExchangeCode,
                PositionType = _originItem.PositionType,
            };

            int temp;
            if (int.TryParse(this.tbOutAmount.Text, out temp))
            {
                changeOutItem.EntrustAmount = temp;
            }

            double dtemp;
            if (double.TryParse(this.tbOutPrice.Text, out dtemp))
            {
                changeOutItem.CommandPrice = dtemp;
            }

            if (changeOutItem.SecuType == SecurityType.Futures)
            {
                changeOutItem.EDirection = EntrustDirection.BuyClose;
            }
            else if (changeOutItem.SecuType == SecurityType.Stock)
            {
                changeOutItem.EDirection = EntrustDirection.SellSpot;
            }

            return changeOutItem;
        }

        private ClosePositionSecurityItem GetChangeInItem()
        {
            var secuItem = this.acSecuIn.GetCurrentItem();

            ClosePositionSecurityItem changeInItem = new ClosePositionSecurityItem();
            changeInItem.SecuCode = secuItem.Id;
            changeInItem.SecuName = secuItem.Name;

            var findItem = _secuInfoList.Find(p => p.SecuCode.Equals(changeInItem.SecuCode));
            if (findItem != null)
            {
                changeInItem.SecuType = findItem.SecuType;
                changeInItem.ExchangeCode = findItem.ExchangeCode;
            }

            int temp;
            if (int.TryParse(this.tbInAmount.Text, out temp))
            {
                changeInItem.EntrustAmount = temp;
            }

            double dtemp;
            if (double.TryParse(this.tbInPrice.Text, out dtemp))
            {
                changeInItem.CommandPrice = dtemp;
            }

            changeInItem.InstanceId = _originItem.InstanceId;
            changeInItem.PortfolioId = _originItem.PortfolioId;
            changeInItem.PortfolioName = _originItem.PortfolioName;

            switch (_originItem.PositionType)
            {
                case PositionType.FuturesLong:
                    {
                        if (changeInItem.SecuType == Model.SecurityInfo.SecurityType.Futures)
                        {
                            changeInItem.PositionType = PositionType.FuturesShort;
                        }
                    }
                    break;
                case PositionType.FuturesShort:
                    {
                        if (changeInItem.SecuType == Model.SecurityInfo.SecurityType.Futures)
                        {
                            changeInItem.PositionType = PositionType.FuturesLong;
                        }
                    }
                    break;
                case PositionType.SpotLong:
                    {
                        if (changeInItem.SecuType == Model.SecurityInfo.SecurityType.Stock)
                        {
                            changeInItem.PositionType = PositionType.SpotShort;
                        }
                    }
                    break;
                case PositionType.SpotShort:
                    {
                        if (changeInItem.SecuType == Model.SecurityInfo.SecurityType.Stock)
                        {
                            changeInItem.PositionType = PositionType.SpotLong;
                        }
                    }
                    break;
                default:
                    break;
            }

            if (changeInItem.SecuType == SecurityType.Futures)
            {
                changeInItem.EDirection = EntrustDirection.SellOpen;
            }
            else if (changeInItem.SecuType == SecurityType.Stock)
            {
                changeInItem.EDirection = EntrustDirection.BuySpot;
            }

            //switch (_originItem.EDirection)
            //{
            //    case Model.EnumType.EntrustDirection.BuySpot:
            //        {
            //            if (outItem.SecuType == Model.SecurityInfo.SecurityType.Stock)
            //            {
            //                outItem.EDirection = Model.EnumType.EntrustDirection.SellSpot;
            //            }
            //        }
            //        break;
            //    case Model.EnumType.EntrustDirection.SellSpot:
            //        {
            //            if (outItem.SecuType == Model.SecurityInfo.SecurityType.Stock)
            //            {
            //                outItem.EDirection = Model.EnumType.EntrustDirection.BuySpot;
            //            }
            //        }
            //        break;
            //    case Model.EnumType.EntrustDirection.BuyClose:
            //        {
            //            if (outItem.SecuType == Model.SecurityInfo.SecurityType.Futures)
            //            {
            //                outItem.EDirection = Model.EnumType.EntrustDirection.SellOpen;
            //            }
            //        }
            //        break;
            //    case Model.EnumType.EntrustDirection.SellOpen:
            //        {
            //            if (outItem.SecuType == Model.SecurityInfo.SecurityType.Futures)
            //            {
            //                outItem.EDirection = Model.EnumType.EntrustDirection.BuyClose;
            //            }
            //        }
            //        break;
            //    default:
            //        break;
            //}

            return changeInItem;
        }

        private bool ValidateChangeItem(ClosePositionSecurityItem changeItem)
        {
            if (string.IsNullOrEmpty(changeItem.SecuCode))
            {
                return false;
            }

            if (changeItem.EntrustAmount <= 0)
            {
                return false;
            }

            if (changeItem.CommandPrice <= 0)
            {
                return false;
            }

            if (changeItem.EDirection != Model.EnumType.EntrustDirection.BuyClose
                && changeItem.EDirection != Model.EnumType.EntrustDirection.SellOpen
                && changeItem.EDirection != Model.EnumType.EntrustDirection.BuySpot
                && changeItem.EDirection != Model.EnumType.EntrustDirection.SellSpot)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region AutoComplete select changed item

        private void AutoComplete_SecuIn_SelectChangedItem(object sender, AutoItem data)
        {
            if (sender == null || data == null)
                return;
            if (string.IsNullOrEmpty(data.Id))
                return;

            List<SecurityItem> secuItemList = new List<SecurityItem>();
            var inItem = _secuInfoList.Find(p => p.SecuCode.Equals(data.Id));
            if (inItem != null)
            {
                secuItemList.Add(inItem);
            }
            var outItem = _secuInfoList.Find(p => p.SecuCode.Equals(_originItem.SecuCode));
            if (outItem != null)
            {
                secuItemList.Add(outItem);
            }

            //QuoteCenter.Instance.Query(secuItemList);

            var inPriceData = QuoteCenter2.Instance.GetMarketData(inItem);
            var outPriceData = QuoteCenter2.Instance.GetMarketData(outItem);

            int inAmount = 0;
            if(inPriceData != null && outPriceData != null)
            {
                SetPrice(inPriceData, this.tbInPrice);
                SetPrice(outPriceData, this.tbOutPrice);

                if (outPriceData.CurrentPrice > 0)
                { 
                    var temp = _originItem.AvailableAmount * inPriceData.CurrentPrice / outPriceData.CurrentPrice;
                    inAmount = AmountRoundUtil.Round((int)temp); 
                }
            }

            if (inAmount > 0)
            {
                this.tbInAmount.Text = string.Format("{0}", inAmount);
            }
        }

        private void SetPrice(MarketData marketData, TextBox textBox)
        {
            if (marketData != null)
            {
                string price = string.Format("{0}", marketData.CurrentPrice);

                textBox.Text = price;
            }
        }

        #endregion
    }
}
