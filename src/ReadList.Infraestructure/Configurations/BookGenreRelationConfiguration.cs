using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadList.Domain.Models;

namespace ReadList.Infraestructure.Configurations
{
    public class BookGenreRelationConfiguration : IEntityTypeConfiguration<BookGenreRelationModel>
    {
        public void Configure(EntityTypeBuilder<BookGenreRelationModel> builder)
        {
            builder
                .HasKey(t => new { t.BookId, t.GenreId });

            builder
                .Property(t => t.BookId)
                .HasColumnName("book_id");

            builder
                .HasOne(r => r.Book)
                .WithMany(b => b.BookGenreRelations)
                .HasForeignKey(r => r.BookId);
            
            builder
                .Property(t => t.GenreId)
                .HasColumnName("genre_id");

            builder
                .HasOne(r => r.Genre)
                .WithMany(b => b.BookGenreRelations)
                .HasForeignKey(r => r.GenreId);

            builder
                .ToTable("books_genres_relations", schema: "readlist");
        }
    }
}
