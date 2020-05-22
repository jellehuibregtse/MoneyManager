using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required,
        MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
    }
}