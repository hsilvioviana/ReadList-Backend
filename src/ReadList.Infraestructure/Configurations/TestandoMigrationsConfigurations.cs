using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReadList.Infraestructure.Configurations
{
    public class TestandoMigrationsConfigurations : IEntityTypeConfiguration<TestandoMigrations>
    {
        public void Configure(EntityTypeBuilder<TestandoMigrations> builder)
        {
            builder
                .HasKey(t => t.Id);

            builder.Property(t => t.Nome)
                .HasColumnName("nome");

            builder.Property(t => t.Numero)
                .HasColumnName("numero");

            builder.Property(t => t.Data)
                .HasColumnName("data");

            builder.ToTable("teste", schema: "readlist");
        }
    }
}
