using Model.Permission;
using System;

namespace Model.Archive
{
    public class ArchiveTokenResourcePermission : TokenResourcePermission
    {
        public int ArchiveId { get; set; }

        public DateTime ArchiveDate { get; set; }
    }
}
