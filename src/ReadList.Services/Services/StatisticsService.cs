using ReadList.Application.ViewModels;
using ReadList.Services.Interfaces;

namespace ReadList.Services.Services
{
    public class StatisticsService : BaseService, IStatisticsService
    {
        protected readonly IBookService _bookService;
        protected readonly IGenreService _genreService;
        
        public StatisticsService(IBookService bookService, IGenreService genreService)
        {
            _bookService = bookService;
            _genreService = genreService;
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
            var years = books.GroupBy(b => b.ReadingYear);

            var list = new List<FormattedBookListViewModel>();

            foreach(var year in years)
            {
                var includeYear = new FormattedBookListViewModel()
                {
                    Key = year.Key.ToString(),
                    Books = year.ToList(),
                };

                includeYear.SetCount();

                list.Add(includeYear);
            }

            return list.OrderByDescending(l => l.Count).ToList();
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

            return list.OrderByDescending(l => l.Count).ToList();
        }

        private static List<FormattedBookListViewModel> MostReadTypes(List<BookViewModel> books)
        {
            var types = books.GroupBy(b => b.IsFiction ? "Ficção" : "Não-Ficção");

            var list = new List<FormattedBookListViewModel>();

            foreach(var type in types)
            {
                var includeType = new FormattedBookListViewModel()
                {
                    Key = type.Key,
                    Books = type.ToList(),
                };

                includeType.SetCount();

                list.Add(includeType);
            }

            return list.OrderByDescending(l => l.Count).ToList();
        }

        private static List<FormattedBookListViewModel> MostReadGenres(List<BookViewModel> books)
        {
            var list = new List<FormattedBookListViewModel>();

            var registeredGenres = new List<string>();

            foreach(var book in books)
            {
                foreach(var genre in book.Genres)
                {
                    if (registeredGenres.Contains(genre))
                    {
                        var indexOfTheGenre = registeredGenres.IndexOf(genre);

                        list[indexOfTheGenre].Books.Add(book);
                    }
                    else
                    {
                        registeredGenres.Add(genre);

                        list.Add(new FormattedBookListViewModel()
                        {
                            Key = genre,
                            Books = new List<BookViewModel>() { book }
                        });
                    }
                }
            }

            list.ForEach(l => l.SetCount());

            return list.OrderByDescending(l => l.Count).ToList();
        }

        private static List<FormattedBookListViewModel> MostReadCountries(List<BookViewModel> books)
        {
            var countries = books.GroupBy(b => b.Language);

            var list = new List<FormattedBookListViewModel>();

            foreach(var country in countries)
            {
                var includeCountry = new FormattedBookListViewModel()
                {
                    Key = country.Key,
                    Books = country.ToList(),
                };

                includeCountry.SetCount();

                list.Add(includeCountry);
            }

            return list.OrderByDescending(l => l.Count).ToList();
        }

        private static List<FormattedBookListViewModel> MostReadLanguages(List<BookViewModel> books)
        {
            var languages = books.GroupBy(b => b.Language);

            var list = new List<FormattedBookListViewModel>();

            foreach(var language in languages)
            {
                var includeLanguage = new FormattedBookListViewModel()
                {
                    Key = language.Key,
                    Books = language.ToList(),
                };

                includeLanguage.SetCount();

                list.Add(includeLanguage);
            }

            return list.OrderByDescending(l => l.Count).ToList();
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
