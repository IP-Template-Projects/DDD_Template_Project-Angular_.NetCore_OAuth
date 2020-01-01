using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityServer.Database.SqlServer
{
    public class OidcContextFactory : IDesignTimeDbContextFactory<OidcContext>
    {
        public OidcContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OidcContext>();
            optionsBuilder.UseSqlServer("",
                builder => builder.MigrationsAssembly(typeof(OidcContext).Assembly.FullName));
            var context = new OidcContext(optionsBuilder.Options);
            return context;
        }
    }
}
