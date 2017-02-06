using log4net;
using Model.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Permission
{
    public class RoleFeaturePermissionDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Create = "procRoleFeaturePermissionInsert";
        private const string SP_Modify = "procRoleFeaturePermissionUpdate";
        private const string SP_Delete = "procRoleFeaturePermissionDelete";
        private const string SP_Select = "procRoleFeaturePermissionSelect";

        public RoleFeaturePermissionDAO()
            : base()
        { 
        }

        public RoleFeaturePermissionDAO(DbHelper dbHelper)
            : base(dbHelper)
        {
        }

        public int Create(RoleFeaturePermission permission)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@RoleId", System.Data.DbType.Int32, permission.RoleId);
            _dbHelper.AddInParameter(dbCommand, "@FeatureId", System.Data.DbType.Int32, permission.FeatureId);
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

        public int Update(RoleFeaturePermission permission)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@RoleId", System.Data.DbType.Int32, permission.RoleId);
            _dbHelper.AddInParameter(dbCommand, "@FeatureId", System.Data.DbType.Int32, permission.FeatureId);
            _dbHelper.AddInParameter(dbCommand, "@Permission", System.Data.DbType.Int32, permission.Permission);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(int roleId, int featureId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@RoleId", System.Data.DbType.Int32, roleId);
            _dbHelper.AddInParameter(dbCommand, "@FeatureId", System.Data.DbType.Int32, featureId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public RoleFeaturePermission Get(int roleId, int featureId)
        {
            var items = GetInternal(roleId, featureId);
            var matchItems = items.Where(p => p.RoleId == roleId && p.FeatureId == featureId).ToList();

            RoleFeaturePermission item = null;
            if (matchItems.Count > 0)
            {
                item = matchItems[0];
            }
            else
            {
                item = new RoleFeaturePermission();
            }

            return item;
        }

        public List<RoleFeaturePermission> Get(int roleId)
        {
            var items = GetInternal(roleId, -1);
            return items.Where(p => p.RoleId == roleId).ToList();
        }

        public List<RoleFeaturePermission> GetAll()
        {
            return GetInternal(-1, -1);
        }

        #region private method

        private List<RoleFeaturePermission> GetInternal(int roleId, int featureId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);
            if (roleId > 0)
            {
                _dbHelper.AddInParameter(dbCommand, "@RoleId", System.Data.DbType.Int32, roleId);
            }

            if (featureId > 0)
            {
                _dbHelper.AddInParameter(dbCommand, "@FeatureId", System.Data.DbType.Int32, featureId);
            }

            List<RoleFeaturePermission> items = new List<RoleFeaturePermission>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    RoleFeaturePermission item = new RoleFeaturePermission();
                    item.Id = (int)reader["Id"];
                    item.RoleId = (int)reader["RoleId"];
                    item.FeatureId = (int)reader["FeatureId"];
                    item.Permission = (int)reader["Permission"];

                    items.Add(item);
                }
            }

            reader.Close();
            _dbHelper.Close(dbCommand);

            return items;
        }

        #endregion
    }
}
