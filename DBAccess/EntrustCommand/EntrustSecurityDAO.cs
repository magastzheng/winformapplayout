using Model.Database;
using Model.EnumType;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DBAccess.EntrustCommand
{
    public class EntrustSecurityDAO: BaseDAO
    {
        private const string SP_ModifyDeal = "procEntrustSecurityUpdateDeal";
        private const string SP_ModifyEntrustStatus = "procEntrustSecurityUpdateEntrustStatus";
        private const string SP_ModifyEntrustStatusByEntrustNo = "procEntrustSecurityUpdateEntrustStatusByEntrustNo";
        private const string SP_ModifyEntrustStatusByRequestId = "procEntrustSecurityUpdateResponseByRequestId";
        private const string SP_UpdateEntrustNo = "procEntrustSecurityUpdateEntrustNo";
        private const string SP_UpdateConfirmNo = "procEntrustSecurityUpdateConfirmNo";

        private const string SP_Delete = "procEntrustSecurityDelete";
        private const string SP_DeleteBySubmitId = "procEntrustSecurityDeleteBySubmitId";
        private const string SP_DeleteByCommandId = "procEntrustSecurityDeleteByCommandId";
        private const string SP_DeleteByCommandIdEntrustStatus = "procEntrustSecurityDeleteByCommandIdEntrustStatus";
        private const string SP_GetAllCombine = "procEntrustSecuritySelectAllCombine";
        private const string SP_GetByCommandId = "procEntrustSecuritySelectByCommandId";
        private const string SP_GetCombineByCommandId = "procEntrustSecuritySelectCombineByCommandId";
        private const string SP_GetCancel = "procEntrustSecuritySelectCancel";
        private const string SP_GetBySubmitId = "procEntrustSecuritySelectCombineBySubmitId";
        private const string SP_GetCancelBySubmitId = "procEntrustSecuritySelectCancelBySubmitId";
        private const string SP_GetCancelCompletedRedoBySubmitId = "procEntrustSecuritySelectCancelCompletedRedoBySubmitId";
        private const string SP_GetCombineByRequestId = "procEntrustSecuritySelectCombineByRequestId";

        private const string SP_GetEntrustFlow = "procEntrustSecuritySelectEntrustFlow";
        private const string SP_GetDealFlow = "procEntrustSecuritySelectDealFlow";

        public EntrustSecurityDAO()
            : base()
        { 
            
        }

        public EntrustSecurityDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        #region update

        public int UpdateDeal(int submitId, int commandId, string secuCode, int dealAmount, double dealBalance, double dealFee)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyDeal);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, secuCode);
            _dbHelper.AddInParameter(dbCommand, "@DealAmount", System.Data.DbType.Int32, dealAmount);
            _dbHelper.AddInParameter(dbCommand, "@DealBalance", System.Data.DbType.Decimal, dealBalance);
            _dbHelper.AddInParameter(dbCommand, "@DealFee", System.Data.DbType.Decimal, dealFee);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateEntrustStatus(int submitId, int commandId, string secuCode, EntrustStatus entrustStatus)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyEntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, secuCode);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)entrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateEntrustStatusByEntrustNo(int entrustNo, EntrustStatus entrustStatus)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyEntrustStatusByEntrustNo);
            _dbHelper.AddInParameter(dbCommand, "@EntrustNo", System.Data.DbType.Int32, entrustNo);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)entrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateEntrustStatusByRequestId(int requestId, int entrustNo, int batchNo, int entrustFailCode, string entrustFailCause)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyEntrustStatusByRequestId);
            _dbHelper.AddInParameter(dbCommand, "@RequestId", System.Data.DbType.Int32, requestId);
            _dbHelper.AddInParameter(dbCommand, "@EntrustNo", System.Data.DbType.Int32, entrustNo);
            _dbHelper.AddInParameter(dbCommand, "@BatchNo", System.Data.DbType.Int32, batchNo);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);
            _dbHelper.AddInParameter(dbCommand, "@EntrustFailCode", System.Data.DbType.Int32, entrustFailCode);
            _dbHelper.AddInParameter(dbCommand, "@EntrustFailCause", System.Data.DbType.String, entrustFailCause);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateEntrustNo(int submitId, int commandId, string secuCode, int entrustNo, int batchNo, EntrustStatus entrustStatus)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_UpdateEntrustNo);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, secuCode);
            _dbHelper.AddInParameter(dbCommand, "@EntrustNo", System.Data.DbType.Int32, entrustNo);
            _dbHelper.AddInParameter(dbCommand, "@BatchNo", System.Data.DbType.Int32, batchNo);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)entrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateConfirmNo(int submitId, int commandId, string secuCode, string confirmNo)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_UpdateConfirmNo);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, secuCode);
            _dbHelper.AddInParameter(dbCommand, "@ConfirmNo", System.Data.DbType.String, confirmNo);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #region delete

        public int Delete(int submitId, int commandId, string secuCode)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, secuCode);

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

        #endregion

        #region get/fetch

        public List<EntrustSecurity> GetByCommandId(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetByCommandId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);

            List<EntrustSecurity> items = ExecuteEntrustSecurity(dbCommand);
            _dbHelper.Close(dbCommand);

            return items;
        }

        //public List<EntrustSecurity> GetBySubmitId(int submitId)
        //{
        //    var combineItems = GetCombineBySubmitId(submitId);

        //    return new List<EntrustSecurity>(combineItems);
        //}

        public List<EntrustSecurity> GetCancel(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCancel);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);

            List<EntrustSecurity> items = ExecuteEntrustSecurity(dbCommand);
            _dbHelper.Close(dbCommand);

            return items;
        }

        public List<EntrustSecurity> GetCancelBySumbitId(int submitId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCancelBySubmitId);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);

            List<EntrustSecurity> items = ExecuteEntrustSecurity(dbCommand);
            _dbHelper.Close(dbCommand);

            return items;
        }

        public List<EntrustSecurity> GetCancelRedoBySubmitId(int submitId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCancelCompletedRedoBySubmitId);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);

            List<EntrustSecurity> items = ExecuteEntrustSecurity(dbCommand);
            _dbHelper.Close(dbCommand);

            return items;
        }

        #endregion

        #region all entrust and deal securities


        #endregion

        #region get combine

        public List<EntrustSecurityCombine> GetAllCombine()
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetAllCombine);

            List<EntrustSecurityCombine> items = new List<EntrustSecurityCombine>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EntrustSecurityCombine item = ParseData(reader);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand);

            return items;
        }

        public EntrustSecurityCombine GetByRequestId(int requestId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCombineByRequestId);
            _dbHelper.AddInParameter(dbCommand, "@RequestId", System.Data.DbType.Int32, requestId);
            var reader = _dbHelper.ExecuteReader(dbCommand);

            EntrustSecurityCombine item = new EntrustSecurityCombine();
            if (reader.HasRows && reader.Read())
            {
                item = ParseData(reader);
            }

            reader.Close();
            _dbHelper.Close(dbCommand);

            return item;
        }

        public List<EntrustSecurityCombine> GetCombineByCommandId(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCombineByCommandId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);

            List<EntrustSecurityCombine> items = new List<EntrustSecurityCombine>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EntrustSecurityCombine item = ParseData(reader);
                    if (item.CommandId == commandId)
                    {
                        items.Add(item);
                    }
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand);

            return items;
        }

        public List<EntrustSecurityCombine> GetCombineBySubmitId(int submitId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetBySubmitId);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);

            List<EntrustSecurityCombine> items = new List<EntrustSecurityCombine>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EntrustSecurityCombine item = ParseData(reader);
                    if (item.SubmitId == submitId)
                    {
                        items.Add(item);
                    }
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand);

            return items;
        }

        #endregion

        #region private method

        private List<EntrustSecurity> ExecuteEntrustSecurity(DbCommand dbCommand)
        {
            List<EntrustSecurity> items = new List<EntrustSecurity>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EntrustSecurity item = new EntrustSecurity();
                    item.RequestId = (int)reader["RequestId"];
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
                    item.BatchNo = (int)reader["BatchNo"];
                    item.DealStatus = (DealStatus)reader["DealStatus"];
                    item.TotalDealAmount = (int)reader["TotalDealAmount"];
                    item.TotalDealBalance = (double)(decimal)reader["TotalDealBalance"];
                    item.TotalDealFee = (double)(decimal)reader["TotalDealFee"];

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

                    if (reader["EntrustFailCode"] != null && reader["EntrustFailCode"] != DBNull.Value)
                    {
                        item.EntrustFailCode = (int)reader["EntrustFailCode"];
                    }

                    if (reader["EntrustFailCause"] != null && reader["EntrustFailCause"] != DBNull.Value)
                    {
                        item.EntrustFailCause = (string)reader["EntrustFailCause"];
                    }

                    items.Add(item);
                }
            }

            reader.Close();
            return items;
        }

        private EntrustSecurityCombine ParseData(DbDataReader reader)
        {
            EntrustSecurityCombine item = new EntrustSecurityCombine();
            item.RequestId = (int)reader["RequestId"];
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
            item.TotalDealAmount = (int)reader["TotalDealAmount"];
            item.TotalDealBalance = (double)(decimal)reader["TotalDealBalance"];
            item.TotalDealFee = (double)(decimal)reader["TotalDealFee"];

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

            if (reader["EntrustFailCode"] != null && reader["EntrustFailCode"] != DBNull.Value)
            {
                item.EntrustFailCode = (int)reader["EntrustFailCode"];
            }

            if (reader["EntrustFailCause"] != null && reader["EntrustFailCause"] != DBNull.Value)
            {
                item.EntrustFailCause = (string)reader["EntrustFailCause"];
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

            return item;
        }

        #endregion
    }
}
