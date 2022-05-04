using ReadList.Application.ViewModels.Book;

namespace ReadList.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<BookViewModel>> Search(Guid userId);
    }
}
