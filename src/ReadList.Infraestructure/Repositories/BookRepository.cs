using Microsoft.EntityFrameworkCore;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;

namespace ReadList.Infraestructure.Repositories
{
    public class BookRepository : BaseRepository<BookModel>, IBookRepository
    {
        protected readonly PostgresDbContext _context;

        public BookRepository(PostgresDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<BookModel>> SearchByUserId(Guid userId, int? startDate, int? endDate)
        {
            return await _context.Book.Where(b => b.UserId == userId
            && (!startDate.HasValue || b.ReadingYear >= startDate)
            && (!endDate.HasValue || b.ReadingYear <= endDate))
            .Include(b => b.BookGenreRelations)
                .ThenInclude(bgr => bgr.Genre)
            .OrderBy(b => b.ReadingYear)
                .ThenBy(b => b.CreatedAt)
            .ToListAsync();
        }

        public async override Task<BookModel> Find(Guid id)
        {
            return await _context.Book.Include(b => b.BookGenreRelations)
                                            .ThenInclude(bgr => bgr.Genre)
                                        .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
