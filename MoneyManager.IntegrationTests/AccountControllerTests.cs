using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyManager.Web;
using Xunit;

// https://gunnarpeipman.com/aspnet-core-integration-test-startup/


namespace MoneyManager.IntegrationTests
{
    public class AccountControllerTests : IClassFixture<MoneyManagerFactory<MockStartup>>
    {
        private readonly WebApplicationFactory<MockStartup> _factory;

        public AccountControllerTests(MoneyManagerFactory<MockStartup> factory)
        {
            var projectDirectory = Directory.GetCurrentDirectory();
            var configurationPath = Path.Combine(projectDirectory, "appsettings.json");

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseSolutionRelativeContentRoot("MoneyManager");

                builder.ConfigureTestServices(services =>
                {
                    services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
                });
                
                builder.ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.AddJsonFile(configurationPath);
                });
            });
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