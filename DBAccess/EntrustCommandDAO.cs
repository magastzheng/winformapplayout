using log4net;
using Model.config;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class EntrustCommandDAO: BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Create = "procEntrustCommandInsert";
        private const string SP_Modify = "procEntrustCommandUpdate";
        private const string SP_ModifyEntrustStatus = "procEntrustCommandUpdateEntrustStatus";
        private const string SP_ModifyDealStatus = "procEntrustCommandUpdateDealStatus";
        private const string SP_ModifyCancel = "procEntrustCommandUpdateCancel";
        private const string SP_DeleteBySubmitId = "procEntrustCommandDeleteBySubmitId";
        private const string SP_DeleteByCommandId = "procEntrustCommandDeleteByCommandId";
        private const string SP_DeleteByCommandIdStatus = "procEntrustCommandDeleteByCommandIdStatus";
        private const string SP_Get = "procEntrustCommandSelectAll";
        private const string SP_GetBySubmitId = "procEntrustCommandSelectBySubmitId";
        private const string SP_GetByCommandId = "procEntrustCommandSelectByCommandId";
        private const string SP_GetByCommandIdEntrustStatus = "procEntrustCommandSelectByCommandIdEntrustStatus";
        private const string SP_GetCancel = "procEntrustCommandSelectCancel";
        private const string SP_GetCancelRedo = "procEntrustCommandSelectCancelRedo";

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
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, item.EntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@DealStatus", System.Data.DbType.Int32, item.DealStatus);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateEntrustCommandStatus(int submitId, EntrustStatus entrustStatus)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyEntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, entrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateDealStatus(int submitId, DealStatus dealStatus)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyDealStatus);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);
            _dbHelper.AddInParameter(dbCommand, "@DealStatus", System.Data.DbType.Int32, dealStatus);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateCancel(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyCancel);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int DeleteBySubmitId(int submitId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_DeleteBySubmitId);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int DeleteByCommandId(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_DeleteByCommandId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int DeleteByCommandIdStatus(int commandId, EntrustStatus status)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_DeleteByCommandIdStatus);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)status);

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

        public List<EntrustCommandItem> GetByCommandId(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetByCommandId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);

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

        public List<EntrustCommandItem> GetByEntrustStatus(int commandId, EntrustStatus status)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetByCommandIdEntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)status);

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

        public List<EntrustCommandItem> GetCancel(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCancel);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);

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

        public List<EntrustCommandItem> GetCancelRedo(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCancelRedo);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);

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
