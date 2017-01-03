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
    public class ArchiveTradeCommandSecurityBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ArchiveTradeCommandSecurityDAO _archivetradecommandsecudao = new ArchiveTradeCommandSecurityDAO();
        public ArchiveTradeCommandSecurityBLL()
        { 
        }

        public int Create(ArchiveTradeCommandSecurity item)
        {
            return _archivetradecommandsecudao.Create(item);
        }

        public int Delete(int archiveId)
        {
            return _archivetradecommandsecudao.Delete(archiveId);
        }

        public List<ArchiveTradeCommandSecurity> Get(int archiveId)
        {
            return _archivetradecommandsecudao.Get(archiveId);
        }
    }
}
