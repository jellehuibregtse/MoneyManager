namespace MoneyManager.Web.ViewModels
{
    public class TransactionEditDto
    {
        public int TransactionId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}