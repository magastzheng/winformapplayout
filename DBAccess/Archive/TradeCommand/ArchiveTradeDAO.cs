using log4net;
using Model.Database;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Archive.TradeCommand
{
    public class ArchiveTradeDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_CreateArchiveTradeCommand = "procArchiveTradeCommandInsert";
        private const string SP_DeleteArchiveTradeCommand = "procArchiveTradeCommandDelete";
        private const string SP_CreateArchiveTradeCommandSecurity = "procArchiveTradeCommandSecurityInsert";
        private const string SP_DeleteArchiveTradeCommandSecurity = "procArchiveTradeCommandSecurityDelete";

        public ArchiveTradeDAO()
            : base()
        { 
        }

        public ArchiveTradeDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(Model.Database.TradeCommand tradeCommand, List<TradeCommandSecurity> securities)
        { 
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(dbCommand);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int ret = -1;
            try
            {
                //delete all old one
                dbCommand.CommandText = SP_CreateArchiveTradeCommand;
                _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, tradeCommand.CommandId);
                _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, tradeCommand.InstanceId);
                _dbHelper.AddInParameter(dbCommand, "@CommandNum", System.Data.DbType.Int32, tradeCommand.CommandNum);
                _dbHelper.AddInParameter(dbCommand, "@ModifiedTimes", System.Data.DbType.Int32, tradeCommand.ModifiedTimes);
                _dbHelper.AddInParameter(dbCommand, "@CommandType", System.Data.DbType.Int32, (int)tradeCommand.ECommandType);
                _dbHelper.AddInParameter(dbCommand, "@ExecuteType", System.Data.DbType.Int32, (int)tradeCommand.EExecuteType);
                _dbHelper.AddInParameter(dbCommand, "@StockDirection", System.Data.DbType.Int32, (int)tradeCommand.EStockDirection);
                _dbHelper.AddInParameter(dbCommand, "@FuturesDirection", System.Data.DbType.Int32, (int)tradeCommand.EFuturesDirection);
                _dbHelper.AddInParameter(dbCommand, "@CommandStatus", System.Data.DbType.Int32, (int)tradeCommand.ECommandStatus);
                _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)tradeCommand.EEntrustStatus);
                _dbHelper.AddInParameter(dbCommand, "@DealStatus", System.Data.DbType.Int32, (int)tradeCommand.EDealStatus);
                _dbHelper.AddInParameter(dbCommand, "@SubmitPerson", System.Data.DbType.Int32, tradeCommand.SubmitPerson);
                _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, tradeCommand.CreatedDate);
                _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, tradeCommand.ModifiedDate);

                _dbHelper.AddInParameter(dbCommand, "@ArchiveDate", System.Data.DbType.DateTime, DateTime.Now);
                _dbHelper.AddInParameter(dbCommand, "@StartDate", System.Data.DbType.DateTime, tradeCommand.DStartDate);
                _dbHelper.AddInParameter(dbCommand, "@EndDate", System.Data.DbType.DateTime, tradeCommand.DEndDate);
                _dbHelper.AddInParameter(dbCommand, "@ModifiedCause", System.Data.DbType.String, tradeCommand.ModifiedCause);
                _dbHelper.AddInParameter(dbCommand, "@CancelCause", System.Data.DbType.String, tradeCommand.CancelCause);
                _dbHelper.AddInParameter(dbCommand, "@Notes", System.Data.DbType.String, tradeCommand.Notes);

                _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

                ret = dbCommand.ExecuteNonQuery();

                if (ret >= 0)
                {
                    dbCommand.CommandText = SP_CreateArchiveTradeCommand;
                    _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, tradeCommand.CommandId);

                    int archiveId = -1;
                    if (ret > 0)
                    {
                        archiveId = (int)dbCommand.Parameters["@return"].Value;
                    }

                    foreach (var security in securities)
                    {
                        dbCommand.Parameters.Clear();
                        dbCommand.CommandText = SP_CreateArchiveTradeCommandSecurity;

                        _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);
                        _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, security.CommandId);
                        _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, security.SecuCode);
                        _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)security.SecuType);
                        _dbHelper.AddInParameter(dbCommand, "@CommandAmount", System.Data.DbType.Int32, security.CommandAmount);
                        _dbHelper.AddInParameter(dbCommand, "@CommandDirection", System.Data.DbType.Int32, (int)security.EDirection);
                        _dbHelper.AddInParameter(dbCommand, "@CommandPrice", System.Data.DbType.Double, security.CommandPrice);

                        ret = dbCommand.ExecuteNonQuery();
                        if (ret > 0)
                        {
                            //int requetId = (int)dbCommand.Parameters["@return"].Value;
                        }
                    }
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                //TODO: add log
                logger.Error(ex);
                ret = -1;
                throw;
            }
            finally
            {
                _dbHelper.Close(dbCommand);
                transaction.Dispose();
            }

            return ret;
        }

        public int Delete(int archiveId)
        { 
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(dbCommand);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int ret = -1;
            try
            {
                dbCommand.CommandText = SP_DeleteArchiveTradeCommand;
                _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);
                ret = dbCommand.ExecuteNonQuery();

                if (ret >= 0)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.CommandText = SP_DeleteArchiveTradeCommandSecurity;

                    _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

                    ret = dbCommand.ExecuteNonQuery();
                    if (ret < 0)
                    {
                        string msg = string.Format("Fail to delete the archive security: {0}", archiveId);
                        logger.Error(msg);
                    }
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                //TODO: add log
                logger.Error(ex);
                ret = -1;
                throw;
            }
            finally
            {
                _dbHelper.Close(dbCommand);
                transaction.Dispose();
            }

            return ret;
        }
    }
}
