using ReadList.Application.ViewModels;
using ReadList.Services.Interfaces;

namespace ReadList.Services.Services
{
    public class StatisticsService : BaseService, IStatisticsService
    {
        protected readonly IBookService _bookService;
        
        public StatisticsService(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<StatisticsViewModel> Statistics(Guid userId)
        {
            var books = await _bookService.Search(userId);

            var statistics = new StatisticsViewModel();

            statistics.YearsWithMoreBooks = YearsWithMoreBooks(books);
            statistics.MostReadAuthors = MostReadAuthors(books);
            statistics.MostReadTypes = MostReadTypes(books);
            statistics.MostReadGenres = MostReadGenres(books);
            statistics.MostReadCountries = MostReadCountries(books);
            statistics.MostReadLanguages = MostReadLanguages(books);
            statistics.OldestBooks = OldestBooks(books);
            statistics.BiggestBooks = BiggestBooks(books);
            
            return statistics;
        }

        private static List<FormattedBookListViewModel> YearsWithMoreBooks(List<BookViewModel> books)
        {
            return new List<FormattedBookListViewModel>();
        }

        private static List<FormattedBookListViewModel> MostReadAuthors(List<BookViewModel> books)
        {
            var authors = books.GroupBy(b => b.Author);

            var list = new List<FormattedBookListViewModel>();

            foreach(var author in authors)
            {
                var includeAuthor = new FormattedBookListViewModel()
                {
                    Key = author.Key,
                    Books = author.ToList(),
                };

                includeAuthor.SetCount();

                list.Add(includeAuthor);
            }

            return list;
        }

        private static List<FormattedBookListViewModel> MostReadTypes(List<BookViewModel> books)
        {
            return new List<FormattedBookListViewModel>();
        }

        private static List<FormattedBookListViewModel> MostReadGenres(List<BookViewModel> books)
        {
            return new List<FormattedBookListViewModel>();
        }

        private static List<FormattedBookListViewModel> MostReadCountries(List<BookViewModel> books)
        {
            return new List<FormattedBookListViewModel>();
        }

        private static List<FormattedBookListViewModel> MostReadLanguages(List<BookViewModel> books)
        {
            return new List<FormattedBookListViewModel>();
        }

        private static List<BookViewModel> OldestBooks(List<BookViewModel> books)
        {
            return books.OrderBy(b => b.ReleaseYear).ToList();
        }

        private static List<BookViewModel> BiggestBooks(List<BookViewModel> books)
        {
            return books.OrderByDescending(b => b.NumberOfPages).ToList();
        }
    }
}
