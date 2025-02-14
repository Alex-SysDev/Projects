namespace FinancialAccountManagementSystem.Dto
{
    public class TransactionCreateDto
    {
        public int AccountId { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
    }
}
