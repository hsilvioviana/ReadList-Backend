using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ReadList.Infraestructure.Context
{
    public class PostgresDbContext : DbContext
    {
        public DbSet<TestandoMigrations>? TestandoMigrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Username=postgres;Password=postgres;Database=postgres");
        }
    }
}
