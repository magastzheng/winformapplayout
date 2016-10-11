using Model.Quote;
using System.Collections.Generic;

namespace Quote
{
    public class Quote : IQuote
    {
        private readonly object locker = new object();
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
    }
}
