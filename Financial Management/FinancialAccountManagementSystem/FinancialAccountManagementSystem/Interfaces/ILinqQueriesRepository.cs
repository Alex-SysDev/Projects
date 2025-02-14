using FinancialAccountManagementSystem.Models;

namespace FinancialAccountManagementSystem.Interfaces
{
    public interface ILinqQueriesRepository
    {
        ICollection<Transaction> GetTransactionsByAccount(int id);
        decimal GetTotalBalance();
        ICollection<Account> GetBalanceBelow(decimal treshold);
        ICollection<Account> GetHighestBalance();

        bool AccountExists(int id);
    }
}
