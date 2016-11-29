using Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    //成交流水
    public class DealFlowItem
    {
        //指令序号
        [BindingAttribute("commandno")]
        public int CommandNo { get; set; }

        //证券代码
        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        //证券名称
        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        //基金序号
        [BindingAttribute("fundno")]
        public string FundNo { get; set; }

        //基金名称
        [BindingAttribute("fundname")]
        public string FundName { get; set; }

        //组合代码
        [BindingAttribute("portfoliocode")]
        public string PortfolioCode { get; set; }

        //组合名称
        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        //价格类型
        [BindingAttribute("pricetype")]
        public string PriceType { get; set; }

        //委托价格
        [BindingAttribute("entrustprice")]
        public double EntrustPrice { get; set; }

        //委托方向
        [BindingAttribute("entrustdirection")]
        public string EntrustDirection { get; set; }

        //成交价格
        [BindingAttribute("dealprice")]
        public double DealPrice { get; set; }

        //成交数量
        [BindingAttribute("dealamount")]
        public int DealAmount { get; set; }

        //成交金额
        [BindingAttribute("dealmoney")]
        public double DealMoney { get; set; }

        //成交时间
        [BindingAttribute("dealtime")]
        public string DealTime { get; set; }

        //股东代码
        [BindingAttribute("shareholdercode")]
        public string ShareHolderCode { get; set; }

        //申报序号
        [BindingAttribute("declareno")]
        public string DeclareNo { get; set; }

        //申报席位
        [BindingAttribute("declareseat")]
        public string DeclareSeat { get; set; }

        //委托批号
        [BindingAttribute("entrustbatchno")]
        public string EntrustBatchNo { get; set; }

        //实例号
        [BindingAttribute("instanceid")]
        public string InstanceId { get; set; }

        //实例编号
        [BindingAttribute("instanceno")]
        public string InstanceNo { get; set; }

        //委托序号
        [BindingAttribute("entrustno")]
        public string EntrustNo { get; set; }

        //成交序号
        [BindingAttribute("dealno")]
        public string DealNo { get; set; }

        public string ExchangeCode { get; set; }
    }
}
