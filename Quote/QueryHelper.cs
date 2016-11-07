using DBAccess.SecurityInfo;
using Model.EnumType;
using Model.Quote;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;

namespace Quote
{
    public class QueryHelper
    {
        public static List<string> AllFields = new List<string>() { "rt_last", "rt_pre_close", "rt_amt", "rt_susp_flag", "rt_trade_status", "rt_high_limit", "rt_low_limit", "rt_upward_vol", "rt_downward_vol" };//, "rt_ask1", "rt_ask2", "rt_ask3", "rt_ask4", "rt_ask5", "rt_ask6", "rt_ask7", "rt_ask8", "rt_ask9", "rt_ask10", "rt_bid1", "rt_bid2", "rt_bid3", "rt_bid4", "rt_bid5", "rt_bid6", "rt_bid7", "rt_bid8", "rt_bid9", "rt_bid10" };
        private static Dictionary<PriceType, string> PriceTypeMap = new Dictionary<PriceType, string>() 
        {
            {PriceType.Market, "rt_last"},
            {PriceType.Last, "rt_last"},
            {PriceType.Automatic, "rt_last"},
            {PriceType.Sell1, "rt_ask1"},
            {PriceType.Sell2, "rt_ask2"},
            {PriceType.Sell3, "rt_ask3"},
            {PriceType.Sell4, "rt_ask4"},
            {PriceType.Sell5, "rt_ask5"},
            {PriceType.Sell6, "rt_ask5"},
            {PriceType.Sell7, "rt_ask5"},
            {PriceType.Sell8, "rt_ask5"},
            {PriceType.Sell9, "rt_ask5"},
            {PriceType.Sell10, "rt_ask5"},
            {PriceType.Buy1, "rt_bid1"},
            {PriceType.Buy2, "rt_bid2"},
            {PriceType.Buy3, "rt_bid3"},
            {PriceType.Buy4, "rt_bid4"},
            {PriceType.Buy5, "rt_bid5"},
            {PriceType.Buy6, "rt_bid5"},
            {PriceType.Buy7, "rt_bid5"},
            {PriceType.Buy8, "rt_bid5"},
            {PriceType.Buy9, "rt_bid5"},
            {PriceType.Buy10, "rt_bid5"},
        };

        private SecurityInfoDAO _dbdao = new SecurityInfoDAO();
        //private List<string> _fields = new List<string>() { "rt_last", "rt_amt", "rt_trade_status", "rt_high_limit", "rt_low_limit", "rt_upward_vol", "rt_downward_vol", "rt_ask1", "rt_ask2", "rt_ask3", "rt_ask4", "rt_ask5", "rt_ask6", "rt_ask7", "rt_ask8", "rt_ask9", "rt_ask10", "rt_bid1", "rt_bid2", "rt_bid3", "rt_bid4", "rt_bid5", "rt_bid6", "rt_bid7", "rt_bid8", "rt_bid9", "rt_bid10" };

        //private List<string> _fields = new List<string>() { "rt_last" };
        //000001<->000001.sz
        private Dictionary<string, string> _secuCodeMap = new Dictionary<string, string>();
        private List<SecurityItem> _secuItemList = new List<SecurityItem>();

        #region private method

        private List<SecurityItem> GetSecuList()
        {
            if (_secuItemList == null || _secuItemList.Count == 0)
            {
                _secuItemList = _dbdao.Get(SecurityType.All);
            }

            return _secuItemList;
        }

        #endregion

        #region public method

        //public List<string> GetFields()
        //{
        //    return _fields;
        //}

        public List<string> GetPriceFields(List<PriceType> priceTypes)
        {
            List<string> fields = new List<string>();
            foreach (var priceType in priceTypes)
            {
                if (PriceTypeMap.ContainsKey(priceType))
                {
                    if (!fields.Contains(PriceTypeMap[priceType]))
                    {
                        fields.Add(PriceTypeMap[priceType]);
                    }
                }
            }

            //add the necessary fields
            foreach (var field in AllFields)
            {
                if (!fields.Contains(field))
                {
                    fields.Add(field);
                }
            }

            return fields;
        }

        public List<string> GetSecuCode()
        {
            List<string> secuCodeList = new List<string>();

            List<SecurityItem> secuItemList = GetSecuList();
            if (secuItemList != null && secuItemList.Count > 0)
            {
                foreach (var secuItem in secuItemList)
                {
                    secuCodeList.Add(GetWindCode(secuItem));
                }
            }

            return secuCodeList;
        }

        public List<string> GetSecuCode(List<string> secuCodes)
        {
            List<string> secuCodeList = new List<string>();
            List<SecurityItem> secuItemList = GetSecuList();
            if (secuItemList == null)
            {
                return secuCodeList;
            }

            foreach (var secuCode in secuCodes)
            {
                var secuItems = secuItemList.FindAll(p => p.SecuCode.Equals(secuCode));
                if (secuItems == null)
                {
                    continue;
                }

                foreach (var secuItem in secuItems)
                {
                    var windcode = GetWindCode(secuItem);
                    if (!secuCodeList.Contains(windcode))
                    {
                        secuCodeList.Add(windcode);
                    }
                }
            }

            return secuCodeList;
        }

        public List<string> GetSecuCode(List<SecurityItem> secuItems)
        {
            List<string> secuCodeList = new List<string>();
            foreach (var secuItem in secuItems)
            {
                var windcode = GetWindCode(secuItem);
                if (!secuCodeList.Contains(windcode))
                {
                    secuCodeList.Add(windcode);
                }
            }

            return secuCodeList;
        }

        #endregion

        #region static method

        public static string GetWindCode(SecurityItem secuItem)
        {
            string windCode = secuItem.SecuCode;
            if (!string.IsNullOrEmpty(secuItem.ExchangeCode))
            {
                if (secuItem.ExchangeCode.Equals(Exchange.SHSE, StringComparison.OrdinalIgnoreCase))
                {
                    windCode += ".SH";
                }
                else if (secuItem.ExchangeCode.Equals(Exchange.SZSE, StringComparison.OrdinalIgnoreCase))
                {
                    windCode += ".SZ";
                }
                else if (secuItem.ExchangeCode.Equals(Exchange.CFFEX, StringComparison.OrdinalIgnoreCase))
                {
                    windCode += ".CF";
                }
            }
            else
            { 
                
            }

            return windCode;
        }

        public static Dictionary<string, int> GetFieldIndex(List<string> fieldList)
        {
            Dictionary<string, int> fieldIndexMap = new Dictionary<string, int>();

            for (int i = 0, count = fieldList.Count; i < count; i++)
            {
                if (!fieldIndexMap.ContainsKey(fieldList[i]))
                {
                    fieldIndexMap.Add(fieldList[i], i);
                }
            }

            return fieldIndexMap;
        }

        public static SuspendFlag GetSuspendFlag(int flagCode)
        {
            flagCode = flagCode % 10;
            SuspendFlag flag = SuspendFlag.Unknown;
            if (Enum.IsDefined(typeof(SuspendFlag), flagCode))
            {
                flag = (SuspendFlag)Enum.ToObject(typeof(SuspendFlag), flagCode);
            }

            return flag;
        }

        public static TradingStatus GetTradingStatus(int statusCode)
        {
            TradingStatus status = TradingStatus.Unknown;
            if (Enum.IsDefined(typeof(TradingStatus), statusCode))
            {
                status = (TradingStatus)Enum.ToObject(typeof(TradingStatus), statusCode);
            }

            return status;
        }
        #endregion
    }
}
