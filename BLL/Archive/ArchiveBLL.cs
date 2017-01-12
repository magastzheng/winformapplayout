using BLL.Archive.Deal;
using BLL.Archive.EntrustCommand;
using BLL.Archive.TradeCommand;
using BLL.Deal;
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
    /// <summary>
    /// Archive order:
    /// TradeCommand -> TradeCommandSecurity [archive those ineffective command]
    /// EntrustCommand -> EntrustSecurity   [archive those ineffective command id]
    /// DealSecurity    [archive those submit id in archive EntrustCommand]
    /// 
    /// While success to archive, does it need to delete the old item?
    /// </summary>
    public class ArchiveBLL
    {
        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private TradeCommandSecurityBLL _tradeCommandSecurityBLL = new TradeCommandSecurityBLL();
        private ArchiveTradeBLL _archiveTradeBLL = new ArchiveTradeBLL();

        private EntrustCommandBLL _entrustCommandBLL = new EntrustCommandBLL();
        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();
        private ArchiveEntrustBLL _archiveEntrustBLL = new ArchiveEntrustBLL();
     
        private DealSecurityBLL _dealSecurityBLL = new DealSecurityBLL();
        private ArchiveDealSecurityBLL _archiveDealSecurityBLL = new ArchiveDealSecurityBLL();

        public ArchiveBLL()
        { 
        }

        #region TradeCommand

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandId"></param>
        /// <returns></returns>
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

        public int DeleteTradeCommand(int archiveId)
        {
            return _archiveTradeBLL.Delete(archiveId);
        }

        #endregion

        #region EntrustCommand

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

        public int DeleteEntrustCommand(int archiveId)
        {
            return _archiveEntrustBLL.Delete(archiveId);
        }

        #endregion

        #region DealSecurity

        /// <summary>
        /// SumbitId -> ArchiveId, ArchiveDate from ArchiveEntrustCommand
        /// 
        /// 
        /// </summary>
        /// <param name="submitId"></param>
        /// <returns></returns>
        public int ArchiveDealSecurity()
        {
            int ret = -1;
            var dealSecurites = _dealSecurityBLL.GetAll();

            var submitIds = dealSecurites.Select(p => p.SubmitId).Distinct().ToList();
            foreach (var submitId in submitIds)
            {
                var archiveCommand = _archiveEntrustBLL.GetCommandBySubmitId(submitId);
                if (archiveCommand != null && archiveCommand.SubmitId == submitId)
                {
                    var sameDealItems = dealSecurites.Where(p => p.SubmitId == submitId).ToList();
                    ret = _archiveDealSecurityBLL.Create(archiveCommand.ArchiveId, archiveCommand.ArchiveDate, sameDealItems);
                    if (ret > 0)
                    {
                        //success
                    }
                    else
                    { 
                        //failure
                    }
                }
            }

            return ret;
        }

        #endregion
    }
}
