using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string FullName => FirstName + LastName;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; }

        public ICollection<Account> Accounts { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}