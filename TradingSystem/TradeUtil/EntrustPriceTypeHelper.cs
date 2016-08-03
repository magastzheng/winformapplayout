using Model.config;
using Model.EnumType;
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
            return GetEntrustPriceTypeByCode(selectItem.Code);
        }

        public static EntrustPriceType GetEntrustPriceTypeByCode(string priceTypeCode)
        {
            if (priceTypeCode == null || string.IsNullOrEmpty(priceTypeCode) || priceTypeCode.Length != 1)
            {
                string msg = string.Format("The EntrustPriceType [{0}] is not supported!", priceTypeCode);
                throw new NotSupportedException(msg);
            }

            var bytes = Encoding.UTF8.GetBytes(priceTypeCode);
            int charCode = (int)bytes[0];
            if (!Enum.IsDefined(typeof(EntrustPriceType), charCode))
            {
                string msg = string.Format("The EntrustPriceType [{0}] is not supported!", priceTypeCode);
                throw new NotSupportedException(msg);
            }

            EntrustPriceType priceType = (EntrustPriceType)Enum.ToObject(typeof(EntrustPriceType), charCode);

            return priceType;
        }
    }
}
