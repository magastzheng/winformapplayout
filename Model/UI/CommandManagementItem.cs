
using Model.Binding;
using Model.Constant;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using System;
namespace Model.UI
{
    public class CommandManagementItem
    {
        [BindingAttribute("date")]
        public string Date 
        {
            get { return DateFormat.Format(DDate, ConstVariable.DateFormat); }
        }

        [BindingAttribute("commandid")]
        public int CommandId { get; set; }

        [BindingAttribute("commandmodifiedtimes")]
        public int CommandModifiedTimes { get; set; }

        [BindingAttribute("funddisplay")]
        public string FundDisplay
        { 
            get { return string.Format("{0}-{1}", FundCode, FundName); } 
        }

        [BindingAttribute("portfoliodisplay")]
        public string PortfolioDisplay
        {
            get { return string.Format("{0}-{1}", PortfolioCode, PortfolioName); }
        }

        [BindingAttribute("instanceid")]
        public int InstanceId { get; set; }

        [BindingAttribute("instancecode")]
        public string InstanceCode { get; set; }

        [BindingAttribute("arbitragetype")]
        public string ArbitrageType 
        {
            get { return EnumTypeDisplayHelper.GetCommandType(EArbitrageType); }
        }

        [BindingAttribute("arbitrageexecutestage")]
        public string ArbitrageExecuteStage
        {
            get { return EnumTypeDisplayHelper.GetExecuteType(EExecutype); }
        }

        [BindingAttribute("investmenttype")]
        public string InvestmentType
        {
            get { return EnumTypeDisplayHelper.GetInvestmentType(EInvestmentType); }
        }

        [BindingAttribute("bearcontract")]
        public string BearContract { get; set; }

        [BindingAttribute("longcontract")]
        public string LongContract { get; set; }

        [BindingAttribute("templateid")]
        public int TemplateId { get; set; }

        [BindingAttribute("templatename")]
        public string TemplateName { get; set; }

        [BindingAttribute("arbitragecopies")]
        public int ArbitrageCopies { get; set; }

        [BindingAttribute("businesstype")]
        public string BusinessType { get; set; }

        [BindingAttribute("executeperson")]
        public string ExecutePerson { get; set; }

        [BindingAttribute("entrustexecutestatus")]
        public string EntrustExecuteStatus
        {
            //TODO
            get { return "";}
        }

        [BindingAttribute("dealexecutestatus")]
        public string DealExecuteStatus 
        {
            //TODO
            get { return ""; }
        }

        [BindingAttribute("executedescription")]
        public string ExecuteDescription { get; set; }

        [BindingAttribute("commandmoney")]
        public int CommandMoney { get; set; }

        [BindingAttribute("thedayentrustamount")]
        public int TheDayEntrustAmount { get; set; }

        [BindingAttribute("thedaydealamount")]
        public int TheDayDealAmount { get; set; }

        [BindingAttribute("cumulativeentrustamount")]
        public int CumulativeEntrustAmount { get; set; }

        [BindingAttribute("cumulativedealamount")]
        public int CumulativeDealAmount { get; set; }

        [BindingAttribute("unentrustamount")]
        public int UnentrustAmount { get; set; }

        [BindingAttribute("undealamount")]
        public int UndealAmount { get; set; }

        [BindingAttribute("cumulativedealmoney")]
        public int CumulativeDealMoney { get; set; }

        [BindingAttribute("thedaydealmoney")]
        public int TheDayDealMoney { get; set; }

        [BindingAttribute("cumulativeentrustmoney")]
        public int CumulativeEntrustMoney { get; set; }

        [BindingAttribute("thedayentrustmoney")]
        public int TheDayEntrustMoney { get; set; }

        [BindingAttribute("commandsubmitreason")]
        public string CommandSubmitReason { get; set; }

        [BindingAttribute("pendingnumber")]
        public int PendingNumber { get; set; }

        [BindingAttribute("undealmoney")]
        public int UndealMoney { get; set; }

        [BindingAttribute("commandsubmitdate")]
        public string CommandSubmitDate
        {
            get { return DateFormat.Format(DCommandSubmitDate, ConstVariable.DateFormat); }
        }

