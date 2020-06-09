using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using MoneyManager.Web;
using Xunit;

namespace MoneyManager.Tests
{
    public class RoutingTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public RoutingTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        /// <summary>
        /// This tests verifies that all URLs given end with response code in range (200-299); It makes sure that
        /// requests don't fail and return content type text/html. 
        /// </summary>
        /// <param name="url">The URL which is being tested.</param>
        /// <returns></returns>
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