using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DBAccess
{
    public class MonitorUnitDAO: BaseDAO
    {
        private const string SP_Create = "procMonitorUnitInsert";
        private const string SP_Modify = "procMonitorUnitUpdate";
        private const string SP_Active = "procMonitorUnitActive";
        private const string SP_Delete = "procMonitorUnitDelete";
        private const string SP_GetCombine = "procMonitorUnitSelectCombine";

        public MonitorUnitDAO()
            : base()
        { 
            
        }

        public MonitorUnitDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(MonitorUnit monitorUnit)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@MonitorUnitName", System.Data.DbType.String, monitorUnit.MonitorUnitName);
            _dbHelper.AddInParameter(dbCommand, "@AccountType", System.Data.DbType.Int32, monitorUnit.EAccountType);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioId", System.Data.DbType.Int32, monitorUnit.PortfolioId);
            _dbHelper.AddInParameter(dbCommand, "@BearContract", System.Data.DbType.String, monitorUnit.BearContract);
            _dbHelper.AddInParameter(dbCommand, "@StockTemplateId", System.Data.DbType.Int32, monitorUnit.StockTemplateId);
            _dbHelper.AddInParameter(dbCommand, "@Owner", System.Data.DbType.Int32, monitorUnit.Owner);
            _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, DateTime.Now);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            int id = -1;
            if (ret > 0)
            {
                id = (int)dbCommand.Parameters["@return"].Value;
            }

            return id;
        }

        public int Update(MonitorUnit monitorUnit)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Modify);
            _dbHelper.AddInParameter(dbCommand, "@MonitorUnitId", System.Data.DbType.Int32, monitorUnit.MonitorUnitId);
            _dbHelper.AddInParameter(dbCommand, "@MonitorUnitName", System.Data.DbType.String, monitorUnit.MonitorUnitName);
            _dbHelper.AddInParameter(dbCommand, "@AccountType", System.Data.DbType.Int32, (int)monitorUnit.EAccountType);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioId", System.Data.DbType.Int32, monitorUnit.PortfolioId);
            _dbHelper.AddInParameter(dbCommand, "@BearContract", System.Data.DbType.String, monitorUnit.BearContract);
            _dbHelper.AddInParameter(dbCommand, "@StockTemplateId", System.Data.DbType.Int32, monitorUnit.StockTemplateId);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            int id = -1;
            if (ret > 0)
            {
                id = (int)dbCommand.Parameters["@return"].Value;
            }

            return id;
        }

        public int Active(int monitorId, MonitorUnitStatus status)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Active);
            _dbHelper.AddInParameter(dbCommand, "@MonitorUnitId", System.Data.DbType.Int32, monitorId);
            _dbHelper.AddInParameter(dbCommand, "@Active", System.Data.DbType.Int32, (int)status);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
           
            return ret;
        }

        public int Delete(int monitorUnitId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@MonitorUnitId", System.Data.DbType.String, monitorUnitId);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            int id = -1;
            if (ret > 0)
            {
                id = (int)dbCommand.Parameters["@return"].Value;
            }

            return id;
        }

        public MonitorUnit Get(int monitorUnitId)
        {
            MonitorUnit item = new MonitorUnit();
            var items = GetCombineInternal(monitorUnitId);
            if (items != null && items.Count > 0)
            {
                item = items[0];
            }

            return item;
        }

        public List<MonitorUnit> GetAll()
        {
            return GetCombineInternal(-1);
        }

        public List<MonitorUnit> GetActive()
        {
            var items = GetAll();
            var activeItems = items.Where(p => p.Status == MonitorUnitStatus.Active).ToList();

            return activeItems;
        }

        #region private methods

        private List<MonitorUnit> GetCombineInternal(int monitorUnitId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetCombine);
            if (monitorUnitId > 0)
            {
                _dbHelper.AddInParameter(dbCommand, "@MonitorUnitId", System.Data.DbType.String, monitorUnitId);
            }

            List<MonitorUnit> monitorUnits = new List<MonitorUnit>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    MonitorUnit item = ParseData(reader);
                    monitorUnits.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return monitorUnits;
        }

        private MonitorUnit ParseData(DbDataReader reader)
        {
            MonitorUnit item = new MonitorUnit();

            item.MonitorUnitId = (int)reader["MonitorUnitId"];
            item.MonitorUnitName = (string)reader["MonitorUnitName"];
            item.EAccountType = (MonitorUnitAccountType)reader["AccountType"];
            item.PortfolioId = (int)reader["PortfolioId"];
            item.BearContract = (string)reader["BearContract"];
            item.StockTemplateId = (int)reader["StockTemplateId"];
            item.Owner = (int)reader["Owner"];
            item.Status = (MonitorUnitStatus)reader["Active"];
            if (item.Status == MonitorUnitStatus.Active)
            {
                item.Selection = true;
            }
            else
            {
                item.Selection = false;
            }

            if (reader["PortfolioName"] != null && reader["PortfolioName"] != DBNull.Value)
            {
                item.PortfolioName = (string)reader["PortfolioName"];
            }

            if (reader["TemplateName"] != null && reader["TemplateName"] != DBNull.Value)
            {
                item.StockTemplateName = (string)reader["TemplateName"];
            }

            return item;
        }

        #endregion
    }
}
