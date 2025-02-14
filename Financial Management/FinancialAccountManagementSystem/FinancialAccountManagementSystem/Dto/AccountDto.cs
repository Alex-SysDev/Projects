namespace FinancialAccountManagementSystem.Dto
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public decimal Balance { get; set; }
    }
}
