using Model.Quote;
using Model.SecurityInfo;
using System.Collections.Generic;

namespace Quote
{
    public interface IQuote
    {
        IDictionary<string, MarketData> GetMarketData(List<string> instrumentIDs);
        void Add(string instrumentID, MarketData marketData);
        MarketData Get(string instrumentID);
        IList<SecurityItem> GetSecurities();
        void AddSecurity(string investmentID, SecurityItem securityItem);
        SecurityItem GetSecurity(string investmentID);
        IList<SecurityItem> GetFuturesContract();
    }
}
