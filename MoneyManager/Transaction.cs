using System;

namespace MoneyManager
{
    public class Transaction
    {
        public int TransactionId { get; }
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