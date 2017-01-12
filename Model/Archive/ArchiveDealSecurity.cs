using Model.Database;
using Model.EnumType;
using System;

namespace Model.Archive
{
    public class ArchiveDealSecurity : DealSecurity
    {
        public ArchiveDealSecurity()
            :base()
        { 
        
        }

        public ArchiveDealSecurity(DealSecurity security)
            : base(security)
        { 
        }

        public ArchiveDealSecurity(ArchiveDealSecurity security)
            : base(security)
        {
            this.ArchiveId = security.ArchiveId;
            this.ArchiveDate = security.ArchiveDate;
        }

        public int ArchiveId { get; set; }

        public DateTime ArchiveDate { get; set; }
    }
}
