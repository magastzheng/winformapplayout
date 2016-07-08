using Model.config;
using Model.EnumType;
using System;
using System.Windows.Forms;

namespace TradingSystem.TradeUtil
{
    public class EntrustPriceTypeHelper
    {
        public static EntrustPriceType GetPriceType(ComboBox comboBox)
        {
            var selectItem = (ComboOptionItem)comboBox.SelectedItem;
            return GetEntrustPriceType(selectItem.Id);
        }

        public static EntrustPriceType GetEntrustPriceType(string priceTypeId)
        { 
            EntrustPriceType priceType = EntrustPriceType.FifthIsLeftOffSH;
            if (priceTypeId != null && !string.IsNullOrEmpty(priceTypeId))
            {
                if (Enum.IsDefined(typeof(EntrustPriceType), priceTypeId))
                {
                    priceType = (EntrustPriceType)Enum.Parse(typeof(EntrustPriceType), priceTypeId);
                }
            }

            return priceType;
        }
    }
}
