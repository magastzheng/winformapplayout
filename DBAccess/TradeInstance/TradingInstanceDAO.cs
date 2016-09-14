using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;

namespace DBAccess.TradeInstance
{
    public class TradingInstanceDAO: BaseDAO
    {
        private const string SP_Create = "procTradingInstanceInsert";
        private const string SP_Modify = "procTradingInstanceUpdate";
        private const string SP_Delete = "procTradingInstanceDelete";
        private const string SP_Get = "procTradingInstanceSelect";
        private const string SP_GetByCode = "procTradingInstanceSelectByCode";
        private const string SP_GetCombine = "procTradingInstanceSelectCombine";
        private const string SP_GetCombineByCode = "procTradingInstanceSelectCombineByCode";
        private const string SP_Exist = "procTradingInstanceExist";

        public TradingInstanceDAO()
            : base()
        { 
            
        }

        public TradingInstanceDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(TradingInstance securityItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@InstanceCode", System.Data.DbType.String, securityItem.InstanceCode);
            _dbHelper.AddInParameter(dbCommand, "@MonitorUnitId", System.Data.DbType.Int32, securityItem.MonitorUnitId);
            _dbHelper.AddInParameter(dbCommand, "@StockDirection", System.Data.DbType.Int32, (int)securityItem.StockDirection);
            _dbHelper.AddInParameter(dbCommand, "@FuturesContract", System.Data.DbType.String, securityItem.FuturesContract);
            _dbHelper.AddInParameter(dbCommand, "@FuturesDirection", System.Data.DbType.Int32, (int)securityItem.FuturesDirection);
            _dbHelper.AddInParameter(dbCommand, "@OperationCopies", System.Data.DbType.Int32, securityItem.OperationCopies);
            _dbHelper.AddInParameter(dbCommand, "@StockPriceType", System.Data.DbType.Int32, (int)securityItem.StockPriceType);
            _dbHelper.AddInParameter(dbCommand, "@FuturesPriceType", System.Data.DbType.Int32, (int)securityItem.FuturesPriceType);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)TradingInstanceStatus.Active);
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

        public int Update(TradingInstance securityItem)
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

