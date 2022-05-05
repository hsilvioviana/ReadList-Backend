namespace ReadList.Application.ViewModels
{
    public class FormattedBookListViewModel
    {
        public string Key { get; set; }
        public int Count { get; set; }
        public List<BookViewModel> Books { get; set; }

        public void SetCount()
        {
            Count = Books.Count();
        }
    }
}
