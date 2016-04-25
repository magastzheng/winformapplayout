using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.strategy
{
    public class TradingInstance
    {
        //交易实例编号
        public int InstanceNo { get; set; }

        //账户组代码
        public string AccountGroupCode { get; set; }

        //交易实例类型
        public string InstanceType { get; set; }

        //操作份数
        public int OperateNum { get; set; }

        //创建日期
        public string CreateDate { get; set; }    

        //外部方案号
        public string ExtInvestPlanNo { get; set; }
    }

    public class SpotTemplateStockItem
    {
        //现货模板序列号
        public int TemplateNo { get; set; }

        //市场代码
        public string MarketNo { get; set; }

        //证券代码
        public string StockCode { get; set; }

        //权重
        public double PowerRate { get; set; }

        //替代市场代码1
        public string ReplaceMarketNo1 { get; set; }

        //替代证券代码1
        public string ReplaceStockCode1 { get; set; }

        //替代权重1
        public double ReplacePowerRate1 { get; set; }

        //替代市场代码2
        public string ReplaceMarketNo2 { get; set; }

        //替代证券代码2
        public string ReplaceStockCode2 { get; set; }

        //替代权重2
        public double ReplacePowerRate2 { get; set; }

        //替代市场代码3
        public string ReplaceMarketNo3 { get; set; }

        //替代证券代码3
        public string ReplaceStockCode3 { get; set; }

        //替代权重3
        public double ReplacePowerRate3 { get; set; }
    }

    public class EntrustItem
    {
        //委托批号
        public int BatchNo { get; set; }

        //账户组代码
        public string AccountGroupCode { get; set; }

        //交易实例编号
        public string InstanceNo { get; set; }

        //交易市场
        public string MarketNo { get; set; }

        //证券代码
        public string StockCode { get; set; }

        //股东代码
        public string StockHolderId { get; set; }

        //申报席位
        public string ReportSeat { get; set; }

        //申报方向
        public string EntrustDirection { get; set; }

        //开平方向
        public string FuturesDirection { get; set; }

        //备兑标志
        public string ConvertFlag { get; set; }

        //委托价格类型
        public string PriceType { get; set; }

        //委托价格
        public double EntrustPrice { get; set; }

        //委托数量
        public int EntrustAmount { get; set; }

        //最小委托比例
        public double LimitEntrustRatio { get; set; }

        //第三方系统自定义号
        public int ExtSystemId { get; set; }

        //第三方系统自定义说明
        public string ThirdReff { get; set; }
    }
}
