using Model.Archive;
using Model.EnumType;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Archive.TradeInstance
{
    public class ArchiveTradeInstanceDAO: BaseDAO
    {
        private const string SP_Create = "procArchiveTradeInstanceInsert";
        private const string SP_DeleteByArchiveId = "procArchiveTradeInstanceDeleteByArchiveId";
        private const string SP_DeleteByInstanceId = "procArchiveTradeInstanceDeleteByInstanceId";
        private const string SP_Select = "procArchiveTradeInstanceSelect";

        public ArchiveTradeInstanceDAO()
            : base()
        { 
        }

        public ArchiveTradeInstanceDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        }

        public int Create(ArchiveTradeInstance item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, item.InstanceId);
            _dbHelper.AddInParameter(dbCommand, "@InstanceCode", System.Data.DbType.String, item.InstanceCode);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioId", System.Data.DbType.Int32, item.PortfolioId);
            _dbHelper.AddInParameter(dbCommand, "@MonitorUnitId", System.Data.DbType.Int32, item.MonitorUnitId);
            _dbHelper.AddInParameter(dbCommand, "@StockDirection", System.Data.DbType.Int32, (int)item.StockDirection);
            _dbHelper.AddInParameter(dbCommand, "@FuturesContract", System.Data.DbType.String, item.FuturesContract);
            _dbHelper.AddInParameter(dbCommand, "@FuturesDirection", System.Data.DbType.Int32, (int)item.FuturesDirection);
            _dbHelper.AddInParameter(dbCommand, "@OperationCopies", System.Data.DbType.Int32, item.OperationCopies);
            _dbHelper.AddInParameter(dbCommand, "@StockPriceType", System.Data.DbType.Int32, (int)item.StockPriceType);
            _dbHelper.AddInParameter(dbCommand, "@FuturesPriceType", System.Data.DbType.Int32, (int)item.FuturesPriceType);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)item.Status);
            _dbHelper.AddInParameter(dbCommand, "@Owner", System.Data.DbType.Int32, (int)item.Owner);
            _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, item.CreatedDate);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, item.ModifiedDate);

            DateTime archiveDate = DateTime.Now;
            if (item.ArchiveDate != null && (item.ArchiveDate > DateTime.MinValue && item.ArchiveDate < DateTime.MaxValue))
            {
                archiveDate = item.ArchiveDate;
            }
            _dbHelper.AddInParameter(dbCommand, "@ArchiveDate", System.Data.DbType.DateTime, archiveDate);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int archiveId = -1;
            if (ret > 0)
            {
                archiveId = (int)dbCommand.Parameters["@return"].Value;
            }

            return archiveId;
        }

        public int DeleteByArchiveId(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_DeleteByArchiveId);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int DeleteByInstanceId(int instanceId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_DeleteByInstanceId);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, instanceId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public ArchiveTradeInstance Get(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);

            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

            var item = new ArchiveTradeInstance();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    item = GetTradeInstance(reader);

                    break;
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand);

            return item;
        }

        private ArchiveTradeInstance GetTradeInstance(DbDataReader reader)
        {
            var item = new ArchiveTradeInstance();
            item.ArchiveId = (int)reader["ArchiveId"];
            item.InstanceId = (int)reader["InstanceId"];
            item.InstanceCode = (string)reader["InstanceCode"];
            item.MonitorUnitId = (int)reader["MonitorUnitId"];
            item.StockDirection = (EntrustDirection)(int)reader["StockDirection"];
            item.FuturesContract = (string)reader["FuturesContract"];
            item.FuturesDirection = (EntrustDirection)(int)reader["FuturesDirection"];
            item.OperationCopies = (int)reader["OperationCopies"];
            item.StockPriceType = (StockPriceType)reader["StockPriceType"];
            item.FuturesPriceType = (FuturesPriceType)reader["FuturesPriceType"];
            item.Status = (TradeInstanceStatus)reader["Status"];
            item.Owner = (int)reader["Owner"];
            item.MonitorUnitName = (string)reader["MonitorUnitName"];
            item.TemplateId = (int)reader["TemplateId"];
            item.TemplateName = (string)reader["TemplateName"];
            item.PortfolioId = (int)reader["PortfolioId"];
            item.PortfolioCode = (string)reader["PortfolioCode"];
            item.PortfolioName = (string)reader["PortfolioName"];
            item.AccountCode = (string)reader["AccountCode"];
            item.AccountName = (string)reader["AccountName"];
            item.AssetNo = (string)reader["AssetNo"];
            item.AssetName = (string)reader["AssetName"];

            if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
            {
                item.CreatedDate = (DateTime)reader["CreatedDate"];
            }

            if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
            {
                item.ModifiedDate = (DateTime)reader["ModifiedDate"];
            }

            if (reader["ArchiveDate"] != null && reader["ArchiveDate"] != DBNull.Value)
            {
                item.ArchiveDate = (DateTime)reader["ArchiveDate"];
            }

            return item;
        }
    }
}
