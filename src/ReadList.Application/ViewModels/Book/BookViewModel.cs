namespace ReadList.Application.ViewModels.Book
{
    public class BookViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public int ReadingYear { get; set; }
        public bool IsFiction { get; set; }
        public List<string> Genres { get; set; } = new List<string>();
        public int NumberOfPages { get; set; }
        public string CountryOfOrigin { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
    }
}
