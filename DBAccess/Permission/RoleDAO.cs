using log4net;
using Model.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Permission
{
    public class RoleDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Create = "procRolesInsert";
        private const string SP_Modify = "procRolesUpdate";
        private const string SP_Delete = "procRolesDelete";
        private const string SP_Select = "procRolesSelect";

        public RoleDAO()
            : base()
        {
        }

        public RoleDAO(DbHelper dbHelper)
            : base(dbHelper)
        {
        }

        public int Create(Role role)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@Id", System.Data.DbType.Int32, (int)role.Id);
            _dbHelper.AddInParameter(dbCommand, "@Name", System.Data.DbType.String, role.Name);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)role.Status);
            
            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int id = -1;
            if (ret > 0)
            {
                id = (int)dbCommand.Parameters["@return"].Value;
            }

            return id;
        }

        public int Update(Role role)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Modify);
            _dbHelper.AddInParameter(dbCommand, "@Id", System.Data.DbType.Int32, (int)role.Id);
            _dbHelper.AddInParameter(dbCommand, "@Name", System.Data.DbType.String, role.Name);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)role.Status);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(RoleType id)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@Id", System.Data.DbType.Int32, (int)id);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public Role Get(RoleType roleType)
        {
            var items = GetInternal(roleType);
            var matchItems = items.Where(p => p.Id == roleType).ToList();

            Role item = null;
            if (matchItems.Count > 0)
            {
                item = matchItems[0];
            }
            else
            {
                item = new Role();
            }
            
            return item;
        }

        public List<Role> GetAll()
        {
            return GetInternal(RoleType.None);
        }

        #region private method

        public List<Role> GetInternal(RoleType roleType)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);

            if (roleType != RoleType.None)
            {
                _dbHelper.AddInParameter(dbCommand, "@Id", System.Data.DbType.Int32, (int)roleType);
            }

            List<Role> items = new List<Role>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Role item = new Role();
                    item.Id = (RoleType)reader["Id"];
                    item.Name = (string)reader["Name"];
                    item.Status = (RoleStatus)reader["Status"];
                    
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
