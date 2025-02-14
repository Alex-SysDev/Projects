using FinancialAccountManagementSystem.Models;

namespace FinancialAccountManagementSystem.Interfaces
{
    public interface IAccountRepository
    {
        ICollection<Account> GetAccounts();
        Account GetAccountById(int id);
        bool CreateAccount(Account account);
        bool UpdateAccount(Account account);
        bool DeleteAccount(Account account);
        bool AccountExists(int id);
        bool Save();
    }
}
