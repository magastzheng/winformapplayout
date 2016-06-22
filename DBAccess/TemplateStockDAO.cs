using log4net;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class TemplateStockDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Get = "procTemplateStockSelect";
        private const string SP_New = "procTemplateStockInsert";
        private const string SP_Modify = "procTemplateStockInsertOrUpdate";
        private const string SP_Delete = "procTemplateStockDelete";
        private const string SP_DeleteAll = "procTemplateStockDeleteAll";

        public TemplateStockDAO()
            : base()
        {
        }

        public TemplateStockDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        }

        public string Create(TemplateStock tempStock)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_New);
            _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, tempStock.TemplateNo);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, tempStock.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@Amount", System.Data.DbType.Int32, tempStock.Amount);
            _dbHelper.AddInParameter(dbCommand, "@MarketCap", System.Data.DbType.Decimal, tempStock.MarketCap);
            _dbHelper.AddInParameter(dbCommand, "@MarketCapOpt", System.Data.DbType.Decimal, tempStock.MarketCapWeight);
            _dbHelper.AddInParameter(dbCommand, "@SettingWeight", System.Data.DbType.Decimal, tempStock.SettingWeight);

            _dbHelper.AddOutParameter(dbCommand, "@ReturnValue", System.Data.DbType.String, 20);

            string newid = string.Empty;
            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            if (ret > 0)
            {
                newid = (string)dbCommand.Parameters["@ReturnValue"].Value;
            }
            return newid;
        }

        public string Update(TemplateStock tempStock)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Modify);
            _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, tempStock.TemplateNo);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, tempStock.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@Amount", System.Data.DbType.Int32, tempStock.Amount);
            _dbHelper.AddInParameter(dbCommand, "@MarketCap", System.Data.DbType.Decimal, tempStock.MarketCap);
            _dbHelper.AddInParameter(dbCommand, "@MarketCapOpt", System.Data.DbType.Decimal, tempStock.MarketCapWeight);
            _dbHelper.AddInParameter(dbCommand, "@SettingWeight", System.Data.DbType.Decimal, tempStock.SettingWeight);

            _dbHelper.AddOutParameter(dbCommand, "@ReturnValue", System.Data.DbType.String, 20);

            string newid = string.Empty;
            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            if (ret > 0)
            {
                newid = (string)dbCommand.Parameters["@ReturnValue"].Value;
            }
            return newid;
        }

        public string Delete(int templateNo, string secuCode)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, templateNo);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, secuCode);

            _dbHelper.AddOutParameter(dbCommand, "@ReturnValue", System.Data.DbType.String, 20);

            string newid = string.Empty;
            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            if (ret >= 0)
            {
                newid = (string)dbCommand.Parameters["@ReturnValue"].Value;
            }
            return newid;
        }

        public int Delete(int templateNo)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_DeleteAll);
            _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, templateNo);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            return ret;
        }

        public List<TemplateStock> Get(int templateId)
        {
            List<TemplateStock> stockTemplates = new List<TemplateStock>();
            if (templateId < 1)
            {
                return stockTemplates;
            }

            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);

            _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, templateId);
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TemplateStock item = new TemplateStock();
                    item.TemplateNo = (int)reader["TemplateId"];
                    item.SecuCode = (string)reader["SecuCode"];
                    item.SecuName = (string)reader["SecuName"];
                    item.Exchange = (string)reader["ExchangeCode"];
                    item.Amount = (int)reader["Amount"];
                    item.MarketCap = (double)(decimal)reader["MarketCap"];
                    item.MarketCapWeight = (double)(decimal)reader["MarketCapOpt"];
                    item.SettingWeight = (double)(decimal)reader["SettingWeight"];

                    stockTemplates.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return stockTemplates;
        }

        public int Replace(int templateNo, List<TemplateStock> tempStocks)
        {
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(_dbHelper.Connection);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int ret = -1;
            try
            {
                //delete all old one
                dbCommand.CommandText = SP_DeleteAll;
                _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, templateNo);
                ret = dbCommand.ExecuteNonQuery();
                if (ret >= 0)
                {
                    foreach (var tempStock in tempStocks)
                    {
                        dbCommand.Parameters.Clear();
                        dbCommand.CommandText = SP_Modify;
                        _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, tempStock.TemplateNo);
                        _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, tempStock.SecuCode);
                        _dbHelper.AddInParameter(dbCommand, "@Amount", System.Data.DbType.Int32, tempStock.Amount);
                        _dbHelper.AddInParameter(dbCommand, "@MarketCap", System.Data.DbType.Decimal, tempStock.MarketCap);
                        _dbHelper.AddInParameter(dbCommand, "@MarketCapOpt", System.Data.DbType.Decimal, tempStock.MarketCapWeight);
                        _dbHelper.AddInParameter(dbCommand, "@SettingWeight", System.Data.DbType.Decimal, tempStock.SettingWeight);

                        _dbHelper.AddOutParameter(dbCommand, "@ReturnValue", System.Data.DbType.String, 20);

                        //string newid = string.Empty;
                        ret = dbCommand.ExecuteNonQuery();
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
    }
}
