using log4net;
using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DBAccess.Archive.Template
{
    public class ArchiveTemplateDAO: BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_CreateArchiveStock = "procArchiveTemplateStockInsert";
        private const string SP_Get = "procArchiveTemplateSelect";
        private const string SP_Create = "procArchiveTemplateInsert";
        private const string SP_Delete = "procArchiveTemplateDelete";

        public ArchiveTemplateDAO()
            : base()
        {
        }

        public ArchiveTemplateDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        }

        public int Archive(ArchiveStockTemplate template, List<TemplateStock> tempStocks)
        {
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(dbCommand);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int archiveId = -1;
            int ret = -1;
            try
            {
                //delete all old one
                dbCommand.CommandText = SP_Create;
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
                _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, DateTime.Now);
                _dbHelper.AddInParameter(dbCommand, "@CreatedUserId", System.Data.DbType.Int32, template.CreatedUserId);

                _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

                ret = dbCommand.ExecuteNonQuery();

                if (ret >= 0)
                {
                    if (ret > 0)
                    {
                        archiveId = (int)dbCommand.Parameters["@return"].Value;
                        template.ArchiveId = archiveId;
                    }

                    foreach (var tempStock in tempStocks)
                    {
                        dbCommand.Parameters.Clear();
                        dbCommand.CommandText = SP_CreateArchiveStock;

                        _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);
                        _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, tempStock.TemplateNo);
                        _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, tempStock.SecuCode);
                        _dbHelper.AddInParameter(dbCommand, "@Amount", System.Data.DbType.Int32, tempStock.Amount);
                        _dbHelper.AddInParameter(dbCommand, "@MarketCap", System.Data.DbType.Decimal, tempStock.MarketCap);
                        _dbHelper.AddInParameter(dbCommand, "@MarketCapOpt", System.Data.DbType.Decimal, tempStock.MarketCapWeight);
                        _dbHelper.AddInParameter(dbCommand, "@SettingWeight", System.Data.DbType.Decimal, tempStock.SettingWeight);

                        _dbHelper.AddOutParameter(dbCommand, "@ReturnValue", System.Data.DbType.String, 50);

                        ret = dbCommand.ExecuteNonQuery();
                        if (ret > 0)
                        {
                            string newid = string.Empty;
                            if (ret > 0)
                            {
                                newid = (string)dbCommand.Parameters["@ReturnValue"].Value;
                            }
                        }
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
                _dbHelper.Close(dbCommand);
                transaction.Dispose();
            }

            return archiveId;
        }

        public int Create(ArchiveStockTemplate template)
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

        public List<ArchiveStockTemplate> Get()
        {
            List<ArchiveStockTemplate> stockTemplates = new List<ArchiveStockTemplate>();
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ArchiveStockTemplate item = new ArchiveStockTemplate();
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
            _dbHelper.Close(dbCommand);

            return stockTemplates;
        }
    }
}
