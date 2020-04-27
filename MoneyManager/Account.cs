using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager
{
    public class Account
    {
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Please enter a name for the account")]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:C0}", ApplyFormatInEditMode = true)]
        public decimal Balance { get; set; }
        
        private readonly List<Transaction> _transactions;
    }
}