using log4net;
using Model.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Permission
{
    public class FeatureDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Create = "procFeaturesInsert";
        private const string SP_Modify = "procFeaturesUpdate";
        private const string SP_Delete = "procFeaturesDelete";
        private const string SP_Select = "procFeaturesSelect";
        private const string SP_SelectByCode = "procFeaturesSelectByCode";

        public FeatureDAO()
            : base()
        {
        }

        public FeatureDAO(DbHelper dbHelper)
            : base(dbHelper)
        {
        }

        public int Create(Feature feature)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@Id", System.Data.DbType.Int32, feature.Id);
            _dbHelper.AddInParameter(dbCommand, "@Code", System.Data.DbType.String, feature.Code);
            _dbHelper.AddInParameter(dbCommand, "@Name", System.Data.DbType.String, feature.Name);
            _dbHelper.AddInParameter(dbCommand, "@Description", System.Data.DbType.String, feature.Description);
            
            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int id = -1;
            if (ret > 0)
            {
                id = (int)dbCommand.Parameters["@return"].Value;
            }

            return id;
        }

        public int Update(Feature feature)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Modify);
            _dbHelper.AddInParameter(dbCommand, "@Id", System.Data.DbType.Int32, feature.Id);
            _dbHelper.AddInParameter(dbCommand, "@Code", System.Data.DbType.String, feature.Code);
            _dbHelper.AddInParameter(dbCommand, "@Name", System.Data.DbType.String, feature.Name);
            _dbHelper.AddInParameter(dbCommand, "@Description", System.Data.DbType.String, feature.Description);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(int featureId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@Id", System.Data.DbType.Int32, featureId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public Feature GetByCode(string code)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_SelectByCode);
            _dbHelper.AddInParameter(dbCommand, "@Code", System.Data.DbType.String, code);

            Feature item = new Feature();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    item.Id = (int)reader["Id"];
                    item.Code = (string)reader["Code"];
                    item.Name = (string)reader["Name"];
                    item.Description = (string)reader["Description"];
                }
            }

            reader.Close();
            _dbHelper.Close(dbCommand);

            return item;
        }

        public Feature Get(int featureId)
        {
            var items = GetInternal(featureId);
            var matchItems = items.Where(p => p.Id == featureId).ToList();

            Feature item = null;
            if (matchItems.Count > 0)
            {
                item = matchItems[0];
            }
            else
            {
                item = new Feature();
            }
            
            return item;
        }

        public List<Feature> Get()
        {
            return GetInternal(-1);
        }

        #region private method

        public List<Feature> GetInternal(int featureId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);
            if (featureId > 0)
            {
                _dbHelper.AddInParameter(dbCommand, "@Id", System.Data.DbType.Int32, featureId);
            }

            List<Feature> items = new List<Feature>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Feature item = new Feature();
                    item.Id = (int)reader["Id"];
                    item.Code = (string)reader["Code"];
                    item.Name = (string)reader["Name"];
                    item.Description = (string)reader["Description"];

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
