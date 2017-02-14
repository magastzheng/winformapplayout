using Model.Permission;
using System;

namespace Model.Archive
{
    public class ArchiveTokenResourcePermission : TokenResourcePermission
    {
        public ArchiveTokenResourcePermission()
        { 
        }

        public ArchiveTokenResourcePermission(TokenResourcePermission item)
            : base(item)
        {
            ArchiveDate = DateTime.Now;
        }

        public int ArchiveId { get; set; }

        public DateTime ArchiveDate { get; set; }
    }
}
