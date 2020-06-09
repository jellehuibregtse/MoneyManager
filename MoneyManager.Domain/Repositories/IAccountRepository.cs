using MoneyManager.Models;
using System.Collections.Generic;

namespace MoneyManager.Repositories
{
    public interface IAccountRepository
    {
        Account GetAccount(int accountId, ApplicationUser applicationUser);
        IEnumerable<Account> GetAllAccounts(ApplicationUser applicationUser);
        void Add(Account account);
        void Update(Account updatedAccount);
        void Delete(int accountId);
    }
}