using Model.EnumType;
using Model.SecurityInfo;
using Model.UI;
using System.Collections.Generic;

namespace DBAccess
{
    public class TradingCommandSecurityDAO: BaseDAO
    {
        private const string SP_Create = "procTradingCommandSecurityInsert";
        private const string SP_ModifyEntrustAmount = "procTradingCommandSecurityUpdateEntrustAmount";
        private const string SP_Delete = "procTradingCommandSecurityDelete";
        private const string SP_Get = "procTradingCommandSecuritySelect";

        public TradingCommandSecurityDAO()
            : base()
        { 
            
        }

        public TradingCommandSecurityDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(CommandSecurityItem secItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, secItem.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, secItem.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)secItem.SecuType);
            _dbHelper.AddInParameter(dbCommand, "@WeightAmount", System.Data.DbType.Int32, secItem.WeightAmount);
            _dbHelper.AddInParameter(dbCommand, "@CommandAmount", System.Data.DbType.Int32, secItem.CommandAmount);
            _dbHelper.AddInParameter(dbCommand, "@CommandDirection", System.Data.DbType.Int32, (int)secItem.EDirection);
            _dbHelper.AddInParameter(dbCommand, "@CommandPrice", System.Data.DbType.Double, secItem.CommandPrice);
     
            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Update(CommandSecurityItem secItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyEntrustAmount);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, secItem.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, secItem.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@EntrustedAmount", System.Data.DbType.Int32, secItem.EntrustedAmount);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(int commandId, string secuCode)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public List<CommandSecurityItem> Get(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);

            List<CommandSecurityItem> items = new List<CommandSecurityItem>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CommandSecurityItem item = new CommandSecurityItem();
                    item.CommandId = (int)reader["CommandId"];
                    item.SecuCode = (string)reader["SecuCode"];
                    item.SecuType = (SecurityType)reader["SecuType"];
                    item.WeightAmount = (int)reader["WeightAmount"];
                    item.CommandAmount = (int)reader["CommandAmount"];
                    item.EDirection = (EntrustDirection)reader["CommandDirection"];
                    item.EntrustedAmount = (int)reader["EntrustedAmount"];
                    item.CommandPrice = (double)(decimal)reader["CommandPrice"];
                    item.EntrustStatus = (EntrustStatus)reader["EntrustStatus"];

                    items.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return items;
        }
    }
}
