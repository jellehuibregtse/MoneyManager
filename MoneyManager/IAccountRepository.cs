using System.Collections;
using System.Collections.Generic;
using MoneyManager;

namespace MoneyManager
{
    public interface IAccountRepository
    {
        Account GetAccount(int accountId);
        IEnumerable<Account> GetAllAccounts();
        Account Add(Account account);
        Account Update(Account updatedAccount);
        Account Delete(int accountId);
    }
}