using Model.Binding;
using Model.UFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class EntrustFlowItem
    {
        [BindingAttribute("commandno")]
        public int CommandNo { get; set; }

        [BindingAttribute("selection")]
        public bool Selection { get; set; }

        [BindingAttribute("market")]
        public string Market { get; set; }

        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("entrustdirection")]
        public string EntrustDirection { get; set; }

        [BindingAttribute("pricetype")]
        public string PriceType { get; set; }

        [BindingAttribute("entrustprice")]
        public double EntrustPrice { get; set; }

        [BindingAttribute("entrustamount")]
        public int EntrustAmount { get; set; }

        [BindingAttribute("entruststatus")]
        public string EntrustStatus { get; set; }

        [BindingAttribute("dealamount")]
        public int DealAmount { get; set; }

        [BindingAttribute("dealmoney")]
        public double DealMoney { get; set; }

        [BindingAttribute("fundname")]
        public string FundName { get; set; }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        [BindingAttribute("investtype")]
        public string InvestmentType { get; set; }

        [BindingAttribute("pricebias")]
        public double PriceBias { get; set; }

        [BindingAttribute("dealtimes")]
        public int DealTimes { get; set; }

        [BindingAttribute("effectdate")]
        public string EffectDate { get; set; }

        [BindingAttribute("entrustdate")]
        public string EntrustedDate { get; set; }

        [BindingAttribute("firstdealdate")]
        public string FirstDealDate { get; set; }

        [BindingAttribute("shareholdercode")]
        public string ShareHolderCode { get; set; }

        [BindingAttribute("declareseat")]
        public string DeclareSeat { get; set; }

        [BindingAttribute("withdrawcause")]
        public string WithdrawCause { get; set; }

        [BindingAttribute("turnoverratio")]
        public double TurnoverRatio { get; set; }

        [BindingAttribute("entrustedtime")]
        public string EntrustedTime { get; set; }

        [BindingAttribute("withdrawamount")]
        public int WithdrawAmount { get; set; }

        [BindingAttribute("instanceid")]
        public int InstanceId { get; set; }

        [BindingAttribute("instanceno")]
        public string InstanceNo { get; set; }

        [BindingAttribute("entrustno")]
        public int EntrustNo { get; set; }

        [BindingAttribute("entrustbatchno")]
        public int EntrustBatchNo { get; set; }

        [BindingAttribute("declareno")]
        public int DeclareNo { get; set; }

        [BindingAttribute("errorno")]
        public int ErrorNo { get; set; }

        [BindingAttribute("errorinfo")]
        public string ErrorInfo { get; set; }

        public int RequestId { get; set; }

        public string FundCode { get; set; }

        public string Operator { get; set; }

        public string PortfolioCode { get; set; }

        public UFXMarketCode EMarketCode { get; set; }

        public UFXEntrustDirection EEntrustDirection { get; set; }
    }
}
