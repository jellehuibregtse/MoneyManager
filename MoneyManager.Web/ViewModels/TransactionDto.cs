﻿namespace MoneyManager.Web.ViewModels
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        
        public string PageTitle { get; set; }
    }
}