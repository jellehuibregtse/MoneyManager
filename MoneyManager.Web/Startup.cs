using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneyManager.DAL;
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => 
                options.UseSqlServer(_config.GetConnectionString("MoneyManagerDBConnection")));
            
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddScoped<IAccountRepository, SqlAccountRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseMvcWithDefaultRoute();
            //app.UseMvc(routes => { routes.MapRoute("default", "{controller=home}/{action=index}/{id?}"); });
        }
    }
}