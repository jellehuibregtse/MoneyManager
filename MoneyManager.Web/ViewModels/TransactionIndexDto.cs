using System.Collections.Generic;
using MoneyManager.Models;

namespace MoneyManager.Web.ViewModels
{
    public class TransactionIndexDto
    {
        public List<Transaction> Transactions { get; set; }
    }
}