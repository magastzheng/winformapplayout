
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
                    marketNo = "7";
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

        public static string GetFuturesDirection(EntrustDirection eDirection)
        {
            string direction = string.Empty;

            switch (eDirection)
            {
                case EntrustDirection.SellOpen:
                    {
                        //平仓
                        direction = "1";
                    }
                    break;
                case EntrustDirection.BuyClose:
                    {
                        //开仓
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

        public static string GenerateThirdReff(int commandId, int submitId, int requestId)
        {
            return string.Format("{0};{1};{2}", commandId, submitId, requestId);
        }

        public static string GetExchangeCode(string marketNo)
        {
            string exchangeCode = string.Empty;

            switch (marketNo)
            { 
                case "1":
                    exchangeCode = "SSE";
                    break;
                case "2":
                    exchangeCode = "SZSE";
                    break;
                case "7":
                    exchangeCode = "CFFEX";
                    break;
                default:
                    break;
            }

            return exchangeCode;
        }

        public static EntrustDirection GetEntrustDirectionType(string directionCode, string exchangeCode)
        {
            EntrustDirection direction = EntrustDirection.None;

            switch (directionCode)
            {
                case "1":
                    {
                        if (exchangeCode.Equals("SSE") || exchangeCode.Equals("SZSE"))
                        {
                            direction = EntrustDirection.BuySpot;
                        }
                        else if (exchangeCode.Equals("CFFEX"))
                        {
                            direction = EntrustDirection.SellOpen;
                        }
                    }
                    break;
                case "2":
                    {
                        if (exchangeCode.Equals("SSE") || exchangeCode.Equals("SZSE"))
                        {
                            direction = EntrustDirection.SellSpot;
                        }
                        else if (exchangeCode.Equals("CFFEX"))
                        {
                            direction = EntrustDirection.BuyClose;
                        }
                    }
                    break;
                default:
                    break;
            }

            return direction;
        }

        public static EntrustStatus GetEntrustStatusType(string statusCode)
        {
            EntrustStatus status = EntrustStatus.NoExecuted;

            return status;
        }

        public static bool ParseThirdReff(string thirdReff, out int commandId, out int submitId, out int requestId)
        {
            bool ret = false;

            if (string.IsNullOrEmpty(thirdReff))
            {
                commandId = -1;
                submitId = -1;
                requestId = -1;

                return ret;
            }

            var parts = thirdReff.Split(';');
            if (parts.Length == 3)
            {
                int temp = -1;
                if (int.TryParse(parts[0], out temp))
                {
                    commandId = temp;
                }
                else
                {
                    commandId = -1;
                }

                if (int.TryParse(parts[1], out temp))
                {
                    submitId = temp;
                }
                else
                {
                    submitId = -1;
                }

                if (int.TryParse(parts[2], out temp))
                {
                    requestId = temp;
                }
                else
                {
                    requestId = -1;
                }

                if(commandId == -1 || submitId == -1 || requestId == -1)
                {
                    ret = false;
                }
                else
                {
                    ret = true;
                }
            }
            else
            {
                commandId = -1;
                submitId = -1;
                requestId = -1;
            }

            return ret;
        }
    }
}
