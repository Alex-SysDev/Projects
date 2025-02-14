using FinancialAccountManagementSystem.Models;

namespace FinancialAccountManagementSystem.Interfaces
{
    public interface ITransactionRepository
    {
        ICollection<Transaction> GetTransactions();
        Transaction GetTransactionById(int id);
        bool CreateTransaction(Transaction transaction);
        bool UpdateTransaction(Transaction transaction);
        bool DeleteTransaction(Transaction transaction);
        bool TransactionExists(int id);
        bool Save();
    }
}
