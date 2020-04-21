using System;
using System.Collections.Generic;

namespace MoneyManager
{
    public class Account
    {
        public int AccountId { get; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Account(int accountId, string name, decimal balance)
        {
            AccountId = accountId;
            Name = name;
            Balance = balance;
        }

        public void AddTransaction(decimal amount, Category category)
        {
            Transactions.Add(new Transaction(Transactions.Count + 1, amount, category));
        }
    }
}