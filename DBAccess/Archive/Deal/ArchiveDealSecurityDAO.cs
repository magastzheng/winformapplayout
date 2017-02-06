using log4net;
using Model.Archive;
using Model.EnumType;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Archive.Deal
{
    public class ArchiveDealSecurityDAO: BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Create = "procArchiveDealSecurityInsert";
        private const string SP_Delete = "procArchiveDealSecurityDelete";
        private const string SP_Select = "procArchiveDealSecuritySelect";

        public ArchiveDealSecurityDAO()
            : base()
        { 
        }

        public ArchiveDealSecurityDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        }

        public int Create(ArchiveDealSecurity item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, item.ArchiveId);
            _dbHelper.AddInParameter(dbCommand, "@RequestId", System.Data.DbType.Int32, item.RequestId);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, item.SubmitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@DealNo", System.Data.DbType.String, item.DealNo);
            _dbHelper.AddInParameter(dbCommand, "@EntrustNo", System.Data.DbType.Int32, item.EntrustNo);
            _dbHelper.AddInParameter(dbCommand, "@BatchNo", System.Data.DbType.Int32, item.BatchNo);
            _dbHelper.AddInParameter(dbCommand, "@ExchangeCode", System.Data.DbType.String, item.ExchangeCode);
            _dbHelper.AddInParameter(dbCommand, "@AccountCode", System.Data.DbType.String, item.AccountCode);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioCode", System.Data.DbType.String, item.PortfolioCode);
            _dbHelper.AddInParameter(dbCommand, "@StockHolderId", System.Data.DbType.String, item.StockHolderId);
            _dbHelper.AddInParameter(dbCommand, "@ReportSeat", System.Data.DbType.String, item.ReportSeat);
            _dbHelper.AddInParameter(dbCommand, "@DealDate", System.Data.DbType.Int32, item.DealDate);
            _dbHelper.AddInParameter(dbCommand, "@DealTime", System.Data.DbType.Int32, item.DealTime);
            _dbHelper.AddInParameter(dbCommand, "@EntrustDirection", System.Data.DbType.Int32, item.EntrustDirection);
            _dbHelper.AddInParameter(dbCommand, "@EntrustAmount", System.Data.DbType.Int32, item.EntrustAmount);
            _dbHelper.AddInParameter(dbCommand, "@EntrustState", System.Data.DbType.Int32, item.EntrustState);
            _dbHelper.AddInParameter(dbCommand, "@DealAmount", System.Data.DbType.Int32, item.DealAmount);
            _dbHelper.AddInParameter(dbCommand, "@DealPrice", System.Data.DbType.Decimal, item.DealPrice);
            _dbHelper.AddInParameter(dbCommand, "@DealBalance", System.Data.DbType.Decimal, item.DealBalance);
            _dbHelper.AddInParameter(dbCommand, "@DealFee", System.Data.DbType.Decimal, item.DealFee);
            _dbHelper.AddInParameter(dbCommand, "@TotalDealAmount", System.Data.DbType.Int32, item.TotalDealAmount);
            _dbHelper.AddInParameter(dbCommand, "@TotalDealBalance", System.Data.DbType.Decimal, item.TotalDealBalance);
            _dbHelper.AddInParameter(dbCommand, "@CancelAmount", System.Data.DbType.Int32, item.CancelAmount);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Create(List<ArchiveDealSecurity> items)
        { 
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(dbCommand);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int ret = -1;
            try
            {
                foreach (var item in items)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.CommandText = SP_Create;

                    _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, item.ArchiveId);
                    _dbHelper.AddInParameter(dbCommand, "@RequestId", System.Data.DbType.Int32, item.RequestId);
                    _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, item.SubmitId);
                    _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
                    _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);
                    _dbHelper.AddInParameter(dbCommand, "@DealNo", System.Data.DbType.String, item.DealNo);
                    _dbHelper.AddInParameter(dbCommand, "@EntrustNo", System.Data.DbType.Int32, item.EntrustNo);
                    _dbHelper.AddInParameter(dbCommand, "@BatchNo", System.Data.DbType.Int32, item.BatchNo);
                    _dbHelper.AddInParameter(dbCommand, "@ExchangeCode", System.Data.DbType.String, item.ExchangeCode);
                    _dbHelper.AddInParameter(dbCommand, "@AccountCode", System.Data.DbType.String, item.AccountCode);
                    _dbHelper.AddInParameter(dbCommand, "@PortfolioCode", System.Data.DbType.String, item.PortfolioCode);
                    _dbHelper.AddInParameter(dbCommand, "@StockHolderId", System.Data.DbType.String, item.StockHolderId);
                    _dbHelper.AddInParameter(dbCommand, "@ReportSeat", System.Data.DbType.String, item.ReportSeat);
                    _dbHelper.AddInParameter(dbCommand, "@DealDate", System.Data.DbType.Int32, item.DealDate);
                    _dbHelper.AddInParameter(dbCommand, "@DealTime", System.Data.DbType.Int32, item.DealTime);
                    _dbHelper.AddInParameter(dbCommand, "@EntrustDirection", System.Data.DbType.Int32, item.EntrustDirection);
                    _dbHelper.AddInParameter(dbCommand, "@EntrustAmount", System.Data.DbType.Int32, item.EntrustAmount);
                    _dbHelper.AddInParameter(dbCommand, "@EntrustState", System.Data.DbType.Int32, item.EntrustState);
                    _dbHelper.AddInParameter(dbCommand, "@DealAmount", System.Data.DbType.Int32, item.DealAmount);
                    _dbHelper.AddInParameter(dbCommand, "@DealPrice", System.Data.DbType.Decimal, item.DealPrice);
                    _dbHelper.AddInParameter(dbCommand, "@DealBalance", System.Data.DbType.Decimal, item.DealBalance);
                    _dbHelper.AddInParameter(dbCommand, "@DealFee", System.Data.DbType.Decimal, item.DealFee);
                    _dbHelper.AddInParameter(dbCommand, "@TotalDealAmount", System.Data.DbType.Int32, item.TotalDealAmount);
                    _dbHelper.AddInParameter(dbCommand, "@TotalDealBalance", System.Data.DbType.Decimal, item.TotalDealBalance);
                    _dbHelper.AddInParameter(dbCommand, "@CancelAmount", System.Data.DbType.Int32, item.CancelAmount);
                    _dbHelper.AddInParameter(dbCommand, "@ArchiveDate", System.Data.DbType.DateTime, item.ArchiveDate);

                    ret = dbCommand.ExecuteNonQuery();
                    if (ret < 0)
                    {
                        string msg = string.Format("Fail to archive - ArchiveId: [{0}], ArchiveDate: [{1}], SecuCode: [{2}]", item.ArchiveId, item.ArchiveDate, item.SecuCode);
                        logger.Error(msg);
                    }
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                //TODO: add log
                logger.Error(ex);
                ret = -1;
                throw;
            }
            finally
            {
                _dbHelper.Close(dbCommand);
                transaction.Dispose();
            }

            return ret;
        }

        public int Delete(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public List<ArchiveDealSecurity> Get(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);

            List<ArchiveDealSecurity> items = new List<ArchiveDealSecurity>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ArchiveDealSecurity item = new ArchiveDealSecurity();
                    item.ArchiveId = (int)reader["ArchiveId"];
                    item.RequestId = (int)reader["RequestId"];
                    item.SubmitId = (int)reader["SubmitId"];
                    item.CommandId = (int)reader["CommandId"];
                    item.SecuCode = (string)reader["SecuCode"];
                    item.DealNo = (string)reader["DealNo"];
                    item.BatchNo = (int)reader["BatchNo"];
                    item.EntrustNo = (int)reader["EntrustNo"];
                    item.ExchangeCode = (string)reader["ExchangeCode"];
                    item.AccountCode = (string)reader["AccountCode"];
                    item.PortfolioCode = (string)reader["PortfolioCode"];
                    item.StockHolderId = (string)reader["StockHolderId"];
                    item.ReportSeat = (string)reader["ReportSeat"];
                    item.DealDate = (int)reader["DealDate"];
                    item.DealTime = (int)reader["DealTime"];
                    item.EntrustDirection = (EntrustDirection)reader["EntrustDirection"];
                    item.EntrustAmount = (int)reader["EntrustAmount"];
                    item.EntrustState = (EntrustStatus)reader["EntrustState"];
                    item.DealAmount = (int)reader["DealAmount"];
                    item.DealPrice = (double)(decimal)reader["DealPrice"];
                    item.DealBalance = (double)(decimal)reader["DealBalance"];
                    item.DealFee = (double)(decimal)reader["DealFee"];
                    item.TotalDealAmount = (int)reader["TotalDealAmount"];
                    item.TotalDealBalance = (double)(decimal)reader["TotalDealBalance"];
                    item.CancelAmount = (int)reader["CancelAmount"];

                    if (reader["ArchiveDate"] != null && reader["ArchiveDate"] != DBNull.Value)
                    {
                        item.ArchiveDate = (DateTime)reader["ArchiveDate"];
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
