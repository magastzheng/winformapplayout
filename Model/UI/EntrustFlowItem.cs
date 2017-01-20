using Model.Binding;
using Model.Constant;
using Model.Converter;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using Model.SecurityInfo;
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
        public string Market
        {
            get 
            {
                return UFXTypeConverter.GetMarketName(EMarketCode);
            }
        }

        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("entrustdirection")]
        public string EntrustDirection
        {
            get 
            {
                return EnumTypeDisplayHelper.GetEntrustDirection(EDirection);
            }
        }

        [BindingAttribute("pricetype")]
        public string PriceType
        {
            get 
            {
                return EntrustPriceTypeConverter.GetPriceTypeName(EEntrustPriceType);
            }
        }

        [BindingAttribute("entrustprice")]
        public double EntrustPrice { get; set; }

        [BindingAttribute("entrustamount")]
        public int EntrustAmount { get; set; }

        [BindingAttribute("entruststatus")]
        public string EntrustStatus
        {
            get { return UFXTypeConverter.GetEntrustState(EEntrustState); }
        }

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

        [BindingAttribute("entrusteddate")]
        public string EntrustedDate
        {
            get { return DateFormat.Format(DEntrustDate, ConstVariable.DateFormat1); }
        }
        
        [BindingAttribute("entrustedtime")]
        public string EntrustedTime
        {
            get { return DateFormat.Format(DEntrustDate, ConstVariable.TimeFormat1); }
        }

        [BindingAttribute("firstdealdate")]
        public string FirstDealDate
        {
            get { return DateFormat.Format(DFirstDealDate, ConstVariable.DateFormat1); }
        }

        [BindingAttribute("shareholdercode")]
        public string ShareHolderCode { get; set; }

        [BindingAttribute("declareseat")]
        public string DeclareSeat { get; set; }

        [BindingAttribute("withdrawcause")]
        public string WithdrawCause { get; set; }

        [BindingAttribute("turnoverratio")]
        public double TurnoverRatio { get; set; }

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

        public int SubmitId { get; set; }

        public string FundCode { get; set; }

        public string Operator { get; set; }

        public string PortfolioCode { get; set; }

        public DateTime DEntrustDate { get; set; }

        public DateTime DFirstDealDate { get; set; }

        public UFXMarketCode EMarketCode { get; set; }

        //public UFXEntrustDirection EEntrustDirection { get; set; }

        //public UFXFuturesDirection EFuturesDirection { get; set; }

        public UFXEntrustState EEntrustState { get; set; }

        public EntrustDirection EDirection { get; set; }

        public EntrustPriceType EEntrustPriceType { get; set; }

        public string ExchangeCode { get; set; }

        public SecurityType SecuType { get; set; }
    }
}
