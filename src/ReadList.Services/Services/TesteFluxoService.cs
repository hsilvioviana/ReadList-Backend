using AutoMapper;
using ReadList.Application.ViewModels;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Services.Interfaces;

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
            var model = _mapper.Map<TesteFluxoModel>(viewModel);

            model.Id = Guid.NewGuid();
            model.DataCriacao = DateTime.Now;
            model.DataModificacao = DateTime.Now;

            await _repository.Criar(model);

            return _mapper.Map<TesteFluxoViewModel>(model);
        }
    }
}
