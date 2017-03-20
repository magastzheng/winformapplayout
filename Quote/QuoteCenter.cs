using Model.Quote;
using Model.SecurityInfo;
using System.Collections.Generic;

namespace Quote
{
    public class QuoteCenter
    {
        private readonly static QuoteCenter _instance = new QuoteCenter();
        private IQuote _quote = new Quote();
        public IQuote Quote
        {
            get { return _quote; }
        }

        private QuoteCenter()
        {

        }

        public static QuoteCenter Instance { get { return _instance; } }

        public MarketData GetMarketData(SecurityItem secuItem)
        {
            string windCode = CodeHelper.GetWindCode(secuItem);

            MarketData marketData = new MarketData
            {
                InstrumentID = windCode
            };


            return _quote.Get(windCode);
        }

        public List<SecurityItem> GetSecurities()
        {
            return _quote.GetSecurities();
        }
    }
}
