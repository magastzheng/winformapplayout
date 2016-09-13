using log4net;
using Model.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Permission
{
    public class ResourceDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Create = "procResourcesInsert";
        private const string SP_Modify = "procResourcesUpdate";
        private const string SP_Delete = "procResourcesDelete";
        private const string SP_Select = "procResourcesSelect";

        public ResourceDAO()
            : base()
        { 
        }

        public ResourceDAO(DbHelper dbHelper)
            : base(dbHelper)
        {
        }

        public int Create(Resource resource)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@RefId", System.Data.DbType.Int32, resource.RefId);
            _dbHelper.AddInParameter(dbCommand, "@Type", System.Data.DbType.Int32, (int)resource.Type);
            _dbHelper.AddInParameter(dbCommand, "@Name", System.Data.DbType.String, resource.Name);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int id = -1;
            if (ret > 0)
            {
                id = (int)dbCommand.Parameters["@return"].Value;
            }

            return id;
        }

        public int Update(Resource resource)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Modify);
            _dbHelper.AddInParameter(dbCommand, "@RefId", System.Data.DbType.Int32, resource.RefId);
            _dbHelper.AddInParameter(dbCommand, "@Type", System.Data.DbType.Int32, (int)resource.Type);
            _dbHelper.AddInParameter(dbCommand, "@Name", System.Data.DbType.String, resource.Name);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(int refId, ResourceType type)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@RefId", System.Data.DbType.Int32, refId);
            _dbHelper.AddInParameter(dbCommand, "@Type", System.Data.DbType.Int32, type);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public Resource Get(int refId, ResourceType type)
        {
            var items = GetInternal(refId, type);

            Resource item = null;
            if (items.Count > 0)
            {
                item = items[0];
            }
            else
            {
                item = new Resource();
            }
            
            return item;
        }

        public List<Resource> GetAll()
        {
            return GetInternal(-1, ResourceType.None);
        }

        #region private method

        public List<Resource> GetInternal(int refId, ResourceType type)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);
            if (refId > 0)
            {
                _dbHelper.AddInParameter(dbCommand, "@RefId", System.Data.DbType.Int32, refId);
            }

            if (type != ResourceType.None)
            {
                _dbHelper.AddInParameter(dbCommand, "@Type", System.Data.DbType.Int32, (int)type);
            }

            List<Resource> items = new List<Resource>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Resource item = new Resource();
                    item.Id = (int)reader["Id"];
                    item.Name = (string)reader["Name"];
                    item.RefId = (int)reader["RefId"];
                    item.Type = (ResourceType)reader["Type"];

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
