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
        /// Convert the single char string into EntrustPriceType, such as "0", "1", "a"
        /// </summary>
        /// <param name="priceTypeCode"></param>
        /// <returns></returns>
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
    }
}
