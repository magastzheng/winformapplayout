using Model.Quote;
using Model.SecurityInfo;
using System.Collections.Generic;

namespace Quote
{
    public interface IQuote
    {
        Dictionary<string, MarketData> GetMarketData(List<string> instrumentIDs);
        void Add(string instrumentID, MarketData marketData);
        MarketData Get(string instrumentID);
        List<SecurityItem> GetSecurities();
        void AddSecurity(string investmentID, SecurityItem securityItem);
        SecurityItem GetSecurity(string investmentID);
    }
}
