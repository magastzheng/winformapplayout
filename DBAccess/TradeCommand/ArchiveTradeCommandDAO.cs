using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.TradeCommand
{
    public class ArchiveTradeCommandDAO : BaseDAO
    {
        public ArchiveTradeCommandDAO()
            : base()
        { 
        }

        public ArchiveTradeCommandDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(ArchiveTradeCommand item)
        {
            return -1;
        }
    }
}
