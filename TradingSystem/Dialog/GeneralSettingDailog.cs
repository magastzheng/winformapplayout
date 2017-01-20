using BLL.Manager;
using Config;
using Forms;
using Model.config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Model.EnumType;
using TradingSystem.TradeUtil;
using Model.Setting;

namespace TradingSystem.Dialog
{
    public partial class GeneralSettingDailog : Forms.BaseDialog
    {
        public GeneralSettingDailog()
        {
            InitializeComponent();

            LoadControl += new FormLoadHandler(Form_LoadControl);
            LoadData += new FormLoadHandler(Form_LoadData);

            //this.cbSpotBuyPrice.SelectedIndexChanged += new EventHandler(ComboBox_Price_SelectedIndexChanged);
            //this.cbSpotSellPrice.SelectedIndexChanged += new EventHandler(ComboBox_Price_SelectedIndexChanged);
            //this.cbFutuBuyPrice.SelectedIndexChanged += new EventHandler(ComboBox_Price_SelectedIndexChanged);
            //this.cbFutuSellPrice.SelectedIndexChanged += new EventHandler(ComboBox_Price_SelectedIndexChanged);

            this.btnConfirm.Click += new EventHandler(Button_Confirm_Click);
            this.btnCancel.Click += new EventHandler(Button_Cancel_Click);
        }

        #region load control
        private bool Form_LoadControl(object sender, object data)
        {
            LoadBasicSettingControl();
            LoadPriceControl();

            return true;
        }

        private void LoadBasicSettingControl()
        {
            var setting = SettingManager.Instance.Get();
            var oddShareModes = ConfigManager.Instance.GetComboConfig().GetComboOption("oddsharemode");
            ComboBoxUtil.SetComboBox(this.cbOddShareMode, oddShareModes, setting.EntrustSetting.OddShareMode.ToString());

            var sseEntrustPriceType = ConfigManager.Instance.GetComboConfig().GetComboOption("shanghaiexchangepricetype");
            ComboBoxUtil.SetComboBox(this.cbSseEntrustPriceType, sseEntrustPriceType, setting.EntrustSetting.SseEntrustPriceType.ToString());

            var szseEntrustPriceType = ConfigManager.Instance.GetComboConfig().GetComboOption("shenzhenexchangepricetype");
            ComboBoxUtil.SetComboBox(this.cbSzseEntrustPriceType, szseEntrustPriceType, setting.EntrustSetting.SzseEntrustPriceType.ToString());

            tbSpotLimitEntrustRatio.Text = string.Format("{0}", setting.UFXSetting.LimitEntrustRatio);
            tbFutuLimitEntrustRatio.Text = string.Format("{0}", setting.UFXSetting.FutuLimitEntrustRatio);
        }

        private void LoadPriceControl()
        {
            var setting = SettingManager.Instance.Get();
            var spotPrices = ConfigManager.Instance.GetComboConfig().GetComboOption("spotprice");
            ComboBoxUtil.SetComboBox(this.cbSpotBuyPrice, spotPrices, setting.EntrustSetting.BuySpotPrice.ToString());

            var spotSellPrices = new ComboOption
            {
                Name = spotPrices.Name,
                Selected = spotPrices.Selected,
                Items = spotPrices.Items.OrderBy(p => p.Order2).ToList()
            };
            ComboBoxUtil.SetComboBox(this.cbSpotSellPrice, spotSellPrices, setting.EntrustSetting.SellSpotPrice.ToString());

            var futurePrice = ConfigManager.Instance.GetComboConfig().GetComboOption("futureprice");
            ComboBoxUtil.SetComboBox(this.cbFutuBuyPrice, futurePrice, setting.EntrustSetting.BuyFutuPrice.ToString());

            var futureSellPrices = new ComboOption
            {
                Name = futurePrice.Name,
                Selected = futurePrice.Selected,
                Items = futurePrice.Items.OrderBy(p => p.Order2).ToList()
            };

            ComboBoxUtil.SetComboBox(this.cbFutuSellPrice, futureSellPrices, setting.EntrustSetting.SellFutuPrice.ToString());
        }

