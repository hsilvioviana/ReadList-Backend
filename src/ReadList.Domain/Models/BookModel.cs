namespace ReadList.Domain.Models
{
    public class BookModel : BaseModel
    {
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public int ReadingYear { get; set; }
        public string Type { get; set; } = string.Empty;
        public List<GenreModel> Genres { get; set; } = new List<GenreModel>();
        public int NumberOfPages { get; set; }
        public string CountryOfOrigin { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;

        public virtual UserModel User { get; set; } = new UserModel();
        public virtual List<BookGenreRelation> Relations { get; set; } = new List<BookGenreRelation>();
    }
}
