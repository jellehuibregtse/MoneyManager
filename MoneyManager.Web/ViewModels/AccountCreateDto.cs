using System;

namespace MoneyManager.Web.ViewModels
{
    public class AccountCreateDto
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}