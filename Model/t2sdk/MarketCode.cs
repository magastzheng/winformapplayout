using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.t2sdk
{
    //交易所
    public enum MarketCode
    {
        ShanghaiStockExchange = 1,
        ShenzhenStockExchange = 2,
        ShanghaiFuturesExchange = 3,
        ZhengzhouCommodityExchange = 4,
        ChinaFinancialFuturesExchange = 7,
    }

    //委托方向
    public enum EntrustDirection
    { 
        //买入
        Buy = 1,

        //卖出
        Sell = 2,

        //债券买入
        BuyBond = 3,

        //债券卖出
        SellBond = 4,

        //融资正回购
        SellRepo = 5,

        //融资逆回购
        AntiRepo = 6,

        //配股认购
        Subscription = 9,

        //申购
        Purchase = 12,
    }

    //委托状态
    public enum EntrustState
    { 
        NoReport = '1',
        WaitReport = '2',
        Reporting = '3',
        Reported = '4',
        //废单
        Scrap = '5',

        PartDone = '6',
        Done = '7',
        PartCancel = '8',
        CancelDone = '9',
        WaitCancel = 'a',

        //未审批
        NotApproved = 'b',
        ApprovalReject = 'c',
        NotApprovedCancel = 'd',
        NotCancel = 'A',
        WaitCancel2 = 'B',
        Canceling = 'C',
        CancelReg = 'D',
        CancelScrap = 'E',
        CancelDone2 = 'F',
    }

    //开平仓
    public enum OpenPositionDirection
    { 
        //开仓
        Open = 1,
        //平仓
        Position = 2,
        //交割
        Delivery = 3,

        //平今仓
        MarketPosition = 4,
    }

    //交易实例类型
    public class TradingInstanceType
    {
        //股指期货期现套利
        public static string StockIndexFutures = "A";
        //股指期货跨期套利
        public static string CrossPeriodArbitrageOfStockIndexFutures = "B";

        //EFT套利
        public static string EFTArbitrage = "C";

        //国债期货期现套利
        public static string TreasuryFuturesArbitrage = "D";

        //国债期货跨期套利
        public static string CrossPeriodArbitrageOfTreasuryBondFutures = "E";

        //个股期权套利
        public static string StockOptionArbitrage = "F";

        //自定义
        public static string Custom = "Z";
    }

    //消息推送类型
    public enum PushMessageType
    { 
        None,

        //委托下达
        EntrustCommit = 'a', 

        //委托确认
        EntrustConfirm = 'b',

        //委托废单
        EntrustScrap = 'c',

        //委托撤单
        EntrustWithdraw = 'd',

        //委托撤成
        EntrustWithdrawDone = 'e',

        //委托撤废
        EntrustWithdrawWaste = 'f',

        //委托成交
        EntrustDeal = 'g',
    }
}
