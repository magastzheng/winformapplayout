using Model.Data;
using Model.SecurityInfo;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class EntrustSecurityDAO: BaseDAO
    {
        private const string SP_Create = "procEntrustSecurityInsert";
        private const string SP_Modify = "procEntrustSecurityUpdate";
        private const string SP_ModifyStatus = "procEntrustSecurityUpdateStatus";
        private const string SP_Delete = "procEntrustSecurityDelete";
        private const string SP_DeleteBySubmitId = "procEntrustSecurityDeleteBySubmitId";
        private const string SP_DeleteByCommandId = "procEntrustSecurityDeleteByCommandId";
        private const string SP_Get = "procEntrustSecuritySelectAll";
        private const string SP_GetBySubmitId = "procEntrustSecuritySelectBySubmitId";
        private const string SP_GetByCommandId = "procEntrustSecuritySelectByCommandId";

        public EntrustSecurityDAO()
            : base()
        { 
            
        }

        public EntrustSecurityDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(EntrustSecurityItem item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, item.SubmitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, item.SecuType);
            _dbHelper.AddInParameter(dbCommand, "@EntrustAmount", System.Data.DbType.Int32, item.EntrustAmount);
            _dbHelper.AddInParameter(dbCommand, "@EntrustPrice", System.Data.DbType.Decimal, item.EntrustPrice);
            _dbHelper.AddInParameter(dbCommand, "@EntrustDirection", System.Data.DbType.Int32, (int)item.EntrustDirection);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)item.EntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, DateTime.Now);
            
            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Update(EntrustSecurityItem item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Modify);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, item.SubmitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@EntrustAmount", System.Data.DbType.Int32, item.EntrustAmount);
            _dbHelper.AddInParameter(dbCommand, "@EntrustPrice", System.Data.DbType.Decimal, item.EntrustPrice);
            _dbHelper.AddInParameter(dbCommand, "@EntrustDirection", System.Data.DbType.Int32, (int)item.EntrustDirection);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)item.EntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateStatus(EntrustSecurityItem item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyStatus);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, item.SubmitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, item.EntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(EntrustSecurityItem item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, item.SubmitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int DeleteBySubmitId(int submitId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public List<EntrustSecurityItem> Get()
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);

            List<EntrustSecurityItem> items = new List<EntrustSecurityItem>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EntrustSecurityItem item = new EntrustSecurityItem();
                    item.SubmitId = (int)reader["SubmitId"];
                    item.CommandId = (int)reader["CommandId"];
                    item.SecuCode = (string)reader["SecuCode"];
                    item.SecuType = (SecurityType)(int)reader["SecuType"];
                    item.EntrustAmount = (int)reader["EntrustAmount"];
                    item.EntrustPrice = (double)(decimal)reader["EntrustPrice"];
                    item.EntrustDirection = (EntrustDirection)reader["EntrustDirection"];
                    item.EntrustStatus = (EntrustStatus)reader["EntrustStatus"];

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

        public List<EntrustSecurityItem> GetBySubmitId(int submitId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);

            List<EntrustSecurityItem> items = new List<EntrustSecurityItem>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EntrustSecurityItem item = new EntrustSecurityItem();
                    item.SubmitId = (int)reader["SubmitId"];
                    item.CommandId = (int)reader["CommandId"];
                    item.SecuCode = (string)reader["SecuCode"];
                    item.SecuType = (SecurityType)(int)reader["SecuType"];
                    item.EntrustAmount = (int)reader["EntrustAmount"];
                    item.EntrustPrice = (double)(decimal)reader["EntrustPrice"];
                    item.EntrustDirection = (EntrustDirection)reader["EntrustDirection"];
                    item.EntrustStatus = (EntrustStatus)reader["EntrustStatus"];

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

        public List<EntrustSecurityItem> GetByCommandId(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);

            List<EntrustSecurityItem> items = new List<EntrustSecurityItem>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EntrustSecurityItem item = new EntrustSecurityItem();
                    item.SubmitId = (int)reader["SubmitId"];
                    item.CommandId = (int)reader["CommandId"];
                    item.SecuCode = (string)reader["SecuCode"];
                    item.SecuType = (SecurityType)(int)reader["SecuType"];
                    item.EntrustAmount = (int)reader["EntrustAmount"];
                    item.EntrustPrice = (double)(decimal)reader["EntrustPrice"];
                    item.EntrustDirection = (EntrustDirection)reader["EntrustDirection"];
                    item.EntrustStatus = (EntrustStatus)reader["EntrustStatus"];

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
