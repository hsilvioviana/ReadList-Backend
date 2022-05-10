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
  
        public async Task<StatisticsResumeViewModel> StatisticsResume(Guid userId)
        {
            var books = await _bookService.Search(userId);

            var statistics = new StatisticsResumeViewModel();

            var registeredGenres = new List<string>();

            foreach(var book in books)
            {
                foreach(var genre in book.Genres)
                {
                    if (!registeredGenres.Contains(genre))
                    {
                        registeredGenres.Add(genre);
                    }
                }
            }

            statistics.YearsWithMoreBooks = $"Anos registrados: {books.GroupBy(b => b.ReadingYear).Count()}";
            statistics.MostReadAuthors = $"Autores registrados: {books.GroupBy(b => b.Author).Count()}";
            statistics.MostReadTypes = $"Tipos registrados: {books.GroupBy(b => b.IsFiction).Count()}";
            statistics.MostReadGenres = $"Gêneros registrados: {registeredGenres.Count()}";
            statistics.MostReadCountries = $"Países registrados: {books.GroupBy(b => b.CountryOfOrigin).Count()}";
            statistics.MostReadLanguages = $"Idiomas registrados: {books.GroupBy(b => b.Language).Count()}";
            statistics.OldestBooks = $"Livros registrados: {books.Count()}";
            statistics.BiggestBooks = $"Livros registrados: {books.Count()}";
            
            return statistics;
        }

        public async Task<List<FormattedBookListViewModel>> YearsWithMoreBooks(Guid userId)
        {
            var books = await _bookService.Search(userId);

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

        public async Task<List<FormattedBookListViewModel>> MostReadAuthors(Guid userId)
        {
            var books = await _bookService.Search(userId);

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

        public async Task<List<FormattedBookListViewModel>> MostReadTypes(Guid userId)
        {
            var books = await _bookService.Search(userId);

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

        public async Task<List<FormattedBookListViewModel>> MostReadGenres(Guid userId)
        {
            var books = await _bookService.Search(userId);

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

        public async Task<List<FormattedBookListViewModel>> MostReadCountries(Guid userId)
        {
            var books = await _bookService.Search(userId);

            var countries = books.GroupBy(b => b.CountryOfOrigin);

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

        public async Task<List<FormattedBookListViewModel>> MostReadLanguages(Guid userId)
        {
            var books = await _bookService.Search(userId);

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

        public async Task<List<BookViewModel>> OldestBooks(Guid userId)
        {
            var books = await _bookService.Search(userId);

            return books.OrderBy(b => b.ReleaseYear).ToList();
        }

        public async Task<List<BookViewModel>> BiggestBooks(Guid userId)
        {
            var books = await _bookService.Search(userId);

            return books.OrderByDescending(b => b.NumberOfPages).ToList();
        }
    }
}
