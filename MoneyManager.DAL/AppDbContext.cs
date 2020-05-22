using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneyManager.Models;

namespace MoneyManager.DAL
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().HasMany(user => user.Accounts).WithOne(account => account.ApplicationUser).IsRequired();
            modelBuilder.Entity<Account>().HasMany(account => account.Transactions).WithOne(transaction => transaction.Account).IsRequired();
            modelBuilder.Entity<Transaction>().HasOne(transaction => transaction.Category).WithOne(category => category.Transaction).IsRequired()
                .HasForeignKey<Transaction>(transaction => transaction.Id);
        }
    }
}