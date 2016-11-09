using Model.SecurityInfo;

namespace Quote
{
    public class SecurityInfoHelper
    {
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

        public static string GetIndexExchangeCode(string secuCode)
        {
            secuCode = secuCode.Trim();
            int temp = -1;
            if (int.TryParse(secuCode, out temp))
            {
                if (temp > 300000 && temp <= 399999)
                {
                    return Exchange.SZSE;
                }
                else if (temp >= 0 && temp <= 100000)
                {
                    return Exchange.SHSE;
                }
            }

            return string.Empty;
        }
    }
}
