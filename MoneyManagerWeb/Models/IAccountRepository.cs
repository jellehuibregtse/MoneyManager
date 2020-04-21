using System.Collections;
using System.Collections.Generic;
using MoneyManager;

namespace MoneyManagerWeb.Models
{
    public interface IAccountRepository
    {
        Account GetAccount(int accountId);
        IEnumerable<Account> GetAllAccounts();
    }
}