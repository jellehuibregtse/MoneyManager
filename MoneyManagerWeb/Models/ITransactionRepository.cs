using System.Collections.Generic;
using MoneyManager;

namespace MoneyManagerWeb.Models
{
    public interface ITransactionRepository
    {
        Transaction GetTransaction(int transactionId);
        IEnumerable<Transaction> GetAllTransactions();
    }
}