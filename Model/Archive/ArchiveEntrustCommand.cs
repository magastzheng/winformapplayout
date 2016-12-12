using Model.Database;
using System;

namespace Model.Archive
{
    public class ArchiveEntrustCommand : EntrustCommand
    {
        public int ArchiveId { get; set; }

        public DateTime ArchiveDate { get; set; }
    }
}
