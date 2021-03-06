﻿using Model;
using Model.Permission;
using Model.strategy;
using System.Collections.Generic;
using System.Linq;

namespace Config
{
    public class LoginManager
    {
        private static readonly LoginManager _instance = new LoginManager();

        private LoginUser _loginUser;

        private List<AccountItem> _accounts = new List<AccountItem>();
        private List<AssetItem> _assets = new List<AssetItem>();
        private List<PortfolioItem> _portfolios = new List<PortfolioItem>();
        private List<HolderItem> _holders = new List<HolderItem>();

        static LoginManager()
        { 
        
        }

        private LoginManager()
        { 
            
        }

        public static LoginManager Instance
        {
            get { return _instance; }
        }

        public LoginUser LoginUser
        {
            get { return _loginUser; }
            set { _loginUser = value; }
        }

        public List<AccountItem> Accounts
        {
            get { return _accounts; }
        }

        public List<AssetItem> Assets
        {
            get { return _assets; }
        }

        public List<PortfolioItem> Portfolios
        {
            get { return _portfolios; }
        }

        #region user

        public User GetUser()
        {
            return _loginUser.LocalUser;
        }

        public int GetUserId()
        {
            if (_loginUser != null && _loginUser.LocalUser != null)
            {
                return _loginUser.LocalUser.Id;
            }

            return -1;
        }

        #endregion

        #region accounts
        public void AddAccount(AccountItem account)
        {
            bool isExisted = false;
            foreach (var acc in _accounts)
            {
                if (acc.AccountCode == account.AccountCode)
                {
                    isExisted = true;
                    break;
                }
            }

            if (!isExisted)
            {
                _accounts.Add(account);
            }
        }

        public AccountItem GetAccount(string accountCode)
        {
            AccountItem account = new AccountItem();
            foreach (var acc in _accounts)
            {
                if (acc.AccountCode == accountCode)
                {
                    account = acc;
                    break;
                }
            }

            return account;
        }
        #endregion

        #region accounts
        public void AddAsset(AssetItem asset)
        {
            bool isExisted = false;
            foreach (var ass in _assets)
            {
                if (ass.AssetNo == asset.AssetNo)
                {
                    isExisted = true;
                    break;
                }
            }

            if (!isExisted)
            {
                _assets.Add(asset);
            }
        }

        public AssetItem GetAsset(string assetNo)
        {
            AssetItem asset = new AssetItem();
            foreach (var ass in _assets)
            {
                if (ass.AssetNo == assetNo)
                {
                    asset = ass;
                    break;
                }
            }

            return asset;
        }
        #endregion

        #region portfolios

        public void AddPortfolio(PortfolioItem portfolio)
        {
            bool isExisted = false;
            foreach (var port in _portfolios)
            {
                if (port.CombiNo == portfolio.CombiNo)
                {
                    isExisted = true;
                    break;
                }
            }

            if (!isExisted)
            {
                _portfolios.Add(portfolio);
            }
        }

        public PortfolioItem GetPortfolio(string combiNo)
        {
            PortfolioItem portfolio = new PortfolioItem();
            foreach (var port in _portfolios)
            {
                if (port.CombiNo == combiNo)
                {
                    portfolio = port;
                    break;
                }
            }

            return portfolio;
        }

        #endregion

        #region holders

        public void AddHolder(HolderItem holder)
        {
            bool isExisted = false;
            foreach (var h in _holders)
            {
                //if (h.StockHolderId == holder.StockHolderId)
                //{
                //    isExisted = true;
                //    break;
                //}

                if (h.AccountCode == holder.AccountCode 
                    && h.AssetNo == holder.AssetNo 
                    && h.CombiNo == holder.CombiNo
                    && h.MarketNo == holder.MarketNo)
                {
                    isExisted = true;
                    break;
                }
            }

            if (!isExisted)
            {
                _holders.Add(holder);
            }
        }

        public HolderItem GetHolder(string holderId)
        {
            HolderItem holder = new HolderItem();
            foreach (var h in _holders)
            {
                if (h.StockHolderId == holderId)
                {
                    holder = h;
                    break;
                }
            }

            return holder;
        }

        public HolderItem GetHolder(string combiNo, string marketNo)
        {
            HolderItem holder = new HolderItem();
            var ports = _holders.Where(p => p.CombiNo == combiNo).ToList();
            var markets = ports.Where(p => p.MarketNo == marketNo).ToList();

            foreach (var h in _holders)
            {
                if (h.CombiNo == combiNo && h.MarketNo == marketNo)
                {
                    holder = h;
                    break;
                }
            }

            return holder;
        }

        #endregion
    }
}
