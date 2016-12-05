using Model.EnumType;
using Model.UFX;

namespace Model.Converter
{
    public static class EntrustDirectionConverter
    {
        public static EntrustDirection GetFuturesEntrustDirection(UFXEntrustDirection entrustDirection, UFXFuturesDirection futuresDirection)
        {
            EntrustDirection eDirection = EntrustDirection.None;

            if (entrustDirection == UFXEntrustDirection.Sell && futuresDirection == UFXFuturesDirection.Open)
            {
                eDirection = EntrustDirection.SellOpen;
            }
            else if (entrustDirection == UFXEntrustDirection.Sell && futuresDirection == UFXFuturesDirection.Close)
            {
                eDirection = EntrustDirection.SellClose;
            }
            else if (entrustDirection == UFXEntrustDirection.Buy && futuresDirection == UFXFuturesDirection.Close)
            {
                eDirection = EntrustDirection.BuyClose;
            }
            else if (entrustDirection == UFXEntrustDirection.Buy && futuresDirection == UFXFuturesDirection.Open)
            {
                eDirection = EntrustDirection.BuyOpen;
            }

            return eDirection;
        }

        public static EntrustDirection GetSecurityEntrustDirection(UFXEntrustDirection entrustDirection)
        {
            EntrustDirection eDirection = EntrustDirection.None;

            switch (entrustDirection)
            { 
                case UFXEntrustDirection.Buy:
                    eDirection = EntrustDirection.BuySpot;
                    break;
                case UFXEntrustDirection.Sell:
                    eDirection = EntrustDirection.SellSpot;
                    break;
                default:
                    break;
            }

            return eDirection;
        }
    }
}
