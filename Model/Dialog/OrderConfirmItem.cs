using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dialog
{
    public class OrderConfirmItem
    {
        public int MonitorId { get; set; }

        public string MonitorName { get; set; }

        public int PortfolioId { get; set; }

        public string PortfolioName { get; set; }

        public int TemplateId { get; set; }

        public string TemplateName { get; set; }

        public int Copies { get; set; }

        public string FuturesContract { get; set; }

        public string InstanceCode { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        //public string StartDate { get; set; }

        //public string EndDate { get; set; }

        //public string StartTime { get; set; }

        //public string EndTime { get; set; }

        public string Notes { get; set; }
    }
}
