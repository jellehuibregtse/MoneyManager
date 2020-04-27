using System.Collections.Generic;
using System.Linq;

namespace MoneyManager.Web.Models
{
    public class MockAccountRepository : IAccountRepository
    {
        private readonly List<Account> _accountList;

        public MockAccountRepository()
        {
            _accountList = new List<Account>
            {
                new Account {AccountId = 1, Name = "Account 1", Balance = (decimal) 10.00},
                new Account {AccountId = 1, Name = "Account 1", Balance = (decimal) 10.00},
                new Account {AccountId = 1, Name = "Account 1", Balance = (decimal) 10.00}
            };
        }

        public Account GetAccount(int accountId)
        {
            return _accountList.FirstOrDefault(e => e.AccountId == accountId);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _accountList;
        }

        public Account Add(Account account)
        {
            account.AccountId = _accountList.Max(e => e.AccountId) + 1;
            _accountList.Add(account);
            return account;
        }

        public Account Update(Account updatedAccount)
        {
            var account = _accountList.FirstOrDefault(e => e.AccountId == updatedAccount.AccountId);

            if (account == null) return null;
            
            account.Name = updatedAccount.Name;
            account.Balance = updatedAccount.Balance;

            return account;
        }

        public Account Delete(int accountId)
        {
            var account = _accountList.FirstOrDefault(e => e.AccountId == accountId);

            if (account == null) return null;
            
            _accountList.Remove(account);

            return account;
        }
    }
}