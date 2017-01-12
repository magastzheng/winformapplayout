using Model.Archive;
using Model.EnumType;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Archive.EntrustCommand
{
    public class ArchiveEntrustCommandDAO : BaseDAO
    {
        private const string SP_Create = "procArchiveEntrustCommandInsert";
        private const string SP_Delete = "procArchiveEntrustCommandDelete";
        private const string SP_Select = "procArchiveEntrustCommandSelect";
        private const string SP_SelectBySubmitId = "procArchiveEntrustCommandSelectBySubmitId";

        public ArchiveEntrustCommandDAO()
            : base()
        { 
        }

        public ArchiveEntrustCommandDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        }

        public int Create(ArchiveEntrustCommand item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, item.SubmitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@Copies", System.Data.DbType.Int32, item.Copies);
            _dbHelper.AddInParameter(dbCommand, "@EntrustNo", System.Data.DbType.Int32, item.EntrustNo);
            _dbHelper.AddInParameter(dbCommand, "@BatchNo", System.Data.DbType.Int32, item.BatchNo);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)item.EntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@DealStatus", System.Data.DbType.Int32, (int)item.DealStatus);
            _dbHelper.AddInParameter(dbCommand, "@SubmitPerson", System.Data.DbType.Int32, item.SubmitPerson);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveDate", System.Data.DbType.DateTime, item.ArchiveDate);
            _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, item.CreatedDate);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, item.ModifiedDate);
            _dbHelper.AddInParameter(dbCommand, "@EntrustFailCode", System.Data.DbType.Int32, item.EntrustFailCode);
            _dbHelper.AddInParameter(dbCommand, "@EntrustFailCause", System.Data.DbType.String, item.EntrustFailCause);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int entrustId = -1;
            if (ret > 0)
            {
                entrustId = (int)dbCommand.Parameters["@return"].Value;
            }

            return entrustId;
        }

        public int Delete(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public List<ArchiveEntrustCommand> Get(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);

            List<ArchiveEntrustCommand> items = new List<ArchiveEntrustCommand>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ArchiveEntrustCommand item = ParseData(reader);

                    items.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return items;
        }

        public ArchiveEntrustCommand GetBySubmitId(int submitId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_SelectBySubmitId);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);

            ArchiveEntrustCommand item = new ArchiveEntrustCommand();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows && reader.Read())
            {
                item = ParseData(reader);
            }

            return item;
        }

        #region ParseData

        private ArchiveEntrustCommand ParseData(DbDataReader reader)
        {
            var item = new ArchiveEntrustCommand();
            item.ArchiveId = (int)reader["ArchiveId"];
            item.SubmitId = (int)reader["SubmitId"];
            item.CommandId = (int)reader["CommandId"];
            item.Copies = (int)reader["Copies"];
            if (reader["EntrustNo"] != null && reader["EntrustNo"] != DBNull.Value)
            {
                item.EntrustNo = (int)reader["EntrustNo"];
            }

            if (reader["BatchNo"] != null && reader["BatchNo"] != DBNull.Value)
            {
                item.BatchNo = (int)reader["BatchNo"];
            }
            item.EntrustStatus = (EntrustStatus)(int)reader["EntrustStatus"];
            item.DealStatus = (DealStatus)(int)reader["DealStatus"];
            item.SubmitPerson = (int)reader["SubmitPerson"];

            if (reader["ArchiveDate"] != null && reader["ArchiveDate"] != DBNull.Value)
            {
                item.ArchiveDate = (DateTime)reader["ArchiveDate"];
            }

            if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
            {
                item.CreatedDate = (DateTime)reader["CreatedDate"];
            }

            if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
            {
                item.ModifiedDate = (DateTime)reader["ModifiedDate"];
            }

            if (reader["EntrustFailCode"] != null && reader["EntrustFailCode"] != DBNull.Value)
            {
                item.EntrustFailCode = (int)reader["EntrustFailCode"];
            }

            if (reader["EntrustFailCause"] != null && reader["EntrustFailCause"] != DBNull.Value)
            {
                item.EntrustFailCause = (string)reader["EntrustFailCause"];
            }

            return item;
        }

        #endregion
    }
}
