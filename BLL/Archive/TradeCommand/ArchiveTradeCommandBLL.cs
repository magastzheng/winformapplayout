using DBAccess.Archive.TradeCommand;
using log4net;
using Model.Archive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Archive.TradeCommand
{
    public class ArchiveTradeCommandBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ArchiveTradeCommandDAO _archivetradecommanddao = new ArchiveTradeCommandDAO();

        public ArchiveTradeCommandBLL()
        { 
        }

        public int Create(ArchiveTradeCommand item)
        {
            return _archivetradecommanddao.Create(item);
        }

        public int Delete(int archiveId)
        {
            return _archivetradecommanddao.Delete(archiveId);
        }

        public ArchiveTradeCommand Get(int archiveId)
        {
            return _archivetradecommanddao.Get(archiveId);
        }
    }
}
