using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyManager.Models;

namespace MoneyManager.Web.ViewModels
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public Category Category { get; set; }
        public SelectList Categories { get; set; }
    }
}