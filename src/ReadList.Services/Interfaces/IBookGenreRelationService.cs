using ReadList.Application.ViewModels;

namespace ReadList.Services.Interfaces
{
    public interface IBookGenreRelationService : IDisposable
    {
        Task Create(CreateBookGenreRelationViewModel viewModel);
        Task DeleteByBookId(Guid bookId);
    }
}
