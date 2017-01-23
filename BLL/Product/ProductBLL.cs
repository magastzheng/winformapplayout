using BLL.Permission;
using Config;
using DBAccess.Product;
using Model.Permission;
using Model.strategy;
using Model.UI;
using System.Collections.Generic;

namespace BLL.Product
{
    public class ProductBLL
    {
        private UFXPortfolioDAO _portfoliodao = new UFXPortfolioDAO();
        private PermissionManager _permissionManager = new PermissionManager();

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
                else
                {
                    portfolio.FundName = string.Empty;
                    portfolio.EAccountType = Model.EnumType.FundAccountType.OpenEndFund;
                }

                var asset = assets.Find(o => o.AssetNo.Equals(portfolio.AssetNo));
                if (asset != null)
                {
                    portfolio.AssetName = asset.AssetName;
                }
                else
                {
                    portfolio.AssetName = string.Empty;
                }

                portfolios.Add(portfolio);
            }

            return Create(portfolios);
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
            var userId = LoginManager.Instance.GetUserId();
            var allPortfolios = _portfoliodao.GetAll();
            var validPortfolios = new List<Portfolio>();
            foreach (var portfolio in allPortfolios)
            {
                if (HasPermission(userId, portfolio))
                {
                    validPortfolios.Add(portfolio);
                }
            }

            return validPortfolios;
        }

        public Portfolio Get(string portfolioCode, int userId)
        {
            Portfolio portfolio = _portfoliodao.Get(portfolioCode);
            if (HasPermission(userId, portfolio))
            {
                return portfolio;
            }
            else
            { 
                return new Portfolio();
            }
        }

        public Portfolio GetById(int portfolioId)
        {
            return _portfoliodao.GetById(portfolioId);
        }

        #region private methods

        private bool HasPermission(int userId, Portfolio portfolio)
        {
            return _permissionManager.HasPermission(userId, portfolio.PortfolioId, ResourceType.Portfolio, Model.Permission.PermissionMask.View);
        }

        private int Create(List<Portfolio> portfolios)
        {
            int ret = -1;
            var userId = LoginManager.Instance.GetUserId();
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
                    if (ret > 0)
                    {
                        _permissionManager.GrantPermission(userId, ret, ResourceType.Portfolio, PermissionMask.View);
                    }
                }
            }

            if (oldPortfolios.Count > 0)
            {
                //TODO: update the status
                foreach (var portfolio in oldPortfolios)
                {
                    _permissionManager.GrantPermission(userId, portfolio.PortfolioId, ResourceType.Portfolio, PermissionMask.View);
                }
            }

            return ret;
        }

        #endregion
    }
}
