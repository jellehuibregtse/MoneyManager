using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager
{
    public class Transaction
    {
        public int TransactionId { get; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:C0}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        public Category Category { get; set; }

        public Transaction(int transactionId, decimal amount, Category category)
        {
            TransactionId = transactionId;
            Amount = amount;
            Category = category;
        }
    }
}