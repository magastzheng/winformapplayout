using Model.config;
using Model.Database;
using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;

namespace DBAccess.Template
{
    public class StockTemplateDAO : BaseDAO
    {
        private const string SP_Create = "procTemplateInsert";
        private const string SP_Modify = "procTemplateUpdate";
        private const string SP_Delete = "procTemplateDelete";
        private const string SP_Get = "procTemplateSelectById";
        private const string SP_GetByUser = "procTemplateSelect";

        public StockTemplateDAO()
            : base()
        { 
        
        }

        public StockTemplateDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        
        }

        public int Create(TemplateItem template)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@TemplateName", System.Data.DbType.String, template.TemplateName);
            //_dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, status);
            _dbHelper.AddInParameter(dbCommand, "@WeightType", System.Data.DbType.Int32, (int)template.EWeightType);
            _dbHelper.AddInParameter(dbCommand, "@ReplaceType", System.Data.DbType.Int32, (int)template.EReplaceType);
            _dbHelper.AddInParameter(dbCommand, "@FuturesCopies", System.Data.DbType.Int32, template.FutureCopies);
            _dbHelper.AddInParameter(dbCommand, "@MarketCapOpt", System.Data.DbType.Decimal, template.MarketCapOpt);
            _dbHelper.AddInParameter(dbCommand, "@BenchmarkId", System.Data.DbType.String, template.Benchmark);
            _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, DateTime.Now);
            _dbHelper.AddInParameter(dbCommand, "@CreatedUserId", System.Data.DbType.Int32, template.CreatedUserId);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            int templateId = -1;
            if (ret > 0)
            {
                templateId = (int)dbCommand.Parameters["@return"].Value;
            }

            return templateId;
        }

        public int Create(string templateName, WeightType weightType, ReplaceType replaceType, int futuresCopies, double marketCapOpt, string benchmarkId, int userId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@TemplateName", System.Data.DbType.String, templateName);
            //_dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, status);
            _dbHelper.AddInParameter(dbCommand, "@WeightType", System.Data.DbType.Int32, (int)weightType);
            _dbHelper.AddInParameter(dbCommand, "@ReplaceType", System.Data.DbType.Int32, (int)replaceType);
            _dbHelper.AddInParameter(dbCommand, "@FuturesCopies", System.Data.DbType.Int32, futuresCopies);
            _dbHelper.AddInParameter(dbCommand, "@MarketCapOpt", System.Data.DbType.Decimal, marketCapOpt);
            _dbHelper.AddInParameter(dbCommand, "@BenchmarkId", System.Data.DbType.String, benchmarkId);
            _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, DateTime.Now);
            _dbHelper.AddInParameter(dbCommand, "@CreatedUserId", System.Data.DbType.Int32, userId);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            int templateId = -1;
            if (ret > 0)
            {
                templateId = (int)dbCommand.Parameters["@return"].Value;
            }

            return templateId;
        }

        public int Update(int templateNo, string templateName, WeightType weightType, ReplaceType replaceType, int futuresCopies, double marketCapOpt, string benchmarkId, int userId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Modify);
            _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, templateNo);
            _dbHelper.AddInParameter(dbCommand, "@TemplateName", System.Data.DbType.String, templateName);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)TemplateStatus.Normal);
            _dbHelper.AddInParameter(dbCommand, "@WeightType", System.Data.DbType.Int32, (int)weightType);
            _dbHelper.AddInParameter(dbCommand, "@ReplaceType", System.Data.DbType.Int32, (int)replaceType);
            _dbHelper.AddInParameter(dbCommand, "@FuturesCopies", System.Data.DbType.Int32, futuresCopies);
            _dbHelper.AddInParameter(dbCommand, "@MarketCapOpt", System.Data.DbType.Decimal, marketCapOpt);
            _dbHelper.AddInParameter(dbCommand, "@BenchmarkId", System.Data.DbType.String, benchmarkId);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);
            _dbHelper.AddInParameter(dbCommand, "@CreatedUserId", System.Data.DbType.Int32, userId);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            int templateId = -1;
            if(ret > 0)
            {
                templateId = (int)dbCommand.Parameters["@return"].Value;
            }
            return templateId;
        }

        public int Update(TemplateItem template)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Modify);
            _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, template.TemplateId);
            _dbHelper.AddInParameter(dbCommand, "@TemplateName", System.Data.DbType.String, template.TemplateName);
            _dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, (int)template.EStatus);
            _dbHelper.AddInParameter(dbCommand, "@WeightType", System.Data.DbType.Int32, (int)template.EWeightType);
            _dbHelper.AddInParameter(dbCommand, "@ReplaceType", System.Data.DbType.Int32, (int)template.EReplaceType);
            _dbHelper.AddInParameter(dbCommand, "@FuturesCopies", System.Data.DbType.Int32, template.FutureCopies);
            _dbHelper.AddInParameter(dbCommand, "@MarketCapOpt", System.Data.DbType.Decimal, template.MarketCapOpt);
            _dbHelper.AddInParameter(dbCommand, "@BenchmarkId", System.Data.DbType.String, template.Benchmark);
            _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);
            _dbHelper.AddInParameter(dbCommand, "@CreatedUserId", System.Data.DbType.Int32, template.CreatedUserId);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);
            int templateId = -1;
            if (ret > 0)
            {
                templateId = (int)dbCommand.Parameters["@return"].Value;
            }
            return templateId;
        }

        public int Delete(int templateNo)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, templateNo);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public TemplateItem Get(int templateId)
        {
            var items = GetInternal(templateId);
            var item = new TemplateItem();
            if (items != null && items.Count > 0)
            {
                item = items[0];
            }

            return item;
        }

        public List<TemplateItem> GetAll()
        {
            return GetInternal(-1);
        }

        public List<TemplateItem> GetByUser(int userId)
        {
            List<TemplateItem> stockTemplates = new List<TemplateItem>();
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetByUser);
            if (userId > 0)
            {
                _dbHelper.AddInParameter(dbCommand, "@UserId", System.Data.DbType.Int32, userId);
            }
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TemplateItem item = new TemplateItem();
                    item.TemplateId = (int)reader["TemplateId"];
                    item.TemplateName = (string)reader["TemplateName"];
                    item.FutureCopies = (int)reader["FuturesCopies"];
                    item.MarketCapOpt = (double)(decimal)reader["MarketCapOpt"];
                    item.EStatus = (TemplateStatus)reader["Status"];
                    item.EWeightType = (WeightType)reader["WeightType"];
                    item.EReplaceType = (ReplaceType)reader["ReplaceType"];
                    item.Benchmark = (string)reader["BenchmarkId"];

                    if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
                    {
                        item.DCreatedDate = (DateTime)reader["CreatedDate"];
                    }

                    if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
                    {
                        item.DModifiedDate = (DateTime)reader["ModifiedDate"];
                    }

                    stockTemplates.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return stockTemplates;
        }

        private List<TemplateItem> GetInternal(int templateId)
        {
            List<TemplateItem> stockTemplates = new List<TemplateItem>();
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);
            if (templateId > 0)
            {
                _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, templateId);
            }
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TemplateItem item = new TemplateItem();
                    item.TemplateId = (int)reader["TemplateId"];
                    item.TemplateName = (string)reader["TemplateName"];
                    item.FutureCopies = (int)reader["FuturesCopies"];
                    item.MarketCapOpt = (double)(decimal)reader["MarketCapOpt"];
                    item.EStatus = (TemplateStatus)reader["Status"];
                    item.EWeightType = (WeightType)reader["WeightType"];
                    item.EReplaceType = (ReplaceType)reader["ReplaceType"];
                    item.Benchmark = (string)reader["BenchmarkId"];

                    if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
                    {
                        item.DCreatedDate = (DateTime)reader["CreatedDate"];
                    }

                    if (reader["ModifiedDate"] != null && reader["ModifiedDate"] != DBNull.Value)
                    {
                        item.DModifiedDate = (DateTime)reader["ModifiedDate"];
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
