using Model.Quote;
using Model.SecurityInfo;
using System.Collections.Generic;

namespace Quote
{
    public class QuoteCenter2
    {
        private readonly static QuoteCenter2 _instance = new QuoteCenter2();
        private IQuote _quote = new Quote();
        public IQuote Quote
        {
            get { return _quote; }
        }

        private QuoteCenter2()
        {

        }

        public static QuoteCenter2 Instance { get { return _instance; } }

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
