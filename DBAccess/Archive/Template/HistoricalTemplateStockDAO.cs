using log4net;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DBAccess.Archive.Template
{
    public class HistoricalTemplateStockDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Create = "procHistTemplateStockInsert";
        private const string SP_Delete = "procHistTemplateStockDelete";
        private const string SP_DeleteAll = "procHistTemplateStockDeleteAll";
        private const string SP_Get = "procHistTemplateStockSelect";

        public HistoricalTemplateStockDAO()
            : base()
        { 
        
        }

        public HistoricalTemplateStockDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        
        }

        public string Create(HistTemplateStock stock)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, stock.ArchiveId);
            _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, stock.TemplateNo);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, stock.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@Amount", System.Data.DbType.Int32, stock.Amount);
            _dbHelper.AddInParameter(dbCommand, "@MarketCap", System.Data.DbType.Decimal, stock.MarketCap);
            _dbHelper.AddInParameter(dbCommand, "@MarketCapOpt", System.Data.DbType.Decimal, stock.MarketCapWeight);
            _dbHelper.AddInParameter(dbCommand, "@SettingWeight", System.Data.DbType.Decimal, stock.SettingWeight);

            _dbHelper.AddOutParameter(dbCommand, "@ReturnValue", System.Data.DbType.String, 50);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            string newid = string.Empty;
            if (ret > 0)
            {
                newid = (string)dbCommand.Parameters["@ReturnValue"].Value;
            }

            return newid;
        }

        public int Create(int archiveId, List<TemplateStock> tempStocks)
        {
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(dbCommand);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int ret = -1;
            try
            {
                foreach (var tempStock in tempStocks)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.CommandText = SP_Create;
                    _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);
                    _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, tempStock.TemplateNo);
                    _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, tempStock.SecuCode);
                    _dbHelper.AddInParameter(dbCommand, "@Amount", System.Data.DbType.Int32, tempStock.Amount);
                    _dbHelper.AddInParameter(dbCommand, "@MarketCap", System.Data.DbType.Decimal, tempStock.MarketCap);
                    _dbHelper.AddInParameter(dbCommand, "@MarketCapOpt", System.Data.DbType.Decimal, tempStock.MarketCapWeight);
                    _dbHelper.AddInParameter(dbCommand, "@SettingWeight", System.Data.DbType.Decimal, tempStock.SettingWeight);

                    _dbHelper.AddOutParameter(dbCommand, "@ReturnValue", System.Data.DbType.String, 20);

                    ret = dbCommand.ExecuteNonQuery();
                    string newid = string.Empty;
                    if (ret > 0)
                    {
                        newid = (string)dbCommand.Parameters["@ReturnValue"].Value;
                    }

                    if (string.IsNullOrEmpty(newid))
                    {
                        ret++;
                    }
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                //TODO: add log
                logger.Error(ex);
                ret = -1;
                throw;
            }
            finally
            {
                _dbHelper.Close(dbCommand.Connection);
                transaction.Dispose();
            }

            return ret;
        }

        public string DeleteOneStock(int archiveId, int templateId, string secuCode)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);
            _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, templateId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, secuCode);

            _dbHelper.AddOutParameter(dbCommand, "@ReturnValue", System.Data.DbType.String, 50);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            string newid = string.Empty;
            if (ret > 0)
            {
                newid = (string)dbCommand.Parameters["@ReturnValue"].Value;
            }

            return newid;
        }

        public int DeleteOneArchive(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_DeleteAll);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public List<HistTemplateStock> Get(int archiveId)
        {
            List<HistTemplateStock> templateStocks = new List<HistTemplateStock>();
            if (archiveId < 1)
            {
                return templateStocks;
            }

            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);

            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    HistTemplateStock item = new HistTemplateStock();
                    item.ArchiveId = (int)reader["ArchiveId"];
                    item.TemplateNo = (int)reader["TemplateId"];
                    item.SecuCode = (string)reader["SecuCode"];
                    item.SecuName = (string)reader["SecuName"];
                    item.Exchange = (string)reader["ExchangeCode"];
                    item.Amount = (int)reader["Amount"];
                    item.MarketCap = (double)(decimal)reader["MarketCap"];
                    item.MarketCapWeight = (double)(decimal)reader["MarketCapOpt"];
                    item.SettingWeight = (double)(decimal)reader["SettingWeight"];

                    templateStocks.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return templateStocks;
        }
    }
}
