using log4net;
using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DBAccess.TradeInstance
{
    public class TradeInstanceTransactionDAO: BaseDAO
    {
        private const string SP_CreateTradeInstance = "procTradeInstanceInsert";
        private const string SP_CreateTradeInstanceSecurity = "procTradeInstanceSecurityInsert";
        private const string SP_ModifyTradeInstance = "procTradeInstanceUpdate";
        private const string SP_ModifyTradeInstanceSecurityInsertOrUpdate = "procTradeInstanceSecurityInsertOrUpdate";
        private const string SP_ModifyTradeInstanceSecurityValidDelete = "procTradeInstanceSecurityValidDelete";

        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TradeInstanceTransactionDAO()
            : base()
        { 
            
        }

        public TradeInstanceTransactionDAO(DbHelper dbHelper)
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
                //delete all old one
                dbCommand.CommandText = SP_CreateTradeInstance;

                _dbHelper.AddInParameter(dbCommand, "@InstanceCode", System.Data.DbType.String, tradeInstance.InstanceCode);
                _dbHelper.AddInParameter(dbCommand, "@PortfolioId", System.Data.DbType.String, tradeInstance.PortfolioId);
                _dbHelper.AddInParameter(dbCommand, "@MonitorUnitId", System.Data.DbType.Int32, tradeInstance.MonitorUnitId);
                _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, tradeInstance.TemplateId);
                _dbHelper.AddInParameter(dbCommand, "@StockDirection", System.Data.DbType.Int32, (int)tradeInstance.StockDirection);
                _dbHelper.AddInParameter(dbCommand, "@FuturesContract", System.Data.DbType.String, tradeInstance.FuturesContract);
                _dbHelper.AddInParameter(dbCommand, "@FuturesDirection", System.Data.DbType.Int32, (int)tradeInstance.FuturesDirection);
                _dbHelper.AddInParameter(dbCommand, "@OperationCopies", System.Data.DbType.Int32, tradeInstance.OperationCopies);
                _dbHelper.AddInParameter(dbCommand, "@StockPriceType", System.Data.DbType.Int32, (int)tradeInstance.StockPriceType);
                _dbHelper.AddInParameter(dbCommand, "@FuturesPriceType", System.Data.DbType.Int32, (int)tradeInstance.FuturesPriceType);
                _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)TradeInstanceStatus.Active);
                _dbHelper.AddInParameter(dbCommand, "@Owner", System.Data.DbType.Int32, (int)tradeInstance.Owner);
                _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, DateTime.Now);

                string notes = tradeInstance.Notes ?? string.Empty;
                _dbHelper.AddInParameter(dbCommand, "@Notes", System.Data.DbType.String, notes);

                _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

                ret = dbCommand.ExecuteNonQuery();
                int instanceId = -1;
                if (ret > 0)
                {
                    instanceId = (int)dbCommand.Parameters["@return"].Value;
                    tradeInstance.InstanceId = instanceId;

                    foreach (var tradeSecuItem in tradeSecuItems)
                    {
                        dbCommand.Parameters.Clear();
                        dbCommand.CommandText = SP_CreateTradeInstanceSecurity;

                        _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, instanceId);
                        _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, tradeSecuItem.SecuCode);
                        _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)tradeSecuItem.SecuType);
                        _dbHelper.AddInParameter(dbCommand, "@PositionType", System.Data.DbType.Int32, (int)tradeSecuItem.PositionType);
                        _dbHelper.AddInParameter(dbCommand, "@InstructionPreBuy", System.Data.DbType.Int32, tradeSecuItem.InstructionPreBuy);
                        _dbHelper.AddInParameter(dbCommand, "@InstructionPreSell", System.Data.DbType.Int32, tradeSecuItem.InstructionPreSell);

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

        public int Update(Model.UI.TradeInstance tradeInstance, List<TradeInstanceSecurity> tradeSecuItems)
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
                dbCommand.CommandText = SP_ModifyTradeInstance;

                //The @PortfolioId MUST not been modified
                _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.String, tradeInstance.InstanceId);
                _dbHelper.AddInParameter(dbCommand, "@InstanceCode", System.Data.DbType.String, tradeInstance.InstanceCode);
                _dbHelper.AddInParameter(dbCommand, "@MonitorUnitId", System.Data.DbType.Int32, tradeInstance.MonitorUnitId);
                _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, tradeInstance.TemplateId);
                _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);
                
                string notes = tradeInstance.Notes ?? string.Empty;
                _dbHelper.AddInParameter(dbCommand, "@Notes", System.Data.DbType.String, notes);
                _dbHelper.AddInParameter(dbCommand, "@FuturesContract", System.Data.DbType.String, tradeInstance.FuturesContract);
                _dbHelper.AddInParameter(dbCommand, "@FuturesDirection", System.Data.DbType.Int32, (int)tradeInstance.FuturesDirection);

                ret = dbCommand.ExecuteNonQuery();
                if (ret > 0)
                {
                    //TODO: FIXED if there is not secuitem, it will be error.
                    foreach (var tradeSecuItem in tradeSecuItems)
                    {
                        dbCommand.Parameters.Clear();
                        dbCommand.CommandText = SP_ModifyTradeInstanceSecurityInsertOrUpdate;

                        _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, tradeInstance.InstanceId);
                        _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, tradeSecuItem.SecuCode);
                        _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)tradeSecuItem.SecuType);
                        _dbHelper.AddInParameter(dbCommand, "@PositionType", System.Data.DbType.Int32, (int)tradeSecuItem.PositionType);
                        _dbHelper.AddInParameter(dbCommand, "@InstructionPreBuy", System.Data.DbType.Int32, tradeSecuItem.InstructionPreBuy);
                        _dbHelper.AddInParameter(dbCommand, "@InstructionPreSell", System.Data.DbType.Int32, tradeSecuItem.InstructionPreSell);

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

        /// <summary>
        /// Modify the security in the tradeinstancesecurity table
        /// </summary>
        /// <param name="tradeInstance">The tradeinstance item</param>
        /// <param name="modifiedSecuItems">Those security items only change the amount or entrust direction.</param>
        /// <param name="cancelSecuItems">Those security items are canceled from the command.</param>
        /// <returns></returns>
        public int Update(Model.UI.TradeInstance tradeInstance, List<TradeInstanceSecurity> modifiedSecuItems, List<TradeInstanceSecurity> cancelSecuItems)
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
                dbCommand.CommandText = SP_ModifyTradeInstance;

                _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.String, tradeInstance.InstanceId);
                _dbHelper.AddInParameter(dbCommand, "@InstanceCode", System.Data.DbType.String, tradeInstance.InstanceCode);
                _dbHelper.AddInParameter(dbCommand, "@MonitorUnitId", System.Data.DbType.Int32, tradeInstance.MonitorUnitId);
                _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, tradeInstance.TemplateId);
                _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

                string notes = tradeInstance.Notes ?? string.Empty;
                _dbHelper.AddInParameter(dbCommand, "@Notes", System.Data.DbType.String, notes);
                _dbHelper.AddInParameter(dbCommand, "@FuturesContract", System.Data.DbType.String, tradeInstance.FuturesContract);
                _dbHelper.AddInParameter(dbCommand, "@FuturesDirection", System.Data.DbType.Int32, (int)tradeInstance.FuturesDirection);

                ret = dbCommand.ExecuteNonQuery();
                if (ret > 0)
                {
                    foreach (var tradeSecuItem in modifiedSecuItems)
                    {
                        dbCommand.Parameters.Clear();
                        dbCommand.CommandText = SP_ModifyTradeInstanceSecurityInsertOrUpdate;

                        _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, tradeInstance.InstanceId);
                        _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, tradeSecuItem.SecuCode);
                        _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)tradeSecuItem.SecuType);
                        _dbHelper.AddInParameter(dbCommand, "@PositionType", System.Data.DbType.Int32, (int)tradeSecuItem.PositionType);
                        _dbHelper.AddInParameter(dbCommand, "@InstructionPreBuy", System.Data.DbType.Int32, tradeSecuItem.InstructionPreBuy);
                        _dbHelper.AddInParameter(dbCommand, "@InstructionPreSell", System.Data.DbType.Int32, tradeSecuItem.InstructionPreSell);

                        _dbHelper.AddOutParameter(dbCommand, "@RowId", System.Data.DbType.String, 20);

                        ret = dbCommand.ExecuteNonQuery();

                        string rowId = string.Empty;
                        if (ret > 0)
                        {
                            rowId = (string)dbCommand.Parameters["@RowId"].Value;
                        }
                    }

                    foreach (var cancelSecuItem in cancelSecuItems)
                    {
                        dbCommand.Parameters.Clear();
                        dbCommand.CommandText = SP_ModifyTradeInstanceSecurityInsertOrUpdate;

                        _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, tradeInstance.InstanceId);
                        _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, cancelSecuItem.SecuCode);
                        _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)cancelSecuItem.SecuType);
                        _dbHelper.AddInParameter(dbCommand, "@InstructionPreBuy", System.Data.DbType.Int32, cancelSecuItem.InstructionPreBuy);
                        _dbHelper.AddInParameter(dbCommand, "@InstructionPreSell", System.Data.DbType.Int32, cancelSecuItem.InstructionPreSell);

                        _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

                        ret = dbCommand.ExecuteNonQuery();

                        int id = -1;
                        if (ret > 0)
                        {
                            id = (int)dbCommand.Parameters["@return"].Value;
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
    }
}
