using ReadList.Domain.Models;

namespace ReadList.Domain.Interfaces
{
    public interface IBookGenreRelationRepository : IDisposable
    {
         Task Create(BookGenreRelationModel model);
    }
}
