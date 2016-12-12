using log4net;
using Model.Database;
using Model.EnumType;
using System.Collections.Generic;

namespace DBAccess
{
    public class DealSecurityDAO: BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Create = "procDealSecurityInsert";
        private const string SP_DeleteByDealNo = "procDealSecurityDeleteByDealNo";
        private const string SP_SelectAll = "procDealSecuritySelectAll";

        public DealSecurityDAO()
            : base()
        { 
            
        }

        public DealSecurityDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(DealSecurity item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
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

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int DeleteByDealNo(string dealNo)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_DeleteByDealNo);
            _dbHelper.AddInParameter(dbCommand, "@DealNo", System.Data.DbType.String, dealNo);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public List<DealSecurity> GetAll()
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_SelectAll);

            List<DealSecurity> items = new List<DealSecurity>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DealSecurity item = new DealSecurity();
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

                    items.Add(item);
                }
            }

            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return items;
        }
    }
}
