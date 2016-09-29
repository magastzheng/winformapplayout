using Model.Binding;
using Model.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class HistStockTemplate : StockTemplate
    {
        public int ArchiveId { get; set; }

        [BindingAttribute("archivedate")]
        public string ArchiveDate 
        {
            get { return DateFormat.Format(DArchiveDate, ConstVariable.DateFormat); }
        }

        public DateTime DArchiveDate { get; set; }
    }
}
