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
            _context = _context;
        }
        
        public Transaction GetTransaction(int transactionId, Account account)
        {
            var transaction = _context.Transactions.Find(transactionId);
            return transaction.Account == account ? transaction : null;
        }

        public IEnumerable<Transaction> GetAllTransactions(Account account)
        {
            return _context.Transactions.Where(transaction => transaction.Account == account);
        }

        public Transaction Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }

        public Transaction Update(Transaction updatedTransaction)
        {
            var transaction = _context.Transactions.Attach(updatedTransaction);
            transaction.State = EntityState.Modified;
            _context.SaveChanges();
            return updatedTransaction;
        }

        public Transaction Delete(int transactionId)
        {
            var transaction = _context.Transactions.Find(transactionId);

            if (transaction == null) return null;

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();

            return transaction;
        }
    }
}