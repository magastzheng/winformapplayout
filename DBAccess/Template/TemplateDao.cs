using log4net;
using Model.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Template
{
    public class TemplateDao: BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_TemplateCreate = "procTemplateInsert";
        private const string SP_StockCopy = "procTemplateStockCopy";

        public TemplateDao()
            : base()
        { 
            
        }

        public TemplateDao(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Copy(int oldTemplateId, TemplateItem template)
        {
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(dbCommand);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int ret = -1;
            int templateId = -1;
            try
            {
                dbCommand.CommandText = SP_TemplateCreate;

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

                ret = dbCommand.ExecuteNonQuery();

                if (ret >= 0)
                {
                    if (ret > 0)
                    {
                        templateId = (int)dbCommand.Parameters["@return"].Value;

                        dbCommand.Parameters.Clear();
                        dbCommand.CommandText = SP_StockCopy;

                        _dbHelper.AddInParameter(dbCommand, "@TemplateId", System.Data.DbType.Int32, templateId);
                        _dbHelper.AddInParameter(dbCommand, "@OldTemplateId", System.Data.DbType.Int32, oldTemplateId);

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
                _dbHelper.Close(dbCommand);
                transaction.Dispose();
            }

            return templateId;
        }
    }
}
