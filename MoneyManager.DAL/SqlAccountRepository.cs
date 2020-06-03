using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyManager.Models;
using MoneyManager.Repositories;

namespace MoneyManager.DAL
{
    public class SqlAccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public SqlAccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public Account GetAccount(int accountId, ApplicationUser applicationUser)
        {
            return _context.Accounts
                .Include(account => account.Transactions).ThenInclude(transaction => transaction.Category)
                .SingleOrDefault(account =>
                    account.Id == accountId && account.ApplicationUser == applicationUser
                );
        }

        public IEnumerable<Account> GetAllAccounts(ApplicationUser applicationUser)
        {
            return _context.Accounts
                .Include(account => account.Transactions)
                .Where(account => account.ApplicationUser == applicationUser);
        }

        public Account Add(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return account;
        }

        public Account Update(Account updatedAccount)
        {
            var account = _context.Accounts.Attach(updatedAccount);
            account.State = EntityState.Modified;
            _context.SaveChanges();

            return updatedAccount;
        }

        public Account Delete(int accountId)
        {
            var account = _context.Accounts.Find(accountId);

            if (account == null) return null;

            _context.Accounts.Remove(account);
            _context.SaveChanges();

            return account;
        }
    }
}