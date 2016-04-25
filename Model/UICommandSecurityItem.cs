using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //指令证券
    public class UICommandSecurityItem
    {
        //选择
        public int Selected { get; set; }

        //证券代码
        public string SecurityCode { get; set; }

        //证券名称
        public string SecurityName { get; set; }

        //指令序号
        public int CommandNo { get; set; }

        //基金代码
        public string FundCode { get; set; }

        //组合名称
        public string PortfolioName { get; set; }

        //指令价格
        public double CommandPrice { get; set; }

        //指令数量
        public int CommandAmount { get; set; }

        //委托方向
        public string EntrustDirection { get; set; }

        //已委托数量
        public int EntrustedAmount { get; set; }

        //价格类型
        public string PriceType { get; set; }

        //委托价格
        public double EntrustedPrice { get; set; }

        //本次委托数量
        public int ThisEntrustAmount { get; set; }

        //已成交数量
        public int DealAmount { get; set; }

        //目标数量
        public int TargetAmount { get; set; }

        //待补足数量
        public int WaitAmount { get; set; }

        //停牌标志
        public string SuspensionFlag  { get; set; }

        //目标份数
        public int TargetCopies { get; set; }

        //指令份数
        public int CommandCopies { get; set; }

        //涨停价
        public double LimitUpPrice { get; set; }

        //跌停价
        public double LimitDownPrice { get; set; }

        //涨跌停
        public string LimitUpOrDown { get; set; }
    }
}
