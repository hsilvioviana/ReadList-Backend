using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadList.Domain.Models;

namespace ReadList.Infraestructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder
                .HasKey(t => t.Id);

            builder
                .Property(t => t.Id)
                .HasColumnName("id");

            builder
                .Property(t => t.Username)
                .HasColumnName("username");
            
            builder
                .Property(t => t.Email)
                .HasColumnName("email");

            builder
                .Property(t => t.Password)
                .HasColumnName("password");
            
            builder
                .Property(t => t.CreatedAt)
                .HasColumnName("created_at");

            builder
                .Property(t => t.UpdatedAt)
                .HasColumnName("updated_at");

            builder
                .ToTable("users", schema: "readlist");
        }
    }
}
