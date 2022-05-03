namespace ReadList.Domain.Models
{
    public class GenreModel : BaseModel
    {
        public string Name { get; set; } = string.Empty;

        // public virtual List<BookModel> Books { get; set; } = new List<BookModel>();
        public virtual List<BookGenreRelationModel> BookGenreRelations { get; set; } = new List<BookGenreRelationModel>();
    }
}
