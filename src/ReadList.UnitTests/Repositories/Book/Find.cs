using Microsoft.EntityFrameworkCore;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using Xunit;

namespace ReadList.UnitTests.Repositories.Book
{
    public class DeleteByBookId
    {
        private static readonly Guid _userId = Guid.NewGuid();
        private static readonly Guid _bookId = Guid.NewGuid();

        [Fact]
        public async Task Find_WhenBookNotFound()
        {
            // Arrange
            var repository = Repository();

            // Act
            var book = await repository.Find(Guid.NewGuid());

            // Assert
            Assert.Null(book);
        }

        [Fact]
        public async Task Find_Success()
        {
            // Arrange
            var repository = Repository();

            // Act
            var book = await repository.Find(_bookId);

            // Assert
            Assert.NotNull(book);
            Assert.Equal("O Pequeno Príncipe", book.Title);
            Assert.Equal(2, book.BookGenreRelations.Count);
            Assert.Equal("joao123", book.User.Username);
        }

        private static IBookRepository Repository()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("BookRepository.Find");
            options = builder.Options;
            var context = new PostgresDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var userModel = new UserModel()
            {
                Id = _userId,
                Username = "joao123",
                Email = "joao123@email.com",
                Password = "123456",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            context.User.Add(userModel);

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

            context.Book.Add(bookModel);

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

            context.Genre.Add(genreModel1);
            context.Genre.Add(genreModel2);


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

            context.BookGenreRelation.Add(bookGenreRelation1);
            context.BookGenreRelation.Add(bookGenreRelation2);

            context.SaveChanges();

            return new BookRepository(context);
        }
    }
}
