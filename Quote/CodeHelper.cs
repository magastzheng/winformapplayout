using Config;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote
{
    public static class CodeHelper
    {
        /// <summary>
        /// Convert the index secucode into internal windcode.
        /// reference by TDF C++ API Quick Start (Appendix 6.1)上证指数代码转换对应表。
        /// </summary>
        /// <param name="secuCode">The secucode released by exchange.</param>
        /// <returns>The internal wind code.</returns>
        private static string GetIndexWindCode(string secuCode)
        {
            secuCode = secuCode.Trim();
            string windCode = string.Empty;
            var item = ConfigManager.Instance.GetCodeMappingConfig().GetItemBySecuCode(secuCode);
            if (item != null)
            {
                windCode = item.WindCode;
            }
            else if (secuCode.Substring(0, 2).Equals("00"))
            {
                windCode = string.Format("99{0}", secuCode.Substring(2));
                windCode = windCode.Trim();
            }
            else
            {
                windCode = secuCode;
            }

            return windCode;
        }

        /// <summary>
        /// Some index secucodes are converted into the wind internal code and we get the code table with the wind
        /// internal code. So It needs to convert into secucode from wind internal code.
        /// The rules are:
        /// 1.use new wind internal code to coding the original secucode, such as 999999 standing for 000001.
        /// 2.replace the first two number from 00 to 99 except the index secucode is not in the mapping, 
        /// such as 990999 standing for 000999
        /// There are two parts: one is configured into the configuration file mapping, the other is converted directly.
        /// reference by TDF C++ API Quick Start (Appendix 6.1)上证指数代码转换对应表。
        /// </summary>
        /// <param name="internalWindCode"></param>
        /// <returns></returns>
        public static string GetIndexSecuCode(string internalWindCode)
        {
            internalWindCode = internalWindCode.Trim();
            string secuCode = internalWindCode;
            var item = ConfigManager.Instance.GetCodeMappingConfig().GetItemByWindCode(internalWindCode);
            if (item != null)
            {
                secuCode = item.SecuCode;
            }
            else if (internalWindCode.Substring(0, 2).Equals("99"))
            {
                secuCode = string.Format("00{0}", internalWindCode.Substring(2));
                secuCode = secuCode.Trim();
            }

            return secuCode;
        }

        /// <summary>
        /// Get the wind code from SecurityItem. The SecurityItem includes secucode, exchange, security type.
        /// The final wind code will end with the point+exchangecode, such as 000001.SZ, 600000.SH, IF1612.CF
        /// </summary>
        /// <param name="secuItem">The SecurityItem, which include secucode, security type, exchange information.</param>
        /// <returns>A wind code consist of wind internal code, point, and exchange code.</returns>
        public static string GetWindCode(SecurityItem secuItem)
        {
            string secuCode = secuItem.SecuCode.Trim();
            string windCode = secuCode;
            if (secuItem.SecuType == SecurityType.Index)
            {
                windCode = CodeHelper.GetIndexWindCode(secuCode);
            }

            string exchangeCode = secuItem.ExchangeCode;
            if (string.IsNullOrEmpty(exchangeCode))
            {
                exchangeCode = SecurityItemHelper.GetExchangeCode(secuItem.SecuCode, secuItem.SecuType);
            }

            if (!string.IsNullOrEmpty(exchangeCode))
            {
                if (exchangeCode.Equals(Exchange.SHSE, StringComparison.OrdinalIgnoreCase))
                {
                    windCode += ".SH";
                }
                else if (exchangeCode.Equals(Exchange.SZSE, StringComparison.OrdinalIgnoreCase))
                {
                    windCode += ".SZ";
                }
                else if (exchangeCode.Equals(Exchange.CFFEX, StringComparison.OrdinalIgnoreCase))
                {
                    windCode += ".CF";
                }
            }
            else
            {
                //do nothing
            }

            return windCode;
        }
    }
}
