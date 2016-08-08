using Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class ClosePositionInstanceItem
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
            get { return string.Join(",", FuturesList); }
        }

        [BindingAttribute("copies")]
        public int Copies { get; set; }

        [BindingAttribute("point")]
        public double Point { get; set; }

        [BindingAttribute("instancecode")]
        public string InstanceCode { get; set; }

        [BindingAttribute("futubuymktval")]
        public double FutuBuyMktVal { get; set; }

        [BindingAttribute("futusellmktval")]
        public double FutuSellMktVal { get; set; }

        [BindingAttribute("spotbuymktval")]
        public double SpotBuyMktVal { get; set; }

        [BindingAttribute("spotsellmktval")]
        public double SpotSellMktVal { get; set; }

        public string PortfolioCode { get; set; }

        public string PortfolioName { get; set; }

        public int TemplateId { get; set; }

        public string TemplateName { get; set; }

        public List<string> FuturesList { get; set; }
    }
}
