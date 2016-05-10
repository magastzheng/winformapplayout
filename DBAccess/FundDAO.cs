using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class FundDAO : BaseDAO
    {
        private const string SP_CreateFund = "procInsertFund";
        private const string SP_ModifyFund = "procUpdateFund";
        private const string SP_DeleteFund = "procDeleteFund";
        private const string SP_GetFund = "procGetFunds";

        public FundDAO()
            : base()
        { 
        
        }

        public FundDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        
        }
    }
}
