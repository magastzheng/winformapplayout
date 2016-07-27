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

            switch (exchange)
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
    }
}
