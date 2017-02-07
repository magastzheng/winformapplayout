using Model.Binding;
using Model.Constant;
using Model.Converter;
using Model.Database;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using Model.SecurityInfo;
using System;

namespace Model.UI
{
    public class EntrustSecurityItem
    {
        public EntrustSecurityItem(EntrustSecurityCombine item)
        {
            this.RequestId = item.RequestId;
            this.SubmitId = item.SubmitId;
            this.CommandId = item.CommandId;
            this.SecuCode = item.SecuCode;
            this.SecuType = item.SecuType;
            this.EntrustAmount = item.EntrustAmount;
            this.EntrustPrice = item.EntrustPrice;
            this.EDirection = item.EntrustDirection;
            this.EEntrustStatus = item.EntrustStatus;
            this.EPriceType = item.PriceType;
            this.EntrustPriceType = item.EntrustPriceType;
            this.DEntrustDate = item.EntrustDate;
            this.DCreatedDate = item.CreatedDate;
            this.DModifiedDate = item.ModifiedDate;
            this.EntrustNo = item.EntrustNo;
            this.BatchNo = item.BatchNo;
            this.EntrustFailCode = item.EntrustFailCode;
            this.EntrustFailCause = item.EntrustFailCause;
            this.InstanceId = item.InstanceId;
            this.InstanceCode = item.InstanceCode;
            this.MonitorUnitId = item.MonitorUnitId;
            this.PortfolioId = item.PortfolioId;
            this.PortfolioCode = item.PortfolioCode;
            this.PortfolioName = item.PortfolioName;
            this.AccountCode = item.AccountCode;
            this.AccountName = item.AccountName;
        }

        [BindingAttribute("requestid")]
        public int RequestId { get; set; }

        [BindingAttribute("submitid")]
        public int SubmitId { get; set; }

        [BindingAttribute("commandid")]
        public int CommandId { get; set; }

        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("entrustamount")]
        public int EntrustAmount { get; set; }

        [BindingAttribute("entrustprice")]
        public double EntrustPrice { get; set; }

        [BindingAttribute("entrustdirection")]
        public string EntrustDirection { get { return EnumTypeDisplayHelper.GetEntrustDirection(EDirection);} }

        [BindingAttribute("entruststatus")]
        public string EntrustStatus { get { return CommandStatusHelper.GetEntrustName(EEntrustStatus); } }

        [BindingAttribute("pricetype")]
        public string PriceType { get { return EnumTypeDisplayHelper.GetPriceType(EPriceType); } }

        [BindingAttribute("entrustno")]
        public int EntrustNo { get; set; }

        [BindingAttribute("entrustbatchno")]
        public int BatchNo { get; set; }

        [BindingAttribute("entrustfailcode")]
        public int EntrustFailCode { get; set; }

        [BindingAttribute("entrustfailcause")]
        public string EntrustFailCause { get; set; }

        [BindingAttribute("entrustdate")]
        public string EntrustDate { get { return DateFormat.Format(DEntrustDate, ConstVariable.DateFormat1); } }

        [BindingAttribute("entrusttime")]
        public string EntrustTime { get { return DateFormat.Format(DEntrustDate, ConstVariable.TimeFormat1); } }

        [BindingAttribute("instanceid")]
        public int InstanceId { get; set; }

        [BindingAttribute("instancecode")]
        public string InstanceCode { get; set; }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        [BindingAttribute("fundname")]
        public string AccountName { get; set; }

        public SecurityType SecuType { get; set; }

        public EntrustDirection EDirection { get; set; }

        public EntrustStatus EEntrustStatus { get; set; }

        public EntrustPriceType EntrustPriceType { get; set; }

        public PriceType EPriceType { get; set; }

        public DateTime DEntrustDate { get; set; }

        public DateTime DCreatedDate { get; set; }

        public DateTime DModifiedDate { get; set; }

        public int MonitorUnitId { get; set; }

        public int PortfolioId { get; set; }

        public string PortfolioCode { get; set; }

        public string AccountCode { get; set; }
    }
}
