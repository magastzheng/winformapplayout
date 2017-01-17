
namespace Model.Quote
{
    public enum SuspendFlag
    { 
        Unknown = -1,

        //不停
        NoSuspension = 0,

        //停1h
        Suspend1Hour = 1,

        //停2h
        Suspend2Hour = 2,

        //停半天
        SuspendHalfDay = 3,

        //停下午
        SuspendAfternoon = 4,

        //停半小时
        SuspendHalfHour = 5,

        //临时停牌
        SuspendTemp = 6,

        //停牌一天
        Suspend1Day = 9,

        //涨跌停
        SuspendLimit = 10,
    }

    public enum TradingStatus
    {
        //无状态/状态未知
        Unknown = 0,

        //正常交易中
        Normal = 1,

        //休市中/暂停交易
        Suspend = 2,

        //已收盘/当日交易结束
        Close = 3,

        //集合竞价中
        CallAuction = 4,

        //暂停交易（深交所临时停牌）
        SuspendSZSE = 5,

        //盘前交易 PreMarket
        PreMarket = 8,

        //盘后交易 AfterMarket
        AfterMarket = 9,

    }

    public enum LimitUpDownFlag
    { 
        Suspend = -1,
        Normal = 0,
        LimitUp = 1,
        LimitDown = 2,
    }

    public class MarketData
    {
        public string InstrumentID;

        public double CurrentPrice;

        public double BuyPrice1;

        public double BuyPrice2;

        public double BuyPrice3;

        public double BuyPrice4;

        public double BuyPrice5;

        public double SellPrice1;

        public double SellPrice2;

        public double SellPrice3;

        public double SellPrice4;

        public double SellPrice5;

        //外盘
        public long BuyAmount;

        //内盘
        public long SellAmount;

        public SuspendFlag SuspendFlag;

        public TradingStatus TradingStatus;

        public LimitUpDownFlag LimitUpDownFlag;

        public double HighLimitPrice;

        public double LowLimitPrice;

        public double PreClose;

        public string UpdateTime;

    }
}
