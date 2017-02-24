using log4net;
using Model.Archive;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Archive.TradeInstance
{
    public class ArchiveTradeInstanceTransactionDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_CreateTradeInstance = "procArchiveTradeInstanceInsert";
        private const string SP_CreateTradeInstanceSecurity = "procArchiveTradeInstanceSecurityInsert";

        private const string SP_DeleteTradeInstanceByArchiveId = "procArchiveTradeInstanceDeleteByArchiveId";
        private const string SP_DeleteTradeInstanceSecurityByArchiveId = "procArchiveTradeInstanceSecurityDeleteByArchiveId";

        private const string SP_DeleteTradeInstanceByInstanceId = "procArchiveTradeInstanceDeleteByInstanceId";
        private const string SP_DeleteTradeInstanceSecurityByInstanceId = "procArchiveTradeInstanceSecurityDeleteByInstanceId";

        public ArchiveTradeInstanceTransactionDAO()
        { 
        }

        public ArchiveTradeInstanceTransactionDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(Model.UI.TradeInstance tradeInstance, List<TradeInstanceSecurity> tradeSecuItems)
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
                DateTime archiveDate = DateTime.Now;

                //delete all old one
                dbCommand.CommandText = SP_CreateTradeInstance;
                _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, tradeInstance.InstanceId);
                _dbHelper.AddInParameter(dbCommand, "@InstanceCode", System.Data.DbType.String, tradeInstance.InstanceCode);
                _dbHelper.AddInParameter(dbCommand, "@PortfolioId", System.Data.DbType.Int32, tradeInstance.PortfolioId);
                _dbHelper.AddInParameter(dbCommand, "@MonitorUnitId", System.Data.DbType.Int32, tradeInstance.MonitorUnitId);
                _dbHelper.AddInParameter(dbCommand, "@StockDirection", System.Data.DbType.Int32, (int)tradeInstance.StockDirection);
                _dbHelper.AddInParameter(dbCommand, "@FuturesContract", System.Data.DbType.String, tradeInstance.FuturesContract);
                _dbHelper.AddInParameter(dbCommand, "@FuturesDirection", System.Data.DbType.Int32, (int)tradeInstance.FuturesDirection);
                _dbHelper.AddInParameter(dbCommand, "@OperationCopies", System.Data.DbType.Int32, tradeInstance.OperationCopies);
                _dbHelper.AddInParameter(dbCommand, "@StockPriceType", System.Data.DbType.Int32, (int)tradeInstance.StockPriceType);
                _dbHelper.AddInParameter(dbCommand, "@FuturesPriceType", System.Data.DbType.Int32, (int)tradeInstance.FuturesPriceType);
                _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)tradeInstance.Status);
                _dbHelper.AddInParameter(dbCommand, "@Owner", System.Data.DbType.Int32, (int)tradeInstance.Owner);
                _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, tradeInstance.CreatedDate);
                _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, tradeInstance.ModifiedDate);
                _dbHelper.AddInParameter(dbCommand, "@ArchiveDate", System.Data.DbType.DateTime, archiveDate);

                _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

                int temp = _dbHelper.ExecuteNonQuery(dbCommand);

                int archiveId = -1;
                if (temp > 0)
                {
                    archiveId = (int)dbCommand.Parameters["@return"].Value;

                    foreach (var tradeSecuItem in tradeSecuItems)
                    {
                        dbCommand.Parameters.Clear();
                        dbCommand.CommandText = SP_CreateTradeInstanceSecurity;

                        _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, tradeSecuItem.InstanceId);
                        _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, tradeSecuItem.SecuCode);
                        _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)tradeSecuItem.SecuType);
                        _dbHelper.AddInParameter(dbCommand, "@PositionType", System.Data.DbType.Int32, (int)tradeSecuItem.PositionType);
                        _dbHelper.AddInParameter(dbCommand, "@InstructionPreBuy", System.Data.DbType.Int32, tradeSecuItem.InstructionPreBuy);
                        _dbHelper.AddInParameter(dbCommand, "@InstructionPreSell", System.Data.DbType.Int32, tradeSecuItem.InstructionPreSell);
                        _dbHelper.AddInParameter(dbCommand, "@BuyBalance", System.Data.DbType.Decimal, tradeSecuItem.BuyBalance);
                        _dbHelper.AddInParameter(dbCommand, "@SellBalance", System.Data.DbType.Decimal, tradeSecuItem.SellBalance);
                        _dbHelper.AddInParameter(dbCommand, "@DealFee", System.Data.DbType.Decimal, tradeSecuItem.DealFee);
                        _dbHelper.AddInParameter(dbCommand, "@BuyToday", System.Data.DbType.Int32, tradeSecuItem.BuyToday);
                        _dbHelper.AddInParameter(dbCommand, "@SellToday", System.Data.DbType.Int32, tradeSecuItem.SellToday);
                        _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, tradeSecuItem.CreatedDate);
                        _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, tradeSecuItem.ModifiedDate);
                        _dbHelper.AddInParameter(dbCommand, "@LastDate", System.Data.DbType.DateTime, tradeSecuItem.LastDate);
                        _dbHelper.AddInParameter(dbCommand, "@ArchiveDate", System.Data.DbType.DateTime, archiveDate);

                        _dbHelper.AddOutParameter(dbCommand, "@RowId", System.Data.DbType.String, 20);

                        ret = dbCommand.ExecuteNonQuery();

                        string rowId = string.Empty;
                        if (ret > 0)
                        {
                            rowId = (string)dbCommand.Parameters["@RowId"].Value;
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

        public int DeleteByArchiveId(int archiveId)
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
                dbCommand.CommandText = SP_DeleteTradeInstanceByInstanceId;
                _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

                int temp = _dbHelper.ExecuteNonQuery(dbCommand);

                if (temp > 0)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.CommandText = SP_DeleteTradeInstanceSecurityByInstanceId;
                    _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

                    _dbHelper.ExecuteNonQuery(dbCommand);
                }
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

        public int DeleteByInstanceId(int instanceId)
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
                dbCommand.CommandText = SP_DeleteTradeInstanceByArchiveId;
                _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, instanceId);

                int temp = _dbHelper.ExecuteNonQuery(dbCommand);

                if (temp > 0)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.CommandText = SP_DeleteTradeInstanceSecurityByArchiveId;
                    _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, instanceId);

                    _dbHelper.ExecuteNonQuery(dbCommand);
                }
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
