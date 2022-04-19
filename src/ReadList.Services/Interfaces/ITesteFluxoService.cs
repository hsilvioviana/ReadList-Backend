using ReadList.Domain.Models;

namespace ReadList.Services.Interfaces
{
    public interface ITesteFluxoService
    {
        public Task<TesteFluxoModel> AdicionarTesteFluxo (TesteFluxoModel model);
    }
}