
using Model.Binding;
using System.ComponentModel;
namespace Model.UFX
{
    //交易所
    public enum UFXMarketCode
    {
        //上海证券交易所
        [Description("上交所")]
        [StandardCode("SSE")]
        ShanghaiSecurityExchange = 1,
        
        //深圳交易所
        [Description("深交所")]
        [StandardCode("SZSE")]
        ShenzhenSecurityExchange = 2,
        
        //上海期货交易所
        [Description("上期所")]
        [StandardCode("SHFE")]
        ShanghaiFuturesExchange = 3,
        
        //郑州商品交易所
        [Description("郑商所")]
        [StandardCode("CZCE")]
        ZhengzhouCommodityExchange = 4,

        //中国金融期货交易所
        [Description("中金所")]
        [StandardCode("CFFEX")]
        ChinaFinancialFuturesExchange = 7,

        //大连商品交易所
        [Description("大商所")]
        [StandardCode("DCE")]
        DalianCommodityExchange = 9,
    }

    //委托方向
    public enum UFXEntrustDirection
    { 
        //买入
        [Description("买入")]
        Buy = 1,

        //卖出
        [Description("卖出")]
        Sell = 2,

        //债券买入
        [Description("债券买入")]
        BuyBond = 3,

        //债券卖出
        [Description("债券卖出")]
        SellBond = 4,

        //融资正回购
        [Description("融资正回购")]
        SellRepo = 5,

        //融资逆回购
        [Description("融资逆回购")]
        AntiRepo = 6,

        //配股认购
        [Description("配股认购")]
        Subscription = 9,

        //债转股
        [Description("债转股")]
        DebtToEquity = 10,

        //债回售
        [Description("债回售")]
        SaleOfDebt = 11,

        //申购
        [Description("申购")]
        Purchase = 12,

        //基金认购
        [Description("基金认购")]
        SubscribingFund = 13,

        //转托管
        [Description("转托管")]
        CustodyTransfer = 17,

        //ETF申购
        [Description("ETF申购")]
        PurchasingETF = 26,

        //ETF赎回
        [Description("ETF赎回")]
        RedeemingETF = 27,

        //行权认购
        [Description("行权认购")]
        ExecuteCallOption = 28,

        //行权认沽
        [Description("行权认沽")]
        ExecutePutOption = 29,

        //提交质押
        [Description("提交质押")]
        SubmitPledge = 30,

        //转回质押
        [Description("转回质押")]
        ReversalOfPledge = 31,

        //基金分拆
        [Description("基金分拆")]
        FundSplit = 50,

        //基金合并
        [Description("基金合并")]
        FundConsolidation = 51,

        //开基申购
        [Description("开基申购")]
        PurchasingOE = 53,

        //开基赎回
        [Description("开基赎回")]
        RedeemingOE = 54,

        //债券认购
        [Description("债券认购")]
        SubscribingBond = 55,

        //保证券锁定
        [Description("保证券锁定")]
        LockSecurity = 63,

        //保证券解锁
        [Description("保证券解锁")]
        UnlockSecurity = 64,

        //融券卖出
        [Description("融券卖出")]
        MarginSelling = 67,

        //买券还券
        [Description("买券还券")]
        BuyCoupon = 68,

        //直接还款
        [Description("直接还款")]
        DirectRepayment = 69,

        //直接还券
        [Description("直接还券")]
        DirectCoupon = 70,

        //融资买入
        [Description("融资买入")]
        Buyout = 75,

        //卖券还款
        [Description("卖券还款")]
        SellingCoupon = 76,
    }

    public enum UFXFuturesDirection
    { 
        //开仓
        [Description("开仓")]
        Open = 1,

        //平仓
        [Description("平仓")]
        Close = 2,
    }

    //委托状态
    public enum UFXEntrustState
    { 
        //未报
        [Description("未报")]
        NoReport = '1',
        //待报
        [Description("待报")]
        WaitReport = '2',
        //正报
        [Description("正报")]
        Reporting = '3',
        //已报
        [Description("已报")]
        Reported = '4',
        //废单
        [Description("废单")]
        Scrap = '5',
        //部成
        [Description("部成")]
        PartDone = '6',
        //已成
        [Description("已成")]
        Done = '7',
        //部撤
        [Description("部撤")]
        PartCancel = '8',
        //已撤
        [Description("已撤")]
        CancelDone = '9',
        //待撤
        [Description("待撤")]
        WaitCancel = 'a',

        //未审批
        [Description("未审批")]
        NotApproved = 'b',
        //审批拒绝
        [Description("审批拒绝")]
        ApprovalReject = 'c',
        //未审批即撤销
        [Description("未审批即撤销")]
        NotApprovedCancel = 'd',
        //未撤
        [Description("未撤")]
        NotCancel = 'A',
        //待撤
        [Description("待撤")]
        WaitCancel2 = 'B',
        //正撤
        [Description("正撤")]
        Canceling = 'C',
        //撤认
        [Description("撤认")]
        CancelReg = 'D',
        //撤废
        [Description("撤废")]
        CancelScrap = 'E',
        //已撤
        [Description("已撤")]
        CancelDone2 = 'F',
    }

    //开平仓
    public enum UFXOpenPositionDirection
    { 
        //开仓
        [Description("开仓")]
        Open = 1,
        //平仓
        [Description("平仓")]
        Position = 2,
        //交割
        [Description("交割")]
        Delivery = 3,

        //平今仓
        [Description("平今仓")]
        MarketPosition = 4,
    }

    //交易实例类型
    public struct UFXTradingInstanceType
    {
        //股指期货期现套利
        public const string StockIndexFutures = "A";
        //股指期货跨期套利
        public const string CrossPeriodArbitrageOfStockIndexFutures = "B";

        //EFT套利
        public const string EFTArbitrage = "C";

        //国债期货期现套利
        public const string TreasuryFuturesArbitrage = "D";

        //国债期货跨期套利
        public const string CrossPeriodArbitrageOfTreasuryBondFutures = "E";

        //个股期权套利
        public const string StockOptionArbitrage = "F";

        //自定义
        public const string Custom = "Z";
    }

    //消息推送类型
    public enum UFXPushMessageType
    { 
        None,

        //委托下达
        EntrustCommit = 'a', 

        //委托确认
        EntrustConfirm = 'b',

        //委托废单
        EntrustFailed = 'c',

        //委托撤单
        EntrustWithdraw = 'd',

        //委托撤成
        EntrustWithdrawDone = 'e',

        //委托撤废
        EntrustWithdrawFailed = 'f',

        //委托成交
        EntrustDeal = 'g',
    }
}
