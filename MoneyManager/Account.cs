using System;
using System.Collections.Generic;

namespace MoneyManager
{
    public class Account
    {
        public int AccountId { get; }
        public string Name { get; }
        public decimal Balance { get; }
        
        private readonly List<Transaction> _transactions;

        public Account(int accountId, string name, decimal balance)
        {
            AccountId = accountId;
            Name = name;
            Balance = balance;
            _transactions = new List<Transaction>();
        }

        public void AddTransaction(decimal amount, Category category)
        {
            _transactions.Add(new Transaction(_transactions.Count + 1, amount, category));
        }
    }
}