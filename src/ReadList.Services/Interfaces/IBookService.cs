using ReadList.Application.ViewModels;

namespace ReadList.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<BookViewModel>> Search(Guid userId);
        Task<List<FormattedBookListViewModel>> SearchDividedByYear(Guid userId);
    }
}
