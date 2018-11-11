using CM.Services.Identity.API.Services;
using CM.Services.Identity.Concrete;
using CM.Services.Identity.Concrete.Global.Login.Application.CommandHandlers;
using CM.Services.Identity.Concrete.Global.Register.Application.Validators;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using CM.Services.Identity.Infrastracture;
using CM.Services.Identity.Infrastracture.Certificate;
using CM.Services.Identity.Infrastracture.Constants;
using CM.Services.Identity.Infrastracture.Contexts;
using CM.Shared.Kernel.Others.Sql;
using CM.Shared.Web;
using CM.Shared.Web.Others.FluentValidation;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CM.Services.Identity.API
{
    public static class IdentityServicesExtentions {

        public static IServiceCollection IncludeIdentityServer(this IServiceCollection services, string connectionString)
        {
            var sqlOptionBuilder = ExtentionsForSql.GetSqlOptionBuilder<ApplicationContext>();

            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(x =>
            {
                x.IssuerUri = "null";
                x.UserInteraction.LoginUrl = "/public/login/login";
                x.UserInteraction.LogoutUrl = "/public/logout/logout";
                x.UserInteraction.ConsentUrl = "/admin/consent";
                x.UserInteraction.ErrorUrl = "/home/error";
            })
                .AddSigningCredential(Certificate.Get())
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.DefaultSchema = SchemaNames.Identity;
                    options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sqlOptionBuilder);
                })
                .AddOperationalStore(options =>
                {
                    options.DefaultSchema = SchemaNames.Identity;
                    options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sqlOptionBuilder);
                })
                .Services.AddTransient<IProfileService, ProfileService>();

            return services;
        }
    }
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings = Configuration.Get<AppSettings>();
        }

        public IConfiguration Configuration { get; }

        public AppSettings AppSettings { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .Initialize<AppSettings>(Configuration, AppSettings.Global.Identity)
                .IncludeCors(AppSettings.Global.Web.HttpsUrl, AppSettings.Global.Wallet.HttpsUrl, AppSettings.Global.WalletViews.HttpsUrl, AppSettings.Global.Exchange.HttpsUrl)
                .IncludeAuthenticationForIdentity()
                .IncludeSqlServer<ApplicationContext>(AppSettings.Global.Sql, AppSettings.Local.Sql)
                .IncludeBus(typeof(LoginCommandHandler))
                .IncludeIdentityServer(AppSettings.Global.Sql.ConnectionString)
                .AddMvc()
                .IncludeFluentValidation(typeof(RegisterCommandValidator))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return ServicesExtentions.RegisterModules(services, new InfrastractureAutofacModule(), new ConcreteAutofacModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCorsForCM();
            app.UseIdentityServer();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}

