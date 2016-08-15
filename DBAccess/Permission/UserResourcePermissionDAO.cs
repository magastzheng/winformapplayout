using log4net;
using Model.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Permission
{
    public class UserResourcePermissionDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Create = "procUserResourcePermissionInsert";
        private const string SP_Modify = "procUserResourcePermissionUpdate";
        private const string SP_Delete = "procUserResourcePermissionDelete";
        private const string SP_Select = "procUserResourcePermissionSelect";

        public UserResourcePermissionDAO()
            : base()
        { 
        }

        public UserResourcePermissionDAO(DbHelper dbHelper)
            : base(dbHelper)
        {
        }

        public int Create(UserResourcePermission permission)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@UserId", System.Data.DbType.Int32, permission.UserId);
            _dbHelper.AddInParameter(dbCommand, "@ResourceId", System.Data.DbType.Int32, permission.ResourceId);
            _dbHelper.AddInParameter(dbCommand, "@Permission", System.Data.DbType.Int32, permission.Permission);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int id = -1;
            if (ret > 0)
            {
                id = (int)dbCommand.Parameters["@return"].Value;
            }

            return id;
        }

        public int Update(UserResourcePermission permission)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@UserId", System.Data.DbType.Int32, permission.UserId);
            _dbHelper.AddInParameter(dbCommand, "@ResourceId", System.Data.DbType.Int32, permission.ResourceId);
            _dbHelper.AddInParameter(dbCommand, "@Permission", System.Data.DbType.Int32, permission.Permission);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(int userId, int resourceId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@UserId", System.Data.DbType.Int32, userId);
            _dbHelper.AddInParameter(dbCommand, "@ResourceId", System.Data.DbType.Int32, resourceId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public UserResourcePermission Get(int userId, int resourceId)
        {
            var items = GetInternal(userId, resourceId);

            UserResourcePermission item = null;
            if (items.Count > 0)
            {
                item = items[0];
            }
            else
            {
                item = new UserResourcePermission();
            }

            return item;
        }

        public List<UserResourcePermission> Get(int userId)
        {
            return GetInternal(userId, -1);
        }

        public List<UserResourcePermission> GetAll()
        {
            return GetInternal(-1, -1);
        }

        #region private method

        private List<UserResourcePermission> GetInternal(int userId, int resourceId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);
            if (userId > 0)
            {
                _dbHelper.AddInParameter(dbCommand, "@UserId", System.Data.DbType.Int32, userId);
            }

            if (resourceId > 0)
            {
                _dbHelper.AddInParameter(dbCommand, "@ResourceId", System.Data.DbType.Int32, resourceId);
            }

            List<UserResourcePermission> items = new List<UserResourcePermission>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    UserResourcePermission item = new UserResourcePermission();
                    item.Id = (int)reader["Id"];
                    item.UserId = (int)reader["UserId"];
                    item.ResourceId = (int)reader["ResourceId"];
                    item.Permission = (int)reader["Permission"];

                    items.Add(item);
                }
            }

            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return items;
        }

        #endregion
    }
}
