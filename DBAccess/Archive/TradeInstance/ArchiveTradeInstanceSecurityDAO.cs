using Model.Archive;
using Model.EnumType;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Archive.TradeInstance
{
    public class ArchiveTradeInstanceSecurityDAO: BaseDAO
    {
        private const string SP_Create = "procArchiveTradeInstanceSecurityInsert";
        private const string SP_DeleteByArchiveId = "procArchiveTradeInstanceSecurityDeleteByArchiveId";
        private const string SP_DeleteByInstanceId = "procArchiveTradeInstanceSecurityDeleteByInstanceId";
        private const string SP_Select = "procArchiveTradeInstanceSecuritySelect";

        public ArchiveTradeInstanceSecurityDAO()
            : base()
        { 
        }

        public ArchiveTradeInstanceSecurityDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        }

        /// <summary>
        /// TODO: create the ArchiveTradeInstance by transaction.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Create(ArchiveTradeInstanceSecurity item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, item.InstanceId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)item.SecuType);
            _dbHelper.AddInParameter(dbCommand, "@PositionType", System.Data.DbType.Int32, (int)item.PositionType);
            _dbHelper.AddInParameter(dbCommand, "@InstructionPreBuy", System.Data.DbType.Int32, item.InstructionPreBuy);
            _dbHelper.AddInParameter(dbCommand, "@InstructionPreSell", System.Data.DbType.Int32, item.InstructionPreSell);
            _dbHelper.AddInParameter(dbCommand, "@BuyBalance", System.Data.DbType.Decimal, item.BuyBalance);
            _dbHelper.AddInParameter(dbCommand, "@SellBalance", System.Data.DbType.Decimal, item.SellBalance);
            _dbHelper.AddInParameter(dbCommand, "@DealFee", System.Data.DbType.Decimal, item.DealFee);
            _dbHelper.AddInParameter(dbCommand, "@BuyToday", System.Data.DbType.Int32, item.BuyToday);
            _dbHelper.AddInParameter(dbCommand, "@SellToday", System.Data.DbType.Int32, item.SellToday);
            _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, item.CreatedDate);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, item.ModifiedDate);
            _dbHelper.AddInParameter(dbCommand, "@LastDate", System.Data.DbType.DateTime, item.LastDate);

            DateTime archiveDate = DateTime.Now;
            if(item.ArchiveDate != null && item.ArchiveDate > DateTime.MinValue && item.ArchiveDate < DateTime.MaxValue)
            {
                archiveDate = item.ArchiveDate;
            }
            
            _dbHelper.AddInParameter(dbCommand, "@ArchiveDate", System.Data.DbType.DateTime, archiveDate);

            _dbHelper.AddOutParameter(dbCommand, "@RowId", System.Data.DbType.String, 20);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int archiveId = -1;
            if (ret > 0)
            {
                string newid = string.Empty;
                if (ret > 0)
                {
                    newid = (string)dbCommand.Parameters["@RowId"].Value;
                }
            }

            return archiveId;
        }

        public int DeleteByArchiveId(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_DeleteByArchiveId);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int DeleteByInstanceId(int instanceId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_DeleteByInstanceId);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, instanceId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public List<ArchiveTradeInstanceSecurity> Get(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);

            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

            var items = new List<ArchiveTradeInstanceSecurity>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var item = GetSecurity(reader);
                    items.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand);

            return items;
        }

        private ArchiveTradeInstanceSecurity GetSecurity(DbDataReader reader)
        {
            var item = new ArchiveTradeInstanceSecurity();

            item.ArchiveId = (int)reader["ArchiveId"];
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

            if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
            {
                item.CreatedDate = (DateTime)reader["CreatedDate"];
            }

            if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
            {
                item.ModifiedDate = (DateTime)reader["ModifiedDate"];
            }

            if (reader["LastDate"] != null && reader["LastDate"] != DBNull.Value)
            {
                item.LastDate = (DateTime)reader["LastDate"];
            }

            if (reader["ArchiveDate"] != null && reader["ArchiveDate"] != DBNull.Value)
            {
                item.ArchiveDate = (DateTime)reader["ArchiveDate"];
            }

            return item;
        }
    }
}
