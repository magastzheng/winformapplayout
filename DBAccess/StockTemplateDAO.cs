using Model.config;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class StockTemplateDAO
    {
        private const string SP_CreateTemplate = "procInsertTemplate";
        private const string SP_ModifyTemplate = "procUpdateTemplate";
        private const string SP_DeleteTemplate = "procDeleteTemplate";
        private const string SP_GetTemplate = "procSelectTemplate";

        private DbHelper _dbHelper;
        public StockTemplateDAO()
        {
            _dbHelper = new DbHelper();
        }

        public StockTemplateDAO(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public int CreateTemplate(string templateName, int weightType, int futuresCopies, double marketCapOpt, string benchmarkId, int userId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_CreateTemplate);
            _dbHelper.AddInParameter(dbCommand, "@TemplateName", System.Data.DbType.String, templateName);
            //_dbHelper.AddInParameter(dbCommand, "@Status", System.Data.DbType.Int32, status);
            _dbHelper.AddInParameter(dbCommand, "@WeightType", System.Data.DbType.Int32, weightType);
            _dbHelper.AddInParameter(dbCommand, "@FuturesCopies", System.Data.DbType.Int32, futuresCopies);
            _dbHelper.AddInParameter(dbCommand, "@MarketCapOpt", System.Data.DbType.Decimal, marketCapOpt);
            _dbHelper.AddInParameter(dbCommand, "@BenchmarkId", System.Data.DbType.String, benchmarkId);
            _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, DateTime.Now);
            _dbHelper.AddInParameter(dbCommand, "@CreatedUserId", System.Data.DbType.Int32, userId);

            int templateId = _dbHelper.ExecuteNonQuery(dbCommand);
            return templateId;
        }

        public List<StockTemplate> GetTemplate(int userId)
        {
            List<StockTemplate> stockTemplates = new List<StockTemplate>();
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_GetTemplate);
            if (userId > 0)
            {
                _dbHelper.AddInParameter(dbCommand, "@UserId", System.Data.DbType.Int32, userId);
            }
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    StockTemplate item = new StockTemplate();
                    item.TemplateNo = (int)reader["TemplateId"];
                    item.TemplateName = (string)reader["TemplateName"];
                    item.FutureCopies = (int)reader["FuturesCopies"];
                    item.MarketCapOpt = (double)(decimal)reader["MarketCapOpt"];
                    item.WeightType = (int)reader["WeightType"];
                    //item.ReplaceType = (int)reader["ReplaceType"];
                    item.Benchmark = (string)reader["BenchmarkId"];

                    stockTemplates.Add(item);
                }
            }

            return stockTemplates;
        }

        public List<Benchmark> GetBenchmark()
        {
            List<Benchmark> benchmarks = new List<Benchmark>();
            string sql = "select BenchmarkId, BenchmarkName, Exchange from benchmark";
            var dbCommand = _dbHelper.GetSqlStringCommand(sql);
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Benchmark benchmark = new Benchmark();
                    benchmark.BenchmarkId = (string)reader[0];
                    benchmark.BenchmarkName = (string)reader[1];
                    benchmark.Exchange = (string)reader[2];
                    benchmarks.Add(benchmark);
                }
            }

            return benchmarks;
        }
    }
}
