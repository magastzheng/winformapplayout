using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Template
{
    public class HistoricalTemplateStockDAO : BaseDAO
    {
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
