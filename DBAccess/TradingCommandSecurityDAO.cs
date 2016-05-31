using Model.TradingCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess
{
    public class TradingCommandSecurityDAO: BaseDAO
    {
        private const string SP_Create = "procTradingCommandSecurityInsert";
        private const string SP_Modify = "procTradingCommandSecurityUpdateEntrustAmount";
        private const string SP_Delete = "procTradingCommandSecurityDelete";
        private const string SP_Get = "procTradingCommandSecuritySelect";

        public TradingCommandSecurityDAO()
            : base()
        { 
            
        }

        public TradingCommandSecurityDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(TradingCommandSecurityItem secItem)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, secItem.CommandId);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, secItem.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)secItem.SecuType);
            _dbHelper.AddInParameter(dbCommand, "@WeightAmount", System.Data.DbType.Int32, secItem.WeightAmount);
            _dbHelper.AddInParameter(dbCommand, "@CommandAmount", System.Data.DbType.Int32, secItem.CommandAmount);
            _dbHelper.AddInParameter(dbCommand, "@CommandPrice", System.Data.DbType.Double, secItem.CommandPrice);
     
            return _dbHelper.ExecuteNonQuery(dbCommand);
        }
    }
}
