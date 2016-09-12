using Model.config;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using System;
using System.Text;
using System.Windows.Forms;

namespace TradingSystem.TradeUtil
{
    public class EntrustPriceTypeHelper
    {
        public static EntrustPriceType GetPriceType(ComboBox comboBox)
        {
            var selectItem = (ComboOptionItem)comboBox.SelectedItem;
            return EntrustPriceTypeConverter.GetPriceType(selectItem.Code);
        }
    }
}
