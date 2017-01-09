using DBAccess.Archive.TradeCommand;
using log4net;
using Model.Archive;
using Model.Database;
using System.Collections.Generic;

namespace BLL.Archive.TradeCommand
{
    public class ArchiveTradeBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ArchiveTradeDAO _archivetradedao = new ArchiveTradeDAO();
        private ArchiveTradeCommandDAO _archivetradecommanddao = new ArchiveTradeCommandDAO();
        private ArchiveTradeCommandSecurityDAO _archivetradecommandsecudao = new ArchiveTradeCommandSecurityDAO();

        public ArchiveTradeBLL()
        { 
        }

        public int Create(Model.Database.TradeCommand tradeCommand, List<TradeCommandSecurity> securities)
        {
            return _archivetradedao.Create(tradeCommand, securities);
        }

        public int Delete(int archiveId)
        {
            return _archivetradedao.Delete(archiveId);
        }

        public ArchiveTradeCommand GetTradeCommand(int archiveId)
        {
            return _archivetradecommanddao.Get(archiveId);
        }

        public List<ArchiveTradeCommandSecurity> GetSecurites(int archiveId)
        {
            return _archivetradecommandsecudao.Get(archiveId);
        } 
    }
}
