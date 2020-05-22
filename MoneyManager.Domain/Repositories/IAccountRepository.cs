using MoneyManager.Models;
using System.Collections.Generic;

namespace MoneyManager.Repositories
{
    public interface IAccountRepository
    {
        Account GetAccount(int accountId, ApplicationUser applicationUser);
        IEnumerable<Account> GetAllAccounts(ApplicationUser applicationUser);
        Account Add(Account account);
        Account Update(Account updatedAccount);
        Account Delete(int accountId);
    }
}