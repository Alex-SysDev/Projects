using FinancialAccountManagementSystem.Data;
using FinancialAccountManagementSystem.Interfaces;
using FinancialAccountManagementSystem.Models;
using System.Net.WebSockets;

namespace FinancialAccountManagementSystem.Repository
{
    public class LinqQueriesRepository : ILinqQueriesRepository
    {
        private readonly DataContext _context;

        public LinqQueriesRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Transaction> GetTransactionsByAccount(int id)
        {
            var transactions = _context.Transactions
                               .Where(t => t.AccountId == id)
                               .ToList();
            return transactions;
        }
        public decimal GetTotalBalance()
        {
            var totalBalance = _context.Accounts
                            .Sum(a => a.Balance);

            return totalBalance;
        }
        public ICollection<Account> GetBalanceBelow(decimal treshold)
        {
            var balanceBelow = _context.Accounts
                            .Where(a => a.Balance < treshold)
                            .ToList();

            return balanceBelow;
        }

        public ICollection<Account> GetHighestBalance()
        {
            var topFive = _context.Accounts
                        .OrderByDescending(a => a.Balance)
                        .Take(5)
                        .ToList();
            return topFive;
        }

        public bool AccountExists(int id)
        {
            return _context.Accounts.Any(a => a.Id == id);
        }
    }
}
