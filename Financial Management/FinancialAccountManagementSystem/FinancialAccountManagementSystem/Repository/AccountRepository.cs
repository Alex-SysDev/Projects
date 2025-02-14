using FinancialAccountManagementSystem.Data;
using FinancialAccountManagementSystem.Interfaces;
using FinancialAccountManagementSystem.Models;

namespace FinancialAccountManagementSystem.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Account> GetAccounts() 
        {
            return _context.Accounts.OrderBy(a => a.Id).ToList();
        }

        public Account GetAccountById(int id)
        {
            return _context.Accounts.Where(a => a.Id == id).FirstOrDefault();
        }

        public bool CreateAccount(Account account)
        {
            _context.Add(account);

            return Save();
        }

        public bool UpdateAccount(Account account)
        {
            _context.Update(account);

            return Save();
        }

        public bool DeleteAccount(Account account)
        {
            _context.Remove(account);

            return Save();
        }

        public bool AccountExists(int id)
        {
            return _context.Accounts.Any(a => a.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }
    }
}
