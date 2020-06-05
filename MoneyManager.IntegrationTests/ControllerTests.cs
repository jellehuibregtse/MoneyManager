using System.IO;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyManager.Web;
using Xunit;

// https://gunnarpeipman.com/aspnet-core-integration-test-startup/
// https://gunnarpeipman.com/aspnet-core-identity-integration-tests/

namespace MoneyManager.IntegrationTests
{
    public abstract class ControllerTests : IClassFixture<MoneyManagerFactory<MockStartup>>
    {
        protected readonly WebApplicationFactory<MockStartup> Factory;

        protected ControllerTests(MoneyManagerFactory<MockStartup> factory)
        {
            Factory = factory;
        }

        protected WebApplicationFactory<MockStartup> GetFactory(bool hasUser = false)
        {
            var projectDirectory = Directory.GetCurrentDirectory();
            var configurationPath = Path.Combine(projectDirectory, "appsettings.json");

            return Factory.WithWebHostBuilder(builder =>
            {
                builder.UseSolutionRelativeContentRoot("MoneyManager");

                builder.ConfigureTestServices(services =>
                {
                    services.AddMvc(options =>
                    {
                        if (!hasUser) return;
                        
                        options.Filters.Add(new AllowAnonymousFilter());
                        options.Filters.Add(new MockUserFilter());

                    }).AddApplicationPart(typeof(Startup).Assembly);
                });

                builder.ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.AddJsonFile(configurationPath);
                });
            });
        }
    }
}