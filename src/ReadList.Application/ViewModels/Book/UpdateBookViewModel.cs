using System.Text.Json.Serialization;

namespace ReadList.Application.ViewModels
{
    public class UpdateBookViewModel
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        
        public string Title { get; set; }
        public string Author { get; set; }
        public int ReleaseYear { get; set; }
        public int ReadingYear { get; set; }
        public bool IsFiction { get; set; }
        public List<string> Genres { get; set; }
        public int NumberOfPages { get; set; }
        public string CountryOfOrigin { get; set; }
        public string Language { get; set; }
    }
}
