using Model.EnumType;
using Model.SecurityInfo;
using Model.UI;
using System;
using System.Collections.Generic;

namespace DBAccess
{
    public class EntrustSecurityDAO: BaseDAO
    {
        private const string SP_Create = "procEntrustSecurityInsert";
        private const string SP_Modify = "procEntrustSecurityUpdate";
        private const string SP_ModifyEntrustStatus = "procEntrustSecurityUpdateEntrustStatus";
        private const string SP_ModifyEntrustNo = "procEntrustSecurityUpdateEntrustNo";
        private const string SP_ModifyEntrustStatusBySubmitId = "procEntrustSecurityUpdateEntrustStatusBySubmitId";
        private const string SP_ModifyDeal = "procEntrustSecurityUpdateDeal";
        private const string SP_ModifyCancel = "procEntrustSecurityUpdateCancel";
        private const string SP_Delete = "procEntrustSecurityDelete";
        private const string SP_DeleteBySubmitId = "procEntrustSecurityDeleteBySubmitId";
        private const string SP_DeleteByCommandId = "procEntrustSecurityDeleteByCommandId";
        private const string SP_DeleteByCommandIdEntrustStatus = "procEntrustSecurityDeleteByCommandIdEntrustStatus";
        private const string SP_Get = "procEntrustSecuritySelectAll";
        private const string SP_GetAllCombine = "procEntrustSecuritySelectAllCombine";
        private const string SP_GetBySubmitId = "procEntrustSecuritySelectBySubmitId";
        private const string SP_GetByCommandId = "procEntrustSecuritySelectByCommandId";
        private const string SP_GetByEntrustStatus = "procEntrustSecuritySelectByEntrustStatus";
        private const string SP_GetCancel = "procEntrustSecuritySelectCancel";
        private const string SP_GetCancelRedo = "procEntrustSecuritySelectCancelRedo";

        //private const string SP_GetEntrustFlow = "procEntrustSecuritySelectEntrustFlow";
        //private const string SP_GetDealFlow = "procEntrustSecuritySelectDealFlow";

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
            _dbHelper.AddInParameter(dbCommand, "@EntrustPriceType", System.Data.DbType.Int32, (int)item.EntrustPriceType);
            _dbHelper.AddInParameter(dbCommand, "@PriceType", System.Data.DbType.Int32, (int)item.PriceType);
            _dbHelper.AddInParameter(dbCommand, "@EntrustDate", System.Data.DbType.DateTime, item.EntrustDate);
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
            _dbHelper.AddInParameter(dbCommand, "@EntrustPriceType", System.Data.DbType.Int32, (int)item.EntrustPriceType);
            _dbHelper.AddInParameter(dbCommand, "@PriceType", System.Data.DbType.Int32, (int)item.PriceType);
            _dbHelper.AddInParameter(dbCommand, "@EntrustNo", System.Data.DbType.Int32, (int)item.EntrustNo);
            _dbHelper.AddInParameter(dbCommand, "@EntrustDate", System.Data.DbType.DateTime, item.EntrustDate);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateEntrustStatus(EntrustSecurityItem item, EntrustStatus entrustStatus)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyEntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, item.SubmitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)entrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateEntrustNo(EntrustSecurityItem item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyEntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, item.SubmitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@EntrustNo", System.Data.DbType.Int32, item.EntrustNo);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateEntrustStatusBySubmitId(int submitId, EntrustStatus entrustStatus)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyEntrustStatusBySubmitId);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)entrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateDeal(EntrustSecurityItem item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyEntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, item.SubmitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@DealStatus", System.Data.DbType.Int32, (int)item.DealStatus);
            _dbHelper.AddInParameter(dbCommand, "@DealAmount", System.Data.DbType.Int32, item.DealAmount);
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

