using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database.SqlServer
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer("",
                builder => builder.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName));
            var context = new DatabaseContext(optionsBuilder.Options);
            return context;
        }
    }
}
