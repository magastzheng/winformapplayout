using log4net;
using Model.Permission;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Permission
{
    public class UserDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Create = "procUsersInsert";
        private const string SP_Modify = "procUsersUpdate";
        private const string SP_Delete = "procUsersDelete";
        private const string SP_Select = "procUsersSelect";
        private const string SP_SelectById = "procUsersSelectById";

        public UserDAO()
            : base()
        { 
        }

        public UserDAO(DbHelper dbHelper)
            : base(dbHelper)
        {
        }

        public int Create(User user)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@Operator", System.Data.DbType.String, user.Operator);
            _dbHelper.AddInParameter(dbCommand, "@Name", System.Data.DbType.String, user.Name);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)user.Status);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int id = -1;
            if (ret > 0)
            {
                id = (int)dbCommand.Parameters["@return"].Value;
            }

            return id;
        }

        public int Update(User user)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Modify);
            _dbHelper.AddInParameter(dbCommand, "@Operator", System.Data.DbType.String, user.Operator);
            _dbHelper.AddInParameter(dbCommand, "@Name", System.Data.DbType.String, user.Name);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)user.Status);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(string operatorNo)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@Operator", System.Data.DbType.String, operatorNo);
            
            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public User Get(string operatorNo)
        {
            var items = GetInternal(operatorNo);
            var matchItems = items.Where(p => p.Operator.Equals(operatorNo)).ToList();

            User item = null;
            if (matchItems.Count > 0)
            {
                item = matchItems[0];
            }
            else
            {
                item = new User();
            }
            
            return item;
        }

        public User GetById(int id)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_SelectById);
            _dbHelper.AddInParameter(dbCommand, "@Id", System.Data.DbType.Int32, id);
            User item = new User();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows && reader.Read())
            {
                item = ParseData(reader);
            }

            reader.Close();
            _dbHelper.Close(dbCommand);

            return item;
        }

        public List<User> Get()
        {
            return GetInternal(string.Empty);
        }

        #region private method

        public List<User> GetInternal(string operatorNo)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);
            if (!string.IsNullOrEmpty(operatorNo))
            {
                _dbHelper.AddInParameter(dbCommand, "@Operator", System.Data.DbType.String, operatorNo);
            }

            List<User> items = new List<User>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var item = ParseData(reader);
                    items.Add(item);
                }
            }

            reader.Close();
            _dbHelper.Close(dbCommand);

            return items;
        }

        private User ParseData(DbDataReader reader)
        {
            User item = new User();
            item.Id = (int)reader["Id"];
            item.Operator = (string)reader["Operator"];
            item.Name = (string)reader["Name"];
            item.Status = (UserStatus)reader["Status"];

            if (reader["CreateDate"] != null && reader["CreateDate"] != DBNull.Value)
            {
                item.CreateDate = (DateTime)reader["CreateDate"];
            }

            if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
            {
                item.ModifieDate = (DateTime)reader["ModifiedDate"];
            }

            return item;
        }

        #endregion
    }
}
