using DBAccess.Archive.EntrustCommand;
using log4net;
using Model.Archive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Archive.EntrustCommand
{
    public class ArchiveEntrustSecurityBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ArchiveEntrustSecurityDAO _archiveentrustcommanddao = new ArchiveEntrustSecurityDAO();

        public ArchiveEntrustSecurityBLL()
        { 
        }

        public int Create(ArchiveEntrustSecurity item)
        {
            return _archiveentrustcommanddao.Create(item);
        }

        public int Delete(int archiveId)
        {
            return _archiveentrustcommanddao.Delete(archiveId);
        }

        public List<ArchiveEntrustSecurity> Get(int archiveId)
        {
            return _archiveentrustcommanddao.Get(archiveId);
        }
    }
}
