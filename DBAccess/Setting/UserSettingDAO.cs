using log4net;
using Model.EnumType;
using Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Setting
{
    public class UserSettingDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_CreateOrUpdate = "procUserSettingInsertOrUpdate";
        private const string SP_Delete = "procUserSettingDelete";
        private const string SP_Select = "procUserSettingSelect";

        public UserSettingDAO()
            : base()
        { 
        
        }

        public UserSettingDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        
        }

        public int Create(int userId, DefaultSetting setting)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_CreateOrUpdate);
            _dbHelper.AddInParameter(dbCommand, "@UserId", System.Data.DbType.Int32, userId);
            _dbHelper.AddInParameter(dbCommand, "@ConnectTimeout", System.Data.DbType.Int32, setting.Timeout);
            _dbHelper.AddInParameter(dbCommand, "@UFXTimeout", System.Data.DbType.Int32, setting.UFXSetting.Timeout);
            _dbHelper.AddInParameter(dbCommand, "@UFXLimitEntrustRatio", System.Data.DbType.Int32, setting.UFXSetting.LimitEntrustRatio);
            _dbHelper.AddInParameter(dbCommand, "@UFXFutuLimitEntrustRatio", System.Data.DbType.Int32, setting.UFXSetting.FutuLimitEntrustRatio);
            _dbHelper.AddInParameter(dbCommand, "@UFXOptLimitEntrustRatio", System.Data.DbType.Int32, setting.UFXSetting.OptLimitEntrustRatio);
            _dbHelper.AddInParameter(dbCommand, "@BuyFutuPrice", System.Data.DbType.Int32, (int)setting.EntrustSetting.BuyFutuPrice);
            _dbHelper.AddInParameter(dbCommand, "@SellFutuPrice", System.Data.DbType.Int32, (int)setting.EntrustSetting.SellFutuPrice);
            _dbHelper.AddInParameter(dbCommand, "@BuySpotPrice", System.Data.DbType.Int32, (int)setting.EntrustSetting.BuySpotPrice);
            _dbHelper.AddInParameter(dbCommand, "@SellSpotPrice", System.Data.DbType.Int32, (int)setting.EntrustSetting.SellSpotPrice);
            _dbHelper.AddInParameter(dbCommand, "@BuySellEntrustOrder", System.Data.DbType.Int32, (int)setting.EntrustSetting.BuySellEntrustOrder);
            _dbHelper.AddInParameter(dbCommand, "@OddShareMode", System.Data.DbType.Int32, (int)setting.EntrustSetting.OddShareMode);
            _dbHelper.AddInParameter(dbCommand, "@SZSEEntrustPriceType", System.Data.DbType.Int32, (int)setting.EntrustSetting.SzseEntrustPriceType);
            _dbHelper.AddInParameter(dbCommand, "@SSEEntrustPriceType", System.Data.DbType.Int32, (int)setting.EntrustSetting.SseEntrustPriceType);


            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public int Delete(int userId)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@UserId", System.Data.DbType.Int32, userId);

            return _dbHelper.ExecuteNonQuery(dbCommand);
        }

        public DefaultSetting Get(int userId)
        {
            DefaultSetting setting = new DefaultSetting 
            {
                UFXSetting = new DefaultUFXSetting(),
                EntrustSetting = new DefaultEntrustSetting(),
            };

            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);
            _dbHelper.AddInParameter(dbCommand, "@UserId", System.Data.DbType.Int32, userId);
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows && reader.Read())
            {
                setting.Timeout = (int)reader["ConnectTimeout"];
                setting.UFXSetting.Timeout = (int)reader["UFXTimeout"];
                setting.UFXSetting.LimitEntrustRatio = (int)reader["UFXLimitEntrustRatio"];
                setting.UFXSetting.FutuLimitEntrustRatio = (int)reader["UFXFutuLimitEntrustRatio"];
                setting.UFXSetting.OptLimitEntrustRatio = (int)reader["UFXOptLimitEntrustRatio"];
                setting.EntrustSetting.BuyFutuPrice = (PriceType)reader["BuyFutuPrice"];
                setting.EntrustSetting.SellFutuPrice = (PriceType)reader["SellFutuPrice"];
                setting.EntrustSetting.BuySpotPrice = (PriceType)reader["BuySpotPrice"];
                setting.EntrustSetting.SellSpotPrice = (PriceType)reader["SellSpotPrice"];
                setting.EntrustSetting.BuySellEntrustOrder = (BuySellEntrustOrder)reader["BuySellEntrustOrder"];
                setting.EntrustSetting.OddShareMode = (OddShareMode)reader["OddShareMode"];
                setting.EntrustSetting.SzseEntrustPriceType = (EntrustPriceType)reader["SZSEEntrustPriceType"];
                setting.EntrustSetting.SseEntrustPriceType = (EntrustPriceType)reader["SSEEntrustPriceType"];
            }
            reader.Close();
            _dbHelper.Close(dbCommand.Connection);

            return setting;
        }
    }
}
