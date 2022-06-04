using Microsoft.EntityFrameworkCore;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using Xunit;

namespace ReadList.UnitTests.Repositories.BookGenreRelation
{
    public class DeleteByBookId
    {
        private static readonly Guid _userId = Guid.NewGuid();
        private static readonly Guid _bookId = Guid.NewGuid();
        private static PostgresDbContext _context;

        [Fact]
        public async Task DeleteByBookId_WhenBookNotFound()
        {
            // Arrange
            var repository = Repository();

            // Act
            await repository.DeleteByBookId(Guid.NewGuid());

            var relations = _context.BookGenreRelation.ToList();

            // Assert
            Assert.Equal(2, relations.Count);
        }

        [Fact]
        public async Task DeleteByBookId_Success()
        {
            // Arrange
            var repository = Repository();

            // Act
            await repository.DeleteByBookId(_bookId);

            var relations = _context.BookGenreRelation.ToList();

            // Assert
            Assert.Empty(relations);
        }

        private static IBookGenreRelationRepository Repository()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("BookGenreRelationRepository.DeleteByBookId");
            options = builder.Options;
            _context = new PostgresDbContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var userModel = new UserModel()
            {
                Id = _userId,
                Username = "joao123",
                Email = "joao123@email.com",
                Password = "123456",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.User.Add(userModel);

            var bookModel = new BookModel()
            {
                Id = _bookId,
                UserId = _userId,
                Title = "O Pequeno Príncipe",
                Author = "Antoine de Saint-Exupéry",
                ReleaseYear = 1943,
                ReadingYear = 2022,
                IsFiction = true,
                NumberOfPages = 132,
                CountryOfOrigin = "França",
                Language = "Português"
            };

            _context.Book.Add(bookModel);

            var genreModel1 = new GenreModel()
            {
                Id = Guid.NewGuid(),
                Name = "Romance"
            };

            var genreModel2 = new GenreModel()
            {
                Id = Guid.NewGuid(),
                Name = "Aventura"
            };

            _context.Genre.Add(genreModel1);
            _context.Genre.Add(genreModel2);


            var bookGenreRelation1 = new BookGenreRelationModel()
            {
                BookId = bookModel.Id,
                GenreId = genreModel1.Id
            };

            var bookGenreRelation2 = new BookGenreRelationModel()
            {
                BookId = bookModel.Id,
                GenreId = genreModel2.Id
            };

            _context.BookGenreRelation.Add(bookGenreRelation1);
            _context.BookGenreRelation.Add(bookGenreRelation2);

            _context.SaveChanges();

            return new BookGenreRelationRepository(_context);
        }
    }
}
