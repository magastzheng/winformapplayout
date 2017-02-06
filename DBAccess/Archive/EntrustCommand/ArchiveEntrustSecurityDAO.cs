using Model.Archive;
using Model.EnumType;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Archive.EntrustCommand
{
    public class ArchiveEntrustSecurityDAO: BaseDAO
    {
        private const string SP_Create = "procArchiveEntrustSecurityInsert";
        private const string SP_Delete = "procArchiveEntrustCommandDelete";
        private const string SP_Select = "procArchiveEntrustSecuritySelect";

        public ArchiveEntrustSecurityDAO()
            : base()
        { 
        }

        public ArchiveEntrustSecurityDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        }

        public int Create(ArchiveEntrustSecurity item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);

            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, item.ArchiveId);
            _dbHelper.AddInParameter(dbCommand, "@RequestId", System.Data.DbType.Int32, item.RequestId);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, item.SubmitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)item.SecuType);
            _dbHelper.AddInParameter(dbCommand, "@EntrustAmount", System.Data.DbType.Int32, item.EntrustAmount);
            _dbHelper.AddInParameter(dbCommand, "@EntrustPrice", System.Data.DbType.Decimal, item.EntrustPrice);
            _dbHelper.AddInParameter(dbCommand, "@EntrustDirection", System.Data.DbType.Int32, (int)item.EntrustDirection);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)item.EntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@EntrustPriceType", System.Data.DbType.Int32, (int)item.EntrustPriceType);
            _dbHelper.AddInParameter(dbCommand, "@PriceType", System.Data.DbType.Decimal, (int)item.PriceType);
            _dbHelper.AddInParameter(dbCommand, "@EntrustNo", System.Data.DbType.Int32, item.EntrustNo);
            _dbHelper.AddInParameter(dbCommand, "@BatchNo", System.Data.DbType.Int32, item.BatchNo);
            _dbHelper.AddInParameter(dbCommand, "@DealStatus", System.Data.DbType.Int32, (int)item.DealStatus);
            _dbHelper.AddInParameter(dbCommand, "@TotalDealAmount", System.Data.DbType.Int32, item.TotalDealAmount);
            _dbHelper.AddInParameter(dbCommand, "@TotalDealBalance", System.Data.DbType.Decimal, item.TotalDealBalance);
            _dbHelper.AddInParameter(dbCommand, "@TotalDealFee", System.Data.DbType.Decimal, item.TotalDealFee);
            _dbHelper.AddInParameter(dbCommand, "@DealTimes", System.Data.DbType.Int32, item.DealTimes);
            _dbHelper.AddInParameter(dbCommand, "@EntrustDate", System.Data.DbType.DateTime, item.EntrustDate);
            _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, item.CreatedDate);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, item.ModifiedDate);
            _dbHelper.AddInParameter(dbCommand, "@EntrustFailCode", System.Data.DbType.Int32, item.EntrustFailCode);
            _dbHelper.AddInParameter(dbCommand, "@EntrustFailCause", System.Data.DbType.String, item.EntrustFailCause);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public List<ArchiveEntrustSecurity> Get(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

            List<ArchiveEntrustSecurity> items = new List<ArchiveEntrustSecurity>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var item = new ArchiveEntrustSecurity();
                    item.ArchiveId = (int)reader["ArchiveId"];
                    item.RequestId = (int)reader["RequestId"];
                    item.SubmitId = (int)reader["SubmitId"];
                    item.CommandId = (int)reader["CommandId"];
                    item.SecuCode = (string)reader["SecuCode"];
                    item.SecuType = (SecurityType)(int)reader["SecuType"];
                    item.EntrustAmount = (int)reader["EntrustAmount"];
                    item.EntrustPrice = (double)(decimal)reader["EntrustPrice"];
                    item.EntrustDirection = (EntrustDirection)(int)reader["EntrustDirection"];
                    item.EntrustStatus = (EntrustStatus)(int)reader["EntrustStatus"];
                    item.EntrustPriceType = (EntrustPriceType)(int)reader["EntrustPriceType"];
                    if (reader["EntrustNo"] != null && reader["EntrustNo"] != DBNull.Value)
                    {
                        item.EntrustNo = (int)reader["EntrustNo"];
                    }
                    if (reader["BatchNo"] != null && reader["BatchNo"] != DBNull.Value)
                    {
                        item.BatchNo = (int)reader["BatchNo"];
                    }
                    if (reader["DealStatus"] != null && reader["DealStatus"] != DBNull.Value)
                    {
                        item.DealStatus = (DealStatus)(int)reader["DealStatus"];
                    }
                    
                    item.TotalDealAmount = (int)reader["TotalDealAmount"];
                    item.TotalDealBalance = (double)(decimal)reader["TotalDealBalance"];
                    item.TotalDealFee = (double)(decimal)reader["TotalDealFee"];
                    item.DealTimes = (int)reader["DealTimes"];

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
            _dbHelper.Close(dbCommand);

            return items;
        }
    }
}