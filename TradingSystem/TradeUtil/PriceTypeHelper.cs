using Model.config;
using Model.EnumType;
using System;
using System.Windows.Forms;

namespace TradingSystem.TradeUtil
{
    public class PriceTypeHelper
    {
        public static PriceType GetPriceType(ComboBox comboBox)
        {
            var selectItem = (ComboOptionItem)comboBox.SelectedItem;
            return GetPriceType(selectItem.Id);
        }

        public static PriceType GetPriceType(string priceTypeId)
        {
            PriceType priceType = PriceType.Market;
            if (priceTypeId != null && !string.IsNullOrEmpty(priceTypeId))
            {
                if (Enum.IsDefined(typeof(PriceType), priceTypeId))
                {
                    priceType = (PriceType)Enum.Parse(typeof(PriceType), priceTypeId);
                }
            }

            return priceType;
        }
    }
}
