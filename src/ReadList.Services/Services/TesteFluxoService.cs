using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Services.Interfaces;

namespace ReadList.Services.Services
{
    public class TesteFluxoService : ITesteFluxoService
    {
        ITesteFluxoRepository _repository;

        public TesteFluxoService (ITesteFluxoRepository repository)
        {
            _repository = repository;
        }

        public async Task<TesteFluxoModel> AdicionarTesteFluxo(TesteFluxoModel model)
        {
            return await _repository.AdicionarTesteFluxo(model);
        }
    }
}
