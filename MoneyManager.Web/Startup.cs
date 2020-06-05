using System;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneyManager.DAL;
using MoneyManager.Models;
using MoneyManager.Repositories;

namespace MoneyManager.Web
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public virtual void ConfigureServices(IServiceCollection services)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                services.AddDbContextPool<AppDbContext>(options =>
                    options.UseSqlServer(_config.GetConnectionString("MoneyManagerDBConnectionProd")));
            else
                services.AddDbContextPool<AppDbContext>(options =>
                    options.UseSqlServer(_config.GetConnectionString("MoneyManagerDBConnection")));

            services.BuildServiceProvider().GetService<AppDbContext>().Database.Migrate();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 3;
                })
                .AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/user/login");

            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddScoped<IAccountRepository, SqlAccountRepository>();
            services.AddScoped<ITransactionRepository, SqlTransactionRepository>();
            services.AddScoped<ICategoryRepository, SqlCategoryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }

            var cultureInfo = new CultureInfo("en-US") {NumberFormat = {CurrencySymbol = "€"}};

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseMvcWithDefaultRoute();
            //app.UseMvc(routes => { routes.MapRoute("default", "{controller=home}/{action=index}/{id?}"); });
        }
    }
}