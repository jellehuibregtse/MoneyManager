using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyManager.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        [StringLength(50, MinimumLength = 3)] public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public Category Category { get; set; }
        public Account Account { get; set; }
    }
}