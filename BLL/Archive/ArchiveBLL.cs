using BLL.Archive.TradeCommand;
using BLL.TradeCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Archive
{
    public class ArchiveBLL
    {
        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private TradeCommandSecurityBLL _tradeCommandSecurityBLL = new TradeCommandSecurityBLL();
        private ArchiveTradeCommandBLL _archiveTradeCommandBLL = new ArchiveTradeCommandBLL();
        private ArchiveTradeCommandSecurityBLL _archiveTradeCommandSecurityBLL = new ArchiveTradeCommandSecurityBLL();

        public ArchiveBLL()
        { 
        }

        public int ArchiveTradeCommand(int commandId)
        {
            Model.Database.TradeCommand tradeCommand = null;
            List<Model.Database.TradeCommandSecurity> tradeSecuItems = null;
            tradeCommand = _tradeCommandBLL.GetTradeCommandItem(commandId);
            if (tradeCommand != null && tradeCommand.CommandId == commandId)
            {
                tradeSecuItems = _tradeCommandSecurityBLL.GetTradeCommandSecurities(commandId);
            }

            return -1;
        }

        public int ArchiveEntrustCommand()
        {
            return -1;
        }


    }
}
