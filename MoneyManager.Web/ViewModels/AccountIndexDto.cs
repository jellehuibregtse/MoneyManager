using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using MoneyManager.Domain.Models;

namespace MoneyManager.Web.ViewModels
{
    public class AccountIndexDto
    {
        public IEnumerable<Account> Accounts { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public SignInManager<ApplicationUser> SignInManager { get; set; }
    }
}