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
    public class PersistedGrantDbContextDesignFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>()
                .UseSqlServer("Server=tcp:127.0.0.1,5433;Database=CM_Services_IdentityDb;User Id=SA;Password=Test1990!;");

            return new PersistedGrantDbContext(optionsBuilder.Options, new OperationalStoreOptions());
        }
    }

    public static class PersistedGrantDbContextExtensions
    {
    }
}