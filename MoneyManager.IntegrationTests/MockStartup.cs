using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyManager.DAL;
using MoneyManager.Models;
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
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (dbContext.Database.GetDbConnection().ConnectionString.ToLower().Contains("database.windows.net"))
                {
                    throw new Exception("LIVE SETTINGS IN TESTS!");
                }

                // if (dbContext.Users.Any(u => u.Id == UserSettings.UserId)) return;
                //
                // var user = new ApplicationUser
                // {
                //     ConcurrencyStamp = DateTime.Now.Ticks.ToString(),
                //     Email = UserSettings.UserEmail,
                //     EmailConfirmed = true,
                //     Id = UserSettings.UserId
                // };
                // user.NormalizedEmail = user.Email;
                // user.NormalizedUserName = user.Email;
                // user.PasswordHash = Guid.NewGuid().ToString();
                // user.UserName = user.Email;
                //
                // dbContext.Users.Add(user);
                // dbContext.SaveChanges();
            }
        }
    }
}