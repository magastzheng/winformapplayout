using Model.SecurityInfo;
using Model.UI;
using System;
using System.Collections.Generic;

namespace DBAccess.TradeInstance
{
    public class TradingInstanceSecurityDAO: BaseDAO
    {
        private const string SP_Create = "procTradingInstanceSecurityInsert";
        private const string SP_ModifyBuyToday = "procTradingInstanceSecurityBuyToday";
        private const string SP_ModifySellToday = "procTradingInstanceSecuritySellToday";
        private const string SP_ModifyPreTrade = "procTradingInstanceSecurityInstructionPreTrade";
        private const string SP_Delete = "procTradingInstanceSecurityDelete";
        private const string SP_Get = "procTradingInstanceSecuritySelect";
        private const string SP_Settle = "procTradingInstanceSecuritySettle";

        public TradingInstanceSecurityDAO()
            : base()
        { 
            
        }

        public TradingInstanceSecurityDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public string Create(TradingInstanceSecurity securityItem)
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

        public int UpdateBuyToday(TradingInstanceSecurity securityItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyBuyToday);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, securityItem.InstanceId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, securityItem.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@BuyAmount", System.Data.DbType.Int32, securityItem.BuyToday);
            _dbHelper.AddInParameter(dbCommand, "@BuyBalance", System.Data.DbType.Decimal, securityItem.BuyBalance);
            _dbHelper.AddInParameter(dbCommand, "@DealFee", System.Data.DbType.Decimal, securityItem.DealFee);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateSellToday(TradingInstanceSecurity securityItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifySellToday);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, securityItem.InstanceId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, securityItem.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@SellAmount", System.Data.DbType.Int32, securityItem.SellToday);
            _dbHelper.AddInParameter(dbCommand, "@SellBalance", System.Data.DbType.Decimal, securityItem.SellBalance);
            _dbHelper.AddInParameter(dbCommand, "@DealFee", System.Data.DbType.Decimal, securityItem.DealFee);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdatePreTrade(TradingInstanceSecurity securityItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyPreTrade);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, securityItem.InstanceId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, securityItem.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@InstructionPreBuy", System.Data.DbType.Int32, securityItem.InstructionPreBuy);
            _dbHelper.AddInParameter(dbCommand, "@InstructionPreSell", System.Data.DbType.Int32, securityItem.InstructionPreSell);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(TradingInstanceSecurity securityItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, securityItem.InstanceId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, securityItem.SecuCode);
            
            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public List<TradingInstanceSecurity> Get(int instanceId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, instanceId);

            List<TradingInstanceSecurity> items = new List<TradingInstanceSecurity>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TradingInstanceSecurity item = new TradingInstanceSecurity();
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
    }
}
