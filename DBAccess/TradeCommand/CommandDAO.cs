using log4net;
using Model.Database;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DBAccess.TradeCommand
{
    public class CommandDAO: BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_CreateTradeCommand = "procTradingCommandInsert";
        private const string SP_CreateTradingSecurity = "procTradingCommandSecurityInsert";

        public CommandDAO()
            : base()
        { 
            
        }

        public CommandDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        /// <summary>
        /// Create the TradingCommand and TradingCommandSecurity by Transaction.
        /// </summary>
        /// <param name="cmdItem"></param>
        /// <param name="secuItems"></param>
        /// <returns>CommandId</returns>
        public int Create(Model.Database.TradeCommand cmdItem, List<TradeCommandSecurity> secuItems)
        {
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(_dbHelper.Connection);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int ret = -1;
            int commandId = -1;
            try
            {
                dbCommand.CommandText = SP_CreateTradeCommand;

                _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, cmdItem.InstanceId);
                _dbHelper.AddInParameter(dbCommand, "@CommandNum", System.Data.DbType.Int32, cmdItem.CommandNum);
                _dbHelper.AddInParameter(dbCommand, "@CommandType", System.Data.DbType.Int32, cmdItem.ECommandType);
                _dbHelper.AddInParameter(dbCommand, "@ExecuteType", System.Data.DbType.Int32, cmdItem.EExecuteType);
                _dbHelper.AddInParameter(dbCommand, "@StockDirection", System.Data.DbType.Int32, (int)cmdItem.EStockDirection);
                _dbHelper.AddInParameter(dbCommand, "@FuturesDirection", System.Data.DbType.Int32, (int)cmdItem.EFuturesDirection);
                _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)cmdItem.EEntrustStatus);
                _dbHelper.AddInParameter(dbCommand, "@DealStatus", System.Data.DbType.Int32, (int)cmdItem.EDealStatus);
                _dbHelper.AddInParameter(dbCommand, "@SubmitPerson", System.Data.DbType.Int32, cmdItem.SubmitPerson);
                
                //command time
                DateTime now = DateTime.Now;
                //9:15
                DateTime startDate = new DateTime(now.Year, now.Month, now.Day, 9, 15, 0);
                if (cmdItem.DStartDate > DateTime.MinValue)
                {
                    startDate = cmdItem.DStartDate;
                }
    
                //15:15
                DateTime endDate = new DateTime(now.Year, now.Month, now.Day, 15, 15, 0);
                if (cmdItem.DEndDate > DateTime.MinValue)
                {
                    endDate = cmdItem.DEndDate;
                }

                _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, now);
                _dbHelper.AddInParameter(dbCommand, "@StartDate", System.Data.DbType.DateTime, startDate);
                _dbHelper.AddInParameter(dbCommand, "@EndDate", System.Data.DbType.DateTime, endDate);

                string notes = (cmdItem.Notes != null) ? cmdItem.Notes : string.Empty;
                _dbHelper.AddInParameter(dbCommand, "@Notes", System.Data.DbType.String, notes);

                _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

                ret = dbCommand.ExecuteNonQuery();

                if (ret >= 0)
                {
                    if (ret > 0)
                    {
                        commandId = (int)dbCommand.Parameters["@return"].Value;
                        cmdItem.CommandId = commandId;
                    }

                    foreach (var secuItem in secuItems)
                    {
                        dbCommand.Parameters.Clear();
                        dbCommand.CommandText = SP_CreateTradingSecurity;

                        secuItem.CommandId = commandId;
                        _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, secuItem.CommandId);
                        _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, secuItem.SecuCode);
                        _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)secuItem.SecuType);
                        //_dbHelper.AddInParameter(dbCommand, "@WeightAmount", System.Data.DbType.Int32, secuItem.WeightAmount);
                        _dbHelper.AddInParameter(dbCommand, "@CommandAmount", System.Data.DbType.Int32, secuItem.CommandAmount);
                        _dbHelper.AddInParameter(dbCommand, "@CommandDirection", System.Data.DbType.Int32, (int)secuItem.EDirection);
                        _dbHelper.AddInParameter(dbCommand, "@CommandPrice", System.Data.DbType.Double, secuItem.CommandPrice);

                        //string newid = string.Empty;
                        ret = dbCommand.ExecuteNonQuery();
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
                _dbHelper.Close(dbCommand.Connection);
                transaction.Dispose();
            }

            return commandId;
        }
    }
}
