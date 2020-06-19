using OpenQA.Selenium;
using Xunit;

namespace MoneyManager.FrontEndTests
{
    public class CategoryFrontEndTests : FrontEndTest
    {
        private const string BaseUri = LocalHost + "/category";

        private IWebElement CreateCategoryButtonElement => Driver.FindElement(By.XPath("/html/body/div/div[1]/a"));
        private IWebElement NameElement => Driver.FindElement(By.Id("Name"));

        private IWebElement CreateAtCategoryButtonElement =>
            Driver.FindElement(By.XPath("/html/body/div/form/div[3]/div/button"));

        public CategoryFrontEndTests()
        {
            Login();
        }

        [Fact]
        public void Get_Category_ReturnsCategoryOverview()
        {
            // Arrange
            // Done in constructor and constructor of the parent method.

            // Act
            Driver.Navigate().GoToUrl(BaseUri);

            // Assert
            Assert.Equal("Category Overview", Title);
            Assert.Contains("Category Overview", Source);
            Assert.True(CreateCategoryButtonElement.Displayed);
        }

        [Fact]
        public void CreateCategory_FromCategoryOverview_ReturnsCreateCategoryView()
        {
            // Arrange
            // Done in constructor and constructor of the parent method.

            // Act
            Driver.Navigate().GoToUrl(BaseUri);
            CreateCategoryButtonElement.Click();

            // Assert
            Assert.Equal("Create a new category", Title);
            Assert.Contains("Create a new category", Source);
            Assert.Contains("Name", Source);
            Assert.True(CreateAtCategoryButtonElement.Displayed);
        }

        [Fact]
        public void CreateCategory_Success_ReturnsCategoryDetails()
        {
            // Arrange
            // Done in constructor and constructor of the parent method.

            // Act
            Driver.Navigate().GoToUrl(BaseUri);
            CreateCategoryButtonElement.Click();
            NameElement.SendKeys("Test Category");
            CreateAtCategoryButtonElement.Click();

            // Assert
            Assert.Equal("Details", Title);
            Assert.Contains("Details", Source);
            Assert.Contains("Test Category", Source);
        }
    }
}