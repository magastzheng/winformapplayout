using Model.Database;
using Model.EnumType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Archive
{
    public class ArchiveTradeCommand : TradeCommand
    {
        public int ArchiveId { get; set; }

        public DispatchStatus DispatchStatus { get; set; }

        public int ModifiedPerson { get; set; }

        public int CancelPerson { get; set; }

        public int ApprovalPerson { get; set; }

        public int DispatchPerson { get; set; }

        public int ExecutePerson { get; set; }

        public string ApprovalCause { get; set; }

        public string DispatchRejectCause { get; set; }

        public DateTime ArchiveDate { get; set; }
    }
}
