namespace ReadList.Domain.Models
{
    public class GenreModel : BaseModel
    {
        public string Name { get; set; }

        public virtual List<BookGenreRelationModel> BookGenreRelations { get; set; }
    }
}
