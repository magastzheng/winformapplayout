using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class MonitorUnit
    {
        public int MonitorUnitId { get; set; }

        public string MonitorUnitName { get; set; }

        public int AccountType { get; set; }

        public int PortfolioId { get; set; }

        public string PortfolioName { get; set; }

        public string BearContract { get; set; }

        public int StockTemplateId { get; set; }

        public string StockTemplateName { get; set; }

        public string Owner { get; set; }
    }
}
