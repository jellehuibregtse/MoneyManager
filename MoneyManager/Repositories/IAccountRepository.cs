using MoneyManager.Models;
using System.Collections.Generic;

namespace MoneyManager.Repositories
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