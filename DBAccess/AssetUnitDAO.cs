
namespace DBAccess
{
    //public class AssetUnitDAO : BaseDAO
    //{
    //    private const string SP_CreateAssetUnit = "procInsertAssetUnit";
    //    private const string SP_ModifyAssetUnit = "procUpdateAssetUnit";
    //    private const string SP_DeleteAssetUnit = "procDeleteAssetUnit";
    //    private const string SP_GetAssetUnit = "procGetAssetUnits";

    //    public AssetUnitDAO()
    //        : base()
    //    { 
        
    //    }

    //    public AssetUnitDAO(DbHelper dbHelper)
    //        : base(dbHelper)
    //    { 
        
    //    }

    //    public int CreateAssetUnit(AssetUnit assetUnit)
    //    {
    //        var dbCommand = _dbHelper.GetStoredProcCommand(SP_CreateAssetUnit);
    //        _dbHelper.AddInParameter(dbCommand, "@AssetUnitCode", System.Data.DbType.String, assetUnit.AssetUnitCode);
    //        _dbHelper.AddInParameter(dbCommand, "@FundId", System.Data.DbType.Int32, assetUnit.FundId);
    //        _dbHelper.AddInParameter(dbCommand, "@AssetUnitName", System.Data.DbType.String, assetUnit.AssetUnitName);
    //        _dbHelper.AddInParameter(dbCommand, "@AssetUnitStatus", System.Data.DbType.Int32, assetUnit.AssetUnitStatus);
    //        _dbHelper.AddInParameter(dbCommand, "@CanOverdraft", System.Data.DbType.Int32, assetUnit.CanOverdraft);
    //        _dbHelper.AddInParameter(dbCommand, "@AssetType", System.Data.DbType.Int32, assetUnit.AssetType);
    //        _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, DateTime.Now);

    //        _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

    //        int ret = _dbHelper.ExecuteNonQuery(dbCommand);
    //        int id = -1;
    //        if (ret > 0)
    //        {
    //            id = (int)dbCommand.Parameters["@return"].Value;
    //        }

    //        return id;
    //    }

    //    public int UpdateAssetUnit(AssetUnit assetUnit)
    //    {
    //        var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyAssetUnit);
    //        _dbHelper.AddInParameter(dbCommand, "@AssetUnitCode", System.Data.DbType.String, assetUnit.AssetUnitCode);
    //        _dbHelper.AddInParameter(dbCommand, "@FundId", System.Data.DbType.Int32, assetUnit.FundId);
    //        _dbHelper.AddInParameter(dbCommand, "@AssetUnitName", System.Data.DbType.String, assetUnit.AssetUnitName);
    //        _dbHelper.AddInParameter(dbCommand, "@AssetUnitStatus", System.Data.DbType.Int32, assetUnit.AssetUnitStatus);
    //        _dbHelper.AddInParameter(dbCommand, "@CanOverdraft", System.Data.DbType.Int32, assetUnit.CanOverdraft);
    //        _dbHelper.AddInParameter(dbCommand, "@AssetType", System.Data.DbType.Int32, assetUnit.AssetType);
    //        _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, assetUnit.ModifiedDate);

    //        _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

    //        int ret = _dbHelper.ExecuteNonQuery(dbCommand);
    //        int id = -1;
    //        if (ret > 0)
    //        {
    //            id = (int)dbCommand.Parameters["@return"].Value;
    //        }

    //        return id;
    //    }

    //    public int DeleteAssetUnit(string assetUnitCode)
    //    {
    //        var dbCommand = _dbHelper.GetStoredProcCommand(SP_DeleteAssetUnit);
    //        _dbHelper.AddInParameter(dbCommand, "@AssetUnitCode", System.Data.DbType.String, assetUnitCode);

    //        _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

    //        int ret = _dbHelper.ExecuteNonQuery(dbCommand);
    //        int id = -1;
    //        if (ret > 0)
    //        {
    //            id = (int)dbCommand.Parameters["@return"].Value;
    //        }

    //        return id;
    //    }

    //    public List<AssetUnit> GetAssetUnit(string assetUnitCode)
    //    {
    //        var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetAssetUnit);
    //        if (!string.IsNullOrEmpty(assetUnitCode))
    //        {
    //            _dbHelper.AddInParameter(dbCommand, "@AssetUnitCode", System.Data.DbType.String, assetUnitCode);
    //        }

    //        List<AssetUnit> assetUnits = new List<AssetUnit>();
    //        var reader = _dbHelper.ExecuteReader(dbCommand);
    //        if (reader.HasRows)
    //        {
    //            while (reader.Read())
    //            {
    //                AssetUnit item = new AssetUnit();
    //                item.AssetUnitId = (int)reader["AssetUnitId"];
    //                item.AssetUnitCode = (string)reader["AssetUnitCode"];
    //                item.AssetUnitName = (string)reader["AssetUnitName"];
    //                item.FundId = (int)reader["FundId"];
    //                item.AssetUnitStatus = (int)reader["AssetUnitStatus"];
    //                item.CanOverdraft = (int)reader["CanOverdraft"];
    //                item.AssetType = (int)reader["AssetType"];

    //                if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
    //                {
    //                    item.CreatedDate = (DateTime)reader["CreatedDate"];
    //                }

    //                if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
    //                {
    //                    item.ModifiedDate = (DateTime)reader["ModifiedDate"];
    //                }
    //                assetUnits.Add(item);
    //            }
    //        }
    //        reader.Close();
    //        _dbHelper.Close(dbCommand.Connection);

    //        return assetUnits;
    //    }
    //}
}
