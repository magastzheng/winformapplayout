using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SecurityInfo
{
    public class SecurityItemHelper
    {
        /// <summary>
        /// Get the exchange name by the exchangeCode.
        /// CFFEX: 中金所
        /// SHSE: 上交所
        /// SZSE: 深交所
        /// </summary>
        /// <param name="exchangeCode">The string value of exchange code.</param>
        /// <returns>The string value of exchange name.</returns>
        public static string GetExchange(string exchangeCode)
        {
            string exchange = string.Empty;

            switch (exchangeCode)
            {
                case Exchange.CFFEX:
                    exchange = "中金所";
                    break;
                case Exchange.SHSE:
                    exchange = "上交所";
                    break;
                case Exchange.SZSE:
                    exchange = "深交所";
                    break;
                default:
                    break;
            }

            return exchange;
        }

        /// <summary>
        /// Get the SecurityType by the secuCode.
        /// FIX: the index has the same secuCode with the stock, such as 000001 in Shanghai Exchange.
        /// </summary>
        /// <param name="secuCode">The string type of secuCode.</param>
        /// <param name="exchangeCode">The string type of the exchange code.</param>
        /// <returns>The SecurityType of the given secuCode in the given exchange.</returns>
        public static SecurityType GetSecurityType(string secuCode, string exchangeCode)
        {
            secuCode = secuCode.Trim();
            if (IsFutures(secuCode) || exchangeCode.Equals(Exchange.CFFEX))
            {
                return SecurityType.Futures;
            }
            else
            {
                int temp = -1;
                if (int.TryParse(secuCode, out temp))
                {
                    if (temp > 0 && temp <= 999999)
                    {
                        if (exchangeCode.Equals(Exchange.SZSE))
                        {
                            if (temp > 0 && temp <= 399000)
                            {
                                return SecurityType.Stock;
                            }
                            else if(temp >= 399001 && temp <= 399998)
                            {
                                return SecurityType.Index;
                            }
                            else
                            {
                                return SecurityType.All;
                            }
                        }
                        else if (exchangeCode.Equals(Exchange.SHSE))
                        {
                            if (temp >= 600000 && temp <= 699999)
                            {
                                return SecurityType.Stock;
                            }
                            else if (temp > 0 && temp <= 999 || temp >= 930901 && temp <= 950110)
                            {
                                return SecurityType.Index;
                            }
                            else
                            {
                                return SecurityType.All;
                            }
                        }
                        return SecurityType.Stock;
                    }
                }
            }

            return SecurityType.All;
        }

        /// <summary>
        /// Get the exchange code by given secuCode and secuType.
        /// </summary>
        /// <param name="secuCode">The string type of secuCode.</param>
        /// <param name="secuType">The SecurityType of secuType.</param>
        /// <returns>A string type of exchange code.</returns>
        public static string GetExchangeCode(string secuCode, SecurityType secuType)
        {
            secuCode = secuCode.Trim();
            if (IsFutures(secuCode) || secuType == SecurityType.Futures)
            {
                return Exchange.CFFEX;
            }
            else
            {
                int temp = -1;
                if (int.TryParse(secuCode, out temp))
                {
                    if (secuType == SecurityType.Stock)
                    {
                        if (temp > 0 && temp <= 399999)
                        {
                            return Exchange.SZSE;
                        }
                        else if (temp >= 60000 && temp <= 699999)
                        {
                            return Exchange.SHSE;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else if (secuType == SecurityType.Index)
                    {
                        if (temp >= 399001 && temp <= 399998)
                        {
                            return Exchange.SZSE;
                        }
                        else if (temp > 0 && temp <= 999 || temp >= 930901 && temp <= 950110)
                        {
                            return Exchange.SHSE;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
                else
                {
                    return string.Empty;
                }
            }

            return string.Empty;
        }

        private static bool IsFutures(string secuCode)
        {
            return secuCode.StartsWith("IF") || secuCode.StartsWith("IH") || secuCode.StartsWith("IC");
        }
    }
}
