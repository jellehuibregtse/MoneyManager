using System.ComponentModel.DataAnnotations;

namespace MoneyManager.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int TransactionId { get; set; }

        [StringLength(50, MinimumLength = 3)] public string Name { get; set; }
        public Transaction Transaction { get; set; }
    }
}