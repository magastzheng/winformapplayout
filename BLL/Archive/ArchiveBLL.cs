using BLL.Archive.EntrustCommand;
using BLL.Archive.TradeCommand;
using BLL.EntrustCommand;
using BLL.TradeCommand;
using Model.Archive;
using Model.Database;
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

        private EntrustCommandBLL _entrustCommandBLL = new EntrustCommandBLL();
        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();
        private ArchiveEntrustBLL _archiveEntrustBLL = new ArchiveEntrustBLL();

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

        public int ArchiveEntrustCommand(int commandId)
        {
            int ret = -1;

            List<Model.Database.EntrustCommand> entrustCommands = null;
            List<EntrustSecurity> entrustSecurities = null;
            entrustCommands = _entrustCommandBLL.GetByCommandId(commandId);
            if (entrustCommands != null && entrustCommands.Count > 0)
            {
                foreach (var entrustCommand in entrustCommands)
                {
                    entrustSecurities = _entrustSecurityBLL.GetBySubmitId(entrustCommand.SubmitId);
                    if (entrustSecurities != null && entrustSecurities.Count > 0)
                    {
                        ret = _archiveEntrustBLL.Create(entrustCommand, entrustSecurities);
                    }
                }
            }

            return ret;
        }


    }
}
