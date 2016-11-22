using Model.Quote;
using Model.SecurityInfo;
using System.Collections.Generic;
using System.Linq;

namespace Quote
{
    public class Quote : IQuote
    {
        private readonly object locker = new object();
        private Dictionary<string, MarketData> _marketDatas = new Dictionary<string, MarketData>();
        private Dictionary<string, SecurityItem> _securityDatas = new Dictionary<string, SecurityItem>();

        public Dictionary<string, MarketData> MarketDatas { get { return _marketDatas; } }

        #region interface

        public List<SecurityItem> GetSecurities()
        {
            return _securityDatas.Values.ToList();
        }

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

        public void Add(string instrumentID, MarketData marketData)
        {
            lock (locker)
            {
                if (!_marketDatas.ContainsKey(instrumentID))
                {
                    _marketDatas.Add(instrumentID, marketData);
                }
                else
                {
                    _marketDatas[instrumentID] = marketData;
                }
            }
        }

        public MarketData Get(string instrumentID)
        {
            if (!_marketDatas.ContainsKey(instrumentID))
            {
                MarketData marketData = new MarketData 
                {
                    InstrumentID = instrumentID,
                };

                Add(instrumentID, marketData);
            }

            return _marketDatas[instrumentID];
        }

        public void AddSecurity(string investmentID, SecurityItem securityItem)
        {
            if (!_securityDatas.ContainsKey(investmentID))
            {
                _securityDatas.Add(investmentID, securityItem);
            }
            else
            {
                _securityDatas[investmentID] = securityItem;
            }
        }

        public SecurityItem GetSecurity(string investmentID)
        {
            if (!_securityDatas.ContainsKey(investmentID))
            {
                int pos = investmentID.IndexOf('.');
                string secuCode = string.Empty;
                if (pos > 0)
                {
                    secuCode = investmentID.Substring(0, pos);
                }
                else
                {
                    secuCode = investmentID;
                }

                SecurityItem securityItem = new SecurityItem 
                {
                    SecuCode = secuCode,
                };

                AddSecurity(investmentID, securityItem);
            }

            return _securityDatas[investmentID];
        }

        public List<SecurityItem> GetFuturesContract()
        {
            List<SecurityItem> futuresItems = new List<SecurityItem>();

            var futures = _securityDatas.Values.Where(p => p.SecuType == SecurityType.Futures).ToList();
            if (futures != null)
            {
                futuresItems.AddRange(futures);
            }

            return futuresItems;
        }
        #endregion

    }
}
