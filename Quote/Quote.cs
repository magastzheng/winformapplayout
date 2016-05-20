using DBAccess;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAPIWrapperCSharp;

namespace Quote
{
    public interface IQuote
    {
        Dictionary<string, MarketData> GetMarketData(List<string> instrumentIDs);
    }

    public class QueryHelper
    {
        private SecurityInfoDAO _dbdao = new SecurityInfoDAO();
        //private List<string> _fields = new List<string>() { "rt_last", "rt_amt", "rt_trade_status", "rt_high_limit", "rt_low_limit", "rt_upward_vol", "rt_downward_vol", "rt_ask1", "rt_ask2", "rt_ask3", "rt_ask4", "rt_ask5", "rt_ask6", "rt_ask7", "rt_ask8", "rt_ask9", "rt_ask10", "rt_bid1", "rt_bid2", "rt_bid3", "rt_bid4", "rt_bid5", "rt_bid6", "rt_bid7", "rt_bid8", "rt_bid9", "rt_bid10" };
        private List<string> _fields = new List<string>() { "rt_last" };
        //000001<->000001.sz
        private Dictionary<string, string> _secuCodeMap = new Dictionary<string, string>();
        private List<SecurityItem> _secuItemList = new List<SecurityItem>();

        #region private method

        private string GetWindCode(SecurityItem secuItem)
        {
            string windCode = secuItem.SecuCode;
            if (secuItem.ExchangeCode.Equals("SSE", StringComparison.OrdinalIgnoreCase))
            {
                windCode += ".sh";
            }
            else if (secuItem.ExchangeCode.Equals("SZSE", StringComparison.OrdinalIgnoreCase))
            {
                windCode += ".sz";
            }

            return windCode;
        }

        private List<SecurityItem> GetSecuList()
        {
            if (_secuItemList == null || _secuItemList.Count == 0)
            {
                _secuItemList = _dbdao.Get(-1);
            }

            return _secuItemList;
        }

        #endregion

        #region public method

        public List<string> GetFields()
        {
            return _fields;
        }

        public Dictionary<string, int> GetFieldIndex()
        {
            Dictionary<string, int> fieldIndexMap = new Dictionary<string, int>();

            for (int i = 0, count = _fields.Count; i < count; i++)
            {
                if (!fieldIndexMap.ContainsKey(_fields[i]))
                {
                    fieldIndexMap.Add(_fields[i], i);
                }
            }

            return fieldIndexMap;
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
    }

    public class Quote : IQuote
    {
        private Dictionary<string, MarketData> _marketDatas = new Dictionary<string, MarketData>();
        public Dictionary<string, MarketData> MarketDatas { get { return _marketDatas; } }

        public Dictionary<string, MarketData> GetMarketData(List<string> instrumentIDs)
        {
            Dictionary<string, MarketData> dataMap = new Dictionary<string, MarketData>();
            foreach (string instrumentID in instrumentIDs)
            {
                if (_marketDatas.ContainsKey(instrumentID))
                {
                    dataMap[instrumentID] = _marketDatas[instrumentID];
                }
            }

            return dataMap;
        }
    }

    public class QuoteCenter
    {
        private readonly static QuoteCenter _instance = new QuoteCenter();
        private Quote _quote = new Quote();
        public Quote Quote
        {
            get { return _quote; }
        }

        private QuoteCenter()
        { 
        
        }

        public void Query()
        {
            QueryHelper queryHelper = new QueryHelper();
            List<string> windCodes = queryHelper.GetSecuCode();
            List<string> fields = queryHelper.GetFields();
            Dictionary<string, string> optionMap = new Dictionary<string, string>();
            Dictionary<string, int> fieldIndexMap = queryHelper.GetFieldIndex();

            WindData wd = WindAPIWrap.Instance.SyncRequestData(windCodes, fields, optionMap);
            if (wd != null)
            {
                FillData(wd, fieldIndexMap);
            }
        }

        public void Query(List<string> secuCodes)
        { 
            QueryHelper queryHelper = new QueryHelper();
            List<string> windCodes = queryHelper.GetSecuCode(secuCodes);
            List<string> fields = queryHelper.GetFields();
            Dictionary<string, string> optionMap = new Dictionary<string,string>();
            Dictionary<string, int> fieldIndexMap = queryHelper.GetFieldIndex();

            WindData wd = WindAPIWrap.Instance.SyncRequestData(windCodes, fields, optionMap);
            if (wd != null)
            {
                FillData(wd, fieldIndexMap);
            }
        }

        public void Quety(List<SecurityItem> secuItems)
        {
            QueryHelper queryHelper = new QueryHelper();
            List<string> windCodes = queryHelper.GetSecuCode(secuItems);
            List<string> fields = queryHelper.GetFields();
            Dictionary<string, string> optionMap = new Dictionary<string, string>();
            Dictionary<string, int> fieldIndexMap = queryHelper.GetFieldIndex();

            WindData wd = WindAPIWrap.Instance.SyncRequestData(windCodes, fields, optionMap);
            if (wd != null)
            {
                FillData(wd, fieldIndexMap);
            }
        }

        private void FillData(WindData wd, Dictionary<string, int> fieldIndexMap)
        {
            int clength = wd.codeList.Length;
            int flength = wd.fieldList.Length;
            int tlength = wd.timeList.Length;
            for (int i = 0; i < tlength; i++)
            {
                int serialLen = clength * flength;

                //loop for the code
                for (int j = 0; j < clength; j++)
                {
                    MarketData marketData = null;
                    string investmentId = wd.codeList[j];
                    if (!_quote.MarketDatas.ContainsKey(investmentId))
                    {
                        marketData = new MarketData
                        {
                            InstrumentID = wd.codeList[j]
                        };

                        _quote.MarketDatas.Add(marketData.InstrumentID, marketData);
                    }
                    else
                    {
                        marketData = _quote.MarketDatas[marketData.InstrumentID];
                    }
                    
                    //loop for the field
                    if (wd.data is Array)
                    {
                        foreach (var kv in fieldIndexMap)
                        {
                            int index = kv.Value;
                            double dval = (double)((Array)wd.data).GetValue(i * serialLen + j * flength + index);
                            switch (kv.Key)
                            {
                                case "rt_last":
                                    marketData.CurrentPrice = dval;
                                    break;
                                case "rt_ask1":
                                    marketData.BuyPrice1 = dval;
                                    break;
                                case "rt_ask2":
                                    marketData.BuyPrice2 = dval;
                                    break;
                                case "rt_ask3":
                                    marketData.BuyPrice3 = dval;
                                    break;
                                case "rt_ask4":
                                    marketData.BuyPrice4 = dval;
                                    break;
                                case "rt_ask5":
                                    marketData.BuyPrice5 = dval;
                                    break;
                                case "rt_ask6":
                                    break;
                                case "rt_ask7":
                                    break;
                                case "rt_ask8":
                                    break;
                                case "rt_ask9":
                                    break;
                                case "rt_ask10":
                                    break;
                                case "rt_bid1":
                                    marketData.SellPrice1 = dval;
                                    break;
                                case "rt_bid2":
                                    marketData.SellPrice2 = dval;
                                    break;
                                case "rt_bid3":
                                    marketData.SellPrice3 = dval;
                                    break;
                                case "rt_bid4":
                                    marketData.SellPrice4 = dval;
                                    break;
                                case "rt_bid5":
                                    marketData.SellPrice5 = dval;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }//end to fill read the field and fill into MarketData object
                }//end to loop secucode
            }//end to loop datetime
        }

        public static QuoteCenter Instance { get { return _instance; } }
    }
}
