using System;
using Domain.Core.Database;
using IdentityServer.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityServer.Database.SqlServer
{
    public class OidcContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IDbContext
    {
        public OidcContext (DbContextOptions options): base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OidcContext).Assembly);
        }
    }
}
