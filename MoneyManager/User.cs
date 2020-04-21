using System;
using System.Collections.Generic;

namespace MoneyManager
{
    public class User
    {
        public int UserId { get;  }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<Account> Accounts { get; set; }

        public User(int userId, string name, string password)
        {
            UserId = userId;
            Name = name;
            Password = password;
            RegistrationDate = new DateTime().Date;
        }

        public void AddAccount(string name, decimal initialBalance)
        {
            Accounts.Add(new Account(Accounts.Count + 1, name, initialBalance));
        }
    }
}