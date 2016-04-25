using Model;
using Model.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config
{
    public class LoginManager
    {
        private static readonly LoginManager _instance = new LoginManager();

        private User _loginUser;

        private List<AccountItem> _accounts = new List<AccountItem>();

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

        public User LoginUser
        {
            get { return _loginUser; }
            set { _loginUser = value; }
        }

        public List<AccountItem> Accounts
        {
            get { return _accounts; }
        }

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
    }
}
