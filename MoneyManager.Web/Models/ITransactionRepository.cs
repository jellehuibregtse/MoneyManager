using System.Collections.Generic;
using MoneyManager;

namespace MoneyManager.Web.Models
{
    public interface ITransactionRepository
    {
        Transaction GetTransaction(int transactionId);
        IEnumerable<Transaction> GetAllTransactions();
    }
}