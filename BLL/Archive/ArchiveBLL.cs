using BLL.Archive.TradeCommand;
using BLL.TradeCommand;
using Model.Archive;
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
        private ArchiveTradeBLL _archiveTradeBLL = new ArchiveTradeBLL();

        public ArchiveBLL()
        { 
        }

        public int ArchiveTradeCommand(int commandId)
        {
            int ret = -1;
            Model.Database.TradeCommand tradeCommand = null;
            List<Model.Database.TradeCommandSecurity> tradeSecuItems = null;
            tradeCommand = _tradeCommandBLL.GetTradeCommandItem(commandId);
            if (tradeCommand != null && tradeCommand.CommandId == commandId)
            {
                tradeSecuItems = _tradeCommandSecurityBLL.GetTradeCommandSecurities(commandId);
            }

            if (tradeSecuItems != null && tradeSecuItems.Count > 0)
            {
                ret = _archiveTradeBLL.Create(tradeCommand, tradeSecuItems);
            }

            return ret;
        }

        public int ArchiveEntrustCommand()
        {
            return -1;
        }


    }
}
