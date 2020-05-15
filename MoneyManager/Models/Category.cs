using System.ComponentModel.DataAnnotations;

namespace MoneyManager.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}