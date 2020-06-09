using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Models;
using MoneyManager.Repositories;
using MoneyManager.Web.Controllers;
using Moq;

namespace MoneyManager.Tests
{
    public class ControllerFactory : IDisposable
    {
        public readonly Mock<UserManager<ApplicationUser>> MockUserManager;
        public readonly Mock<IAccountRepository> MockAccountRepository;
        public readonly Mock<ITransactionRepository> MockTransactionRepository;
        public readonly Mock<ControllerContext> MockControllerContext;
        public readonly HomeController HomeController;

        public readonly List<ApplicationUser> Users = MockHelpers.TestUsers();
        public readonly List<Account> Accounts = MockHelpers.TestAccounts();
        public readonly List<Transaction> Transactions = MockHelpers.TestTransactions();
        
        public ControllerFactory()
        {
            // Arrange
            MockUserManager = MockHelpers.MockUserManager(Users);

            MockAccountRepository = new Mock<IAccountRepository>();
            MockTransactionRepository = new Mock<ITransactionRepository>();

            MockAccountRepository
                .Setup(repository => repository.GetAllAccounts(Users[0]))
                .Returns(Accounts);
            MockTransactionRepository
                .Setup(repository => repository.GetAllTransactions(Users[0]))
                .Returns(Transactions);
                
            HomeController = new HomeController(MockAccountRepository.Object, MockTransactionRepository.Object,
                MockUserManager.Object);
        }
        
        public void Dispose()
        {
        }
    }
}