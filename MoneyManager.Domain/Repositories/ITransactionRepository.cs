using System.Collections.Generic;
using MoneyManager.Models;

namespace MoneyManager.Repositories
{
    public interface ITransactionRepository
    {
        Transaction GetTransaction(int transactionId);
        //IEnumerable<Transaction> GetAllTransactions(Account account);
        Transaction Add(Transaction transaction);
    }
}