namespace ReadList.Domain.Models
{
    public class GenreModel : BaseModel
    {
        public string Name { get; set; } = string.Empty;

        public virtual List<BookGenreRelation> Relations { get; set; } = new List<BookGenreRelation>();
    }
}
