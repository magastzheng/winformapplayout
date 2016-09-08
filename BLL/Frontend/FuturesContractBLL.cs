using BLL.Template;
using DBAccess;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Frontend
{
    public class FuturesContractBLL
    {
        private FuturesContractDAO _fcdbdao = new FuturesContractDAO();
        private BenchmarkBLL _benchmarkBLL = new BenchmarkBLL();

        public FuturesContractBLL()
        { 
        }

        public List<FuturesContract> Get(string secuCode)
        {
            return _fcdbdao.Get(secuCode);
        }

        public List<FuturesContract> GetAll()
        {
            return _fcdbdao.Get("");
        }

        public double GetMoney(string futuresContract, int copies, double price)
        {
            int multiple = GetContractMultiple(futuresContract);
            double money = copies * multiple * price;

            return money;
        }

        #region private method

        private int GetContractMultiple(string futuresContract)
        {
            int multiple = 0;
            var benchmarks = _benchmarkBLL.GetAll();
            if (futuresContract.StartsWith("IC"))
            {
                var findItem = benchmarks.Find(p => p.BenchmarkId.Equals("000905") || p.BenchmarkId.Equals("399905"));
                if (findItem != null)
                {
                    multiple = findItem.ContractMultiple;
                }
            }
            else if (futuresContract.StartsWith("IF"))
            {
                var findItem = benchmarks.Find(p => p.BenchmarkId.Equals("000300") || p.BenchmarkId.Equals("399300"));
                if(findItem != null)
                {
                    multiple = findItem.ContractMultiple;
                }
            }
            else if (futuresContract.StartsWith("IH"))
            {
                var findItem = benchmarks.Find(p => p.BenchmarkId.Equals("000016"));
                if (findItem != null)
                {
                    multiple = findItem.ContractMultiple;
                }
            }
            else
            { 
                string msg = string.Format("未支持的股指期货合约: {0}", futuresContract);
                throw new NotSupportedException(msg);
            }

            return multiple;
        }

        #endregion
    }
}
