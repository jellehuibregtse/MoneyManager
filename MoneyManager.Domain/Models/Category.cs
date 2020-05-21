using System.ComponentModel.DataAnnotations;

namespace MoneyManager.Domain.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Transaction Transaction { get; set; }
    }
}