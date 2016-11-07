using Model.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EnumType.EnumTypeConverter
{
    public static class EntrustPriceTypeConverter
    {
        /// <summary>
        /// Convert the single char string into EntrustPriceType, such as "0", "1", "a".
        /// NOTE: the type is from UFX interface dictionary.
        /// </summary>
        /// <param name="priceTypeCode">A given string code for the EntrustPriceType.</param>
        /// <returns>A EntrustPriceType for the code standby.</returns>
        public static EntrustPriceType GetPriceType(string priceTypeCode)
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

        /// <summary>
        /// Get the string type of name for the specified EntrustPriceType
        /// </summary>
        /// <param name="priceType">A given EntrustPriceType.</param>
        /// <returns>A string type of the name.</returns>
        public static string GetPriceTypeName(EntrustPriceType priceType)
        {
            return EnumAttributeHelper.GetEnumDescription<EntrustPriceType>(priceType);
        }
    }
}
