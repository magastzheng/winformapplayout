
namespace Model.EnumType
{
    //对现货、期货对应的看多、看空状态，目前版本仅使用现货看多、期货看空状态
    //持有股票则认为是现货看多，卖出期货指数认为是期货看空
    //对当前版本：股票对应现货看多，期指对应期货看空
    public enum PositionType
    {
        //现货看多
        SpotLong = 1,
        
        //现货看空
        SpotShort = 2,
        
        //期货看多
        FuturesLong = 3,
        
        //期货看空
        FuturesShort = 4,
    }
}
