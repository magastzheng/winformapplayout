using Model.Binding;
using Model.config;
using Model.Data;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class CancelRedoItem
    {
        [BindingAttribute("selection")]
        public bool Selection { get; set; }

        [BindingAttribute("commandid")]
        public int CommandId { get; set; }

        [BindingAttribute("entrustno")]
        public int EntrustNo { get; set; }

        [BindingAttribute("exchange")]
        public string Exchange { get { return ExchangeCode; } }

        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("entrustdirection")]
        public EntrustDirection EntrustDirection { get; set; }

        [BindingAttribute("commandprice")]
        public string CommandPrice
        {
            get 
            {
                if (ECommandPrice == config.PriceType.None)
                {
                    return string.Empty;
                }
                else
                {
                    return ECommandPrice.ToString();
                }
            }
        }

        [BindingAttribute("pricesetting")]
        public string PriceSetting
        {
            get
            {
                if (EPriceSetting == config.PriceType.None)
                {
                    return string.Empty;
                }
                else
                {
                    return EPriceSetting.ToString();
                }
            }
        }

        [BindingAttribute("pricetype")]
        public string PriceType
        {
            get
            {
                return EEntrustPriceType.ToString();
            }
        }


        [BindingAttribute("entrustprice")]
        public double EntrustPrice { get; set; }

        [BindingAttribute("entrustamount")]
        public int EntrustAmount { get; set; }

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
                return EOriginPriceType.ToString();
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
                return EntrustDate.ToString("yyyyMMdd");
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

        public int SubmitId { get; set; }

        public PriceType EPriceSetting { get; set; }

        public PriceType ECommandPrice { get; set; }

        public EntrustPriceType EEntrustPriceType { get; set; }

        public EntrustPriceType EOriginPriceType { get; set; }
    }
}
