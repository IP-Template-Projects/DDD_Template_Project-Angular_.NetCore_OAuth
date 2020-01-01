using Microsoft.EntityFrameworkCore;

namespace Database.SqlServer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext (DbContextOptions options): base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }
    }

   
}
