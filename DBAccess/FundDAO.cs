using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    //public class FundDAO : BaseDAO
    //{
    //    private const string SP_CreateFund = "procInsertFund";
    //    private const string SP_ModifyFund = "procUpdateFund";
    //    private const string SP_DeleteFund = "procDeleteFund";
    //    private const string SP_GetFund = "procGetFunds";

    //    public FundDAO()
    //        : base()
    //    { 
        
    //    }

    //    public FundDAO(DbHelper dbHelper)
    //        : base(dbHelper)
    //    { 
        
    //    }

    //    public int CreateFund(Fund fund)
    //    {
    //        var dbCommand = _dbHelper.GetStoredProcCommand(SP_CreateFund);
    //        _dbHelper.AddInParameter(dbCommand, "@FundCode", System.Data.DbType.String, fund.FundCode);
    //        _dbHelper.AddInParameter(dbCommand, "@FundName", System.Data.DbType.String, fund.FundName);
    //        _dbHelper.AddInParameter(dbCommand, "@ManagerCode", System.Data.DbType.String, fund.ManagerCode);
    //        _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, DateTime.Now);

    //        _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

    //        int ret = _dbHelper.ExecuteNonQuery(dbCommand);
    //        int fundId = -1;
    //        if (ret > 0)
    //        {
    //            fundId = (int)dbCommand.Parameters["@return"].Value;
    //        }

    //        return fundId;
    //    }

    //    public int UpdateFund(Fund fund)
    //    {
    //        var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyFund);
    //        _dbHelper.AddInParameter(dbCommand, "@FundCode", System.Data.DbType.String, fund.FundCode);
    //        _dbHelper.AddInParameter(dbCommand, "@FundName", System.Data.DbType.String, fund.FundName);
    //        _dbHelper.AddInParameter(dbCommand, "@ManagerCode", System.Data.DbType.String, fund.ManagerCode);
    //        _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

    //        _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

    //        int ret = _dbHelper.ExecuteNonQuery(dbCommand);
    //        int fundId = -1;
    //        if (ret > 0)
    //        {
    //            fundId = (int)dbCommand.Parameters["@return"].Value;
    //        }

    //        return fundId;
    //    }

    //    public int DeleteFund(string fundCode)
    //    {
    //        var dbCommand = _dbHelper.GetStoredProcCommand(SP_DeleteFund);
    //        _dbHelper.AddInParameter(dbCommand, "@FundCode", System.Data.DbType.String, fundCode);
    //        _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

    //        int ret = _dbHelper.ExecuteNonQuery(dbCommand);
    //        int fundId = -1;
    //        if (ret > 0)
    //        {
    //            fundId = (int)dbCommand.Parameters["@return"].Value;
    //        }

    //        return fundId;
    //    }

    //    public List<Fund> GetFund(string fundCode)
    //    {
    //        var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetFund);
    //        if (!string.IsNullOrEmpty(fundCode))
    //        {
    //            _dbHelper.AddInParameter(dbCommand, "@FundCode", System.Data.DbType.String, fundCode);
    //        }

    //        List<Fund> funds = new List<Fund>();
    //        var reader = _dbHelper.ExecuteReader(dbCommand);
    //        if (reader.HasRows)
    //        {
    //            while (reader.Read())
    //            {
    //                Fund item = new Fund();
    //                item.FundId = (int)reader["FundId"];
    //                item.FundCode = (string)reader["FundCode"];
    //                item.FundName = (string)reader["FundName"];
    //                item.ManagerCode = (string)reader["ManagerCode"];
    //                if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
    //                {
    //                    item.CreatedDate = (DateTime)reader["CreatedDate"];
    //                }

    //                if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
    //                {
    //                    item.ModifiedDate = (DateTime)reader["ModifiedDate"];
    //                }
    //                funds.Add(item);
    //            }
    //        }
    //        reader.Close();
    //        _dbHelper.Close(dbCommand.Connection);

    //        return funds;
    //    }
    //}
}
