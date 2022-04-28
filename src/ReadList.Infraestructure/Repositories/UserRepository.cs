using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;

namespace ReadList.Infraestructure.Repositories
{
    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {
        PostgresDbContext _context;

        public UserRepository(PostgresDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
