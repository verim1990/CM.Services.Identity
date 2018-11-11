using CM.Services.Identity.API.Infrastracture;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using CM.Services.Identity.Infrastracture.Contexts;
using CM.Shared.Kernel.Others.Sql;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CM.Services.Identity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build()
                .RegisterSql<ApplicationContext>(async (context, services) => {
                    var userManager = services.GetService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetService<RoleManager<ApplicationRole>>();

                    await context.Seed(userManager, roleManager);
                })
                .RegisterSql<PersistedGrantDbContext>()
                .RegisterSql<ConfigurationDbContext>(async (context, services) =>
                {
                    var configuration = services.GetService<IConfiguration>();
                    var appConfig = new AppConfig(configuration.Get<AppSettings>());

                    await context.Seed(appConfig.GetClients(), appConfig.GetApis(), appConfig.GetResources());
                })
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
