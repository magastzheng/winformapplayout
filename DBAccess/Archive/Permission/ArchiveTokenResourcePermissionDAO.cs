using Model.Archive;
using Model.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Archive.Permission
{
    public class ArchiveTokenResourcePermissionDAO : BaseDAO
    {
        private const string SP_Create = "procArchiveTokenResourcePermissionInsert";
        private const string SP_Delete = "procArchiveTokenResourcePermissionDelete";
        private const string SP_Select = "procArchiveTokenResourcePermissionSelectByArchiveId";

        public ArchiveTokenResourcePermissionDAO()
            : base()
        { 
        }

        public ArchiveTokenResourcePermissionDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        }

        public int Create(ArchiveTokenResourcePermission permission)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);

            _dbHelper.AddInParameter(dbCommand, "@ArchiveDate", System.Data.DbType.DateTime, permission.ArchiveDate);
            _dbHelper.AddInParameter(dbCommand, "@Id", System.Data.DbType.Int32, permission.Id);
            _dbHelper.AddInParameter(dbCommand, "@Token", System.Data.DbType.Int32, permission.Token);
            _dbHelper.AddInParameter(dbCommand, "@TokenType", System.Data.DbType.Int32, (int)permission.TokenType);
            _dbHelper.AddInParameter(dbCommand, "@ResourceId", System.Data.DbType.Int32, permission.ResourceId);
            _dbHelper.AddInParameter(dbCommand, "@ResourceType", System.Data.DbType.Int32, (int)permission.ResourceType);
            _dbHelper.AddInParameter(dbCommand, "@Permission", System.Data.DbType.Int32, permission.Permission);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int id = -1;
            if (ret > 0)
            {
                id = (int)dbCommand.Parameters["@return"].Value;
            }

            return id;
        }

        public int Delete(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);
            
            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public ArchiveTokenResourcePermission GetSingle(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);
            
            var reader = _dbHelper.ExecuteReader(dbCommand);

            ArchiveTokenResourcePermission item = new ArchiveTokenResourcePermission();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    item.ArchiveId = (int)reader["ArchiveId"];
                    item.ArchiveDate = (DateTime)reader["ArchiveDate"];
                    item.Id = (int)reader["Id"];
                    item.Token = (int)reader["Token"];
                    item.TokenType = (TokenType)reader["TokenType"];
                    item.ResourceId = (int)reader["ResourceId"];
                    item.ResourceType = (ResourceType)reader["ResourceType"];
                    item.Permission = (int)reader["Permission"];
                }
            }

            reader.Close();
            _dbHelper.Close(dbCommand);

            return item;
        }
    }
}
