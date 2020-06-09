using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using MoneyManager.Models;
using MoneyManager.Web.ViewModels;
using Xunit;

namespace MoneyManager.Tests
{
    public class HomeControllerUnitTests : IClassFixture<ControllerFactory>
    {
        private readonly ControllerFactory _factory;
        
        public HomeControllerUnitTests(ControllerFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfAccountsAndTransactions()
        {
            // Arrange
            _factory.HomeController.ControllerContext = _factory.MockControllerContext.Object;
        
            // Act
            var result = _factory.HomeController.Index();
        
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeDto>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Transactions.Count());
            Assert.Equal(2, model.Accounts.Count());
        }

        [Fact]
        public async Task CreateAUser()
        {
            // Arrange
            var newUser = new ApplicationUser {UserName = "user@mail.com"};
            const string password = "P@ssw0rd!";

            // Act
            await _factory.MockUserManager.Object.CreateAsync(newUser, password);

            // Assert
            Assert.Equal(3, _factory.Users.Count);
        }
    }
}