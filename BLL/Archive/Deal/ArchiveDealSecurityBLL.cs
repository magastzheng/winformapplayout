using DBAccess.Archive.Deal;
using Model.Archive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Archive.Deal
{
    public class ArchiveDealSecurityBLL
    {
        private ArchiveDealSecurityDAO _archivedealsecuritydao = new ArchiveDealSecurityDAO();

        public ArchiveDealSecurityBLL()
        { 
        }

        public int Create(ArchiveDealSecurity item)
        {
            return _archivedealsecuritydao.Create(item);
        }

        public int Delete(int archiveId)
        {
            return _archivedealsecuritydao.Delete(archiveId);
        }

        public List<ArchiveDealSecurity> Get(int archiveId)
        {
            return _archivedealsecuritydao.Get(archiveId);
        }
    }
}
