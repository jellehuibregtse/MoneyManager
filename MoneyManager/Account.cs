using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager
{
    public class Account
    {
        public int AccountId { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
        
        private readonly List<Transaction> _transactions;
    }
}