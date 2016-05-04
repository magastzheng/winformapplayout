using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class TemplateStockDAO : BaseDAO
    {
        private const string SP_GetTemplateStock = "procSelectTemplateStock";
        private const string SP_NewTemplateStock = "procInsertTemplateStock";
        private const string SP_ModifyTemplateStock = "procUpdateTemplateStock";
        private const string SP_DeleteTemplateStock = "procDeleteTemplateStock";

        public TemplateStockDAO()
            : base()
        {
        }

        public TemplateStockDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        }

        public string CreateTemplateStock(TemplateStock tempStock)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_NewTemplateStock);
            _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, tempStock.TemplateNo);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, tempStock.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@Amount", System.Data.DbType.Int32, tempStock.Amount);
            _dbHelper.AddInParameter(dbCommand, "@MarketCap", System.Data.DbType.Decimal, tempStock.MarketCap);
            _dbHelper.AddInParameter(dbCommand, "@MarketCapOpt", System.Data.DbType.Decimal, tempStock.MarketCapWeight);
            _dbHelper.AddInParameter(dbCommand, "@SettingWeight", System.Data.DbType.Decimal, tempStock.SettingWeight);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.String);

            string newid = string.Empty;
            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            if (ret > 0)
            {
                newid = (string)dbCommand.Parameters["@return"].Value;
            }
            return newid;
        }

        public string UpdateTemplateStock(TemplateStock tempStock)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_ModifyTemplateStock);
            _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, tempStock.TemplateNo);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, tempStock.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@Amount", System.Data.DbType.Int32, tempStock.Amount);
            _dbHelper.AddInParameter(dbCommand, "@MarketCap", System.Data.DbType.Decimal, tempStock.MarketCap);
            _dbHelper.AddInParameter(dbCommand, "@MarketCapOpt", System.Data.DbType.Decimal, tempStock.MarketCapWeight);
            _dbHelper.AddInParameter(dbCommand, "@SettingWeight", System.Data.DbType.Decimal, tempStock.SettingWeight);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.String);

            string newid = string.Empty;
            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            if (ret > 0)
            {
                newid = (string)dbCommand.Parameters["@return"].Value;
            }
            return newid;
        }

        public List<TemplateStock> GetTemplateStock(int templateId)
        {
            List<TemplateStock> stockTemplates = new List<TemplateStock>();
            if (templateId < 1)
            {
                return stockTemplates;
            }

            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetTemplateStock);

            _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, templateId);
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TemplateStock item = new TemplateStock();
                    item.TemplateNo = (int)reader["TemplateId"];
                    item.SecuCode = (string)reader["SecuCode"];
                    item.Amount = (int)reader["Amount"];
                    item.MarketCap = (double)(decimal)reader["MarketCap"];
                    item.MarketCapWeight = (double)(decimal)reader["MarketCapOpt"];
                    item.SettingWeight = (double)(decimal)reader["SettingWeight"];

                    stockTemplates.Add(item);
                }
            }

            return stockTemplates;
        }
    }
}
