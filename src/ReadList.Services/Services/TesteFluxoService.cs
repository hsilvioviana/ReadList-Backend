using AutoMapper;
using ReadList.Application.ViewModels;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Services.Interfaces;
using ReadList.Application.Validation;

namespace ReadList.Services.Services
{
    public class TesteFluxoService : ITesteFluxoService
    {
        ITesteFluxoRepository _repository;
        IMapper _mapper;

        public TesteFluxoService (ITesteFluxoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TesteFluxoViewModel> Criar(CriarTesteFluxoViewModel viewModel)
        {
            var result = new CriarTesteFluxoValidation().Validate(viewModel);

            if(!result.IsValid) 
            {
                throw new Exception(result.Errors[0].ToString());
            }

            var model = _mapper.Map<TesteFluxoModel>(viewModel);

            model.Id = Guid.NewGuid();
            model.DataCriacao = DateTime.Now;
            model.DataModificacao = DateTime.Now;

            await _repository.Criar(model);

            return _mapper.Map<TesteFluxoViewModel>(model);
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
