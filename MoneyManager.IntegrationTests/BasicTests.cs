using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using MoneyManager.Web;
using Xunit;

namespace MoneyManager.IntegrationTests
{
    public class BasicTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public BasicTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home")]
        [InlineData("/Account")]
        [InlineData("/Account/Index")]
        [InlineData("/Account/Create")]
        [InlineData("/Account/Details")]
        [InlineData("/Account/Edit")]
        [InlineData("/Account/Delete")]
        [InlineData("/Transaction")]
        [InlineData("/Transaction/Index")]
        [InlineData("/Transaction/Add")]
        [InlineData("/Transaction/Details")]
        [InlineData("/Transaction/Edit")]
        [InlineData("/Transaction/Delete")]
        [InlineData("/Category")]
        [InlineData("/Category/Index")]
        [InlineData("/Category/Create")]
        [InlineData("/Category/Details")]
        [InlineData("/Category/Edit")]
        [InlineData("/Category/Delete")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            
            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}