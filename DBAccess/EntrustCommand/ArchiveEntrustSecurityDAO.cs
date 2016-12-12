using Model.Archive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.EntrustCommand
{
    public class ArchiveEntrustSecurityDAO: BaseDAO
    {
        private const string SP_Create = "procArchiveEntrustSecurityInsert";
        private const string SP_Delete = "procArchiveEntrustCommandDelete";
        private const string SP_Select = "procArchiveEntrustSecuritySelect";

        public ArchiveEntrustSecurityDAO()
            : base()
        { 
        }

        public ArchiveEntrustSecurityDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        }

        public int Create(ArchiveEntrustSecurity item)
        {
            return -1;
        }

        public int Delete(int archiveId)
        {
            return -1;
        }

        public void Get(int archiveId)
        { 
        
        }
    }
}