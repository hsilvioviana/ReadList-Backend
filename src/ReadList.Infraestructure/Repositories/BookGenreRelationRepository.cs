using Microsoft.EntityFrameworkCore;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;

namespace ReadList.Infraestructure.Repositories
{
    public class BookGenreRelationRepository : IBookGenreRelationRepository
    {
        PostgresDbContext _context;

        public BookGenreRelationRepository(PostgresDbContext context)
        {
            _context = context;
        }

        public async Task Create(BookGenreRelationModel model)
        {
            await _context.BookGenreRelation.AddAsync(model);
        
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByBookId(Guid bookId)
        {
            var relationsToBeRemoved = await _context.BookGenreRelation.Where(bgr => bgr.BookId == bookId).ToListAsync();

            _context.BookGenreRelation.RemoveRange(relationsToBeRemoved);

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
           _context?.Dispose();
        }
    }
}
