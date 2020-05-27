using MoneyManager.Models;

namespace MoneyManager.Web.ViewModels
{
    public class TransactionDetailsDto
    {
        public Transaction Transaction { get; set; }
        public string PageTitle { get; set; }
    }
}