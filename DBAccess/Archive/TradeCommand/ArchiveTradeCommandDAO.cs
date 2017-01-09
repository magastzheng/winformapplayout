using Model.Archive;
using Model.EnumType;
using System;
using System.Data.Common;

namespace DBAccess.Archive.TradeCommand
{
    public class ArchiveTradeCommandDAO : BaseDAO
    {
        private const string SP_Create = "procArchiveTradeCommandInsert";
        private const string SP_Delete = "procArchiveTradeCommandDelete";
        private const string SP_Select = "procArchiveTradeCommandSelect";

        public ArchiveTradeCommandDAO()
            : base()
        { 
        }

        public ArchiveTradeCommandDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(ArchiveTradeCommand item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, item.InstanceId);
            _dbHelper.AddInParameter(dbCommand, "@CommandNum", System.Data.DbType.Int32, item.CommandNum);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedTimes", System.Data.DbType.Int32, item.ModifiedTimes);
            _dbHelper.AddInParameter(dbCommand, "@CommandType", System.Data.DbType.Int32, (int)item.ECommandType);
            _dbHelper.AddInParameter(dbCommand, "@ExecuteType", System.Data.DbType.Int32, (int)item.EExecuteType);
            _dbHelper.AddInParameter(dbCommand, "@StockDirection", System.Data.DbType.Int32, (int)item.EStockDirection);
            _dbHelper.AddInParameter(dbCommand, "@FuturesDirection", System.Data.DbType.Int32, (int)item.EFuturesDirection);
            _dbHelper.AddInParameter(dbCommand, "@CommandStatus", System.Data.DbType.Int32, (int)item.ECommandStatus);
            _dbHelper.AddInParameter(dbCommand, "@DispatchStatus", System.Data.DbType.Int32, (int)item.DispatchStatus);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)item.EEntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@DealStatus", System.Data.DbType.Int32, (int)item.EDealStatus);
            _dbHelper.AddInParameter(dbCommand, "@SubmitPerson", System.Data.DbType.Int32, item.SubmitPerson);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedPerson", System.Data.DbType.Int32, item.ModifiedPerson);
            _dbHelper.AddInParameter(dbCommand, "@CancelPerson", System.Data.DbType.Int32, item.CancelPerson);
            _dbHelper.AddInParameter(dbCommand, "@ApprovalPerson", System.Data.DbType.Int32, item.ApprovalPerson);
            _dbHelper.AddInParameter(dbCommand, "@DispatchPerson", System.Data.DbType.Int32, item.DispatchPerson);
            _dbHelper.AddInParameter(dbCommand, "@ExecutePerson", System.Data.DbType.Int32, item.ExecutePerson);
            _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, item.CreatedDate);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, item.ModifiedDate);
            
            //if(item.ArchiveDate != null && 
            _dbHelper.AddInParameter(dbCommand, "@ArchiveDate", System.Data.DbType.DateTime, DateTime.Now);
            _dbHelper.AddInParameter(dbCommand, "@StartDate", System.Data.DbType.DateTime, item.DStartDate);
            _dbHelper.AddInParameter(dbCommand, "@EndDate", System.Data.DbType.DateTime, item.DEndDate);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedCause", System.Data.DbType.String, item.ModifiedCause);
            _dbHelper.AddInParameter(dbCommand, "@CancelCause", System.Data.DbType.String, item.CancelCause);
            _dbHelper.AddInParameter(dbCommand, "@ApprovalCause", System.Data.DbType.String, item.ApprovalCause);
            _dbHelper.AddInParameter(dbCommand, "@DispatchRejectCause", System.Data.DbType.String, item.DispatchRejectCause);
            _dbHelper.AddInParameter(dbCommand, "@Notes", System.Data.DbType.String, item.Notes);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int archiveId = -1;
            if (ret > 0)
            {
                archiveId = (int)dbCommand.Parameters["@return"].Value;
            }

            return archiveId;
        }

        public int Delete(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public ArchiveTradeCommand Get(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);

            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

            var item = new ArchiveTradeCommand();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    item = GetTradeCommand(reader);

                    break;
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return item;
        }

        private ArchiveTradeCommand GetTradeCommand(DbDataReader reader)
        {
            var item = new ArchiveTradeCommand();
            item.ArchiveId = (int)reader["ArchiveId"];
            item.CommandId = (int)reader["CommandId"];
            item.InstanceId = (int)reader["InstanceId"];
            item.CommandNum = (int)reader["CommandNum"];
            item.ECommandStatus = (CommandStatus)(int)reader["CommandStatus"];
            item.ModifiedTimes = (int)reader["ModifiedTimes"];
            item.ECommandType = (CommandType)reader["CommandType"];
            item.EExecuteType = (ExecuteType)reader["ExecuteType"];
            item.EStockDirection = (EntrustDirection)(int)reader["StockDirection"];
            item.EFuturesDirection = (EntrustDirection)(int)reader["FuturesDirection"];
            item.EEntrustStatus = (EntrustStatus)(int)reader["EntrustStatus"];
            item.EDealStatus = (DealStatus)(int)reader["DealStatus"];
            item.SubmitPerson = (int)reader["SubmitPerson"];
            item.MonitorUnitId = (int)reader["MonitorUnitId"];
            item.InstanceCode = (string)reader["InstanceCode"];
            item.MonitorUnitName = (string)reader["MonitorUnitName"];
            item.TemplateId = (int)reader["StockTemplateId"];
            item.BearContract = (string)reader["BearContract"];
            item.PortfolioId = (int)reader["PortfolioId"];
            item.PortfolioCode = (string)reader["PortfolioCode"];
            item.PortfolioName = (string)reader["PortfolioName"];
            item.AccountCode = (string)reader["AccountCode"];
            item.AccountName = (string)reader["AccountName"];
            item.TemplateName = (string)reader["TemplateName"];
            item.Notes = (string)reader["Notes"];

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

            if (reader["StartDate"] != null && reader["StartDate"] != DBNull.Value)
            {
                item.DStartDate = (DateTime)reader["StartDate"];
            }

            if (reader["EndDate"] != null && reader["EndDate"] != DBNull.Value)
            {
                item.DEndDate = (DateTime)reader["EndDate"];
            }

            if (reader["CancelDate"] != null && reader["CancelDate"] != DBNull.Value)
            {
                item.CancelDate = (DateTime)reader["CancelDate"];
            }

            if (reader["ModifiedCause"] != null && reader["ModifiedCause"] != DBNull.Value)
            {
                item.ModifiedCause = (string)reader["ModifiedCause"];
            }

            if (reader["CancelCause"] != null && reader["CancelCause"] != DBNull.Value)
            {
                item.CancelCause = (string)reader["CancelCause"];
            }

            return item;
        }
    }
}
