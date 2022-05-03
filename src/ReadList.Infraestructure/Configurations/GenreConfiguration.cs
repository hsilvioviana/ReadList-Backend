using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadList.Domain.Models;

namespace ReadList.Infraestructure.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<GenreModel>
    {
        public void Configure(EntityTypeBuilder<GenreModel> builder)
        {
            builder
                .HasKey(t => t.Id);

            builder
                .Property(t => t.Id)
                .HasColumnName("id");

            builder
                .Property(t => t.Name)
                .HasColumnName("name");
            
            builder
                .Property(t => t.CreatedAt)
                .HasColumnName("created_at");

            builder
                .Property(t => t.UpdatedAt)
                .HasColumnName("updated_at");

            builder
                .ToTable("genres", schema: "readlist");
        }
    }
}
