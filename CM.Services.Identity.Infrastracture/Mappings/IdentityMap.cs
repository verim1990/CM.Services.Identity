using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using CM.Services.Identity.Infrastracture.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CM.Services.Identity.Infrastracture.Mappings
{
    public class IdentityMap
    {
        public IdentityMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().ToTable(TableNames.Users, SchemaNames.Identity)
                .HasKey(x => x.Id);
            modelBuilder.Entity<ApplicationRole>().ToTable(TableNames.Roles, SchemaNames.Identity)
                .HasKey(x => x.Id);
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable(TableNames.UserRoles, SchemaNames.Identity)
                .HasKey(x => new {x.RoleId, x.UserId});
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable(TableNames.UserLogin, SchemaNames.Identity)
                .HasKey(x => new {x.ProviderKey, x.LoginProvider});
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable(TableNames.RoleClaims, SchemaNames.Identity)
                .HasKey(x => x.Id);
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable(TableNames.UserClaims, SchemaNames.Identity)
                .HasKey(x => x.Id);
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable(TableNames.UserToken, SchemaNames.Identity)
                .HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
        }
    }
}