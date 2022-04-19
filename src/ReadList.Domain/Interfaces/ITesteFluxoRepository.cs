using ReadList.Domain.Models;

namespace ReadList.Domain.Interfaces
{
    public interface ITesteFluxoRepository
    {
        public Task<TesteFluxoModel> AdicionarTesteFluxo(TesteFluxoModel model);
    }
}
