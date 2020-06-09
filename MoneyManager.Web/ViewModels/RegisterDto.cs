﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MoneyManager.Web.ViewModels
{
    public class RegisterDto
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required,
         EmailAddress,
         Remote("IsEmailInUse", "User")]
        public string Email { get; set; }

        [Required,
         DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password),
         Display(Name = "Confirm password"),
         Compare("Password",
             ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}