using ReadList.Application.ViewModels;

namespace ReadList.Services.Interfaces
{
    public interface IBookService : IDisposable
    {
        Task<List<BookViewModel>> Search(Guid userId);
        Task Create(CreateBookViewModel viewModel);
        Task<List<FormattedBookListViewModel>> SearchDividedByYear(Guid userId);
    }
}
