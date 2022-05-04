using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ReadList.Domain.Models;

namespace ReadList.Infraestructure.Context
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
        { 
            
        }

        public DbSet<UserModel> User => Set<UserModel>();
        public DbSet<BookModel> Book => Set<BookModel>();
        public DbSet<GenreModel> Genre => Set<GenreModel>();
        // public DbSet<BookGenreRelationModel> BookGenreRelation => Set<BookGenreRelationModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
