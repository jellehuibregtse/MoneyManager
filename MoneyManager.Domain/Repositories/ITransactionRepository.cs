using MoneyManager.Models;

namespace MoneyManager.Repositories
{
    public interface ITransactionRepository
    {
        Transaction GetTransaction(int transactionId);
        Transaction Add(Transaction transaction);
        Transaction Update(Transaction updatedTransaction);
        Transaction Delete(int transactionId);
    }
}