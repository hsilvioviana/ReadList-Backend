using Microsoft.EntityFrameworkCore;
using ReadList.Domain.Models;

namespace ReadList.Infraestructure.Context
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
        { 
            
        }

        public DbSet<TesteFluxoModel> TesteFluxo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TesteFluxoModel>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TesteFluxoModel>()
                .Property(t => t.Nome)
                .HasColumnName("nome");
            
            modelBuilder.Entity<TesteFluxoModel>()
                .Property(t => t.Numero)
                .HasColumnName("numero");

            modelBuilder.Entity<TesteFluxoModel>()
                .ToTable("teste", schema: "readlist");
        }
    }
}
