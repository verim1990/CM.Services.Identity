using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Options;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Services.Identity.Infrastracture.Contexts
{
    public class ConfigurationDbContextDesignFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>()
                .UseSqlServer("Server=tcp:127.0.0.1,5433;Database=CM_Services_IdentityDb;User Id=SA;Password=Test1990!;");

            return new ConfigurationDbContext(optionsBuilder.Options, new ConfigurationStoreOptions());
        }
    }
    
    public static class ConfigurationDbContextExtensions
    {
        public static async Task Seed(this ConfigurationDbContext context, 
            IEnumerable<Client> clients,
            IEnumerable<ApiResource> apis,
            IEnumerable<IdentityResource> resources)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in clients)
                {
                    await context.Clients.AddAsync(client.ToEntity());
                }
                await context.SaveChangesAsync();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in resources)
                {
                    await context.IdentityResources.AddAsync(resource.ToEntity());
                }
                await context.SaveChangesAsync();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var api in apis)
                {
                    await context.ApiResources.AddAsync(api.ToEntity());
                }

                await context.SaveChangesAsync();
            }
        }
    }
}