        #endregion

        #region loaddata

        private bool Form_LoadData(object sender, object data)
        {

            return true;
        }

        #endregion

        private DefaultSetting GetSetting()
        {
            var oldSetting = SettingManager.Instance.Get();

            DefaultSetting setting = new DefaultSetting
            {
                Timeout = oldSetting.Timeout,
                UFXSetting = new DefaultUFXSetting
                {
                    Timeout = oldSetting.Timeout,
                },

                EntrustSetting = new DefaultEntrustSetting(),
            };

            PriceType spotBuyPrice = PriceTypeHelper.GetPriceType(cbSpotBuyPrice);
            PriceType spotSellPrice = PriceTypeHelper.GetPriceType(cbSpotSellPrice);
            PriceType futuBuyPrice = PriceTypeHelper.GetPriceType(cbFutuBuyPrice);
            PriceType futuSellPrice = PriceTypeHelper.GetPriceType(cbFutuSellPrice);

            setting.EntrustSetting.BuySpotPrice = spotBuyPrice;
            setting.EntrustSetting.SellSpotPrice = spotSellPrice;
            setting.EntrustSetting.BuyFutuPrice = futuBuyPrice;
            setting.EntrustSetting.SellFutuPrice = futuSellPrice;

            setting.EntrustSetting.SseEntrustPriceType = EntrustPriceTypeHelper.GetPriceType(cbSseEntrustPriceType);
            setting.EntrustSetting.SzseEntrustPriceType = EntrustPriceTypeHelper.GetPriceType(cbSzseEntrustPriceType);

            setting.EntrustSetting.OddShareMode = ComboBoxHelper.GetPriceType(cbOddShareMode);
            int temp = 0;
            if (int.TryParse(tbSpotLimitEntrustRatio.Text, out temp))
            {
                setting.UFXSetting.LimitEntrustRatio = temp;
            }

            if (int.TryParse(tbFutuLimitEntrustRatio.Text, out temp))
            {
                setting.UFXSetting.FutuLimitEntrustRatio = temp;
            }
            
            return setting;
        }


        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            var setting = GetSetting();
            var oldSetting = SettingManager.Instance.Get();

            if (IsChanged(setting, oldSetting))
            {
                SettingManager.Instance.Update(setting);
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private bool IsChanged(DefaultSetting newSetting, DefaultSetting oldSetting)
        {
            return newSetting.Timeout != oldSetting.Timeout
                || newSetting.UFXSetting.Timeout != oldSetting.UFXSetting.Timeout
                || newSetting.UFXSetting.LimitEntrustRatio != oldSetting.UFXSetting.LimitEntrustRatio
                || newSetting.UFXSetting.FutuLimitEntrustRatio != oldSetting.UFXSetting.FutuLimitEntrustRatio
                || newSetting.EntrustSetting.OddShareMode != oldSetting.EntrustSetting.OddShareMode
                || newSetting.EntrustSetting.SseEntrustPriceType != oldSetting.EntrustSetting.SseEntrustPriceType
                || newSetting.EntrustSetting.SzseEntrustPriceType != oldSetting.EntrustSetting.SzseEntrustPriceType
                || newSetting.EntrustSetting.BuySpotPrice != oldSetting.EntrustSetting.BuySpotPrice
                || newSetting.EntrustSetting.SellSpotPrice != oldSetting.EntrustSetting.SellSpotPrice
                || newSetting.EntrustSetting.BuyFutuPrice != oldSetting.EntrustSetting.BuyFutuPrice
                || newSetting.EntrustSetting.SellFutuPrice != oldSetting.EntrustSetting.SellFutuPrice
                || newSetting.EntrustSetting.AutoRatio != oldSetting.EntrustSetting.AutoRatio
                || newSetting.EntrustSetting.BuySellEntrustOrder != oldSetting.EntrustSetting.BuySellEntrustOrder;
        }
    }
}