        /// <summary>
        /// Get all the TradingInstance if the input is not greater than 0. 
        /// </summary>
        /// <param name="secuType"></param>
        /// <returns></returns>
        public List<TradingInstance> Get(int instanceId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);
            if (instanceId > 0)
            {
                _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, instanceId);
            }

            List<TradingInstance> items = new List<TradingInstance>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TradingInstance item = new TradingInstance();
                    item.InstanceId = (int)reader["InstanceId"];
                    item.InstanceCode = (string)reader["InstanceCode"];
                    item.MonitorUnitId = (int)reader["MonitorUnitId"];
                    item.StockDirection = (EntrustDirection)(int)reader["StockDirection"];
                    item.FuturesContract = (string)reader["FuturesContract"];
                    item.FuturesDirection = (EntrustDirection)(int)reader["FuturesDirection"];
                    item.OperationCopies = (int)reader["OperationCopies"];
                    item.StockPriceType = (StockPriceType)reader["StockPriceType"];
                    item.FuturesPriceType = (FuturesPriceType)reader["FuturesPriceType"];
                    item.Status = (TradingInstanceStatus)reader["Status"];
                    item.Owner = (int)reader["Owner"];

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

        public List<TradingInstance> GetAll()
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);

            List<TradingInstance> items = new List<TradingInstance>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TradingInstance item = new TradingInstance();
                    item.InstanceId = (int)reader["InstanceId"];
                    item.InstanceCode = (string)reader["InstanceCode"];
                    item.MonitorUnitId = (int)reader["MonitorUnitId"];
                    item.StockDirection = (EntrustDirection)(int)reader["StockDirection"];
                    item.FuturesContract = (string)reader["FuturesContract"];
                    item.FuturesDirection = (EntrustDirection)(int)reader["FuturesDirection"];
                    item.OperationCopies = (int)reader["OperationCopies"];
                    item.StockPriceType = (StockPriceType)reader["StockPriceType"];
                    item.FuturesPriceType = (FuturesPriceType)reader["FuturesPriceType"];
                    item.Status = (TradingInstanceStatus)reader["Status"];
                    item.Owner = (int)reader["Owner"];

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

        /// <summary>
        /// Get all the TradingInstance if the input is not greater than 0. 
        /// </summary>
        /// <param name="secuType"></param>
        /// <returns></returns>
        public TradingInstance Get(string instanceCode)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetByCode);
            _dbHelper.AddInParameter(dbCommand, "@InstanceCode", System.Data.DbType.String, instanceCode);

            TradingInstance item = new TradingInstance();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    item.InstanceId = (int)reader["InstanceId"];
                    item.InstanceCode = (string)reader["InstanceCode"];
                    item.MonitorUnitId = (int)reader["MonitorUnitId"];
                    item.StockDirection = (EntrustDirection)(int)reader["StockDirection"];
                    item.FuturesContract = (string)reader["FuturesContract"];
                    item.FuturesDirection = (EntrustDirection)(int)reader["FuturesDirection"];
                    item.OperationCopies = (int)reader["OperationCopies"];
                    item.StockPriceType = (StockPriceType)reader["StockPriceType"];
                    item.FuturesPriceType = (FuturesPriceType)reader["FuturesPriceType"];
                    item.Status = (TradingInstanceStatus)reader["Status"];
                    item.Owner = (int)reader["Owner"];

                    if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
                    {
                        item.CreatedDate = (DateTime)reader["CreatedDate"];
                    }

                    if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
                    {
                        item.ModifiedDate = (DateTime)reader["ModifiedDate"];
                    }
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return item;
        }

        public TradingInstance GetCombine(int instanceId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCombine);
            _dbHelper.AddInParameter(dbCommand, "@InstanceId", System.Data.DbType.Int32, instanceId);

            TradingInstance item = new TradingInstance();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    
                    item.InstanceId = (int)reader["InstanceId"];
                    item.InstanceCode = (string)reader["InstanceCode"];
                    item.MonitorUnitId = (int)reader["MonitorUnitId"];
                    item.StockDirection = (EntrustDirection)(int)reader["StockDirection"];
                    item.FuturesContract = (string)reader["FuturesContract"];
                    item.FuturesDirection = (EntrustDirection)(int)reader["FuturesDirection"];
                    item.OperationCopies = (int)reader["OperationCopies"];
                    item.StockPriceType = (StockPriceType)reader["StockPriceType"];
                    item.FuturesPriceType = (FuturesPriceType)reader["FuturesPriceType"];
                    item.Status = (TradingInstanceStatus)reader["Status"];
                    item.Owner = (int)reader["Owner"];
                    item.MonitorUnitName = (string)reader["MonitorUnitName"];
                    item.TemplateId = (int)reader["TemplateId"];
                    item.TemplateName = (string)reader["TemplateName"];
                    item.PortfolioId = (int)reader["PortfolioId"];
                    item.PortfolioCode = (string)reader["PortfolioCode"];
                    item.PortfolioName = (string)reader["PortfolioName"];

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

        public List<TradingInstance> GetCombineAll()
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCombine);

            List<TradingInstance> items = new List<TradingInstance>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TradingInstance item = new TradingInstance();
                    item.InstanceId = (int)reader["InstanceId"];
                    item.InstanceCode = (string)reader["InstanceCode"];
                    item.MonitorUnitId = (int)reader["MonitorUnitId"];
                    item.StockDirection = (EntrustDirection)(int)reader["StockDirection"];
                    item.FuturesContract = (string)reader["FuturesContract"];
                    item.FuturesDirection = (EntrustDirection)(int)reader["FuturesDirection"];
                    item.OperationCopies = (int)reader["OperationCopies"];
                    item.StockPriceType = (StockPriceType)reader["StockPriceType"];
                    item.FuturesPriceType = (FuturesPriceType)reader["FuturesPriceType"];
                    item.Status = (TradingInstanceStatus)reader["Status"];
                    item.Owner = (int)reader["Owner"];
                    item.MonitorUnitName = (string)reader["MonitorUnitName"];
                    item.TemplateId = (int)reader["TemplateId"];
                    item.TemplateName = (string)reader["TemplateName"];
                    item.PortfolioId = (int)reader["PortfolioId"];
                    item.PortfolioCode = (string)reader["PortfolioCode"];
                    item.PortfolioName = (string)reader["PortfolioName"];

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

        public TradingInstance GetCombineByCode(string instanceCode)
        {
            
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCombineByCode);
            _dbHelper.AddInParameter(dbCommand, "@InstanceCode", System.Data.DbType.String, instanceCode);

            TradingInstance item = new TradingInstance();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    item.InstanceId = (int)reader["InstanceId"];
                    item.InstanceCode = (string)reader["InstanceCode"];
                    item.MonitorUnitId = (int)reader["MonitorUnitId"];
                    item.StockDirection = (EntrustDirection)(int)reader["StockDirection"];
                    item.FuturesContract = (string)reader["FuturesContract"];
                    item.FuturesDirection = (EntrustDirection)(int)reader["FuturesDirection"];
                    item.OperationCopies = (int)reader["OperationCopies"];
                    item.StockPriceType = (StockPriceType)reader["StockPriceType"];
                    item.FuturesPriceType = (FuturesPriceType)reader["FuturesPriceType"];
                    item.Status = (TradingInstanceStatus)reader["Status"];
                    item.Owner = (int)reader["Owner"];
                    item.MonitorUnitName = (string)reader["MonitorUnitName"];
                    item.TemplateId = (int)reader["TemplateId"];
                    item.TemplateName = (string)reader["TemplateName"];
                    item.PortfolioId = (int)reader["PortfolioId"];
                    item.PortfolioCode = (string)reader["PortfolioCode"];
                    item.PortfolioName = (string)reader["PortfolioName"];

                    if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
                    {
                        item.CreatedDate = (DateTime)reader["CreatedDate"];
                    }

                    if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
                    {
                        item.ModifiedDate = (DateTime)reader["ModifiedDate"];
                    }
                }
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
    }
}
