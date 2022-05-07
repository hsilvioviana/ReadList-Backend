using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;

namespace ReadList.Infraestructure.Repositories
{
    public class GenreRepository : BaseRepository<GenreModel>, IGenreRepository
    {
        protected readonly PostgresDbContext _context;

        public GenreRepository(PostgresDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
