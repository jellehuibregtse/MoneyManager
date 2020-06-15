using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyManager.DAL;
using MoneyManager.Web;

namespace MoneyManager.IntegrationTests
{
    public class MockStartup : Startup
    {
        public MockStartup(IConfiguration config) : base(config)
        {
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
            
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using var serviceScope = serviceScopeFactory.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
 
            if (dbContext.Database.GetDbConnection().ConnectionString.ToLower().Contains("database.windows.net"))
            {
                throw new Exception("Using the live settings in test suite.");
            }
 
            // Initialize database
        }
    }
}