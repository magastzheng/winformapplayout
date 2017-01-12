using DBAccess.Archive.Deal;
using Model.Archive;
using Model.Database;
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

        #region create/insert into database

        public int Create(ArchiveDealSecurity item)
        {
            return _archivedealsecuritydao.Create(item);
        }

        public int Create(List<ArchiveDealSecurity> items)
        {
            return _archivedealsecuritydao.Create(items);
        }

        public int Create(int archiveId, DateTime archiveDate, List<DealSecurity> items)
        {
            List<ArchiveDealSecurity> archiveItems = new List<ArchiveDealSecurity>();
            items.ForEach(p => {
                ArchiveDealSecurity archiveItem = new ArchiveDealSecurity(p);
                archiveItem.ArchiveId = archiveId;
                archiveItem.ArchiveDate = archiveDate;

                archiveItems.Add(archiveItem);
            });

            return Create(archiveItems);
        }

        #endregion

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
