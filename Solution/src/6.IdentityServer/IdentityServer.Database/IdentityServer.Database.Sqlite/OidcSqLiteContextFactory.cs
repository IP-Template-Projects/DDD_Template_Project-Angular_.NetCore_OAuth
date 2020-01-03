using IdentityServer.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IdentityServer.Database.SQLite
{
    public class OidcSqLiteContextFactory : IDesignTimeDbContextFactory<OidcSqLiteContext>
    {

        public OidcSqLiteContext CreateDbContext(string[] args)
        {
            var config = ConfigurationResolver.GetConfiguration();
            var optionsBuilder = new DbContextOptionsBuilder<OidcSqLiteContext>();
            optionsBuilder.UseSqlite(config.GetConnectionString("SQLite"),
                builder => builder.MigrationsAssembly(typeof(OidcSqLiteContext).Assembly.FullName));
            var context = new OidcSqLiteContext(optionsBuilder.Options);
            return context;
        }
        protected OidcSqLiteContext CreateNewInstance(DbContextOptions<OidcSqLiteContext> options)
        {
            return new OidcSqLiteContext(options);
        }
    }
}
