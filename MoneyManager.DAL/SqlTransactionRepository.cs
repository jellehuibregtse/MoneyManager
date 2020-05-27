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
        
        public Transaction GetTransaction(int transactionId)
        {
            return _context.Transactions.Find(transactionId);
        }

        public Transaction Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return transaction;
        }
        
        
    }
}