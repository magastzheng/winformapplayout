using Model.Quote;
using Model.SecurityInfo;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Quote
{
    public class Quote : IQuote
    {
        private readonly object locker = new object();
        private IDictionary<string, MarketData> _marketDatas = new ConcurrentDictionary<string, MarketData>();// Dictionary<string, MarketData>();
        private IDictionary<string, SecurityItem> _securityDatas = new ConcurrentDictionary<string, SecurityItem>();

        public IDictionary<string, MarketData> MarketDatas { get { return _marketDatas; } }

        #region interface

        public IList<SecurityItem> GetSecurities()
        {
            return _securityDatas.Values.ToList();
        }

        public IDictionary<string, MarketData> GetMarketData(List<string> instrumentIDs)
        {
            IDictionary<string, MarketData> dataMap = new Dictionary<string, MarketData>();
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
            if (!_marketDatas.ContainsKey(instrumentID))
            {
                _marketDatas.Add(instrumentID, marketData);
            }
            else
            {
                _marketDatas[instrumentID] = marketData;
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

        public IList<SecurityItem> GetFuturesContract()
        {
            IList<SecurityItem> futuresItems = new List<SecurityItem>();

            var futures = _securityDatas.Values.Where(p => p.SecuType == SecurityType.Futures).ToList();
            if (futures != null)
            {
                futures.ForEach(p => futuresItems.Add(p));
            }
            
            return futuresItems;
        }
        #endregion

    }
}
