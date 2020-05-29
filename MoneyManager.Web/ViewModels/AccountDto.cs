using System.Collections.Generic;
using MoneyManager.Models;

namespace MoneyManager.Web.ViewModels
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}