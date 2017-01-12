using DBAccess.Archive.EntrustCommand;
using log4net;
using Model.Archive;
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
        private ArchiveEntrustCommandDAO _archiveentrustcommanddao = new ArchiveEntrustCommandDAO();
        private ArchiveEntrustSecurityDAO _archiveentrustsecuritydao = new ArchiveEntrustSecurityDAO();

        public ArchiveEntrustBLL()
        { 
        }

        #region create

        public int Create(Model.Database.EntrustCommand entrustCommand, List<EntrustSecurity> securities)
        {
            return _archiveentrustdao.Create(entrustCommand, securities);
        }

        #endregion

        #region delete

        public int Delete(int archiveId)
        {
            return _archiveentrustdao.Delete(archiveId);
        }

        #endregion

        #region get command

        public List<ArchiveEntrustCommand> GetCommand(int commandId)
        {
            return _archiveentrustcommanddao.Get(commandId);
        }

        public ArchiveEntrustCommand GetCommandBySubmitId(int submitId)
        {
            return _archiveentrustcommanddao.GetBySubmitId(submitId);
        }

        #endregion

        #region get entrust security

        public List<ArchiveEntrustSecurity> GetSecurities(int archiveId)
        {
            return _archiveentrustsecuritydao.Get(archiveId);
        }

        #endregion
    }
}
