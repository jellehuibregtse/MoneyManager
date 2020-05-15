using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyManager.Models
{
    public class User
    {
        [Key,
         DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,
        MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
        public string Name { get; set; }
        [Required,
        RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid e-mail format")]
        public string Email { get; set; }
        [Required,
         DataType(DataType.Password),
        StringLengthAttribute(255)]
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<Account> Accounts { get; set; }
    }
}