using log4net;
using Model.config;
using System.Collections.Generic;

namespace DBAccess.SecurityInfo
{
    public class BenchmarkDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_Create = "procBenchmarkInsert";
        private const string SP_Modify = "procBenchmarkUpdate";
        private const string SP_Delete = "procBenchmarkDelete";
        private const string SP_Get = "procBenchmarkSelect";

        public BenchmarkDAO()
            : base()
        { 
            
        }

        public BenchmarkDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(Benchmark benchmark)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@BenchmarkId", System.Data.DbType.String, benchmark.BenchmarkId);
            _dbHelper.AddInParameter(dbCommand, "@BenchmarkName", System.Data.DbType.String, benchmark.BenchmarkName);
            _dbHelper.AddInParameter(dbCommand, "@Exchange", System.Data.DbType.String, benchmark.Exchange);
            _dbHelper.AddInParameter(dbCommand, "@ContractMultiple", System.Data.DbType.Int32, benchmark.ContractMultiple);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Update(Benchmark benchmark)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Modify);
            _dbHelper.AddInParameter(dbCommand, "@BenchmarkId", System.Data.DbType.String, benchmark.BenchmarkId);
            _dbHelper.AddInParameter(dbCommand, "@BenchmarkName", System.Data.DbType.String, benchmark.BenchmarkName);
            _dbHelper.AddInParameter(dbCommand, "@Exchange", System.Data.DbType.String, benchmark.Exchange);
            _dbHelper.AddInParameter(dbCommand, "@ContractMultiple", System.Data.DbType.Int32, benchmark.ContractMultiple);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(string benchmarkId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Modify);
            _dbHelper.AddInParameter(dbCommand, "@BenchmarkId", System.Data.DbType.String, benchmarkId);
            
            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public Benchmark Get(string benchmarkId)
        {
            Benchmark item = new Benchmark();
            var items = GetInternal(benchmarkId);
            if (items != null && items.Count > 0)
            {
                item = items[0];
            }

            return item;
        }

        public List<Benchmark> GetAll()
        {
            return GetInternal(string.Empty);
        }

        #region private method

        public List<Benchmark> GetInternal(string benchmarkId)
        { 
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);
            if (!string.IsNullOrEmpty(benchmarkId))
            {
                _dbHelper.AddInParameter(dbCommand, "@BenchmarkId", System.Data.DbType.String, benchmarkId);
            }

            List<Benchmark> items = new List<Benchmark>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Benchmark benchmark = new Benchmark();
                    benchmark.BenchmarkId = (string)reader["BenchmarkId"];
                    benchmark.BenchmarkName = (string)reader["BenchmarkName"];
                    benchmark.Exchange = (string)reader["Exchange"];
                    benchmark.ContractMultiple = (int)reader["ContractMultiple"];

                    items.Add(benchmark);
                }
            }

            reader.Close();
            _dbHelper.Close(dbCommand);

            return items;
        }

        #endregion
    }
}
