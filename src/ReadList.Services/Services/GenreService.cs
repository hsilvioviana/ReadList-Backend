using AutoMapper;
using ReadList.Application.ViewModels;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Services.Interfaces;

namespace ReadList.Services.Services
{
    public class GenreService : BaseService, IGenreService
    {
        protected readonly IGenreRepository _repository;
        protected readonly IBookGenreRelationService _bookGenreRelationService;
        protected readonly IMapper _mapper;
        
        public GenreService(IGenreRepository repository, IBookGenreRelationService bookGenreRelationService, IMapper mapper)
        {
            _repository = repository;
            _bookGenreRelationService = bookGenreRelationService;
            _mapper = mapper;
        }

        public async Task CreateMany(List<string> genresToAdd, Guid bookId)
        {
            var genresAlreadyOnDataBase = await _repository.Search();

            var genresAlreadyOnDataBaseNames = genresAlreadyOnDataBase.Select(g => g.Name).ToList();

            var genresToBeCreated = (from genreToAdd in genresToAdd where !genresAlreadyOnDataBaseNames.Contains(genreToAdd) select genreToAdd).ToList();

            var genresAlreadyCreated = (from genreAlreadyOnDataBase in genresAlreadyOnDataBase where genresToAdd.Contains(genreAlreadyOnDataBase.Name) select genreAlreadyOnDataBase).ToList();

            foreach(var genre in genresAlreadyCreated)
            {
                var newRelation = new CreateBookGenreRelationViewModel()
                {
                    BookId = bookId,
                    GenreId = genre.Id
                };

                await _bookGenreRelationService.Create(newRelation);
            }

            foreach (var genreName in genresToBeCreated)
            {
                var newGenre = new GenreModel() 
                {
                    Id = Guid.NewGuid(),
                    Name = genreName,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _repository.Create(newGenre);

                var newRelation = new CreateBookGenreRelationViewModel()
                {
                    BookId = bookId,
                    GenreId = newGenre.Id
                };

                await _bookGenreRelationService.Create(newRelation);   
            }
        }

        public async Task Reset(Guid bookId)
        {
            await _bookGenreRelationService.DeleteByBookId(bookId); 
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
