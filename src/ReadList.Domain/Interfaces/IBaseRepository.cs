namespace ReadList.Domain.Interfaces
{
    public interface IBaseRepository<TModel> : IDisposable
    {
        Task<IList<TModel>> Buscar();
        Task<TModel> BuscarPorId(Guid id);        
        Task Criar(TModel model);
        Task CriarVarios(List<TModel> models);
        Task Alterar(TModel model);
        Task Deletar(Guid id);
    }
}
