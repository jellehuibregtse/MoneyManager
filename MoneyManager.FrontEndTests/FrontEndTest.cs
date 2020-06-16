using System;
using System.Diagnostics;
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

        protected FrontEndTest()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("headless");
            
            Driver = new ChromeDriver(chromeOptions);
        }

        public void Dispose()
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}