using System.Collections.Generic;
using MoneyManager.Models;

namespace MoneyManager.Repositories
{
    public interface ITransactionRepository
    {
        Transaction GetTransaction(int transactionId, ApplicationUser applicationUser);
        IEnumerable<Transaction> GetAllTransactions(ApplicationUser applicationUser);
        void Add(Transaction transaction);
        void Update(Transaction updatedTransaction);
        void Delete(int transactionId);
    }
}