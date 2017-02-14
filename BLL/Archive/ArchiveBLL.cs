using BLL.Archive.Deal;
using BLL.Archive.EntrustCommand;
using BLL.Archive.Permission;
using BLL.Archive.TradeCommand;
using BLL.Deal;
using BLL.EntrustCommand;
using BLL.Permission;
using BLL.TradeCommand;
using log4net;
using Model.Archive;
using Model.Database;
using Model.Permission;
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
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private TradeCommandBLL _tradeCommandBLL = new TradeCommandBLL();
        private TradeCommandSecurityBLL _tradeCommandSecurityBLL = new TradeCommandSecurityBLL();
        private ArchiveTradeBLL _archiveTradeBLL = new ArchiveTradeBLL();

        private EntrustCombineBLL _entrustCombineBLL = new EntrustCombineBLL();
        private EntrustCommandBLL _entrustCommandBLL = new EntrustCommandBLL();
        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();
        private ArchiveEntrustBLL _archiveEntrustBLL = new ArchiveEntrustBLL();
     
        private DealSecurityBLL _dealSecurityBLL = new DealSecurityBLL();
        private ArchiveDealSecurityBLL _archiveDealSecurityBLL = new ArchiveDealSecurityBLL();

        private TokenResourcePermissionBLL _tokenResourcePermissionBLL = new TokenResourcePermissionBLL();
        private ArchiveTokenResourcePermissionBLL _archiveTokenResourcePermissionBLL = new ArchiveTokenResourcePermissionBLL();

        public ArchiveBLL()
        { 
        }

        #region TradeCommand

        public int Archive()
        {
            int total = -1;
            var tradeCommands = _tradeCommandBLL.GetInvalidTradeCommands();
            if (tradeCommands == null || tradeCommands.Count == 0)
            {
                return total;
            }

            foreach (var tradeCommand in tradeCommands)
            {
                int ret = ArchiveTradeCommand(tradeCommand.CommandId);
                if (ret > 0)
                {
                    total++;
                    _tradeCommandBLL.Delete(tradeCommand.CommandId);
                }
                else
                { 
                    string msg = string.Format("Fail to archive the TradeCommand - CommandId: {0}", tradeCommand.CommandId);
                    logger.Error(msg);
                }
            }

            return total;
        }

        /// <summary>
        /// Archive the TradeCommand item.
        /// </summary>
        /// <param name="commandId">An integer value of the TradeCommand ID.</param>
        /// <returns>An positive value if it success otherwise it fails.</returns>
        public int ArchiveTradeCommand(int commandId)
        {
            int ret = -1;
            Model.Database.TradeCommand tradeCommand = null;
            List<Model.Database.TradeCommandSecurity> tradeSecuItems = null;
            tradeCommand = _tradeCommandBLL.GetTradeCommand(commandId);
            if (tradeCommand != null && tradeCommand.CommandId == commandId)
            {
                tradeSecuItems = _tradeCommandSecurityBLL.GetTradeCommandSecurities(commandId);
            }

            if (tradeSecuItems != null && tradeSecuItems.Count > 0)
            {
                ret = _archiveTradeBLL.Create(tradeCommand, tradeSecuItems);
            }

            if (ret > 0)
            {
                ret = ArchiveEntrustCommand(commandId);
                if (ret > 0)
                {
                    _entrustCommandBLL.DeleteByCommandId(commandId);

                    var permItems = _tokenResourcePermissionBLL.GetByResource(commandId, ResourceType.TradeCommand);
                    if (permItems != null && permItems.Count > 0)
                    {
                        ArchivePermission(commandId, ResourceType.TradeCommand, permItems);
                    }
                }
                else
                {
                    string msg = string.Format("Fail to archive the EntrustCommand - CommandId: {0}", commandId);
                    logger.Error(msg);
                }
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
            int total = -1;

            var entrustCommands = _entrustCommandBLL.GetByCommandId(commandId);
            if (entrustCommands != null && entrustCommands.Count > 0)
            {
                foreach (var entrustCommand in entrustCommands)
                {
                    int ret = -1;
                    var entrustSecurities = _entrustSecurityBLL.GetBySubmitId(entrustCommand.SubmitId);
                    if (entrustSecurities != null && entrustSecurities.Count > 0)
                    {
                        ret = _archiveEntrustBLL.Create(entrustCommand, entrustSecurities);
                        if (ret > 0)
                        {
                            total++;
                            ret = _entrustCombineBLL.Delete(entrustCommand.SubmitId);

                            ret = ArchiveDealSecurity(entrustCommand.SubmitId);
                        }
                    }
                }
            }

            return total;
        }

        public int DeleteEntrustCommand(int archiveId)
        {
            return _archiveEntrustBLL.Delete(archiveId);
        }

        #endregion

        #region DealSecurity

        /// <summary>
        /// Archive the deal security by the submitId.
        /// </summary>
        /// <param name="submitId">An integer value of the submitId.</param>
        /// <returns>A positive value to indicate the total deal securities archiving. -1 if it fail to archive.</returns>
        public int ArchiveDealSecurity(int submitId)
        {
            int ret = -1;
            var dealSecurities = _dealSecurityBLL.GetBySubmitId(submitId);
            if (dealSecurities != null && dealSecurities.Count > 0)
            {
                var archiveCommand = _archiveEntrustBLL.GetCommandBySubmitId(submitId);
                if (archiveCommand != null && archiveCommand.SubmitId == submitId)
                {
                    ret = _archiveDealSecurityBLL.Create(archiveCommand.ArchiveId, archiveCommand.ArchiveDate, dealSecurities);
                    if (ret < 0)
                    {
                        string msg = string.Format("Fail to archive the deal security - submitId: {0}, ArchiveId: {1}, ArchiveDate: {2}, Count: {3}",
                            archiveCommand.SubmitId, archiveCommand.ArchiveId, archiveCommand.ArchiveDate, dealSecurities.Count);
                        logger.Error(msg);
                    }
                    else
                    {
                        _dealSecurityBLL.DeleteBySubmitId(submitId);
                    }
                }
            }

            return ret;
        }

        public int DeleteDealSecurity(int submitId)
        {
            return _archiveDealSecurityBLL.Delete(submitId);
        }

        #endregion

        #region permission

        public int ArchivePermission(int resourceId, ResourceType resourceType, List<TokenResourcePermission> permissions)
        {
            int ret = _archiveTokenResourcePermissionBLL.Create(permissions);
            if (ret > 0)
            {
                ret = DeletePermission(resourceId, resourceType);
            }

            return ret;
        }

        public int DeletePermission(int resourceId, ResourceType resourceType)
        {
            return _tokenResourcePermissionBLL.Delete(resourceId, resourceType);
        }

        #endregion
    }
}
