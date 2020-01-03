using System;
using IdentityServer.Core.Helpers;
using IdentityServer.Core.UserManagement;
using IdentityServer.Database.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    public static class ServiceRegistration
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            return services.ConfigureDatabase()
                    .ConfigureIdentity()
                    .ConfigureIdentityServer();
        }
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services)
        {
            return services.AddDbContext<OidcSqlServerContext>();
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<OidcSqlServerContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection ConfigureIdentityServer(this IServiceCollection services)
        {
            services.AddIdentityServer().AddDeveloperSigningCredential()
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(ConfigurationResolver.GetConfiguration().GetConnectionString("Development"));
                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30; // interval in seconds
                })
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                .AddAspNetIdentity<User>();
            return services;
        }
    }
}
