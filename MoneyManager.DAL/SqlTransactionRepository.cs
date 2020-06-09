using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyManager.Models;
using MoneyManager.Repositories;

namespace MoneyManager.DAL
{
    public class SqlTransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public SqlTransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public Transaction GetTransaction(int transactionId, ApplicationUser applicationUser)
        {
            return _context.Transactions
                .Include(transaction => transaction.Category)
                .Include(transaction => transaction.Account)
                .SingleOrDefault(transaction => transaction.Id == transactionId);
        }

        public IEnumerable<Transaction> GetAllTransactions(ApplicationUser applicationUser)
        {
            return _context.Transactions
                .Include(transaction => transaction.Category)
                .Include(transaction => transaction.Account)
                .Include(transaction => transaction.ApplicationUser)
                .Where(transaction => transaction.ApplicationUser == applicationUser);
        }

        public void Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public void Update(Transaction updatedTransaction)
        {
            var transaction = _context.Transactions.Attach(updatedTransaction);
            transaction.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int transactionId)
        {
            var transaction = _context.Transactions.Find(transactionId);

            if (transaction == null) return;

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
        }
    }
}