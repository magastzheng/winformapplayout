using Model.SecurityInfo;
using Quote;
using System.Collections.Generic;

namespace BLL.SecurityInfo
{
    public class SecurityInfoManager
    {
        private static readonly SecurityInfoManager _instance = new SecurityInfoManager();
        private IQuote _quote;

        private SecurityInfoManager()
        {
            _quote = QuoteCenter2.Instance.Quote;
        }

        static SecurityInfoManager()
        {

        }

        public SecurityItem Get(string secuCode, SecurityType secuType)
        {
            var allSecuItems = Get();
            var secuItem = allSecuItems.Find(p => p.SecuCode.Equals(secuCode) && p.SecuType == secuType);
            if (secuItem == null)
            {
                secuItem = new SecurityItem
                {
                    SecuCode = secuCode,
                    SecuType = secuType,
                    ExchangeCode = SecurityItemHelper.GetExchangeCode(secuCode, secuType),
                };

                string investmentID = CodeHelper.GetWindCode(secuItem);
                Add(investmentID, secuItem);
            }

            return secuItem;
        }

        public SecurityItem Get(string secuCode, string exchangeCode)
        {
            var allSecuItems = Get();
            //var secuItem = allSecuItems.Find(p => p.SecuCode.Equals(secuCode) && (p.SecuType == SecurityType.Stock || p.SecuType == SecurityType.Futures));
            var secuItem = allSecuItems.Find(p => p.SecuCode.Equals(secuCode) && p.ExchangeCode.Equals(exchangeCode));
            if (secuItem == null)
            {
                secuItem = new SecurityItem
                {
                    SecuCode = secuCode,
                    SecuType = SecurityItemHelper.GetSecurityType(secuCode, exchangeCode),
                    ExchangeCode = exchangeCode,
                    //ExchangeCode = SecurityItemHelper.GetExchangeCode(secuCode),
                };

                string investmentID = CodeHelper.GetWindCode(secuItem);
                Add(investmentID, secuItem);
            }

            return secuItem;
        }

        public List<SecurityItem> Get()
        {
            return _quote.GetSecurities();
        }

        public static SecurityInfoManager Instance { get { return _instance; } }

        #region private method

        public void Add(string investmentID, SecurityItem secuItem)
        {
            _quote.AddSecurity(investmentID, secuItem);
        }

        #endregion
    }
}
