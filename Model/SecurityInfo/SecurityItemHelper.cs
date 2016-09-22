using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SecurityInfo
{
    public class SecurityItemHelper
    {
        public static string GetExchange(string exchangeCode)
        {
            string exchange = string.Empty;

            switch (exchangeCode)
            {
                case Exchange.CFFEX:
                    exchange = "中金所";
                    break;
                case Exchange.SHSE:
                    exchange = "深交所";
                    break;
                case Exchange.SZSE:
                    exchange = "上交所";
                    break;
            }

            return exchange;
        }

        public static SecurityType GetSecurityType(string secuCode)
        {
            secuCode = secuCode.Trim();
            if (secuCode.StartsWith("IF") || secuCode.StartsWith("IH") || secuCode.StartsWith("IC"))
            {
                return SecurityType.Futures;
            }
            else
            {
                int temp = -1;
                if (int.TryParse(secuCode, out temp))
                {
                    if (temp > 0 && temp <= 699999)
                    {
                        return SecurityType.Stock;
                    }
                }
            }

            return SecurityType.All;
        }

        public static string GetExchangeCode(string secuCode)
        {
            secuCode = secuCode.Trim();
            if (secuCode.StartsWith("IF") || secuCode.StartsWith("IH") || secuCode.StartsWith("IC"))
            {
                return Exchange.CFFEX;
            }
            else
            {
                int temp = -1;
                if (int.TryParse(secuCode, out temp))
                {
                    if (temp > 0 && temp <= 399999)
                    {
                        return Exchange.SZSE;
                    }
                    else if (temp >= 60000 && temp <= 699999)
                    {
                        return Exchange.SHSE;
                    }
                }
            }

            return string.Empty;
        }
    }
}
