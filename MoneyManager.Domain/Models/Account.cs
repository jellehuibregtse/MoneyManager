using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MoneyManager.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        [StringLength(50, MinimumLength = 3)] public string Name { get; set; }

        [Display(Name = "Initial Balance")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal InitialBalance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal Balance => InitialBalance + Transactions
            .Select(transaction => transaction.Amount)
            .DefaultIfEmpty(0)
            .Sum();

        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}