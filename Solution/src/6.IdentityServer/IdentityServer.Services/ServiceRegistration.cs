using System;
using IdentityServer.Core;
using IdentityServer.Database.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    public static class ServiceRegistration
    {
        //public static IServiceCollection ConfigureDevelopmentDependencies(this IServiceCollection services)
        //{
        //    return services
        //        .ConfigureMocks()
        //        .ConfigureGenericRepositories()
        //        .ConfigureRepositories();

        //}

        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            return services.ConfigureDatabase();
            //.ConfigureGenericRepositories()
            //.ConfigureRepositories();
        }
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services)
        {
            return services.AddDbContext<OidcContext>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<OidcContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityServer().AddDeveloperSigningCredential()
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(configuration.GetConnectionString("Default"));
                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30; // interval in seconds
                })
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<User>();
        }

        //private static IServiceCollection ConfigureGenericRepositories(this IServiceCollection services)
        //{
        //    return services
        //        .AddScoped<IRepository<Category>, RepositoryBase<Category>>()
        //        .AddScoped<IRepository<Order>, RepositoryBase<Order>>()
        //        .AddScoped<IRepository<Product>, RepositoryBase<Product>>();
        //}

        //private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        //{
        //    return services
        //        .AddScoped<ICategoryRepository, ICategoryRepository>()
        //        .AddScoped<IOrderRepository, OrderRepository>()
        //        .AddScoped<IProductRepository, ProductRepository>()
        //        .AddScoped<IUserRepository, UserRepository>();
        //}

        //private static IServiceCollection ConfigureMocks(this IServiceCollection services)
        //{
        //    return services
        //        .AddSingleton<IEmailSender, EmailSenderMock>();
        //}
    }
}
