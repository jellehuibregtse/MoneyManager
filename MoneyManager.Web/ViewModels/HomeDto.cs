using System.Collections.Generic;
using MoneyManager.Models;

namespace MoneyManager.Web.ViewModels
{
    public class HomeDto
    {
        public List<Transaction> Transactions { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}