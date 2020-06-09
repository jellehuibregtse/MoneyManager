using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using MoneyManager.Models;
using Moq;

// https://stackoverflow.com/questions/49165810/how-to-mock-usermanager-in-net-core-testing

namespace MoneyManager.Tests
{
    public static class MockHelpers
    {
        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success)
                .Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }

        public static List<ApplicationUser> TestUsers()
        {
            return new List<ApplicationUser>
            {
                new ApplicationUser {UserName = "user1@mail.com", Id = "1"},
                new ApplicationUser {UserName = "user2@mail.com", Id = "2"}
            };
        }

        public static List<Account> TestAccounts()
        {
            return new List<Account>
            {
                new Account {Id = 1, Name = "Account 1", InitialBalance = 0},
                new Account {Id = 2, Name = "Account 2", InitialBalance = 0}
            };
        }

        public static List<Transaction> TestTransactions()
        {
            return new List<Transaction>
            {
                new Transaction {Id = 1, Name = "Transaction 1", Amount = 0},
                new Transaction {Id = 2, Name = "Transaction 2", Amount = 0}
            };
        }
    }
}