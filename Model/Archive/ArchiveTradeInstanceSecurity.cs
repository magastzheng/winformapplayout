using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Archive
{
    public class ArchiveTradeInstanceSecurity : TradeInstanceSecurity
    {
        public ArchiveTradeInstanceSecurity()
        { 
        }

        public ArchiveTradeInstanceSecurity(TradeInstanceSecurity security)
            : base(security)
        { 
        
        }

        public ArchiveTradeInstanceSecurity(ArchiveTradeInstanceSecurity security)
            : base(security)
        {
            ArchiveId = security.ArchiveId;
            ArchiveDate = security.ArchiveDate;
        }

        public int ArchiveId { get; set; }

        public DateTime ArchiveDate { get; set; }
    }
}
