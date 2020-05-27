namespace MoneyManager.Web.ViewModels
{
    public class TransactionAddDto
    {
        public int TransactionId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}