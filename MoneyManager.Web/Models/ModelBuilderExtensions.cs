using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MoneyManager.Web.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    AccountId = 1,
                    Name = "Account 1",
                    Balance = (decimal)100.00
                },
                new Account
                {
                    AccountId = 2,
                    Name = "Account 2",
                    Balance = (decimal)200.00
                });
        }
    }
}
