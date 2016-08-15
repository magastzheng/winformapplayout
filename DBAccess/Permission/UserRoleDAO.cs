using log4net;
using Model.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Permission
{
    public class UserRoleDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Create = "procUserRoleInsert";
        //private const string SP_Modify = "procUserRoleUpdate";
        private const string SP_Delete = "procUserRoleDelete";
        private const string SP_Select = "procUserRoleSelect";

        public UserRoleDAO()
            : base()
        { 
        }

        public UserRoleDAO(DbHelper dbHelper)
            : base(dbHelper)
        {
        }

        public int Create(UserRole userRole)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@UserId", System.Data.DbType.Int32, userRole.UserId);
            _dbHelper.AddInParameter(dbCommand, "@RoleId", System.Data.DbType.Int32, userRole.RoleId);
            
            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int id = -1;
            if (ret > 0)
            {
                id = (int)dbCommand.Parameters["@return"].Value;
            }

            return id;
        }

        public int Delete(int userId, int roleId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@UserId", System.Data.DbType.Int32, userId);
            _dbHelper.AddInParameter(dbCommand, "@RoleId", System.Data.DbType.Int32, roleId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public UserRole Get(int userId, int roleId)
        {
            var items = GetInternal(userId, roleId);
            UserRole item = null;
            if (items.Count > 0)
            {
                item = items[0];
            }
            else
            {
                item = new UserRole();
            }
           
            return item;
        }

        public List<UserRole> GetAll()
        {
            return GetInternal(-1, -1);
        }

        #region private method

        public List<UserRole> GetInternal(int userId, int roleId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);
            if (userId > 0)
            {
                _dbHelper.AddInParameter(dbCommand, "@UserId", System.Data.DbType.Int32, userId);
            }

            if (roleId > 0)
            {
                _dbHelper.AddInParameter(dbCommand, "@RoleId", System.Data.DbType.Int32, roleId);
            }

            List<UserRole> items = new List<UserRole>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    UserRole item = new UserRole();
                    item.Id = (int)reader["Id"];
                    item.UserId = (int)reader["UserId"];
                    item.RoleId = (int)reader["RoleId"];

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
