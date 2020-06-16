using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MoneyManager.FrontEndTests
{
    public class FrontEndTest : IDisposable
    {
        protected const string LocalHost = "https://localhost:5001";
        protected readonly IWebDriver Driver;

        protected string Title => Driver.Title;
        protected string Source => Driver.PageSource;

        private const string UserName = "selenium@example.com";
        private const string Password = "@Password123";

        protected FrontEndTest()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("headless");
            // Run in 1920 by 1080 to fix unreachable elements
            chromeOptions.AddArgument("window-size=1920,1080");

            Driver = new ChromeDriver(chromeOptions);
        }

        public void Dispose()
        {
            Driver.Quit();
            Driver.Dispose();
        }

        protected void Login()
        {
            Driver.Navigate().GoToUrl(LocalHost);

            Driver.FindElement(By.Id("Email")).SendKeys(UserName);
            Driver.FindElement(By.Id("Password")).SendKeys(Password);

            Driver.FindElement(By.XPath("/html/body/form/button")).Click();

            string errorMessage = null;

            try
            {
                errorMessage = Driver.FindElement(By.XPath("/html/body/form/div[2]/ul/li")).Text;
            }
            catch (NoSuchElementException)
            {
            }

            // Login failed
            if (string.IsNullOrEmpty(errorMessage) &&
                !Driver.Url.StartsWith(LocalHost + "/user")) return;

            // Register test user
            Driver.FindElement(By.XPath("/html/body/form/p/a")).Click();

            Driver.FindElement(By.Id("FirstName")).SendKeys("Selenium");
            Driver.FindElement(By.Id("LastName")).SendKeys("Example");
            Driver.FindElement(By.Id("Email")).SendKeys(UserName);
            Driver.FindElement(By.Id("Password")).SendKeys(Password);
            Driver.FindElement(By.Id("ConfirmPassword")).SendKeys(Password);

            Driver.FindElement(By.XPath("/html/body/form/button")).Click();
        }
    }
}