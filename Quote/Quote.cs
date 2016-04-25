using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote
{
    public interface IQuote
    {
        Dictionary<string, MarketData> GetMarketData(List<string> instrumentIDs);
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

        public static QuoteCenter Instance { get { return _instance; } }
    }
}
