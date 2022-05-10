using ReadList.Application.ViewModels;

namespace ReadList.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<StatisticsResumeViewModel> StatisticsResume(Guid userId);
        Task<List<FormattedBookListViewModel>> YearsWithMoreBooks(Guid userId);
        Task<List<FormattedBookListViewModel>> MostReadAuthors(Guid userId);
        Task<List<FormattedBookListViewModel>> MostReadTypes(Guid userId);
        Task<List<FormattedBookListViewModel>> MostReadGenres(Guid userId);
        Task<List<FormattedBookListViewModel>> MostReadCountries(Guid userId);
        Task<List<FormattedBookListViewModel>> MostReadLanguages(Guid userId);
        Task<List<BookViewModel>> OldestBooks(Guid userId);
        Task<List<BookViewModel>> BiggestBooks(Guid userId);
    }
}
