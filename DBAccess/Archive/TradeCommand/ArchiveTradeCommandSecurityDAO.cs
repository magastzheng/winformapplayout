using Model.Archive;
using Model.EnumType;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Archive.TradeCommand
{
    public class ArchiveTradeCommandSecurityDAO : BaseDAO
    {
        private const string SP_Create = "procArchiveTradeCommandSecurityInsert";
        private const string SP_Delete = "procArchiveTradeCommandSecurityDelete";
        private const string SP_Select = "procArchiveTradeCommandSecuritySelect";

        public ArchiveTradeCommandSecurityDAO()
            : base()
        { 
            
        }

        public ArchiveTradeCommandSecurityDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(ArchiveTradeCommandSecurity item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, item.ArchiveId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)item.SecuType);
            _dbHelper.AddInParameter(dbCommand, "@CommandAmount", System.Data.DbType.Int32, item.CommandAmount);
            _dbHelper.AddInParameter(dbCommand, "@CommandDirection", System.Data.DbType.Int32, (int)item.EDirection);
            _dbHelper.AddInParameter(dbCommand, "@CommandPrice", System.Data.DbType.Double, item.CommandPrice);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);

            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public List<ArchiveTradeCommandSecurity> Get(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

            List<ArchiveTradeCommandSecurity> items = new List<ArchiveTradeCommandSecurity>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ArchiveTradeCommandSecurity item = new ArchiveTradeCommandSecurity();
                    item.ArchiveId = (int)reader["ArchiveId"];
                    item.CommandId = (int)reader["CommandId"];
                    item.SecuCode = (string)reader["SecuCode"];
                    item.SecuType = (SecurityType)(int)reader["SecuType"];
                    item.CommandAmount = (int)reader["CommandAmount"];
                    item.EDirection = (EntrustDirection)(int)reader["CommandDirection"];
                    item.CommandPrice = (double)(decimal)reader["CommandPrice"];
                    item.EntrustStatus = (EntrustStatus)(int)reader["EntrustStatus"];

                    items.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return items;
        }
    }
}
