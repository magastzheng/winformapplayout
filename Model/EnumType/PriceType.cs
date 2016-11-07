
using System.ComponentModel;
namespace Model.EnumType
{
    public enum PriceType
    {
        None = -1,
        //市价
        Market = 0,  
        //指定价
        Assign = 1,
        //最新价
        Last = 2,
        //自动盘口
        Automatic = 3,
        //任意价
        Arbitrary = 4,
        //卖一价
        Sell1 = 10,
        Sell2,
        Sell3,
        Sell4,
        Sell5,
        Sell6,
        Sell7,
        Sell8,
        Sell9,
        Sell10,
        //买一价
        Buy1 = 20,
        Buy2,
        Buy3,
        Buy4,
        Buy5,
        Buy6,
        Buy7,
        Buy8,
        Buy9,
        Buy10,
    }

    public enum EntrustPriceType
    { 
        //限价
        [Description("限价")]
        FixedPrice = '0',

        //任意价（上交所股票期权）
        [Description("任意价")]
        Arbitrary = '1',

        //市价剩转限价（上交所股票期权）
        [Description("市价剩转限价")]
        TransferPrice = '2',

        //市价剩余撤销（上交所股票期权）
        [Description("市价剩余撤销")]
        ResidualMarketWithdrawal = '3',

        //FOK限价（上交所股票期权）
        [Description("限价")]
        FOKLimitSH = '4',

        //FOK市价（上交所股票期权）
        [Description("市价")]
        FOKMarketSH = '5',

        //五档即成剩撤（上交所市价）
        [Description("五档即成剩撤")]
        FifthIsLeftOffSH = 'a',

        //五档即成剩转（上交所市价）
        [Description("五档即成剩转")]
        FifthIsLeftTurnSH = 'b',

        //五档即成剩撤（深交所市价）
        [Description("五档即成剩撤")]
        FifthIsLeftOffSZ = 'A',

        //五档即成剩转（深交所市价）
        [Description("五档即成剩转")]
        LeftTurnSZ = 'C',

        //对手方最优（深交所市价）
        [Description("对手方最优")]
        OppSideOptSZ = 'D',

        //本方最优（深交所市价）
        [Description("本方最优")]
        BestBestSZ = 'E',

        //全额成或撤（FOK市价）（深交所市价）
        [Description("全额成或撤")]
        FOKMarketSZ = 'F',

        //全额成或撤（FOK限价）（上期所、中金所、深交所）
        [Description("全额成或撤")]
        FOKLimit = 'G',

        //即成剩撤（FAK）（上期所、郑商所、中金所）
        [Description("即成剩撤")]
        FAK = 'K',

        //任意价转限价（中金所）
        [Description("任意价转限价")]
        ArbitraryToLimit = 'X',

        //五档即成剩撤（中金所五档市价）
        [Description("五档即成剩撤")]
        FifthIsLeftOffCFX = 'L',

        //五档即成剩转（中金所五档市价转限价）
        [Description("五档即成剩转")]
        FifthIsLeftTurnCFX = 'M',

        //最优一档即成剩撤（中金所最优价）
        [Description("最优一档即成剩撤")]
        BestOneLeftOff = 'N',

        //最优一档即成剩转（中金所最优价）
        [Description("最优一档即成剩转")]
        BestOneLeftTurn = 'O',
    }
}
