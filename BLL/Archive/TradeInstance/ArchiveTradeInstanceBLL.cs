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
    public class ArchiveTradeInstanceBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ArchiveTradeInstanceDAO _archivetradeinstancedao = new ArchiveTradeInstanceDAO();

        public ArchiveTradeInstanceBLL()
        { 
        }

        public int Create(ArchiveTradeInstance tradeInstance)
        {
            return _archivetradeinstancedao.Create(tradeInstance);
        }

        public int DeleteByInstanceId(int instanceId)
        {
            return _archivetradeinstancedao.DeleteByInstanceId(instanceId);
        }

        public int DeleteByArchiveId(int archiveId)
        {
            return _archivetradeinstancedao.DeleteByArchiveId(archiveId);
        }

        public ArchiveTradeInstance Get(int archiveId)
        {
            return _archivetradeinstancedao.Get(archiveId);
        }
    }
}
