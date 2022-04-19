using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;

namespace ReadList.Infraestructure.Repositories
{
    public class TesteFluxoRepository : ITesteFluxoRepository
    {
        PostgresDbContext _context;
        public TesteFluxoRepository (PostgresDbContext context)
        {
            _context = context;
        }

        public async Task<TesteFluxoModel> AdicionarTesteFluxo (TesteFluxoModel model)
        {
            await _context.TesteFluxo.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
