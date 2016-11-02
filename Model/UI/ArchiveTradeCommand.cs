using Model.Database;
using Model.EnumType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class ArchiveTradeCommand : TradeCommand
    {
        public int ArchiveId { get; set; }

        public CommandStatus CommandStatus { get; set; }

        public DispatchStatus DispatchStatus { get; set; }

        public int ModifiedPerson { get; set; }

        public int CancelPerson { get; set; }

        public int ApprovalPerson { get; set; }

        public int DispatchPerson { get; set; }

        public int ExecutePerson { get; set; }

        public string ModifiedCause { get; set; }

        public string CancelCause { get; set; }

        public string ApprovalCause { get; set; }

        public string DispatchRejectCause { get; set; }

        public string CommandNotes { get; set; }
    }
}
