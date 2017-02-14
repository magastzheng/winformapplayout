using DBAccess.Archive.Permission;
using log4net;
using Model.Archive;
using Model.Permission;
using System;
using System.Collections.Generic;

namespace BLL.Archive.Permission
{
    public class ArchiveTokenResourcePermissionBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ArchiveTokenResourcePermissionDAO _archivetokenresourcepermissiondao = new ArchiveTokenResourcePermissionDAO();

        public ArchiveTokenResourcePermissionBLL()
        { 
        }

        public int Create(TokenResourcePermission item)
        {
            ArchiveTokenResourcePermission archiveItem = new ArchiveTokenResourcePermission(item);

            return _archivetokenresourcepermissiondao.Create(archiveItem);
        }

        public int Create(List<TokenResourcePermission> items)
        {
            List<ArchiveTokenResourcePermission> archiveItems = new List<ArchiveTokenResourcePermission>();
            items.ForEach(p => 
            { 
                var archiveItem = new ArchiveTokenResourcePermission(p);
                archiveItems.Add(archiveItem);
            });

            return _archivetokenresourcepermissiondao.Create(archiveItems);
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
