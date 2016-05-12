using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class MonitorUnitDAO: BaseDAO
    {
        private const string SP_Create = "procMonitorUnitInsert";
        private const string SP_Modify = "procMonitorUnitUpdate";
        private const string SP_Delete = "procMonitorUnitDelete";
        private const string SP_Get = "procMonitorUnitSelect";

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
            _dbHelper.AddInParameter(dbCommand, "@AccountType", System.Data.DbType.Int32, monitorUnit.AccountType);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioId", System.Data.DbType.Int32, monitorUnit.PortfolioId);
            _dbHelper.AddInParameter(dbCommand, "@BearContract", System.Data.DbType.String, monitorUnit.BearContract);
            _dbHelper.AddInParameter(dbCommand, "@StockTemplateId", System.Data.DbType.Int32, monitorUnit.StockTemplateId);
            _dbHelper.AddInParameter(dbCommand, "@Owner", System.Data.DbType.String, monitorUnit.Owner);
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
            _dbHelper.AddInParameter(dbCommand, "@AccountType", System.Data.DbType.Int32, monitorUnit.AccountType);
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

        public List<MonitorUnit> Get(int monitorUnitId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);
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
                    MonitorUnit item = new MonitorUnit();
                    item.MonitorUnitId = (int)reader["MonitorUnitId"];
                    item.MonitorUnitName = (string)reader["MonitorUnitName"];
                    item.AccountType = (int)reader["AccountType"];
                    item.PortfolioId = (int)reader["PortfolioId"];
                    item.BearContract = (string)reader["BearContract"];
                    item.StockTemplateId = (int)reader["StockTemplateId"];
                    item.Owner = (string)reader["Owner"];
                    //item.CreatedDate = (DateTime)reader["CreatedDate"];
                    //if (reader["ModifiedDate"] != null)
                    //{
                    //    item.ModifiedDate = (DateTime)reader["ModifiedDate"];
                    //}
                    monitorUnits.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return monitorUnits;
        }
    }
}
