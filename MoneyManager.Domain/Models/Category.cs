using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager.Models
{
    public class Category
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3)] public string Name { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}