using System.Collections.Generic;
using MoneyManager.Models;

namespace MoneyManager.Web.ViewModels
{
    public class TransactionIndexDto
    {
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}