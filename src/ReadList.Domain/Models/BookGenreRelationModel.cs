namespace ReadList.Domain.Models
{
    public class BookGenreRelationModel
    {
        public Guid BookId { get; set; }
        public Guid GenreId { get; set; }

        public virtual BookModel Book { get; set; } = new BookModel();
        public virtual GenreModel Genre { get; set; } = new GenreModel();
    }
}
