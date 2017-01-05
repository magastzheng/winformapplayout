using DBAccess.SecurityInfo;
using Model.config;
using Model.SecurityInfo;
using Quote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SecurityInfo
{
    public class FuturesContractManager
    {
        private const int MultipleIH = 300;
        private const int MultipleIF = 300;
        private const int MultipleIC = 200;

        private static readonly FuturesContractManager _instance = new FuturesContractManager();
        private IQuote _quote;
        private BenchmarkBLL _benchmarkBLL;
        private FuturesContractDAO _futurescontractdao;

        private List<SecurityItem> _futuresItems;
        private List<Benchmark> _benchmarks;

        private FuturesContractManager()
        {
            _quote = QuoteCenter2.Instance.Quote;
            _benchmarkBLL = new BenchmarkBLL();
            _futurescontractdao = new FuturesContractDAO();
            _futuresItems = new List<SecurityItem>();
            _benchmarks = new List<Benchmark>();
        }

        public static FuturesContractManager Instance { get { return _instance; } }

        public List<SecurityItem> GetAll()
        {
            if (_futuresItems == null)
            {
                _futuresItems = new List<SecurityItem>();
            }

            if (_futuresItems.Count == 0)
            {
                var quoteItems = _quote.GetFuturesContract();
                if (quoteItems != null && quoteItems.Count > 0)
                {
                    _futuresItems.AddRange(quoteItems);
                }

                var dbItems = _futurescontractdao.GetAll();
                if (dbItems != null && dbItems.Count > 0)
                {
                    foreach (var dbItem in dbItems)
                    {
                        var findItem = _futuresItems.Find(p => p.SecuCode.Equals(dbItem.Code));
                        if (findItem == null)
                        {
                            SecurityItem newItem = new SecurityItem
                            {
                                SecuCode = dbItem.Code,
                                SecuName = dbItem.Name,
                                SecuType = SecurityType.Futures,
                                ExchangeCode = dbItem.Exchange,
                            };

                            _futuresItems.Add(newItem);
                        }
                    }
                }
            }

            return _futuresItems;
        }

        public SecurityItem Get(string secuCode)
        {
            var allItems = GetAll();
            var target = allItems.Find(p => p.SecuCode.Equals(secuCode));

            return target;
        }

        public double GetMoney(string futuresContract, int copies, double price)
        {
            int multiple = GetContractMultiple(futuresContract);
            double money = copies * multiple * price;

            return money;
        }

        #region

        private int GetContractMultiple(string futuresContract)
        {
            int multiple = 0;
            var benchmarks = GetBenchmarks();
            if (futuresContract.StartsWith("IC"))
            {
                var findItem = benchmarks.Find(p => p.BenchmarkId.Equals("000905") || p.BenchmarkId.Equals("399905"));
                if (findItem != null)
                {
                    multiple = findItem.ContractMultiple;
                }
                else
                {
                    multiple = MultipleIC;
                }
            }
            else if (futuresContract.StartsWith("IF"))
            {
                var findItem = benchmarks.Find(p => p.BenchmarkId.Equals("000300") || p.BenchmarkId.Equals("399300"));
                if (findItem != null)
                {
                    multiple = findItem.ContractMultiple;
                }
                else
                {
                    multiple = MultipleIF;
                }
            }
            else if (futuresContract.StartsWith("IH"))
            {
                var findItem = benchmarks.Find(p => p.BenchmarkId.Equals("000016"));
                if (findItem != null)
                {
                    multiple = findItem.ContractMultiple;
                }
                else
                {
                    multiple = MultipleIH;
                }
            }
            else
            {
                string msg = string.Format("未支持的股指期货合约: {0}", futuresContract);
                throw new NotSupportedException(msg);
            }

            return multiple;
        }

        private List<Benchmark> GetBenchmarks()
        {
            if (_benchmarks == null)
            {
                _benchmarks = new List<Benchmark>();
            }

            if (_benchmarks.Count == 0)
            {
                var dbItems = _benchmarkBLL.GetAll();
                if (dbItems != null && dbItems.Count > 0)
                {
                    _benchmarks.AddRange(dbItems);
                }
            }

            return _benchmarks;
        }
        #endregion
    }
}
