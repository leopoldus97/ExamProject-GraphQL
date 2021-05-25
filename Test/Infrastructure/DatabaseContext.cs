using Microsoft.EntityFrameworkCore;
using Test.Core.Entity;

namespace Test.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder mb) {
            base.OnModelCreating(mb);

            mb.Entity<City>(entity => {
                entity.Property(e => e.CreationDate).HasDefaultValueSql("(now())");
            });

            mb.Entity<Country>(entity => {
                entity.Property(e => e.CreationDate).HasDefaultValueSql("(now())");
            });
        }
    }
}