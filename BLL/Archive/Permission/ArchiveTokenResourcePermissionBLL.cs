using DBAccess.Archive.Permission;
using log4net;
using Model.Archive;

namespace BLL.Archive.Permission
{
    public class ArchiveTokenResourcePermissionBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ArchiveTokenResourcePermissionDAO _archivetokenresourcepermissiondao = new ArchiveTokenResourcePermissionDAO();

        public ArchiveTokenResourcePermissionBLL()
        { 
        }

        public int Create(ArchiveTokenResourcePermission item)
        {
            return _archivetokenresourcepermissiondao.Create(item);
        }

        public int Delete(int archiveId)
        {
            return _archivetokenresourcepermissiondao.Delete(archiveId);
        }

        public ArchiveTokenResourcePermission Get(int archiveId)
        {
            return _archivetokenresourcepermissiondao.GetSingle(archiveId);
        }
    }
}
