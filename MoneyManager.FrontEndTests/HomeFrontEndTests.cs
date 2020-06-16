using OpenQA.Selenium;
using Xunit;

namespace MoneyManager.FrontEndTests
{
    public class HomeFrontEndTests : FrontEndTest
    {
        private IWebElement UserDropdownElement =>
            Driver.FindElement(By.XPath("//*[@id=\"navbarSupportedContent\"]/ul[2]/li/a"));
        private IWebElement LogoutButtonElement =>
            Driver.FindElement(By.XPath("//*[@id=\"navbarSupportedContent\"]/ul[2]/li/div/form"));
        
        public HomeFrontEndTests()
        {
            Login();
        }

        [Fact]
        public void Get_Home_ReturnsDashboardView()
        {
            // Arrange
            // Done in constructor and constructor of the parent method.
            
            // Act
            Driver.Navigate().Refresh();
            
            // Assert
            Assert.Equal("Dashboard", Title);
            Assert.Contains("Accounts", Source);
            Assert.Contains("Recent transactions", Source);
        }

        [Fact]
        public void Logout_FromDashboard_ReturnsLoginView()
        {
            // Arrange
            // Done in constructor and constructor of the parent method.
            
            // Act
            UserDropdownElement.Click();
            LogoutButtonElement.Click();
            
            // Assert
            Assert.Equal("User Login", Title);
            Assert.Contains("Please Login", Source);
        }
    }
}