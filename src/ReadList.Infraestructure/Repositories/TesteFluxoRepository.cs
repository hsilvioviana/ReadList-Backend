using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;

namespace ReadList.Infraestructure.Repositories
{
    public class TesteFluxoRepository : BaseRepository<TesteFluxoModel>, ITesteFluxoRepository
    {
        PostgresDbContext _context;
        public TesteFluxoRepository (PostgresDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
