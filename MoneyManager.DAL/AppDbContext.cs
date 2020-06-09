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

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
            modelBuilder.Entity<Category>().ToTable("Category");
            
            // Setup the required many to one relation for account.
            modelBuilder.Entity<Account>()
                .HasMany(account => account.Transactions)
                .WithOne(transaction => transaction.Account)
                .HasForeignKey(transaction => transaction.AccountId)
                .IsRequired();

            // Setup the optional many to one relation for category.
            modelBuilder.Entity<Category>()
                .HasMany(category => category.Transactions)
                .WithOne(transaction => transaction.Category);
        }    
    }
}