using Model.Binding;
using Model.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class ArchiveStockTemplate : StockTemplate
    {
        public ArchiveStockTemplate()
            : base()
        {
        }

        public ArchiveStockTemplate(StockTemplate temp)
            : base(temp)
        { 
        }

        public ArchiveStockTemplate(ArchiveStockTemplate temp)
            : base(temp)
        {
            ArchiveId = temp.ArchiveId;
            DArchiveDate = temp.DArchiveDate;
        }

        public int ArchiveId { get; set; }

        [BindingAttribute("archivedate")]
        public string ArchiveDate 
        {
            get { return DateFormat.Format(DArchiveDate, ConstVariable.DateFormat); }
        }

        public DateTime DArchiveDate { get; set; }
    }
}
