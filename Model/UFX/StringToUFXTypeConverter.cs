
using System;
namespace Model.UFX
{
    public static class StringToUFXTypeConverter
    {
        public static UFXMarketCode GetMarketCode(string code)
        {
            UFXMarketCode eCode = UFXMarketCode.ShanghaiStockExchange;
            bool isExisted = false;
            int temp;
            if (int.TryParse(code, out temp))
            {
                if (Enum.IsDefined(typeof(UFXMarketCode), temp))
                {
                    isExisted = true;
                    eCode = (UFXMarketCode)Enum.ToObject(typeof(UFXMarketCode), temp);
                }
            }

            if (!isExisted)
            {
                string msg = string.Format("The UFXMarketCode [{0}] is not supported!", code);
                throw new NotSupportedException(msg);
            }

            return eCode;
        }

        public static UFXEntrustDirection GetEntrustDirection(string direction)
        {
            UFXEntrustDirection eDirection = UFXEntrustDirection.Buy;

            bool isExisted = false;
            int temp;
            if (int.TryParse(direction, out temp))
            {
                if (Enum.IsDefined(typeof(UFXEntrustDirection), temp))
                {
                    isExisted = true;
                    eDirection = (UFXEntrustDirection)Enum.ToObject(typeof(UFXEntrustDirection), temp);
                }
            }

            if (!isExisted)
            {
                string msg = string.Format("The UFXEntrustDirection [{0}] is not supported!", direction);
                throw new NotSupportedException(msg);
            }

            return eDirection;
        }
    }
}
