using DBAccess.SecurityInfo;
using Model.config;
using System.Collections.Generic;

namespace BLL.Template
{
    public class BenchmarkBLL
    {
        private BenchmarkDAO _benchmarkdao = new BenchmarkDAO();
        public BenchmarkBLL()
        { 
        }

        public int Create(Benchmark benchmark)
        {
            return _benchmarkdao.Create(benchmark);
        }

        public int Update(Benchmark benchmark)
        {
            return _benchmarkdao.Update(benchmark);
        }

        public int Delete(string benchmarkId)
        {
            return _benchmarkdao.Delete(benchmarkId);
        }

        public List<Benchmark> GetAll()
        {
            return _benchmarkdao.Get(string.Empty);
        }
    }
}
