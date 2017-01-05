using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DBAccess.TradeInstance
{
    public class  TradeInstanceDAO: BaseDAO
    {
        private const string SP_Create = "procTradeInstanceInsert";
        private const string SP_Modify = "procTradeInstanceUpdate";
        private const string SP_Delete = "procTradeInstanceDelete";
        private const string SP_GetCombine = "procTradeInstanceSelectCombine";
        private const string SP_GetCombineByCode = "procTradeInstanceSelectCombineByCode";
        private const string SP_Exist = "procTradeInstanceExist";

        public TradeInstanceDAO()
            : base()
        { 
            
        }

        public TradeInstanceDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(Model.UI.TradeInstance securityItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@InstanceCode", System.Data.DbType.String, securityItem.InstanceCode);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioId", System.Data.DbType.Int32, securityItem.PortfolioId);
            _dbHelper.AddInParameter(dbCommand, "@MonitorUnitId", System.Data.DbType.Int32, securityItem.MonitorUnitId);
            _dbHelper.AddInParameter(dbCommand, "@StockDirection", System.Data.DbType.Int32, (int)securityItem.StockDirection);
            _dbHelper.AddInParameter(dbCommand, "@FuturesContract", System.Data.DbType.String, securityItem.FuturesContract);
            _dbHelper.AddInParameter(dbCommand, "@FuturesDirection", System.Data.DbType.Int32, (int)securityItem.FuturesDirection);
            _dbHelper.AddInParameter(dbCommand, "@OperationCopies", System.Data.DbType.Int32, securityItem.OperationCopies);
            _dbHelper.AddInParameter(dbCommand, "@StockPriceType", System.Data.DbType.Int32, (int)securityItem.StockPriceType);
            _dbHelper.AddInParameter(dbCommand, "@FuturesPriceType", System.Data.DbType.Int32, (int)securityItem.FuturesPriceType);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)TradeInstanceStatus.Active);
            _dbHelper.AddInParameter(dbCommand, "@Owner", System.Data.DbType.Int32, (int)securityItem.Owner);
            _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, DateTime.Now);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            
            int instanceId = -1;
            if(ret > 0)
            {
                instanceId = (int)dbCommand.Parameters["@return"].Value;
            }

            return instanceId;
        }

        public int Update(Model.UI.TradeInstance securityItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Modify);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, securityItem.InstanceId);
            _dbHelper.AddInParameter(dbCommand, "@InstanceCode", System.Data.DbType.String, securityItem.InstanceCode);
            _dbHelper.AddInParameter(dbCommand, "@MonitorUnitId", System.Data.DbType.Int32, securityItem.MonitorUnitId);
            _dbHelper.AddInParameter(dbCommand, "@StockDirection", System.Data.DbType.Int32, (int)securityItem.StockDirection);
            _dbHelper.AddInParameter(dbCommand, "@FuturesContract", System.Data.DbType.String, securityItem.FuturesContract);
            _dbHelper.AddInParameter(dbCommand, "@FuturesDirection", System.Data.DbType.Int32, (int)securityItem.FuturesDirection);
            _dbHelper.AddInParameter(dbCommand, "@OperationCopies", System.Data.DbType.Int32, securityItem.OperationCopies);
            _dbHelper.AddInParameter(dbCommand, "@StockPriceType", System.Data.DbType.Int32, (int)securityItem.StockPriceType);
            _dbHelper.AddInParameter(dbCommand, "@FuturesPriceType", System.Data.DbType.Int32, (int)securityItem.FuturesPriceType);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)securityItem.Status);
            _dbHelper.AddInParameter(dbCommand, "@Owner", System.Data.DbType.Int32, (int)securityItem.Owner);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            return ret;
        }

        public int Delete(int instanceId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, instanceId);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            
            return ret;
        }

        public Model.UI.TradeInstance GetCombine(int instanceId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCombine);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, instanceId);

            Model.UI.TradeInstance item = new Model.UI.TradeInstance();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows && reader.Read())
            {
                item = ParseData(reader);
            }

            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return item;
        }

        public List<Model.UI.TradeInstance> GetCombineAll()
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCombine);

            List<Model.UI.TradeInstance> items = new List<Model.UI.TradeInstance>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Model.UI.TradeInstance item = ParseData(reader);
                    
                    items.Add(item);
                }
            }

            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return items;
        }

        public Model.UI.TradeInstance GetCombineByCode(string instanceCode)
        {
            
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCombineByCode);
            _dbHelper.AddInParameter(dbCommand, "@InstanceCode", System.Data.DbType.String, instanceCode);

            Model.UI.TradeInstance item = new Model.UI.TradeInstance();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows && reader.Read())
            {
                item = ParseData(reader);
            }

            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return item;
        }

        public int Exist(string instanceCode)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Exist);
            _dbHelper.AddInParameter(dbCommand, "@InstanceCode", System.Data.DbType.String, instanceCode);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int existed = -1;
            if (ret > 0)
            {
                existed = (int)dbCommand.Parameters["@return"].Value;
            }

            return existed;
        }

        #region private methods

        public Model.UI.TradeInstance ParseData(DbDataReader reader)
        {
            Model.UI.TradeInstance item = new Model.UI.TradeInstance();

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

            return item;
        }

        #endregion
    }
}
