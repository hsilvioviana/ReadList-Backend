using AutoMapper;
using ReadList.Application.ViewModels;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Services.Interfaces;

namespace ReadList.Services.Services
{
    public class BookGenreRelationService : BaseService, IBookGenreRelationService
    {
        protected readonly IBookGenreRelationRepository _repository;
        protected readonly IMapper _mapper;
        
        public BookGenreRelationService(IBookGenreRelationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Create(CreateBookGenreRelationViewModel viewModel)
        {
            var model = _mapper.Map<BookGenreRelationModel>(viewModel);

            await _repository.Create(model);
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
