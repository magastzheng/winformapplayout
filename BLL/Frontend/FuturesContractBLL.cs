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
    }
}
