using AutoMapper;
using ReadList.Application.ViewModels;
using ReadList.Domain.Interfaces;
using ReadList.Services.Interfaces;

namespace ReadList.Services.Services
{
    public class BookService : IBookService
    {
        protected readonly IBookRepository _repository;
        protected readonly IMapper _mapper;
        
        public BookService(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BookViewModel>> Search(Guid userId)
        {
            var models = await _repository.SearchByUserId(userId);

            return _mapper.Map<List<BookViewModel>>(models);
        }

        public async Task<List<FormattedBookListViewModel>> SearchDividedByYear(Guid userId)
        {
            var models = await _repository.SearchByUserId(userId);

            var viewModels = _mapper.Map<List<BookViewModel>>(models);

            var list = new List<FormattedBookListViewModel>();

            var registeredYears = new List<int>();

            foreach(var book in viewModels)
            {
                if (registeredYears.Contains(book.ReadingYear))
                {
                    var indexOfTheYear = registeredYears.IndexOf(book.ReadingYear);

                    list[indexOfTheYear].Books.Add(book);
                }
                else
                {
                    registeredYears.Add(book.ReadingYear);

                    list.Add(new FormattedBookListViewModel()
                    {
                        Key = book.ReadingYear.ToString(),
                        Books = new List<BookViewModel>() { book }
                    });
                }
            }

            list.ForEach(l => l.SetCount());

            return list;
        }
    }
}
