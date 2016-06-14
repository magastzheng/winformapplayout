using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class EntrustCommandDAO: BaseDAO
    {
        private const string SP_Create = "procEntrustCommandInsert";
        private const string SP_Modify = "procEntrustCommandUpdate";
        private const string SP_ModifyStatus = "procEntrustCommandUpdateStatus";
        private const string SP_DeleteBySubmitId = "procEntrustCommandDeleteBySubmitId";
        private const string SP_DeleteByCommandId = "procEntrustCommandDeleteByCommandId";
        private const string SP_Get = "procEntrustCommandSelectAll";
        private const string SP_GetBySubmitId = "procEntrustCommandSelectBySubmitId";
        private const string SP_GetByCommandId = "procEntrustCommandSelectByCommandId";
        private const string SP_GetByStatus = "procEntrustCommandSelectByStatus";

        public EntrustCommandDAO()
            : base()
        { 
            
        }

        public EntrustCommandDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(EntrustCommandItem item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@Copies", System.Data.DbType.Int32, item.Copies);
            _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, DateTime.Now);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int entrustId = -1;
            if (ret > 0)
            {
                entrustId = (int)dbCommand.Parameters["@return"].Value;
            }

            return entrustId;
        }

        public int Update(EntrustCommandItem item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Modify);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, item.SubmitId);
            _dbHelper.AddInParameter(dbCommand, "@EntrustNo", System.Data.DbType.Int32, item.EntrustNo);
            _dbHelper.AddInParameter(dbCommand, "@BatchNo", System.Data.DbType.Int32, item.BatchNo);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, item.Status);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateStatus(EntrustCommandItem item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyStatus);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, item.SubmitId);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, item.Status);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public List<EntrustCommandItem> Get()
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);

            List<EntrustCommandItem> items = new List<EntrustCommandItem>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EntrustCommandItem item = new EntrustCommandItem();
                    item.SubmitId = (int)reader["SubmitId"];
                    item.CommandId = (int)reader["CommandId"];
                    item.Copies = (int)reader["Copies"];
                    item.EntrustNo = (int)reader["EntrustNo"];
                    item.BatchNo = (int)reader["BatchNo"];
                    item.Status = (int)reader["Status"];

                    if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
                    {
                        item.CreatedDate = (DateTime)reader["CreatedDate"];
                    }

                    if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
                    {
                        item.ModifiedDate = (DateTime)reader["ModifiedDate"];
                    }

                    items.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return items;
        }

        public List<EntrustCommandItem> GetByCommandId()
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetByCommandId);

            List<EntrustCommandItem> items = new List<EntrustCommandItem>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EntrustCommandItem item = new EntrustCommandItem();
                    item.SubmitId = (int)reader["SubmitId"];
                    item.CommandId = (int)reader["CommandId"];
                    item.Copies = (int)reader["Copies"];
                    item.EntrustNo = (int)reader["EntrustNo"];
                    item.BatchNo = (int)reader["BatchNo"];
                    item.Status = (int)reader["Status"];

                    if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
                    {
                        item.CreatedDate = (DateTime)reader["CreatedDate"];
                    }

                    if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
                    {
                        item.ModifiedDate = (DateTime)reader["ModifiedDate"];
                    }

                    items.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return items;
        }

        public EntrustCommandItem GetBySubmitId(int submitId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetBySubmitId);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);

            EntrustCommandItem item = new EntrustCommandItem();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    
                    item.SubmitId = (int)reader["SubmitId"];
                    item.CommandId = (int)reader["CommandId"];
                    item.Copies = (int)reader["Copies"];
                    item.EntrustNo = (int)reader["EntrustNo"];
                    item.BatchNo = (int)reader["BatchNo"];
                    item.Status = (int)reader["Status"];

                    if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
                    {
                        item.CreatedDate = (DateTime)reader["CreatedDate"];
                    }

                    if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
                    {
                        item.ModifiedDate = (DateTime)reader["ModifiedDate"];
                    }

                    break;
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return item;
        }

        public List<EntrustCommandItem> GetByStatus(EntrustStatus status)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetByStatus);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)status);

            List<EntrustCommandItem> items = new List<EntrustCommandItem>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EntrustCommandItem item = new EntrustCommandItem();
                    item.SubmitId = (int)reader["SubmitId"];
                    item.CommandId = (int)reader["CommandId"];
                    item.Copies = (int)reader["Copies"];
                    item.EntrustNo = (int)reader["EntrustNo"];
                    item.BatchNo = (int)reader["BatchNo"];
                    item.Status = (int)reader["Status"];

                    if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
                    {
                        item.CreatedDate = (DateTime)reader["CreatedDate"];
                    }

                    if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
                    {
                        item.ModifiedDate = (DateTime)reader["ModifiedDate"];
                    }

                    items.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return items;
        }
    }
}
