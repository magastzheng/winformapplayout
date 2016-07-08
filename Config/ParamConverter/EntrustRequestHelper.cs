
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

        //public static string 
    }
}
