using ReadList.Domain.Models;

namespace ReadList.Domain.Interfaces
{
    public interface IBookRepository : IBaseRepository<BookModel>
    {
        Task<List<BookModel>> SearchByUserId(Guid userId);
    }
}
