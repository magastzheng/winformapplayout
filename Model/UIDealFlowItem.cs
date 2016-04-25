using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //成交流水
    public class UIDealFlowItem
    {
        //指令序号
        public int CommandNo { get; set; }

        //证券代码
        public string SecurityCode { get; set; }

        //证券名称
        public string SecurityName { get; set; }

        //基金序号
        public string FundNo { get; set; }

        //基金名称
        public string FundName { get; set; }

        //组合代码
        public string PortfolioCode { get; set; }

        //组合名称
        public string PortfolioName { get; set; }

        //价格类型
        public string PriceType { get; set; }

        //委托价格
        public double EntrustPrice { get; set; }

        //委托方向
        public string EntrustDirection { get; set; }

        //成交价格
        public double DealPrice { get; set; }

        //成交数量
        public int DealAmount { get; set; }

        //成交金额
        public double DealMoney { get; set; }

        //成交时间
        public string DealTime { get; set; }

        //股东代码
        public string ShareHolderCode { get; set; }

        //申报序号
        public string DeclareNo { get; set; }

        //申报席位
        public string DeclareSeat { get; set; }

        //委托批号
        public string EntrustBatchNo { get; set; }

        //实例号
        public string InstanceId { get; set; }

        //实例编号
        public string InstanceNo { get; set; }

        //委托序号
        public string EntrustNo { get; set; }

        //成交序号
        public string DealNo { get; set; }
    }
}
