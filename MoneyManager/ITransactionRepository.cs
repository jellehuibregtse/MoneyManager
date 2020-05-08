using System.Collections.Generic;
using MoneyManager;

namespace MoneyManager
{
    public interface ITransactionRepository
    {
        Transaction GetTransaction(int transactionId);
        IEnumerable<Transaction> GetAllTransactions();
    }
}