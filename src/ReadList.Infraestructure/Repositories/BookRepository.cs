using Microsoft.EntityFrameworkCore;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;

namespace ReadList.Infraestructure.Repositories
{
    public class BookRepository : BaseRepository<BookModel>, IBookRepository
    {
        PostgresDbContext _context;

        public BookRepository(PostgresDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<BookModel>> SearchByUserId(Guid userId)
        {
            return await _context.Book.Where(b => b.UserId == userId)
            .Include(b => b.BookGenreRelations)
                .ThenInclude(bgr => bgr.Genre)
            .OrderBy(b => b.ReadingYear)
                .ThenBy(b => b.CreatedAt)
            .ToListAsync();
        }
    }
}
