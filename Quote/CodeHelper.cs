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
        public static string GetIndexWindCode(string secuCode)
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

        public static string GetIndexSecuCode(string windCode)
        {
            windCode = windCode.Trim();
            string secuCode = string.Empty;
            var item = ConfigManager.Instance.GetCodeMappingConfig().GetItemByWindCode(windCode);
            if (item != null)
            {
                secuCode = item.SecuCode;
            }
            else if(windCode.Substring(0, 2).Equals("99"))
            {
                secuCode = string.Format("00{0}", windCode.Substring(2));
                secuCode = secuCode.Trim();
            }

            return secuCode;
        }

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
                if (secuItem.SecuType == SecurityType.Index)
                {
                    exchangeCode = SecurityInfoHelper.GetIndexExchangeCode(secuItem.SecuCode);
                }
                else
                { 
                    exchangeCode = SecurityInfoHelper.GetExchangeCode(secuItem.SecuCode);
                }
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
