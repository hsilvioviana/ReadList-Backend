namespace ReadList.Domain.Models
{
    public class BookModel : BaseModel
    {
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public int ReadingYear { get; set; }
        public bool IsFiction { get; set; }
        public int NumberOfPages { get; set; }
        public string CountryOfOrigin { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;

        // public virtual List<GenreModel> Genres { get; set; } = new List<GenreModel>();
        public virtual List<BookGenreRelationModel> BookGenreRelations { get; set; } = new List<BookGenreRelationModel>();
        public virtual UserModel User { get; set; } = new UserModel();
    }
}
