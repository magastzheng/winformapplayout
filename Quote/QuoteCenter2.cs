using Model.Quote;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote
{
    public class QuoteCenter2
    {
        private readonly static QuoteCenter2 _instance = new QuoteCenter2();
        private Quote _quote = new Quote();
        public Quote Quote
        {
            get { return _quote; }
        }

        private QuoteCenter2()
        {

        }

        public static QuoteCenter2 Instance { get { return _instance; } }

        public MarketData GetMarketData(SecurityItem secuItem)
        {
            string windCode = QueryHelper.GetWindCode(secuItem);

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
