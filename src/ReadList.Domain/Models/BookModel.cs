namespace ReadList.Domain.Models
{
    public class BookModel : BaseModel
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int ReleaseYear { get; set; }
        public int ReadingYear { get; set; }
        public bool IsFiction { get; set; }
        public int NumberOfPages { get; set; }
        public string CountryOfOrigin { get; set; }
        public string Language { get; set; }
        public virtual List<BookGenreRelationModel> BookGenreRelations { get; set; }
        public virtual UserModel User { get; set; }
    }
}
