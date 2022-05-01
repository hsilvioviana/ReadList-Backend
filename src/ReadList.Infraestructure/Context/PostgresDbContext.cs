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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<UserModel>()
                .Property(t => t.Id)
                .HasColumnName("id");

            modelBuilder.Entity<UserModel>()
                .Property(t => t.Username)
                .HasColumnName("username");
            
            modelBuilder.Entity<UserModel>()
                .Property(t => t.Email)
                .HasColumnName("email");

            modelBuilder.Entity<UserModel>()
                .Property(t => t.Password)
                .HasColumnName("password");
            
            modelBuilder.Entity<UserModel>()
                .Property(t => t.CreatedAt)
                .HasColumnName("created_at");

            modelBuilder.Entity<UserModel>()
                .Property(t => t.UpdatedAt)
                .HasColumnName("updated_at");

            modelBuilder.Entity<UserModel>()
                .ToTable("users", schema: "readlist");
        }
    }
}
