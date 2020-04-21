using System.Collections.Generic;
using System.Linq;
using MoneyManager;

namespace MoneyManagerWeb.Models
{
    public class MockAccountRepository : IAccountRepository
    {
        private readonly List<Account> _accountList;

        public MockAccountRepository()
        {
            _accountList = new List<Account>
            {
                new Account(accountId: 1, name: "Account 1", balance: (decimal) 10.00),
                new Account(accountId: 1, name: "Account 2", balance: (decimal) 20.00),
                new Account(accountId: 1, name: "Account 3", balance: (decimal) 30.00)
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
    }
}