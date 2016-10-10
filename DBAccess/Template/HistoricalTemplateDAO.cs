using log4net;
using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;

namespace DBAccess.Template
{
    public class HistoricalTemplateDAO: BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Get = "procHistTemplateSelect";
        private const string SP_Create = "procHistTemplateInsert";
        private const string SP_Delete = "procHistTemplateDelete";

        public HistoricalTemplateDAO()
            : base()
        {
        }

        public HistoricalTemplateDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        }

        public int Create(HistStockTemplate template)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, template.TemplateId);
            _dbHelper.AddInParameter(dbCommand, "@TemplateName", System.Data.DbType.String, template.TemplateName);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)template.EStatus);
            _dbHelper.AddInParameter(dbCommand, "@WeightType", System.Data.DbType.Int32, (int)template.EWeightType);
            _dbHelper.AddInParameter(dbCommand, "@ReplaceType", System.Data.DbType.Int32, (int)template.EReplaceType);
            _dbHelper.AddInParameter(dbCommand, "@FuturesCopies", System.Data.DbType.Int32, template.FutureCopies);
            _dbHelper.AddInParameter(dbCommand, "@MarketCapOpt", System.Data.DbType.Decimal, template.MarketCapOpt);
            _dbHelper.AddInParameter(dbCommand, "@BenchmarkId", System.Data.DbType.String, template.Benchmark);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveDate", System.Data.DbType.DateTime, DateTime.Now);
            _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, template.DCreatedDate);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, template.DModifiedDate);
            _dbHelper.AddInParameter(dbCommand, "@CreatedUserId", System.Data.DbType.Int32, template.CreatedUserId);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            int archiveId = -1;
            if (ret > 0)
            {
                archiveId = (int)dbCommand.Parameters["@return"].Value;
            }

            return archiveId;
        }

        public int Delete(int archiveId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public List<HistStockTemplate> Get()
        {
            List<HistStockTemplate> stockTemplates = new List<HistStockTemplate>();
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    HistStockTemplate item = new HistStockTemplate();
                    item.ArchiveId = (int)reader["ArchiveId"];
                    item.TemplateId = (int)reader["TemplateId"];
                    item.TemplateName = (string)reader["TemplateName"];
                    item.FutureCopies = (int)reader["FuturesCopies"];
                    item.MarketCapOpt = (double)(decimal)reader["MarketCapOpt"];
                    item.EStatus = (TemplateStatus)reader["Status"];
                    item.EWeightType = (WeightType)reader["WeightType"];
                    item.EReplaceType = (ReplaceType)reader["ReplaceType"];
                    item.Benchmark = (string)reader["BenchmarkId"];
                    item.CreatedUserId = (int)reader["CreatedUserId"];

                    if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
                    {
                        item.DCreatedDate = (DateTime)reader["CreatedDate"];
                    }

                    if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
                    {
                        item.DModifiedDate = (DateTime)reader["ModifiedDate"];
                    }

                    if (reader["ArchiveDate"] != null && reader["ArchiveDate"] != DBNull.Value)
                    { 
                        item.DArchiveDate = (DateTime)reader["ArchiveDate"];
                    }

                    stockTemplates.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return stockTemplates;
        }
    }
}
