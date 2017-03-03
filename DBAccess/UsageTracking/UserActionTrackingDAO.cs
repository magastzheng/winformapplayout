using Model.Permission;
using Model.UsageTracking;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.UsageTracking
{
    public class UserActionTrackingDAO : BaseDAO
    {
        private const string SP_Create = "procUserActionTrackingInsert";
        private const string SP_SelectByUser = "procUserActionTrackingSelectByUser";
        private const string SP_SelectByUserPeriod = "procUserActionTrackingSelectByUserPeriod";
        private const string SP_SelectByResource = "procUserActionTrackingSelectByResource";

        public UserActionTrackingDAO()
            : base()
        { 
        
        }

        public UserActionTrackingDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(UserActionTracking item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@UserId", System.Data.DbType.Int32, item.UserId);
            _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, item.CreatedDate);
            _dbHelper.AddInParameter(dbCommand, "@Action", System.Data.DbType.Int32, (int)item.ActionType);
            _dbHelper.AddInParameter(dbCommand, "@ResourceType", System.Data.DbType.Int32, (int)item.ResourceType);
            _dbHelper.AddInParameter(dbCommand, "@ResourceId", System.Data.DbType.Int32, item.ResourceId);
            _dbHelper.AddInParameter(dbCommand, "@Num", System.Data.DbType.Int32, item.Num);
            _dbHelper.AddInParameter(dbCommand, "@ActionStatus", System.Data.DbType.Int32, (int)item.ActionStatus);
            _dbHelper.AddInParameter(dbCommand, "@Details", System.Data.DbType.String, item.Details);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int commandId = -1;
            if (ret > 0)
            {
                commandId = (int)dbCommand.Parameters["@return"].Value;
            }

            return commandId;
        }

        public List<UserActionTracking> GetByUser(int userId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_SelectByUser);
            _dbHelper.AddInParameter(dbCommand, "@UserId", System.Data.DbType.Int32, userId);

            List<UserActionTracking> items = new List<UserActionTracking>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    UserActionTracking item = ParseData(reader);
                    items.Add(item);
                }
            }

            reader.Close();
            _dbHelper.Close(dbCommand);

            return items;
        }

        public List<UserActionTracking> GetByUserPeriod(int userId, DateTime startDate, DateTime endDate)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_SelectByUserPeriod);
            _dbHelper.AddInParameter(dbCommand, "@UserId", System.Data.DbType.Int32, userId);
            _dbHelper.AddInParameter(dbCommand, "@StartDate", System.Data.DbType.DateTime, startDate);
            _dbHelper.AddInParameter(dbCommand, "@EndDate", System.Data.DbType.DateTime, endDate);

            List<UserActionTracking> items = new List<UserActionTracking>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    UserActionTracking item = ParseData(reader);

                    items.Add(item);
                }
            }

            reader.Close();
            _dbHelper.Close(dbCommand);

            return items;
        }

        public List<UserActionTracking> GetByResource(int resourceId, ResourceType resourceType)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_SelectByResource);
            _dbHelper.AddInParameter(dbCommand, "@ResourceId", System.Data.DbType.Int32, resourceId);
            _dbHelper.AddInParameter(dbCommand, "@ResourceType", System.Data.DbType.Int32, resourceType);

            List<UserActionTracking> items = new List<UserActionTracking>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    UserActionTracking item = ParseData(reader);

                    items.Add(item);
                }
            }

            reader.Close();
            _dbHelper.Close(dbCommand);

            return items;
        }

        private UserActionTracking ParseData(DbDataReader reader)
        {
            UserActionTracking item = new UserActionTracking();
            item.UserId = (int)reader["UserId"];
            item.CreatedDate = (DateTime)reader["CreatedDate"];
            item.ActionType = (ActionType)reader["Action"];
            item.ResourceType = (ResourceType)reader["ResourceType"];
            item.ResourceId = (int)reader["ResourceId"];
            item.Num = (int)reader["Num"];
            item.ActionStatus = (ActionStatus)reader["ActionStatus"];
            item.Details = (string)reader["Details"];

            return item;
        }
    }
}