        [BindingAttribute("commandsubmittime")]
        public string CommandSubmitTime
        {
            get { return DateFormat.Format(DCommandSubmitDate, ConstVariable.TimeFormat); }
        }

        [BindingAttribute("commandsubmitperson")]
        public string CommandSubmitPerson
        {
            get;
            set;
        }

        [BindingAttribute("commandwaitsubmitperson")]
        public string CommandWaitSubmitPerson { get; set; }

        [BindingAttribute("startdate")]
        public string StartDate 
        {
            get { return DateFormat.Format(DStartDate, ConstVariable.DateFormat); }
        }

        [BindingAttribute("enddate")]
        public string EndDate
        {
            get { return DateFormat.Format(DEndDate, ConstVariable.DateFormat); }
        }

        [BindingAttribute("starttime")]
        public string StartTime
        {
            get { return DateFormat.Format(DStartDate, ConstVariable.TimeFormat); }
        }

        [BindingAttribute("endtime")]
        public string EndTime
        {
            get { return DateFormat.Format(DEndDate, ConstVariable.TimeFormat); }
        }

        [BindingAttribute("commandstatus")]
        public string CommandStatus 
        {
            //TODO
            get { return ECommandStatus.ToString(); }
        }

        [BindingAttribute("commandtype")]
        public string CommandType
        { 
            //TODO:
            get { return string.Empty; }
        }

        [BindingAttribute("dealvaluation")]
        public string DealValuation { get; set; }

        [BindingAttribute("notes")]
        public string Notes { get; set; }

        [BindingAttribute("approvalstatus")]
        public string ApprovalStatus { get; set; }

        [BindingAttribute("dispatchstatus")]
        public string DispatchStatus { get; set; }

        [BindingAttribute("approvaltime")]
        public string ApprovalTime
        {
            get { return DateFormat.Format(DApprovalDate, ConstVariable.TimeFormat); }
        }

        [BindingAttribute("approvalperson")]
        public string ApprovalPerson { get; set; }

        [BindingAttribute("dispatchtime")]
        public string DispatchTime 
        {
            get { return DateFormat.Format(DDispatchDate, ConstVariable.TimeFormat); }
        }

        [BindingAttribute("dispatchperson")]
        public string DispatchPerson { get; set; }

        [BindingAttribute("refusalreasonofapproval")]
        public string RefusalReasonOfApproval { get; set; }

        [BindingAttribute("refusalreasonofdispatch")]
        public string RefusalReasonOfDispatch { get; set; }

        [BindingAttribute("canceltime")]
        public string CancelTime
        {
            get { return DateFormat.Format(DCancelDate, ConstVariable.TimeFormat); }
        }

        [BindingAttribute("modifyoperator")]
        public string ModifyOperator { get; set; }

        [BindingAttribute("modifyreason")]
        public string ModifyReason { get; set; }

        [BindingAttribute("cancelreason")]
        public string CancelReason { get; set; }

        [BindingAttribute("securitytargettype")]
        public string SecurityTargetType
        {
            get { return EnumTypeDisplayHelper.GetWeightType(EWeightType); }
        }

        [BindingAttribute("commandlevel")]
        public string CommandLevel { get; set; }

        [BindingAttribute("netbuymoney")]
        public int NetBuyMoney { get; set; }

        public DateTime DDate { get; set; }

        public string FundCode { get; set; }

        public string FundName { get; set; }

        public string PortfolioCode { get; set; }

        public string PortfolioName { get; set; }

        public CommandType EArbitrageType { get; set; }

        public ExecuteType EExecutype { get; set; }

        public InvestmentType EInvestmentType { get; set; }

        public EntrustStatus EEntrustStatus { get; set; }

        public DealStatus EDealStatus { get; set; }

        public DateTime DCommandSubmitDate { get; set; }

        public DateTime DStartDate { get; set; }

        public DateTime DEndDate { get; set; }

        public DateTime DApprovalDate { get; set; }

        public DateTime DDispatchDate { get; set; }

        public DateTime DCancelDate { get; set; }

        public WeightType EWeightType { get; set; }

        public CommandStatus ECommandStatus { get; set; }
    }
}
