using FinancialAccountManagementSystem.Data;
using FinancialAccountManagementSystem.Models;

namespace FinancialAccountManagementSystem
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            if (!_context.Accounts.Any())
            {
                var accounts = new List<Account>()
                {
                    new Account() { AccountNumber = "11111", AccountHolder = "Alexander Palacio", Balance = 5000 },
                    new Account() { AccountNumber = "22222", AccountHolder = "Juliana Castilla", Balance = 1000 },
                    new Account() { AccountNumber = "33333", AccountHolder = "Gabby Ace", Balance = 3000 }
                };
                _context.Accounts.AddRange(accounts);
                _context.SaveChanges();
            }

            if (!_context.Transactions.Any())
            {
                var transactions = new List<Transaction>
                {
                    new Transaction() { AccountId = 1, TransactionType = "Deposit", Amount = 1000, TransactionDate = DateTime.Now },
                    new Transaction() { AccountId = 1, TransactionType = "Withdrawal", Amount = 500, TransactionDate = DateTime.Now.AddMinutes(-10) },
                    new Transaction() { AccountId = 2, TransactionType = "Deposit", Amount = 500, TransactionDate = DateTime.Now },
                    new Transaction() { AccountId = 3, TransactionType = "Withdrawal", Amount = 100, TransactionDate = DateTime.Now.AddHours(-1) }
                };

                _context.Transactions.AddRange(transactions);
                _context.SaveChanges();
            }
        }
    }
}
