
namespace Model.EnumType
{
    public enum EntrustDirection
    {
        None = -1,

        //买入
        Buy = 1,

        //卖出
        Sell = 2,

        //调成到买卖
        AdjustedToBuySell = 3,

        //调整到买入
        AdjustedToBuy = 4,

        //调整到卖出
        AdjustedToSell = 5,

        //买入现货
        BuySpot = 10,

        //卖出现货
        SellSpot = 11,

        //卖出开仓
        SellOpen = 12,

        //买入平仓
        BuyClose = 13,

        //买入开仓
        BuyOpen = 14,

        //卖出平仓
        SellClose = 15,
    }
}
