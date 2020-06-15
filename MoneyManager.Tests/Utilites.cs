using System.Collections.Generic;
using MoneyManager.DAL;
using MoneyManager.Models;

namespace MoneyManager.IntegrationTests
{
    public class Utilites
    {
        public static void InitializeDbForTests(AppDbContext db)
        {
            db.Accounts.AddRange(GetSeedingMessages());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(AppDbContext db)
        {
            db.Accounts.RemoveRange(db.Accounts);
            InitializeDbForTests(db);
        }

        private static IEnumerable<Account> GetSeedingMessages()
        {
            return new List<Account>
            {
                new Account {Id = 1, InitialBalance = 0, Name = "Account 1"},
                new Account {Id = 2, InitialBalance = 0, Name = "Account 2"},
                new Account {Id = 3, InitialBalance = 0, Name = "Account 3"}
            };
        }
    }
}