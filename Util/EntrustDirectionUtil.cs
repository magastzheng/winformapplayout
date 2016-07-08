using Model.EnumType;
using System;

namespace Util
{
    public class EntrustDirectionUtil
    {
        public static EntrustDirection GetEntrustDirection(string tradeDirection)
        {
            int temp = 0;
            EntrustDirection direction = EntrustDirection.Buy;
            if (int.TryParse(tradeDirection, out temp))
            {
                if (Enum.IsDefined(typeof(EntrustDirection), temp))
                {
                    direction = (EntrustDirection)Enum.ToObject(typeof(EntrustDirection), temp);
                }
            }

            return direction;
        }
    }
}
