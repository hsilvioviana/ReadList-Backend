using ReadList.Application.ViewModels;

namespace ReadList.Services.Interfaces
{
    public interface ITesteFluxoService
    {
        Task<TesteFluxoViewModel> Criar(CriarTesteFluxoViewModel viewModel);
    }
}
