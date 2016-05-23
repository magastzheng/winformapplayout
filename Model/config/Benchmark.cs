using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.config
{
    public class Benchmark
    {
        public string BenchmarkId { get; set; }
        public string BenchmarkName { get; set; }
        public string Exchange { get; set; }
        public int ContractMultiple { get; set; }
    }
}
