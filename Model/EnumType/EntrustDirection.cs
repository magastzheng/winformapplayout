
namespace Model.EnumType
{
    public enum EntrustDirection
    {
        None = -1,
        Buy = 1,
        Sell = 2,
        AdjustedToBuySell = 3,
        AdjustedToBuy = 4,
        AdjustedToSell = 5,

        BuySpot = 10,
        SellSpot = 11,
        SellOpen = 12,
        BuyClose = 13
    }
}
