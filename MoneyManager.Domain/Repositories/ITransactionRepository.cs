using MoneyManager.Models;
using System.Collections.Generic;

namespace MoneyManager.Repositories
{
    public interface ITransactionRepository
    {
        Transaction GetTransaction(int transactionId, Account account);
        IEnumerable<Transaction> GetAllTransactions(Account account);
        Transaction Add(Transaction transaction);
        Transaction Update(Transaction updatedTransaction);
        Transaction Delete(int transactionId);
    }
}