        public int DeleteByCommandIdEntrustStatus(int commandId, EntrustStatus status)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_DeleteByCommandIdEntrustStatus);

            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)status);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public List<EntrustSecurityItem> GetAll()
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
                    item.EntrustPriceType = (EntrustPriceType)reader["EntrustPriceType"];
                    item.PriceType = (PriceType)reader["PriceType"];
                    item.EntrustNo = (int)reader["EntrustNo"];
                    item.DealStatus = (DealStatus)reader["DealStatus"];
                    item.DealAmount = (int)reader["DealAmount"];

                    if (reader["EntrustDate"] != null && reader["EntrustDate"] != DBNull.Value)
                    {
                        item.EntrustDate = (DateTime)reader["EntrustDate"];
                    }

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
                    item.EntrustPriceType = (EntrustPriceType)reader["EntrustPriceType"];
                    item.PriceType = (PriceType)reader["PriceType"];
                    item.EntrustNo = (int)reader["EntrustNo"];
                    item.DealStatus = (DealStatus)reader["DealStatus"];
                    item.DealAmount = (int)reader["DealAmount"];

                    if (reader["EntrustDate"] != null && reader["EntrustDate"] != DBNull.Value)
                    {
                        item.EntrustDate = (DateTime)reader["EntrustDate"];
                    }

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
                    item.EntrustPriceType = (EntrustPriceType)reader["EntrustPriceType"];
                    item.PriceType = (PriceType)reader["PriceType"];
                    item.EntrustNo = (int)reader["EntrustNo"];
                    item.DealStatus = (DealStatus)reader["DealStatus"];
                    item.DealAmount = (int)reader["DealAmount"];

                    if (reader["EntrustDate"] != null && reader["EntrustDate"] != DBNull.Value)
                    {
                        item.EntrustDate = (DateTime)reader["EntrustDate"];
                    }

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

        public List<EntrustSecurityItem> GetByEntrustStatus(int submitId, int commandId, EntrustStatus entrustStatus)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetByEntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, entrustStatus);

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
                    item.EntrustPriceType = (EntrustPriceType)reader["EntrustPriceType"];
                    item.PriceType = (PriceType)reader["PriceType"];
                    item.EntrustNo = (int)reader["EntrustNo"];
                    item.DealStatus = (DealStatus)reader["DealStatus"];
                    item.DealAmount = (int)reader["DealAmount"];

                    if (reader["EntrustDate"] != null && reader["EntrustDate"] != DBNull.Value)
                    {
                        item.EntrustDate = (DateTime)reader["EntrustDate"];
                    }

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

        public List<EntrustSecurityItem> GetCancel(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCancel);
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
                    item.EntrustPriceType = (EntrustPriceType)reader["EntrustPriceType"];
                    item.PriceType = (PriceType)reader["PriceType"];
                    item.EntrustNo = (int)reader["EntrustNo"];
                    item.DealStatus = (DealStatus)reader["DealStatus"];
                    item.DealAmount = (int)reader["DealAmount"];

                    if (reader["EntrustDate"] != null && reader["EntrustDate"] != DBNull.Value)
                    {
                        item.EntrustDate = (DateTime)reader["EntrustDate"];
                    }

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

        public List<EntrustSecurityItem> GetCancelRedo(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCancelRedo);
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
                    item.EntrustPriceType = (EntrustPriceType)reader["EntrustPriceType"];
                    item.PriceType = (PriceType)reader["PriceType"];
                    item.EntrustNo = (int)reader["EntrustNo"];
                    item.DealStatus = (DealStatus)reader["DealStatus"];
                    item.DealAmount = (int)reader["DealAmount"];

                    if (reader["EntrustDate"] != null && reader["EntrustDate"] != DBNull.Value)
                    {
                        item.EntrustDate = (DateTime)reader["EntrustDate"];
                    }

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

        #region get combine

        public List<EntrustSecurityCombineItem> GetAllCombine()
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetAllCombine);

            List<EntrustSecurityCombineItem> items = new List<EntrustSecurityCombineItem>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EntrustSecurityCombineItem item = new EntrustSecurityCombineItem();
                    item.SubmitId = (int)reader["SubmitId"];
                    item.CommandId = (int)reader["CommandId"];
                    item.SecuCode = (string)reader["SecuCode"];
                    item.SecuType = (SecurityType)(int)reader["SecuType"];
                    item.EntrustAmount = (int)reader["EntrustAmount"];
                    item.EntrustPrice = (double)(decimal)reader["EntrustPrice"];
                    item.EntrustDirection = (EntrustDirection)reader["EntrustDirection"];
                    item.EntrustStatus = (EntrustStatus)reader["EntrustStatus"];
                    item.EntrustPriceType = (EntrustPriceType)reader["EntrustPriceType"];
                    item.PriceType = (PriceType)reader["PriceType"];
                    item.EntrustNo = (int)reader["EntrustNo"];
                    item.DealStatus = (DealStatus)reader["DealStatus"];
                    item.DealAmount = (int)reader["DealAmount"];

                    if (reader["EntrustDate"] != null && reader["EntrustDate"] != DBNull.Value)
                    {
                        item.EntrustDate = (DateTime)reader["EntrustDate"];
                    }

                    if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
                    {
                        item.CreatedDate = (DateTime)reader["CreatedDate"];
                    }

                    if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
                    {
                        item.ModifiedDate = (DateTime)reader["ModifiedDate"];
                    }

                    item.BatchNo = (int)reader["BatchNo"];
                    item.InstanceId = (int)reader["InstanceId"];
                    item.InstanceCode = (string)reader["InstanceCode"];
                    item.MonitorUnitId = (int)reader["MonitorUnitId"];
                    item.PortfolioId = (int)reader["PortfolioId"];
                    item.PortfolioCode = (string)reader["PortfolioCode"];
                    item.PortfolioName = (string)reader["PortfolioName"];
                    item.AccountCode = (string)reader["AccountCode"];
                    item.AccountName = (string)reader["AccountName"];

                    items.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return items;
        }

        #endregion
    }
}
