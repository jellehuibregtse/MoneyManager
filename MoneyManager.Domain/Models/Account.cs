using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MoneyManager.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int TransactionId { get; set; }
        [StringLength(50, MinimumLength = 3)] public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}