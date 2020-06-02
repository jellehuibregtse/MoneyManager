using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MoneyManager.Models;

namespace MoneyManager.Web.ViewModels
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        [Display(Name = "Initial Balance")]
        [DataType(DataType.Currency)]
        public decimal InitialBalance { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}