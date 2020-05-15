using System.Collections.Generic;
using System.Linq;
using MoneyManager.Models;
using MoneyManager.Repositories;

namespace MoneyManager.Web.Models
{
    public class MockAccountRepository : IAccountRepository
    {
        private readonly List<Account> _accountList;

        public MockAccountRepository()
        {
            _accountList = new List<Account>
            {
                new Account {Id = 1, Name = "Account 1", Balance = (decimal) 10.00},
                new Account {Id = 1, Name = "Account 1", Balance = (decimal) 10.00},
                new Account {Id = 1, Name = "Account 1", Balance = (decimal) 10.00}
            };
        }

        public Account GetAccount(int accountId)
        {
            return _accountList.FirstOrDefault(e => e.Id == accountId);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _accountList;
        }

        public Account Add(Account account)
        {
            account.Id = _accountList.Max(e => e.Id) + 1;
            _accountList.Add(account);
            return account;
        }

        public Account Update(Account updatedAccount)
        {
            var account = _accountList.FirstOrDefault(e => e.Id == updatedAccount.Id);

            if (account == null) return null;
            
            account.Name = updatedAccount.Name;
            account.Balance = updatedAccount.Balance;

            return account;
        }

        public Account Delete(int accountId)
        {
            var account = _accountList.FirstOrDefault(e => e.Id == accountId);

            if (account == null) return null;
            
            _accountList.Remove(account);

            return account;
        }
    }
}