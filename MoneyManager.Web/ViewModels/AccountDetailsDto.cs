
using MoneyManager.Domain.Models;

namespace MoneyManager.Web.ViewModels
{
    // This is an example of a Data Transfer Object or DTO in short.
    public class AccountDetailsDto
    {
        public Account Account { get; set; }
        public string PageTitle { get; set; }
    }
}