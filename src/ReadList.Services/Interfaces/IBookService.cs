using ReadList.Application.ViewModels;

namespace ReadList.Services.Interfaces
{
    public interface IBookService : IDisposable
    {
        Task<List<BookViewModel>> Search(Guid userId);
        Task Create(CreateBookViewModel viewModel);
        Task Update(UpdateBookViewModel viewModel);
        Task Delete(DeleteBookViewModel viewModel);
        Task<List<FormattedBookListViewModel>> SearchDividedByYear(Guid userId);
    }
}
