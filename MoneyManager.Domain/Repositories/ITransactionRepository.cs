using MoneyManager.Domain.Models;
using System.Collections.Generic;

namespace MoneyManager.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Transaction GetTransaction(int transactionId);
        IEnumerable<Transaction> GetAllTransactions();
    }
}