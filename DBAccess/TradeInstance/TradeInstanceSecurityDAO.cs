using log4net;
using Model.EnumType;
using Model.SecurityInfo;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DBAccess.TradeInstance
{
    public class TradeInstanceSecurityDAO: BaseDAO
    {
        private const string SP_Create = "procTradeInstanceSecurityInsert";
        private const string SP_Transfer = "procTradeInstanceSecurityTransfer";
        private const string SP_ModifyBuyToday = "procTradeInstanceSecurityBuyToday";
        private const string SP_ModifySellToday = "procTradeInstanceSecuritySellToday";
        private const string SP_ModifyPreTrade = "procTradeInstanceSecurityInstructionPreTrade";
        private const string SP_Delete = "procTradeInstanceSecurityDelete";
        private const string SP_Get = "procTradeInstanceSecuritySelect";
        private const string SP_Settle = "procTradeInstanceSecuritySettle";

        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TradeInstanceSecurityDAO()
            : base()
        { 
            
        }

        public TradeInstanceSecurityDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public string Create(TradeInstanceSecurity securityItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, securityItem.InstanceId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, securityItem.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)securityItem.SecuType);
            _dbHelper.AddInParameter(dbCommand, "@PositionType", System.Data.DbType.Int32, (int)securityItem.PositionType);
            _dbHelper.AddInParameter(dbCommand, "@InstructionPreBuy", System.Data.DbType.Int32, securityItem.InstructionPreBuy);
            _dbHelper.AddInParameter(dbCommand, "@InstructionPreSell", System.Data.DbType.Int32, securityItem.InstructionPreSell);

            _dbHelper.AddOutParameter(dbCommand, "@RowId", System.Data.DbType.String, 20);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            string rowId = string.Empty;
            if (ret > 0)
            {
                rowId = (string)dbCommand.Parameters["@RowId"].Value;
            }

            return rowId;
        }

        public int UpdateBuyToday(TradeInstanceSecurity securityItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyBuyToday);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, securityItem.InstanceId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, securityItem.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@BuyAmount", System.Data.DbType.Int32, securityItem.BuyToday);
            _dbHelper.AddInParameter(dbCommand, "@BuyBalance", System.Data.DbType.Decimal, securityItem.BuyBalance);
            _dbHelper.AddInParameter(dbCommand, "@DealFee", System.Data.DbType.Decimal, securityItem.DealFee);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateSellToday(TradeInstanceSecurity securityItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifySellToday);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, securityItem.InstanceId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, securityItem.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@SellAmount", System.Data.DbType.Int32, securityItem.SellToday);
            _dbHelper.AddInParameter(dbCommand, "@SellBalance", System.Data.DbType.Decimal, securityItem.SellBalance);
            _dbHelper.AddInParameter(dbCommand, "@DealFee", System.Data.DbType.Decimal, securityItem.DealFee);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdatePreTrade(TradeInstanceSecurity securityItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyPreTrade);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, securityItem.InstanceId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, securityItem.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@InstructionPreBuy", System.Data.DbType.Int32, securityItem.InstructionPreBuy);
            _dbHelper.AddInParameter(dbCommand, "@InstructionPreSell", System.Data.DbType.Int32, securityItem.InstructionPreSell);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(TradeInstanceSecurity securityItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, securityItem.InstanceId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, securityItem.SecuCode);
            
            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public List<TradeInstanceSecurity> Get(int instanceId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, instanceId);

            List<TradeInstanceSecurity> items = new List<TradeInstanceSecurity>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TradeInstanceSecurity item = new TradeInstanceSecurity();
                    item.InstanceId = (int)reader["InstanceId"];
                    item.SecuCode = (string)reader["SecuCode"];
                    item.SecuType = (SecurityType)reader["SecuType"];
                    item.PositionType = (PositionType)reader["PositionType"];

                    if (reader["InstructionPreBuy"] != null && reader["InstructionPreBuy"] != DBNull.Value)
                    {
                        item.InstructionPreBuy = (int)reader["InstructionPreBuy"];
                    }

                    if (reader["InstructionPreSell"] != null && reader["InstructionPreSell"] != DBNull.Value)
                    {
                        item.InstructionPreSell = (int)reader["InstructionPreSell"];

                    }

                    if (reader["PositionAmount"] != null && reader["PositionAmount"] != DBNull.Value)
                    {
                        item.PositionAmount = (int)reader["PositionAmount"];
                    }

                    item.BuyBalance = (double)(decimal)reader["BuyBalance"];
                    item.SellBalance = (double)(decimal)reader["SellBalance"];
                    item.DealFee = (double)(decimal)reader["DealFee"];
                    item.BuyToday = (int)reader["BuyToday"];
                    item.SellToday = (int)reader["SellToday"];
                    item.CreatedDate = (DateTime)reader["CreatedDate"];
                    if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
                    {
                        item.ModifiedDate = (DateTime)reader["ModifiedDate"];
                    }
                    item.LastDate = (DateTime)reader["LastDate"];

                    items.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return items;
        }

        public int Settle()
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Settle);
           
            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Transfer(List<TradeInstanceSecurity> destSecuItem, List<TradeInstanceSecurity> srcSecuItem)
        {
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(_dbHelper.Connection);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int ret = -1;
            try
            {
                dbCommand.CommandText = SP_Transfer;

                foreach (var secuItem in srcSecuItem)
                {
                    dbCommand.Parameters.Clear();
                    _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, secuItem.InstanceId);
                    _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, secuItem.SecuCode);
                    _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)secuItem.SecuType);
                    _dbHelper.AddInParameter(dbCommand, "@PositionType", System.Data.DbType.Int32, (int)secuItem.PositionType);
                    _dbHelper.AddInParameter(dbCommand, "@PositionAmount", System.Data.DbType.Int32, secuItem.PositionAmount);

                    ret = dbCommand.ExecuteNonQuery();
                }

                foreach (var secuItem in destSecuItem)
                {
                    dbCommand.Parameters.Clear();
                    _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, secuItem.InstanceId);
                    _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, secuItem.SecuCode);
                    _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)secuItem.SecuType);
                    _dbHelper.AddInParameter(dbCommand, "@PositionType", System.Data.DbType.Int32, (int)secuItem.PositionType);
                    _dbHelper.AddInParameter(dbCommand, "@PositionAmount", System.Data.DbType.Int32, secuItem.PositionAmount);

                    ret = dbCommand.ExecuteNonQuery();
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

            return ret;
        }
    }
}
