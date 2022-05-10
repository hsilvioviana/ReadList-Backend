namespace ReadList.Application.ViewModels
{
    public class StatisticsViewModel
    {
        public List<FormattedBookListViewModel> YearsWithMoreBooks { get; set; }
        public List<FormattedBookListViewModel> MostReadAuthors { get; set; }
        public List<FormattedBookListViewModel> MostReadTypes { get; set; }
        public List<FormattedBookListViewModel> MostReadGenres { get; set; }
        public List<FormattedBookListViewModel> MostReadCountries { get; set; }
        public List<FormattedBookListViewModel> MostReadLanguages { get; set; }
        public List<BookViewModel> OldestBooks { get; set; }
        public List<BookViewModel> BiggestBooks { get; set; }
    }
}
