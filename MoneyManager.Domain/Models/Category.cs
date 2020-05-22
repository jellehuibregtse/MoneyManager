using System.ComponentModel.DataAnnotations;

namespace MoneyManager.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public Transaction Transaction { get; set; }
    }
}