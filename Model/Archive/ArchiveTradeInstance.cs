using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Archive
{
    public class ArchiveTradeInstance : TradeInstance
    {
        public ArchiveTradeInstance()
        { 
        
        }

        public ArchiveTradeInstance(TradeInstance instance)
            : base(instance)
        { 
        }

        public ArchiveTradeInstance(ArchiveTradeInstance archive)
            : base(archive)
        {
            ArchiveId = archive.ArchiveId;
            ArchiveDate = archive.ArchiveDate;
        }

        public int ArchiveId { get; set; }

        public DateTime ArchiveDate { get; set; }
    }
}
