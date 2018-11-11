using System.Linq;
using System.Threading.Tasks;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using CM.Services.Identity.Infrastracture.Constants;
using CM.Services.Identity.Infrastracture.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CM.Services.Identity.Infrastracture.Contexts
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(SchemaNames.Default);

            new IdentityMap(builder);
        }
    }

    public class ApplicationContextDesignFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer("Server=tcp:127.0.0.1,5433;Database=CM_Services_IdentityDb;User Id=SA;Password=Test1990!;");

            return new ApplicationContext(optionsBuilder.Options);
        }
    }

    public static class ApplicationContextExtensions
    {
        public static async Task Seed(this ApplicationContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            string roleName = "Admin";
            
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new ApplicationRole(roleName));
            }

            var user = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@admin.pl",
                Name = "Admin",
                LastName = "Admin"
            };
            var adminResult = await userManager.CreateAsync(user, "Admin1990!");

            if (adminResult.Succeeded)
            {
                var adminRoleResult = await userManager.AddToRoleAsync(user, roleName);
                
                if (adminRoleResult.Succeeded)
                {
                    var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    await userManager.ConfirmEmailAsync(user, confirmationToken);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}

