using Model.Binding;
using Model.Constant;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using Model.SecurityInfo;
using System;

namespace Model.UI
{
    public class CommandManagementEntrustItem
    {
        [BindingAttribute("entrustno")]
        public int EntrustNo { get; set; }

        [BindingAttribute("fundname")]
        public string FundName { get; set; }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("entrustdirection")]
        public string EntrustDirection
        {
            get { return EnumTypeDisplayHelper.GetEntrustDirection(EDirection); }
        }

        [BindingAttribute("pricetype")]
        public string PriceType
        {
            get { return EntrustPriceTypeConverter.GetPriceTypeName(EEntrustPriceType); }
        }

        [BindingAttribute("entrustprice")]
        public double EntrustPrice { get; set; }

        [BindingAttribute("effectivedate")]
        public string EffectiveDate
        {
            get { return DateFormat.Format(DEntrustDate, ConstVariable.DateFormat); }
        }

        [BindingAttribute("entrustamount")]
        public int EntrustAmount { get; set; }

        [BindingAttribute("entruststatus")]
        public string EntrustStatus
        {
            get { return CommandStatusHelper.GetEntrustName(EEntrustStatus); }
        }

        [BindingAttribute("todaydealamount")]
        public int TodayDealAmount { get; set; }

        [BindingAttribute("todaydealmoney")]
        public double TodayDealMoney { get; set; }

        [BindingAttribute("entrusttime")]
        public string EntrustTime
        {
            get { return DateFormat.Format(DEntrustDate, ConstVariable.TimeFormat); }
        }

        [BindingAttribute("dealpercent")]
        public double DealPercent { get; set; }

        public SecurityType SecuType { get; set; }

        public EntrustDirection EDirection { get; set; }

        public EntrustStatus EEntrustStatus { get; set; }

        public EntrustPriceType EEntrustPriceType { get; set; }

        public DateTime DEntrustDate { get; set; }

        public int CommandId { get; set; }
    }
}
