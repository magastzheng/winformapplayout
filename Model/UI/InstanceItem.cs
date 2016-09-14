using Model.Binding;
using Model.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class InstanceItem
    {
        [BindingAttribute("instanceid")]
        public int InstanceId { get; set; }

        [BindingAttribute("instancecode")]
        public string InstanceCode { get; set; }

        [BindingAttribute("templatename")]
        public string TemplateName { get; set; }

        [BindingAttribute("fundcode")]
        public string FundCode { get; set; }

        [BindingAttribute("fundname")]
        public string FundName { get; set; }

        [BindingAttribute("assetunitcode")]
        public string AssetUnitCode { get; set; }

        [BindingAttribute("assetunitname")]
        public string AssetUnitName { get; set; }

        [BindingAttribute("portfoliocode")]
        public string PortfolioCode { get; set; }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        [BindingAttribute("monitorunitname")]
        public string MonitorUnitName { get; set; }

        [BindingAttribute("creator")]
        public string Creator { get; set; }

        [BindingAttribute("createddate")]
        public string CreatedDate 
        {
            get { return DateFormat.Format(DCreatedDate, ConstVariable.DateFormat); }
        }

        [BindingAttribute("createdtime")]
        public string CreatedTime
        {
            get { return DateFormat.Format(DCreatedDate, ConstVariable.TimeFormat); }
        }

        public int Owner { get; set; }

        public int TemplateId { get; set; }

        public int PortfolioId { get; set; }

        public DateTime DCreatedDate { get; set; }
    }
}
