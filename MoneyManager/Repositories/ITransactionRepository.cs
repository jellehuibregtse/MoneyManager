using MoneyManager.Models;
using System.Collections.Generic;

namespace MoneyManager.Repositories
{
    public interface ITransactionRepository
    {
        Transaction GetTransaction(int transactionId);
        IEnumerable<Transaction> GetAllTransactions();
    }
}