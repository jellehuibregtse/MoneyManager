using System.Collections.Generic;
using MoneyManager.Models;

namespace MoneyManager.Repositories
{
    public interface ITransactionRepository
    {
        Transaction GetTransaction(int transactionId, ApplicationUser applicationUser);
        IEnumerable<Transaction> GetAllTransactions(ApplicationUser applicationUser);
        Transaction Add(Transaction transaction);
        Transaction Update(Transaction updatedTransaction);
        Transaction Delete(int transactionId);
    }
}