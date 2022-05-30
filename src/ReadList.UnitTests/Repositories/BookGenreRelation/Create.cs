using Microsoft.EntityFrameworkCore;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using Xunit;

namespace ReadList.UnitTests.Repositories.BookGenreRelation
{
    public class Create
    {
        private static readonly Guid _userId = Guid.NewGuid();
        private static readonly Guid _bookId = Guid.NewGuid();
        private static readonly Guid _genreId1 = Guid.NewGuid();
        private static readonly Guid _genreId2 = Guid.NewGuid();
        private static PostgresDbContext _context;

        [Fact]
        public async Task Create_Success()
        {
            // Arrange
            var repository = Repository();

            var bookGenreRelation1 = new BookGenreRelationModel()
            {
                BookId = _bookId,
                GenreId = _genreId1
            };

            var bookGenreRelation2 = new BookGenreRelationModel()
            {
                BookId = _bookId,
                GenreId = _genreId2
            };

            // Act
            await repository.Create(bookGenreRelation1);
            await repository.Create(bookGenreRelation2);

            var relations = _context.BookGenreRelation.ToList();

            // Assert
            Assert.Equal(2, relations.Count);
        }

        private static IBookGenreRelationRepository Repository()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("BookGenreRelationRepository.Create");
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
                Id = _genreId1,
                Name = "Romance"
            };

            var genreModel2 = new GenreModel()
            {
                Id = _genreId2,
                Name = "Aventura"
            };

            _context.Genre.Add(genreModel1);
            _context.Genre.Add(genreModel2);

            _context.SaveChanges();

            return new BookGenreRelationRepository(_context);
        }
    }
}
