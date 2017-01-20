using Model.Binding;
using Model.Constant;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using Model.SecurityInfo;
using System;

namespace Model.UI
{
    public class CancelSecurityItem
    {
        [BindingAttribute("selection")]
        public bool Selection { get; set; }

        [BindingAttribute("commandid")]
        public int CommandId { get; set; }

        [BindingAttribute("entrustno")]
        public int EntrustNo { get; set; }

        [BindingAttribute("exchange")]
        public string Exchange 
        { 
            get 
            {
                return SecurityItemHelper.GetExchange(ExchangeCode); 
            } 
        }

        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("entrustdirection")]
        public string EntrustDirection
        {
            get { return EnumTypeDisplayHelper.GetEntrustDirection(EDirection); }
        }

        [BindingAttribute("commandprice")]
        public string CommandPrice
        {
            get
            {
                return EnumTypeDisplayHelper.GetPriceType(ECommandPrice);
            }
        }

        [BindingAttribute("reportprice")]
        public double ReportPrice { get; set; }

        [BindingAttribute("leftamount")]
        public int LeftAmount { get; set; }

        [BindingAttribute("reportamount")]
        public int ReportAmount { get; set; }

        [BindingAttribute("dealamount")]
        public int DealAmount { get; set; }

        [BindingAttribute("originpricetype")]
        public string OriginPriceType
        {
            get
            {
                return EnumTypeDisplayHelper.GetEntrustPriceType(EOriginPriceType);
            }
        }

        [BindingAttribute("turnoverratio")]
        public double TurnoverRatio { get; set; }

        [BindingAttribute("fundname")]
        public string FundName { get; set; }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        [BindingAttribute("investmenttypename")]
        public string InvestmentTypeName { get; set; }

        [BindingAttribute("lastprice")]
        public double LastPrice { get; set; }

        [BindingAttribute("pricebias")]
        public double PriceBias { get; set; }

        [BindingAttribute("releasedate")]
        public string ReleaseDate
        {
            get
            {
                return DateFormat.Format(EntrustDate, ConstVariable.DateFormat1);
            }
        }

        [BindingAttribute("firstdeal")]
        public int FirstDeal { get; set; }

        [BindingAttribute("dealmoney")]
        public double DealMoney { get; set; }

        [BindingAttribute("entrusttime")]
        public string EntrustTime
        {
            get
            {
                return EntrustDate.ToString("hhmmss");
            }
        }

        [BindingAttribute("dealtimes")]
        public int DealTimes { get; set; }

        [BindingAttribute("cancelamount")]
        public int CancelAmount { get; set; }

        [BindingAttribute("orderno")]
        public int OrderNo { get; set; }

        [BindingAttribute("reportno")]
        public int ReportNo { get; set; }

        [BindingAttribute("entrustbatchno")]
        public int EntrustBatchNo { get; set; }

        [BindingAttribute("limitupprice")]
        public double LimitUpPrice { get; set; }

        [BindingAttribute("limitdownprice")]
        public double LimitDownPrice { get; set; }

        public string ExchangeCode { get; set; }

        public SecurityType SecuType { get; set; }

        public DateTime EntrustDate { get; set; }

        public DateTime FirstDealDate { get; set; }

        public int SubmitId { get; set; }

        public EntrustDirection EDirection { get; set; }

        public PriceType ECommandPrice { get; set; }

        public EntrustPriceType EOriginPriceType { get; set; }
    }

    public class CancelRedoItem : CancelSecurityItem
    {
        [BindingAttribute("pricesetting")]
        public string PriceSetting
        {
            get
            {
                return EnumTypeDisplayHelper.GetPriceType(EPriceSetting);
            }
        }

        [BindingAttribute("pricetype")]
        public string PriceType
        {
            get
            {
                return EnumTypeDisplayHelper.GetEntrustPriceType(EEntrustPriceType);
            }
        }


        [BindingAttribute("entrustprice")]
        public double EntrustPrice { get; set; }

        [BindingAttribute("entrustamount")]
        public int EntrustAmount { get; set; }

        public PriceType EPriceSetting { get; set; }

        public EntrustPriceType EEntrustPriceType { get; set; }
    }
}
