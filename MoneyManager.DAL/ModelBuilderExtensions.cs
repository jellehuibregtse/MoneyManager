using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MoneyManager.Models;

namespace MoneyManager.DAL
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(builder =>
            {
                builder.HasData(
                    new Account
                    {
                        Id = 1,
                        Name = "Account 1",
                        Balance = (decimal) 100.00,
                    },
                    new Account
                    {
                        Id = 2,
                        Name = "Account 2",
                        Balance = (decimal) 200.00,
                    });
            });
        }
    }
}
