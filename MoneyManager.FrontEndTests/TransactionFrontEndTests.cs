using Xunit;

namespace MoneyManager.FrontEndTests
{
    public class TransactionFrontEndTests : FrontEndTest
    {
        private const string BaseUri = LocalHost + "/transaction";
        
        public TransactionFrontEndTests()
        {
            Login();
        }

        [Fact]
        public void Get_Transaction_ReturnsTransactionOverview()
        {
            // Arrange
            // Done in constructor and constructor of the parent method.
            
            // Act
            Driver.Navigate().GoToUrl(BaseUri);
            
            // Assert
            Assert.Equal("Transaction Overview", Title);
            Assert.Contains("Transaction Overview", Source);
        }
    }
}