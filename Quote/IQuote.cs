using Model.Quote;
using System.Collections.Generic;

namespace Quote
{
    public interface IQuote
    {
        Dictionary<string, MarketData> GetMarketData(List<string> instrumentIDs);
        void Add(string instrumentID, MarketData marketData);
        MarketData Get(string instrumentID);
    }
}
