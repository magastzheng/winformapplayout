
using Model.EnumType;
using System.Text;
namespace Config.ParamConverter
{
    public class EntrustRequestHelper
    {
        public static string GetMarketNo(string exchangeCode)
        {
            string marketNo = string.Empty;
            switch (exchangeCode)
            { 
                case "SSE":
                    marketNo = "1";
                    break;
                case "SZSE":
                    marketNo = "2";
                    break;
                case "CFFEX":
                    break;
                default:
                    break;
            }

            return marketNo;
        }

        public static string GetEntrustDirection(EntrustDirection eDirection)
        {
            string direction = string.Empty;

            switch (eDirection)
            {
                case EntrustDirection.BuySpot:
                case EntrustDirection.BuyClose:
                    {
                        direction = "1";
                    }
                    break;
                case EntrustDirection.SellSpot:
                case EntrustDirection.SellOpen:
                    {
                        direction = "2";
                    }
                    break;
                default:
                    break;
            }

            return direction;
        }

        public static string GetEntrustPriceType(EntrustPriceType eEntrustPriceType)
        {
            string entrustpricetype = string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append((char)eEntrustPriceType);
            entrustpricetype = sb.ToString();

            return entrustpricetype;
        }
    }
}
