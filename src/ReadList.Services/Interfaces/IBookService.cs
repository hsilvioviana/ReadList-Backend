using ReadList.Application.QueryParams;
using ReadList.Application.ViewModels;

namespace ReadList.Services.Interfaces
{
    public interface IBookService : IDisposable
    {
        Task<List<BookViewModel>> Search(Guid userId, DateRangeQueryParam range);
        Task<BookViewModel> Find(FindBookViewModel viewModel);
        Task Create(CreateBookViewModel viewModel);
        Task Update(UpdateBookViewModel viewModel);
        Task Delete(DeleteBookViewModel viewModel);
        Task<List<FormattedBookListViewModel>> SearchDividedByYear(Guid userId);
    }
}
