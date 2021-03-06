﻿using log4net;
using Model.EnumType;
using Model.UI;
using System.Collections.Generic;
using System.Data.Common;

namespace DBAccess.Product
{
    public class UFXPortfolioDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Create = "procUFXPortfolioInsert";
        private const string SP_ModifyName = "procUFXPortfolioUpdateName";
        private const string SP_ModifyStatus = "procUFXPortfolioUpdateStatus";
        private const string SP_Get = "procUFXPortfolioSelect";
        private const string SP_GetById = "procUFXPortfolioSelectById";
        private const string SP_Delete = "procUFXPortfolioDelete";

        public UFXPortfolioDAO()
            : base()
        {

        }

        public UFXPortfolioDAO(DbHelper dbHelper)
            : base(dbHelper)
        {

        }

        /// <summary>
        /// Insert the Portfolio into the ufxportfolio table. It will return the new row id as PortfolioId if success, otherwise it will return -1.
        /// </summary>
        /// <param name="portfolio">An instance of Portfolio</param>
        /// <returns>An integer value with positive value as new PortfolioId; -1 while there is a duplicated PortfolioCode.</returns>
        public int Create(Portfolio portfolio)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioCode", System.Data.DbType.String, portfolio.PortfolioNo);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioName", System.Data.DbType.String, portfolio.PortfolioName);
            _dbHelper.AddInParameter(dbCommand, "@AccountCode", System.Data.DbType.String, portfolio.FundCode);
            _dbHelper.AddInParameter(dbCommand, "@AccountName", System.Data.DbType.String, portfolio.FundName);
            _dbHelper.AddInParameter(dbCommand, "@AccountType", System.Data.DbType.Int32, (int)portfolio.EAccountType);
            _dbHelper.AddInParameter(dbCommand, "@AssetNo", System.Data.DbType.String, portfolio.AssetNo);
            _dbHelper.AddInParameter(dbCommand, "@AssetName", System.Data.DbType.String, portfolio.AssetName);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            int id = -1;
            if (ret > 0)
            {
                id = (int)dbCommand.Parameters["@return"].Value;
            }

            return id;
        }

        public int UpdateName(Portfolio portfolio)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyName);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioCode", System.Data.DbType.String, portfolio.PortfolioNo);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioName", System.Data.DbType.String, portfolio.PortfolioName);
         
            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int UpdateName(string portfolioCode, PortfolioStatus portfolioStatus)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyName);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioCode", System.Data.DbType.String, portfolioCode);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioStatus", System.Data.DbType.Int32, (int)portfolioStatus);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(string portfolioCode)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioCode", System.Data.DbType.String, portfolioCode);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public Portfolio Get(string portfolioCode)
        {
            Portfolio item = new Portfolio();
            var items = GetInternal(portfolioCode);
            if (items != null && items.Count > 0)
            {
                item = items[0];
            }

            return item;
        }

        public List<Portfolio> GetAll()
        {
            return GetInternal(string.Empty);
        }

        public Portfolio GetById(int portfolioId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetById);
            _dbHelper.AddInParameter(dbCommand, "@PortfolioId", System.Data.DbType.Int32, portfolioId);
            var reader = _dbHelper.ExecuteReader(dbCommand);

            Portfolio portfolio = null;
            if (reader.HasRows && reader.Read())
            {
                portfolio = GetPortfolio(reader);
            }
            else
            {
                portfolio = new Portfolio();
            }

            reader.Close();
            _dbHelper.Close(dbCommand);

            return portfolio;
        }

        #region private methods

        public List<Portfolio> GetInternal(string portfolioCode)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);
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
                    Portfolio item = GetPortfolio(reader);
                    portfolios.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand);

            return portfolios;
        }

        private Portfolio GetPortfolio(DbDataReader reader)
        {
            Portfolio item = new Portfolio();
            item.PortfolioId = (int)reader["PortfolioId"];
            item.PortfolioNo = (string)reader["PortfolioCode"];
            item.PortfolioName = (string)reader["PortfolioName"];
            item.FundCode = (string)reader["AccountCode"];
            item.FundName = (string)reader["AccountName"];
            item.EAccountType = (FundAccountType)reader["AccountType"];
            item.AssetNo = (string)reader["AssetNo"];
            item.AssetName = (string)reader["AssetName"];
            item.PortfolioStatus = (PortfolioStatus)reader["PortfolioStatus"];

            return item;
        }

        #endregion
    }
}
