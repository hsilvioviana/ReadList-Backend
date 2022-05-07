using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadList.Domain.Models;

namespace ReadList.Infraestructure.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<BookModel>
    {
        public void Configure(EntityTypeBuilder<BookModel> builder)
        {
            builder
                .HasKey(t => t.Id);

            builder
                .Property(t => t.Id)
                .HasColumnName("id");

            builder
                .Property(t => t.UserId)
                .HasColumnName("user_id");

            builder
                .HasOne(b => b.User)
                .WithMany(u => u.Books)
                .HasForeignKey(b => b.UserId);
            
            builder
                .Property(t => t.Title)
                .HasColumnName("title");

            builder
                .Property(t => t.Author)
                .HasColumnName("author");

            builder
                .Property(t => t.ReleaseYear)
                .HasColumnName("release_year");

            builder
                .Property(t => t.ReadingYear)
                .HasColumnName("reading_year");

            builder
                .Property(t => t.IsFiction)
                .HasColumnName("is_fiction");

            builder
                .Property(t => t.NumberOfPages)
                .HasColumnName("number_of_pages");

            builder
                .Property(t => t.CountryOfOrigin)
                .HasColumnName("country_of_origin");

            builder
                .Property(t => t.Language)
                .HasColumnName("language");
            
            builder
                .Property(t => t.CreatedAt)
                .HasColumnName("created_at");

            builder
                .Property(t => t.UpdatedAt)
                .HasColumnName("updated_at");

            builder
                .ToTable("books", schema: "readlist");
        }
    }
}
