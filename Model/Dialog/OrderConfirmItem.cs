using Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dialog
{
    public class OrderConfirmItem
    {
        [BindingAttribute("portfoliodisplay")]
        public string PortfolioDisplay
        {
            get { return string.Format("{0}-{1}", PortfolioCode, PortfolioName); }
        }

        [BindingAttribute("templatedisplay")]
        public string TemplateDisplay
        {
            get { return string.Format("{0}-{1}", TemplateId, TemplateName); }
        }

        [BindingAttribute("futurescontract")]
        public string FuturesContract
        {
            get
            {
                string ret = string.Empty;
                if (FuturesList != null && FuturesList.Count > 0)
                {
                    if (FuturesList.Count > 1)
                    {
                        ret = string.Join(",", FuturesList);
                    }
                    else
                    {
                        ret = FuturesList[0];
                    }
                }

                return ret;
            }
        }

        [BindingAttribute("copies")]
        public int Copies { get; set; }

        [BindingAttribute("point")]
        public double Point { get; set; }

        [BindingAttribute("instancecode")]
        public string InstanceCode { get; set; }

        [BindingAttribute("startdate")]
        public int StartDate { get; set; }

        [BindingAttribute("enddate")]
        public int EndDate { get; set; }

        [BindingAttribute("starttime")]
        public int StartTime { get; set; }

        [BindingAttribute("endtime")]
        public int EndTime { get; set; }

        [BindingAttribute("notes")]
        public string Notes { get; set; }

        public int MonitorId { get; set; }

        public string MonitorName { get; set; }

        public int PortfolioId { get; set; }

        public string PortfolioCode { get; set; }

        public string PortfolioName { get; set; }

        public int TemplateId { get; set; }

        public string TemplateName { get; set; }

        public List<string> FuturesList { get; set; }
    }
}
