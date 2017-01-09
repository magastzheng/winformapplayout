using DBAccess.Archive.EntrustCommand;
using log4net;
using Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Archive.EntrustCommand
{
    public class ArchiveEntrustBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ArchiveEntrustDAO _archiveentrustdao = new ArchiveEntrustDAO();

        public ArchiveEntrustBLL()
        { 
        }

        public int Create(Model.Database.EntrustCommand entrustCommand, List<EntrustSecurity> securities)
        {
            return _archiveentrustdao.Create(entrustCommand, securities);
        }

        public int Delete(int archiveId)
        {
            return _archiveentrustdao.Delete(archiveId);
        }
    }
}
