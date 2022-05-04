using Microsoft.EntityFrameworkCore;
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

        public async Task<UserModel> SearchByUsername(string username)
        {
            return await DbSet.Where(U => U.Username == username).FirstOrDefaultAsync();
        }

        public async Task<UserModel> SearchByEmail(string email)
        {
            return await DbSet.Where(U => U.Email == email).FirstOrDefaultAsync();
        }
    }
}
