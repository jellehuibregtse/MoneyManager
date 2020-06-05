using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MoneyManager.IntegrationTests
{
    public class AccountControllerTests : ControllerTests
    {
        public AccountControllerTests(MoneyManagerFactory<MockStartup> factory) : base(factory)
        {
        }

        /// <summary>
        /// This tests verifies that all URLs given end with response code in range (200-299); It makes sure that
        /// requests don't fail and return content type text/html. 
        /// </summary>
        /// <param name="url">The URL which is being tested.</param>
        /// <returns></returns>
        [Theory]
        [InlineData("/Account/Index")]
        [InlineData("/Account/Create")]
        [InlineData("/Account/Details")]
        [InlineData("/Account/Edit")]
        [InlineData("/Account/Delete")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        /// <summary>
        /// Tests if the client does not go to the url given. For anonymous users this test should pass, as they
        /// should not have access to pages which require login.
        /// </summary>
        /// <param name="url">The URL which is being tested.</param>
        /// <returns></returns>
        [Theory]
        [InlineData("/Account/Index")]
        [InlineData("/Account/Create")]
        [InlineData("/Account/Details")]
        [InlineData("/Account/Edit")]
        [InlineData("/Account/Delete")]
        public async Task Get_SecurePageRequiresAnAuthenticatedUser(string url)
        {
            // Arrange
            var client = Factory.CreateClient(new WebApplicationFactoryClientOptions {AllowAutoRedirect = false});

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("http://localhost/user/login", response.Headers.Location.OriginalString);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("/Account/Index")]
        [InlineData("/Account/Create")]
        [InlineData("/Account/Details")]
        [InlineData("/Account/Edit")]
        [InlineData("/Account/Delete")]
        public async Task Get_SecurePageIsAvailableForAuthenticatedUser(string url)
        {
            // Arrange
            var client = GetFactory(true).CreateClient();
 
            // Act
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStreamAsync();
 
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}