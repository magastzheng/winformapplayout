using DBAccess.Product;
using Model.EnumType;
using Model.strategy;
using Model.UI;
using System.Collections.Generic;

namespace BLL.Product
{
    public class ProductBLL
    {
        private UFXPortfolioDAO _portfoliodao = new UFXPortfolioDAO();

        public ProductBLL()
        { 
        }

        public int Create(List<AccountItem> funds, List<AssetItem> assets, List<PortfolioItem> portItems)
        {
            List<Portfolio> portfolios = new List<Portfolio>();
            foreach (var portItem in portItems)
            {
                Portfolio portfolio = new Portfolio 
                {
                    PortfolioNo = portItem.CombiNo,
                    PortfolioName = portItem.CombiName,
                    FundCode = portItem.AccountCode,
                    AssetNo = portItem.AssetNo,
                };

                var fund = funds.Find(o => o.AccountCode.Equals(portfolio.FundCode));
                if (fund != null)
                {
                    portfolio.FundName = fund.AccountName;
                    portfolio.EAccountType = fund.AccountType;
                }

                var asset = assets.Find(o => o.AssetNo.Equals(portfolio.AssetNo));
                if (asset != null)
                {
                    portfolio.AssetName = asset.AssetName;
                }

                portfolios.Add(portfolio);
            }

            return Create(portfolios);
        }

        public int Create(List<Portfolio> portfolios)
        {
            int ret = -1;

            var oldPortfolios = GetAll();

            foreach (var portfolio in portfolios)
            {
                var existedItem = oldPortfolios.Find(p => p.PortfolioNo.Equals(portfolio.PortfolioNo));
                if (existedItem != null)
                {
                    oldPortfolios.Remove(existedItem);
                }
                else
                {
                    ret = _portfoliodao.Create(portfolio);
                }
            }

            if (oldPortfolios.Count > 0)
            { 
                //TODO: update the status
            }

            return ret;
        }

        public int Update(List<Portfolio> portfolios)
        {
            int ret = -1;
            foreach (var portfolio in portfolios)
            {
                ret = _portfoliodao.UpdateName(portfolio);
            }

            return ret;
        }

        public int Delete(List<Portfolio> portfolios)
        {
            int ret = -1;
            foreach (var portfolio in portfolios)
            {
                ret = _portfoliodao.Delete(portfolio.PortfolioNo);
            }

            return ret;
        }

        public List<Portfolio> GetAll()
        {
            return _portfoliodao.Get(string.Empty);
        }

        public Portfolio Get(string portfolioCode)
        {
            var items = _portfoliodao.Get(portfolioCode);
            if (items.Count == 1)
            {
                return items[0];
            }
            else
            {
                return new Portfolio();
            }
        }
    }
}
