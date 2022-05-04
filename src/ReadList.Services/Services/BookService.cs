using AutoMapper;
using ReadList.Application.ViewModels.Book;
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
    }
}
