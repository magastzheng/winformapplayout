using DBAccess.Archive.TradeInstance;
using log4net;
using Model.Archive;
using Model.UI;
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
        private ArchiveTradeInstanceTransactionDAO _archivetradeinstancetransactiondao = new ArchiveTradeInstanceTransactionDAO();

        public ArchiveTradeInstanceBLL()
        { 
        }

        public int Create(Model.UI.TradeInstance tradeInstance, List<TradeInstanceSecurity> tradeSecuItems)
        {
            return _archivetradeinstancetransactiondao.Create(tradeInstance, tradeSecuItems);
        }

        /// <summary>
        /// FIXME: while the old TradeInstance is deleted, the old InstanceId will be re-used in new TradeInstance.
        /// So there will be duplicated InstanceId but they are belong to different TradeInstance.
        /// TODO:
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public int DeleteByInstanceId(int instanceId)
        {
            return _archivetradeinstancetransactiondao.DeleteByInstanceId(instanceId);
        }

        public int DeleteByArchiveId(int archiveId)
        {
            return _archivetradeinstancetransactiondao.DeleteByArchiveId(archiveId);
        }

        public ArchiveTradeInstance Get(int archiveId)
        {
            return _archivetradeinstancedao.Get(archiveId);
        }
    }
}
