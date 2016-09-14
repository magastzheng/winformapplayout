using Model.UI;
using System;
using System.Collections.Generic;

namespace DBAccess.SecurityInfo
{
    public class FuturesContractDAO: BaseDAO
    {
        private const string SP_Create = "procFuturesContractInsert";
        private const string SP_Get = "procFuturesContractSelect";

        public FuturesContractDAO()
            : base()
        { 
            
        }

        public FuturesContractDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Create(FuturesContract item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@Code", System.Data.DbType.String, item.Code);
            _dbHelper.AddInParameter(dbCommand, "@Name", System.Data.DbType.String, item.Name);
            _dbHelper.AddInParameter(dbCommand, "@Exchange", System.Data.DbType.String, item.Exchange);
            _dbHelper.AddInParameter(dbCommand, "@PriceLimits", System.Data.DbType.Decimal, item.PriceLimits);
            _dbHelper.AddInParameter(dbCommand, "@Deposit", System.Data.DbType.Decimal, item.Deposit);
            _dbHelper.AddInParameter(dbCommand, "@ListedDate", System.Data.DbType.DateTime, item.FirstTradingDay);
            _dbHelper.AddInParameter(dbCommand, "@LastTradingDay", System.Data.DbType.DateTime, item.LastTradingDay);
            _dbHelper.AddInParameter(dbCommand, "@LastDeliveryDay", System.Data.DbType.DateTime, item.LastDeliveryDay);

            
            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public List<FuturesContract> Get(string code)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Get);
            if (!string.IsNullOrEmpty(code))
            {
                _dbHelper.AddInParameter(dbCommand, "@Code", System.Data.DbType.String, code);
            }

            List<FuturesContract> itemList = new List<FuturesContract>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    FuturesContract item = new FuturesContract();
                    item.Code = (string)reader["Code"];
                    item.Name = (string)reader["Name"];
                    item.Exchange = (string)reader["Exchange"];
                    item.PriceLimits = (double)(decimal)reader["PriceLimits"];
                    item.Deposit = (double)(decimal)reader["Deposit"];
                    if (reader["ListedDate"] != null && reader["ListedDate"] != DBNull.Value)
                    {
                        item.FirstTradingDay = (DateTime)reader["ListedDate"];
                    }
                    if (reader["LastTradingDay"] != DBNull.Value)
                    {
                        item.LastTradingDay = (DateTime)reader["LastTradingDay"];
                    }

                    if (reader["LastDeliveryDay"] != DBNull.Value)
                    {
                        item.LastDeliveryDay = (DateTime)reader["LastDeliveryDay"];
                    }

                    itemList.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return itemList;
        }
    }
}
