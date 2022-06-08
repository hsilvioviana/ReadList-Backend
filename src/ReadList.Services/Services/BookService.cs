using AutoMapper;
using ReadList.Application.CustomExceptions;
using ReadList.Application.QueryParams;
using ReadList.Application.Validations;
using ReadList.Application.ViewModels;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Services.Interfaces;

namespace ReadList.Services.Services
{
    public class BookService : BaseService, IBookService
    {
        protected readonly IBookRepository _repository;
        protected readonly IGenreService _genreService;
        protected readonly IMapper _mapper;
        
        public BookService(IBookRepository repository, IGenreService genreService, IMapper mapper)
        {
            _repository = repository;
            _genreService = genreService;
            _mapper = mapper;
        }

        public async Task<List<BookViewModel>> Search(Guid userId, DateRangeQueryParam range)
        {
            var models = await _repository.SearchByUserId(userId, range.StartDate, range.EndDate);

            return _mapper.Map<List<BookViewModel>>(models);
        }

        public async Task<BookViewModel> Find(FindBookViewModel viewModel)
        {
            var model = await _repository.Find(viewModel.Id);

            ThrowErrorWhen(model, "Equal", null, new EntityNotFoundException("Livro não encontrado."));

            ThrowErrorWhen(model.UserId, "NotEqual", viewModel.UserId, new UnauthorizedActionException("Você não tem autorização para visualizar este livro."));

            return _mapper.Map<BookViewModel>(model);
        }

        public async Task Create(CreateBookViewModel viewModel)
        {
            Validate(new CreateBookValidation(), viewModel);

            var model =  _mapper.Map<BookModel>(viewModel);

            model.Id = Guid.NewGuid();
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;

            await _repository.Create(model);

            await _genreService.CreateMany(viewModel.Genres, model.Id);
        }

        public async Task Update(UpdateBookViewModel viewModel)
        {
            Validate(new UpdateBookValidation(), viewModel);

            var model =  await _repository.Find(viewModel.Id);

            ThrowErrorWhen(model, "Equal", null, new EntityNotFoundException("Livro não encontrado."));

            ThrowErrorWhen(model.UserId, "NotEqual", viewModel.UserId, new UnauthorizedActionException("Você não tem autorização para editar este livro."));

            model.Title = viewModel.Title;
            model.Author = viewModel.Author;
            model.ReleaseYear = viewModel.ReleaseYear;
            model.ReadingYear = viewModel.ReadingYear;
            model.IsFiction = viewModel.IsFiction;
            model.NumberOfPages = viewModel.NumberOfPages;
            model.CountryOfOrigin = viewModel.CountryOfOrigin;
            model.Language = viewModel.Language;
            model.UpdatedAt = DateTime.Now;

            await _repository.Update(model);

            await _genreService.Reset(model.Id);

            await _genreService.CreateMany(viewModel.Genres, model.Id);
        }

        public async Task Delete(DeleteBookViewModel viewModel)
        {
            var model =  await _repository.Find(viewModel.Id);

            ThrowErrorWhen(model, "Equal", null, new EntityNotFoundException("Livro não encontrado."));

            ThrowErrorWhen(model.UserId, "NotEqual", viewModel.UserId, new UnauthorizedActionException("Você não tem autorização para deletar este livro."));

            await _repository.Delete(model.Id);
        }

        public async Task<List<FormattedBookListViewModel>> SearchDividedByYear(Guid userId)
        {
            var models = await _repository.SearchByUserId(userId, null, null);

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _repository?.Dispose();
        }
    }
}
