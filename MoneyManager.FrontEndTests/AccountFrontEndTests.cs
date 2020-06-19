using OpenQA.Selenium;
using Xunit;

namespace MoneyManager.FrontEndTests
{
    public class AccountFrontEndTests : FrontEndTest
    {
        private const string BaseUri = LocalHost + "/account";

        private IWebElement CreateAccountButtonElement => Driver.FindElement(By.XPath("/html/body/div/div[1]/a"));

        private IWebElement CreateButtonAtAccountElement =>
            Driver.FindElement(By.XPath("/html/body/div/form/div[4]/div/button"));

        private IWebElement CreateButtonAtTransactionElement =>
            Driver.FindElement(By.XPath("/html/body/div/form/div[5]/div/button"));

        private IWebElement AddTransactionButtonElement => Driver.FindElement(By.XPath("/html/body/div/div[1]/a"));
        private IWebElement NameElement => Driver.FindElement(By.Id("Name"));
        private IWebElement InitialBalanceElement => Driver.FindElement(By.Id("InitialBalance"));

        private IWebElement FirstAccountDetailsButtonElement =>
            Driver.FindElement(By.XPath("/html/body/div/div[2]/table/tbody/tr[1]/td[3]/div/a[1]"));

        public AccountFrontEndTests()
        {
            Login();
        }

        [Fact]
        public void Get_Account_ReturnsAccountOverview()
        {
            // Arrange
            // Done in constructor and constructor of the parent method.

            // Act
            Driver.Navigate().GoToUrl(BaseUri);

            // Assert
            Assert.Equal("Accounts Overview", Title);
            Assert.Contains("Accounts Overview", Source);
            Assert.True(CreateAccountButtonElement.Displayed);
        }

        [Fact]
        public void CreateAccount_FromAccountsOverview_ReturnsCreateAccountView()
        {
            // Arrange
            // Done in constructor and constructor of the parent method.

            // Act
            Driver.Navigate().GoToUrl(BaseUri);
            CreateAccountButtonElement.Click();

            // Assert
            Assert.Equal("Create a new account", Title);
            Assert.Contains("Create a new account", Source);
            Assert.Contains("Name", Source);
            Assert.Contains("Initial Balance", Source);
            Assert.Contains("Create", Source);
            Assert.True(CreateButtonAtAccountElement.Displayed);
        }

        [Fact]
        public void CreateAccount_EmptyForm_ReturnsErrorMessage()
        {
            // Arrange
            // Done in constructor and constructor of the parent method.

            // Act
            Driver.Navigate().GoToUrl(BaseUri);
            CreateAccountButtonElement.Click();
            CreateButtonAtAccountElement.Click();

            var errorMessage = Driver.FindElement(By.Id("Name-error")).Text;

            // Assert
            Assert.Equal("This field is required.", errorMessage);
        }

        [Fact]
        public void CreateAccount_Success_ReturnsAccountDetails()
        {
            // Arrange
            // Done in constructor and constructor of the parent method.

            // Act
            Driver.Navigate().GoToUrl(BaseUri);
            CreateAccountButtonElement.Click();
            NameElement.SendKeys("Test Account");
            InitialBalanceElement.SendKeys("100");
            CreateButtonAtAccountElement.Click();

            // Assert
            Assert.Equal("Account Details", Title);
            Assert.Contains("Transactions", Source);
            Assert.True(AddTransactionButtonElement.Displayed);
        }

        [Fact]
        public void AddTransaction_FromAccountDetails_ReturnsAddTransactionView()
        {
            // Arrange
            // Done in constructor and constructor of the parent method.
            
            // Act
            Driver.Navigate().GoToUrl(BaseUri);
            FirstAccountDetailsButtonElement.Click();
            AddTransactionButtonElement.Click();
            
            // Assert
            Assert.Equal("Add a transaction", Title);
            Assert.Contains("Add a transaction", Source);
            Assert.Contains("Name", Source);
            Assert.Contains("Amount", Source);
            Assert.Contains("Category", Source);
            Assert.Contains("Create", Source);
            Assert.True(CreateButtonAtTransactionElement.Displayed);
        }

        [Fact]
        public void AddTransaction_EmptyForm_ReturnsErrorMessage()
        {
            // Arrange
            // Done in constructor and constructor of the parent method.
            
            // Act
            Driver.Navigate().GoToUrl(BaseUri);
            FirstAccountDetailsButtonElement.Click();
            AddTransactionButtonElement.Click();
            CreateButtonAtTransactionElement.Click();

            var errorMessage = Driver.FindElement(By.Id("Name-error")).Text;
            
            // Assert
            Assert.Equal("This field is required.", errorMessage);
        }
    }
}