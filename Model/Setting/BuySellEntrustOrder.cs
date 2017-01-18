
namespace Model.Setting
{
    public enum BuySellEntrustOrder
    {
        //按市值从高到低
        CapitalHigh2Low  = 1,

        //按市值从低到高
        CapitalLow2High = 2,

        //中金所、深交所、上交所
        CFXSZSESSE  = 3,

        //上交所、深交所、中金所
        SSESZSECFX = 4,
    }
}
