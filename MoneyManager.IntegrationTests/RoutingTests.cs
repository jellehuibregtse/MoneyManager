using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using MoneyManager.Web;
using Xunit;

namespace MoneyManager.IntegrationTests
{
    public class RoutingTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public RoutingTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private HttpClient GetClient(bool allowAutoRedirect = true)
        {
            return _factory.CreateClient(new WebApplicationFactoryClientOptions
                {AllowAutoRedirect = allowAutoRedirect});
        }

        /// <summary>
        /// This tests verifies that all URLs given end with response code in range (200-299); It makes sure that
        /// requests don't fail and return content type text/html. 
        /// </summary>
        /// <param name="url">The URL which is being tested.</param>
        /// <returns></returns>
        [Theory]
        [InlineData("/")]
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
            var client = GetClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/")]
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
        public async Task Get_SecurePageRequiresAnAuthenticatedUser(string url)
        {
            // Arrange
            var client = GetClient(false);

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("http://localhost/user/login",
                response.Headers.Location.OriginalString);
        }

        [Theory]
        [InlineData("/")]
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
        public async Task Get_SecurePageIsAvailableForAuthenticatedUser(string url)
        {
            // Arrange
            var client = GetClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}