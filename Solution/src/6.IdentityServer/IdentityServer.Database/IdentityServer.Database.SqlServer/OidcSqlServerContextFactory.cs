using IdentityServer.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IdentityServer.Database.SqlServer
{
    public class OidcSqlServerContextFactory : IDesignTimeDbContextFactory<OidcSqlServerContext>
    {
        public OidcSqlServerContext CreateDbContext(string[] args)
        {
            var config = ConfigurationResolver.GetConfiguration();
            var optionsBuilder = new DbContextOptionsBuilder<OidcSqlServerContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("SQLServer"),
                builder => builder.MigrationsAssembly(typeof(OidcSqlServerContext).Assembly.FullName));
            var context = new OidcSqlServerContext(optionsBuilder.Options);
            return context;
        }
    }
}
