using OpenQA.Selenium;
using Xunit;

namespace MoneyManager.FrontEndTests
{
    public class UserFrontEndTests : FrontEndTest
    {
        private const string BaseUri = LocalHost + "/user";

        private IWebElement EmailElement => Driver.FindElement(By.Id("Email"));
        private IWebElement PasswordElement => Driver.FindElement(By.Id("Password"));
        private IWebElement ConfirmPasswordElement => Driver.FindElement(By.Id("ConfirmPassword"));
        private IWebElement FirstNameElement => Driver.FindElement(By.Id("FirstName"));
        private IWebElement LastNameElement => Driver.FindElement(By.Id("LastName"));
        private IWebElement LoginButtonElement => Driver.FindElement(By.XPath("/html/body/form/button"));
        private IWebElement RegisterButtonElement => Driver.FindElement(By.XPath("/html/body/form/button"));

        [Fact]
        public void Get_Login_ReturnsLoginView()
        {
            // Arrange
            // Done in the constructor of parent method.

            // Act
            Driver.Navigate().GoToUrl("https://localhost:5001/user/login");

            // Assert
            Assert.Equal("User Login", Title);
            Assert.Contains("Please Login", Source);
        }

        [Fact]
        public void Login_WrongCredentials_ReturnsErrorMessage()
        {
            // Arrange
            // Done in the constructor of parent method.

            // Act
            Driver.Navigate().GoToUrl(BaseUri + "/login");

            EmailElement.SendKeys("test@example.com");
            PasswordElement.SendKeys("password");

            LoginButtonElement.Click();

            var errorMessage = Driver.FindElement(By.XPath("/html/body/form/div[2]/ul/li")).Text;

            // Arrange
            Assert.Equal("Invalid Login Attempt", errorMessage);
        }

        [Fact]
        public void Register_NotMatchingPasswords_ReturnsErrorMessage()
        {
            // Arrange
            // Done in the constructor of parent method.

            // Act
            Driver.Navigate().GoToUrl(BaseUri + "/register");

            FirstNameElement.SendKeys("John");
            LastNameElement.SendKeys("Doe");
            EmailElement.SendKeys("johndoe@example.com");
            PasswordElement.SendKeys("password123");
            ConfirmPasswordElement.SendKeys("wrongpassword123");

            RegisterButtonElement.Click();

            var errorMessage = Driver.FindElement(By.XPath("/html/body/form/span[5]")).Text;

            // Assert
            Assert.Equal("Passwords do not match.", errorMessage);
        }

        [Fact]
        public void Register_EmptyForm_ReturnsErrorMessage()
        {
            // Arrange
            // Done in the constructor of the parent method.

            // Act
            Driver.Navigate().GoToUrl(BaseUri + "/register");

            RegisterButtonElement.Click();

            var errorMessage = new[]
            {
                Driver.FindElement(By.XPath("/html/body/form/span[1]")).Text,
                Driver.FindElement(By.XPath("/html/body/form/span[2]")).Text,
                Driver.FindElement(By.XPath("/html/body/form/span[3]")).Text,
                Driver.FindElement(By.XPath("/html/body/form/span[4]")).Text,
                Driver.FindElement(By.XPath("/html/body/form/div/ul/li[1]")).Text,
                Driver.FindElement(By.XPath("/html/body/form/div/ul/li[2]")).Text,
                Driver.FindElement(By.XPath("/html/body/form/div/ul/li[3]")).Text,
                Driver.FindElement(By.XPath("/html/body/form/div/ul/li[4]")).Text,
            };
            
            // Assert
            Assert.Equal("The First Name field is required.", errorMessage[0]);
            Assert.Equal("The Last Name field is required.", errorMessage[1]);
            Assert.Equal("The Email field is required.", errorMessage[2]);
            Assert.Equal("The Password field is required.", errorMessage[3]);
            Assert.Equal("The First Name field is required.", errorMessage[4]);
            Assert.Equal("The Last Name field is required.", errorMessage[5]);
            Assert.Equal("The Email field is required.", errorMessage[6]);
            Assert.Equal("The Password field is required.", errorMessage[7]);

        }
    }
}