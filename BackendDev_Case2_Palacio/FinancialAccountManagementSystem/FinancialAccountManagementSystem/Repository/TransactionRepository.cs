using FinancialAccountManagementSystem.Data;
using FinancialAccountManagementSystem.Interfaces;
using FinancialAccountManagementSystem.Models;

namespace FinancialAccountManagementSystem.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;

        public TransactionRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Transaction> GetTransactions()
        {
            return _context.Transactions.OrderBy(t => t.Id).ToList();
        }
        public Transaction GetTransactionById(int id)
        {
            return _context.Transactions.Where(t => t.Id == id).FirstOrDefault();
        }
        public bool CreateTransaction(Transaction transaction)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.Id == transaction.AccountId);
            if (account == null)
                return false;

            if (transaction.TransactionType == "Deposit")
            {
                account.Balance += transaction.Amount;
            }
            else if (transaction.TransactionType == "Withdrawal")
            {
                if (account.Balance < transaction.Amount)
                {
                    return false;
                }
                account.Balance -= transaction.Amount;
            }
            else
            {
                return false;
            }

            _context.Transactions.Add(transaction);
            return Save();
        }
        public bool UpdateTransaction(Transaction transaction)
        {
            _context.Update(transaction);

            return Save();
        }
        public bool DeleteTransaction(Transaction transaction)
        {
            _context.Remove(transaction);

            return Save();
        }
        public bool TransactionExists(int id)
        {
            return _context.Transactions.Any(t => t.Id == id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }        
    }
}
