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
    public class ArchiveEntrustCommandBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ArchiveEntrustCommandDAO _archiveentrustcommanddao = new ArchiveEntrustCommandDAO();
        public ArchiveEntrustCommandBLL()
        { 
        }

        public int Create(ArchiveEntrustCommand item)
        {
            return _archiveentrustcommanddao.Create(item);
        }

        public int Delete(int archiveId)
        {
            return _archiveentrustcommanddao.Delete(archiveId);
        }

        public List<ArchiveEntrustCommand> Get(int commandId)
        {
            return _archiveentrustcommanddao.Get(commandId);
        }
    }
}
