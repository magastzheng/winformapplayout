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
            _dbHelper.AddInParameter(dbCommand, "@Name", System.Data.DbType.String, role.Name);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)role.Status);
            _dbHelper.AddInParameter(dbCommand, "@Type", System.Data.DbType.Int32, (int)role.Type);

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
            _dbHelper.AddInParameter(dbCommand, "@Id", System.Data.DbType.Int32, role.Id);
            _dbHelper.AddInParameter(dbCommand, "@Name", System.Data.DbType.String, role.Name);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)role.Status);
            _dbHelper.AddInParameter(dbCommand, "@Type", System.Data.DbType.Int32, (int)role.Type);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(int roleId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@Id", System.Data.DbType.Int32, roleId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public Role Get(int roleId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);
            _dbHelper.AddInParameter(dbCommand, "@Id", System.Data.DbType.Int32, roleId);

            Role item = new Role();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    item.Id = (int)reader["Id"];
                    item.Name = (string)reader["Name"];
                    item.Status = (RoleStatus)reader["Status"];
                    item.Type = (RoleType)reader["Type"];
                    break;
                }
            }

            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return item;
        }

        public List<Role> Get()
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);

            List<Role> items = new List<Role>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Role item = new Role();
                    item.Id = (int)reader["Id"];
                    item.Name = (string)reader["Name"];
                    item.Status = (RoleStatus)reader["Status"];
                    item.Type = (RoleType)reader["Type"];

                    items.Add(item);
                }
            }

            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return items;
        }
    }
}
