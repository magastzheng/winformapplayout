using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class PortfolioDAO : BaseDAO
    {
        private const string SP_CreatePortfolio = "procInsertPortfolio";
        private const string SP_ModifyPortfolio = "procUpdatePortfolio";
        private const string SP_DeletePortfolio = "procDeletePortfolio";
        private const string SP_GetPortfolio = "procGetPortfolios";

        public PortfolioDAO()
            : base()
        {

        }

        public PortfolioDAO(DbHelper dbHelper)
            : base(dbHelper)
        {

        }

        public int CreatePortfolio(Portfolio portfolio)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_CreatePortfolio);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioCode", System.Data.DbType.String, portfolio.PortfolioCode);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioName", System.Data.DbType.String, portfolio.PortfolioName);
            _dbHelper.AddInParameter(dbCommand, "@AssetUnitId", System.Data.DbType.Int32, portfolio.AssetUnitId);
            _dbHelper.AddInParameter(dbCommand, "@FundId", System.Data.DbType.Int32, portfolio.FundId);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioType", System.Data.DbType.Int32, portfolio.PortfolioType);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioStatus", System.Data.DbType.Int32, portfolio.PortfolioStatus);
            _dbHelper.AddInParameter(dbCommand, "@FuturesInvestType", System.Data.DbType.String, portfolio.FuturesInvestType);
            _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, DateTime.Now);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            int id = -1;
            if (ret > 0)
            {
                id = (int)dbCommand.Parameters["@return"].Value;
            }

            return id;
        }

        public int UpdatePortfolio(Portfolio portfolio)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyPortfolio);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioCode", System.Data.DbType.String, portfolio.PortfolioCode);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioName", System.Data.DbType.String, portfolio.PortfolioName);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioType", System.Data.DbType.Int32, portfolio.PortfolioType);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioStatus", System.Data.DbType.Int32, portfolio.PortfolioStatus);
            _dbHelper.AddInParameter(dbCommand, "@FuturesInvestType", System.Data.DbType.String, portfolio.FuturesInvestType);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            int id = -1;
            if (ret > 0)
            {
                id = (int)dbCommand.Parameters["@return"].Value;
            }

            return id;
        }

        public int DeletePortfolio(string portfolioCode)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_DeletePortfolio);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioCode", System.Data.DbType.String, portfolioCode);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            int id = -1;
            if (ret > 0)
            {
                id = (int)dbCommand.Parameters["@return"].Value;
            }

            return id;
        }

        public List<Portfolio> GetPortfolio(string portfolioCode)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetPortfolio);
            if (!string.IsNullOrEmpty(portfolioCode))
            {
                _dbHelper.AddInParameter(dbCommand, "@PortfolioCode", System.Data.DbType.String, portfolioCode);
            }

            List<Portfolio> portfolios = new List<Portfolio>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Portfolio item = new Portfolio();
                    item.PortfolioId = (int)reader["PortfolioId"];
                    item.PortfolioCode = (string)reader["PortfolioCode"];
                    item.PortfolioName = (string)reader["PortfolioName"];
                    item.AssetUnitId = (int)reader["AssetUnitId"];
                    item.FundId = (int)reader["FundId"];
                    item.PortfolioType = (int)reader["PortfolioType"];
                    item.PortfolioStatus = (int)reader["PortfolioStatus"];
                    item.FuturesInvestType = (string)reader["FuturesInvestType"];
                    if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
                    {
                        item.CreatedDate = (DateTime)reader["CreatedDate"];
                    }
                    if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
                    {
                        item.ModifiedDate = (DateTime)reader["ModifiedDate"];
                    }
                    portfolios.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return portfolios;
        }
    }
}
