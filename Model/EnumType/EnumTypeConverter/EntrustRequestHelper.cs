
using Model.Constant;
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
                case ConstVariable.ShanghaiExchange:
                    marketNo = "1";
                    break;
                case ConstVariable.ShenzhenExchange:
                    marketNo = "2";
                    break;
                case ConstVariable.ChinaFinancialFuturesExchange:
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

        public static bool TryParseThirdReff(string str, out int commandId, out int submitId, out int requestId)
        {
            commandId = 0;
            submitId = 0;
            requestId = 0;

            bool result = false;
            if(str.Contains(";"))
            {
                var arr = str.Split(';');
                int[] intArr = new int[arr.Length];
                for(int i = 0, count = arr.Length; i < count; i++)
                {
                    int temp = 0;
                    if(int.TryParse(arr[i], out temp))
                    {
                        intArr[i] = temp;
                    }
                    else
                    {
                        intArr[i] = 0;
                    }

                }

                if (arr.Length == 3)
                {
                    commandId = intArr[0];
                    submitId = intArr[1];
                    requestId = intArr[2];

                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        public static string GetExchangeCode(string marketNo)
        {
            string exchangeCode = string.Empty;

            switch (marketNo)
            { 
                case "1":
                    exchangeCode = ConstVariable.ShanghaiExchange;
                    break;
                case "2":
                    exchangeCode = ConstVariable.ShenzhenExchange;
                    break;
                case "7":
                    exchangeCode = ConstVariable.ChinaFinancialFuturesExchange;
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
                        if (exchangeCode.Equals(ConstVariable.ShanghaiExchange) || exchangeCode.Equals(ConstVariable.ShenzhenExchange))
                        {
                            direction = EntrustDirection.BuySpot;
                        }
                        else if (exchangeCode.Equals(ConstVariable.ChinaFinancialFuturesExchange))
                        {
                            direction = EntrustDirection.SellOpen;
                        }
                    }
                    break;
                case "2":
                    {
                        if (exchangeCode.Equals(ConstVariable.ShanghaiExchange) || exchangeCode.Equals(ConstVariable.ShenzhenExchange))
                        {
                            direction = EntrustDirection.SellSpot;
                        }
                        else if (exchangeCode.Equals(ConstVariable.ChinaFinancialFuturesExchange))
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
