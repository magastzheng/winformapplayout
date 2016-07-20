
using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;

namespace DBAccess
{
    public class TradingCommandDAO: BaseDAO
    {
        private const string SP_Create = "procTradingCommandInsert";
        private const string SP_Modify = "procTradingCommandUpdateStatus";
        private const string SP_ModifyTargetNum = "procTradingCommandUpdateTargetNum";
        private const string SP_ModifyTargetNumBySumbitId = "procTradingCommandUpdateTargetNumBySubmitId";
        private const string SP_Delete = "procTradingCommandDelete";
        //private const string SP_Get = "procTradingCommandSelect";
        private const string SP_Get = "procTradingCommandSelectCombine";

        public TradingCommandDAO()
            : base()
        { 
            
        }

        public TradingCommandDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(TradingCommandItem cmdItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, cmdItem.InstanceId);
            _dbHelper.AddInParameter(dbCommand, "@CommandNum", System.Data.DbType.Int32, cmdItem.CommandNum);
            _dbHelper.AddInParameter(dbCommand, "@CommandType", System.Data.DbType.Int32, cmdItem.ECommandType);
            _dbHelper.AddInParameter(dbCommand, "@ExecuteType", System.Data.DbType.Int32, cmdItem.EExecuteType);
            _dbHelper.AddInParameter(dbCommand, "@StockDirection", System.Data.DbType.String, (int)cmdItem.EStockDirection);
            _dbHelper.AddInParameter(dbCommand, "@FuturesDirection", System.Data.DbType.Int32, (int)cmdItem.EFuturesDirection);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)cmdItem.EEntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@DealStatus", System.Data.DbType.Int32, (int)cmdItem.EDealStatus);

            //command time
            DateTime now = DateTime.Now;
            //9:15
            DateTime startDate = new DateTime(now.Year, now.Month, now.Day, 9, 15, 0);
            //15:15
            DateTime endDate = new DateTime(now.Year, now.Month, now.Day, 15, 15, 0);
            _dbHelper.AddInParameter(dbCommand, "@StartDate", System.Data.DbType.DateTime, startDate);
            _dbHelper.AddInParameter(dbCommand, "@EndDate", System.Data.DbType.DateTime, endDate);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int commandId = -1;
            if (ret > 0)
            {
                commandId = (int)dbCommand.Parameters["@return"].Value;
            }

            return commandId;
        }

        public int Update(TradingCommandItem cmdItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Modify);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, cmdItem.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)cmdItem.EEntrustStatus);
            _dbHelper.AddInParameter(dbCommand, "@DealStatus", System.Data.DbType.Int32, (int)cmdItem.EDealStatus);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateTargetNum(int commandId, int targetNum)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyTargetNum);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);
            _dbHelper.AddInParameter(dbCommand, "@TargetNum", System.Data.DbType.Int32, targetNum);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);
            
            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateTargetNumBySubmitId(int submitId, int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyTargetNumBySumbitId);
            _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public TradingCommandItem Get(int commandId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);

            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, commandId);
            TradingCommandItem item = new TradingCommandItem();

            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    item.CommandId = (int)reader["CommandId"];
                    item.InstanceId = (int)reader["InstanceId"];
                    item.CommandNum = (int)reader["CommandNum"];
                    item.TargetNum = (int)reader["TargetNum"];
                    item.ModifiedTimes = (int)reader["ModifiedTimes"];
                    item.ECommandType = (CommandType)reader["CommandType"];
                    item.EExecuteType = (ExecuteType)reader["ExecuteType"];
                    item.EStockDirection = (EntrustDirection)(int)reader["StockDirection"];
                    item.EFuturesDirection = (EntrustDirection)(int)reader["FuturesDirection"];
                    item.EEntrustStatus = (EntrustStatus)reader["EntrustStatus"];
                    item.EDealStatus = (DealStatus)reader["DealStatus"];
                    item.SubmitPerson = (string)reader["SubmitPerson"];
                    item.MonitorUnitId = (int)reader["MonitorUnitId"];
                    item.InstanceCode = (string)reader["InstanceCode"];
                    item.MonitorUnitName = (string)reader["MonitorUnitName"];
                    item.PortfolioId = (int)reader["PortfolioId"];
                    item.PortfolioCode = (string)reader["PortfolioCode"];
                    item.PortfolioName = (string)reader["PortfolioName"];
                    item.FundCode = (string)reader["AccountCode"];
                    item.FundName = (string)reader["AccountName"];

                    if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
                    {
                        item.CreatedDate = (DateTime)reader["CreatedDate"];
                    }

                    if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
                    {
                        item.ModifiedDate = (DateTime)reader["ModifiedDate"];
                    }

                    if (reader["StartDate"] != null && reader["StartDate"] != DBNull.Value)
                    {
                        item.DStartDate = (DateTime)reader["StartDate"];
                    }

                    if (reader["EndDate"] != null && reader["EndDate"] != DBNull.Value)
                    {
                        item.DEndDate = (DateTime)reader["EndDate"];
                    }
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return item;
        }

        public List<TradingCommandItem> GetAll()
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);
            
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, -1);

            List<TradingCommandItem> items = new List<TradingCommandItem>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TradingCommandItem item = new TradingCommandItem();
                    item.CommandId = (int)reader["CommandId"];
                    item.InstanceId = (int)reader["InstanceId"];
                    item.CommandNum = (int)reader["CommandNum"];
                    item.TargetNum = (int)reader["TargetNum"];
                    item.ModifiedTimes = (int)reader["ModifiedTimes"];
                    item.ECommandType = (CommandType)reader["CommandType"];
                    item.EExecuteType = (ExecuteType)reader["ExecuteType"];
                    item.EStockDirection = (EntrustDirection)(int)reader["StockDirection"];
                    item.EFuturesDirection = (EntrustDirection)(int)reader["FuturesDirection"];
                    item.EEntrustStatus = (EntrustStatus)reader["EntrustStatus"];
                    item.EDealStatus = (DealStatus)reader["DealStatus"];
                    item.SubmitPerson = (string)reader["SubmitPerson"];
                    item.MonitorUnitId = (int)reader["MonitorUnitId"];
                    item.InstanceCode = (string)reader["InstanceCode"];
                    item.MonitorUnitName = (string)reader["MonitorUnitName"];
                    item.PortfolioId = (int)reader["PortfolioId"];
                    item.PortfolioCode = (string)reader["PortfolioCode"];
                    item.PortfolioName = (string)reader["PortfolioName"];
                    item.FundCode = (string)reader["AccountCode"];
                    item.FundName = (string)reader["AccountName"];

                    if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
                    {
                        item.CreatedDate = (DateTime)reader["CreatedDate"];
                    }

                    if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
                    {
                        item.ModifiedDate = (DateTime)reader["ModifiedDate"];
                    }

                    if (reader["StartDate"] != null && reader["StartDate"] != DBNull.Value)
                    {
                        item.DStartDate = (DateTime)reader["StartDate"];
                    }

                    if (reader["EndDate"] != null && reader["EndDate"] != DBNull.Value)
                    {
                        item.DEndDate = (DateTime)reader["EndDate"];
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
