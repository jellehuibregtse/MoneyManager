using System.Collections.Generic;
using MoneyManager.DAL;
using MoneyManager.Models;

namespace MoneyManager.IntegrationTests
{
    public static class Utilities
    {
        public static void InitializeDbForTests(AppDbContext context)
        {
            context.Accounts.AddRange(GetSeedingAccounts());
            context.Transactions.AddRange(GetSeedingTransactions());
            context.Categories.AddRange(GetSeedingCategories());

            context.SaveChanges();
        }

        public static void ReinitializeDbForTests(AppDbContext context)
        {
            context.Accounts.RemoveRange(context.Accounts);
            context.Transactions.RemoveRange(context.Transactions);
            context.Categories.RemoveRange(context.Categories);

            InitializeDbForTests(context);
        }

        private static IEnumerable<Account> GetSeedingAccounts()
        {
            return new List<Account>
            {
                new Account {Id = 1, Name = "Account 1", InitialBalance = 0},
                new Account {Id = 2, Name = "Account 2", InitialBalance = 0},
                new Account {Id = 3, Name = "Account 3", InitialBalance = 0}
            };
        }

        private static IEnumerable<Transaction> GetSeedingTransactions()
        {
            return new List<Transaction>
            {
                new Transaction {Id = 1, Name = "Transaction 1", AccountId = 1},
                new Transaction {Id = 2, Name = "Transaction 2", AccountId = 2},
                new Transaction {Id = 3, Name = "Transaction 3", AccountId = 3}
            };
        }

        private static IEnumerable<Category> GetSeedingCategories()
        {
            return new List<Category>
            {
                new Category {Id = 1, Name = "Category 1"},
                new Category {Id = 2, Name = "Category 2"},
                new Category {Id = 3, Name = "Category 3"}
            };
        }
    }
}