using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager
{
    public class User
    {
        public int UserId { get;  }
        public string Name { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<Account> Accounts { get; set; }
    }
}