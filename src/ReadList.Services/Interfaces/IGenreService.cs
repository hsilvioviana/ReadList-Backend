namespace ReadList.Services.Interfaces
{
    public interface IGenreService : IDisposable
    {
        Task CreateMany(List<string> genres, Guid bookId);
    }
}
