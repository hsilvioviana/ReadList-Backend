using ReadList.Application.QueryParams;
using ReadList.Application.ViewModels;

namespace ReadList.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<StatisticsResumeViewModel> Resume(Guid userId, DateRangeQueryParam range);
        Task<List<FormattedBookListViewModel>> YearsWithMoreBooks(Guid userId, DateRangeQueryParam range);
        Task<List<FormattedBookListViewModel>> MostReadAuthors(Guid userId, DateRangeQueryParam range);
        Task<List<FormattedBookListViewModel>> MostReadTypes(Guid userId, DateRangeQueryParam range);
        Task<List<FormattedBookListViewModel>> MostReadGenres(Guid userId, DateRangeQueryParam range);
        Task<List<FormattedBookListViewModel>> MostReadCountries(Guid userId, DateRangeQueryParam range);
        Task<List<FormattedBookListViewModel>> MostReadLanguages(Guid userId, DateRangeQueryParam range);
        Task<List<BookViewModel>> OldestBooks(Guid userId, DateRangeQueryParam range);
        Task<List<BookViewModel>> BiggestBooks(Guid userId, DateRangeQueryParam range);
    }
}
