using Model.Binding;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    //指令证券
    public class CommandSecurityItem
    {
        //选择
        [BindingAttribute("selection")]
        public bool Selection { get; set; }

        //证券代码
        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        //证券名称
        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        //指令序号
        [BindingAttribute("commandid")]
        public int CommandId { get; set; }

        //基金代码
        [BindingAttribute("fundcode")]
        public string FundCode { get; set; }

        //组合名称
        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        //指令价格
        [BindingAttribute("commandprice")]
        public double CommandPrice { get; set; }

        //指令数量
        [BindingAttribute("commandamount")]
        public int CommandAmount { get; set; }

        //委托方向
        [BindingAttribute("entrustdirection")]
        public string EntrustDirection { get; set; }

        //已委托数量
        [BindingAttribute("entrustedamount")]
        public int EntrustedAmount { get; set; }

        //价格类型
        [BindingAttribute("pricetype")]
        public string PriceType { get; set; }

        //委托价格
        [BindingAttribute("entrustprice")]
        public double EntrustPrice { get; set; }

        //本次委托数量
        [BindingAttribute("thisentrustamout")]
        public int ThisEntrustAmount { get; set; }

        //已成交数量
        [BindingAttribute("dealamount")]
        public int DealAmount { get; set; }

        //目标数量
        [BindingAttribute("targetamount")]
        public int TargetAmount { get; set; }

        //待补足数量
        [BindingAttribute("waitamount")]
        public int WaitAmount { get; set; }

        //停牌标志
        [BindingAttribute("suspensionflag")]
        public string SuspensionFlag  { get; set; }

        //目标份数
        [BindingAttribute("targetcopies")]
        public int TargetCopies { get; set; }

        //指令份数
        [BindingAttribute("commandcopies")]
        public int CommandCopies { get; set; }

        //涨停价
        [BindingAttribute("limitupprice")]
        public double LimitUpPrice { get; set; }

        //跌停价
        [BindingAttribute("limitdownprice")]
        public double LimitDownPrice { get; set; }

        //涨跌停
        [BindingAttribute("limitupdown")]
        public string LimitUpOrDown { get; set; }

        public SecurityType SecuType { get; set; }

        public int WeightAmount { get; set; }

        public EntrustStatus EntrustStatus { get; set; }
    }
}
