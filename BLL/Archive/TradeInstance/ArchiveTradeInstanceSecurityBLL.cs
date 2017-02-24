using DBAccess.Archive.TradeInstance;
using log4net;
using Model.Archive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Archive.TradeInstance
{
    public class ArchiveTradeInstanceSecurityBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ArchiveTradeInstanceSecurityDAO _archivetradeinstancesecuritydao = new ArchiveTradeInstanceSecurityDAO();

        public ArchiveTradeInstanceSecurityBLL()
        { 
        }

        public int Create(ArchiveTradeInstanceSecurity security)
        {
            return _archivetradeinstancesecuritydao.Create(security);
        }

        public int DeleteByArchiveId(int archiveId)
        {
            return _archivetradeinstancesecuritydao.DeleteByArchiveId(archiveId);
        }

        public int DeleteByInstanceId(int instanceId)
        {
            return _archivetradeinstancesecuritydao.DeleteByInstanceId(instanceId);
        }

        public List<ArchiveTradeInstanceSecurity> Get(int archiveId)
        {
            return _archivetradeinstancesecuritydao.Get(archiveId);
        }
    }
}
