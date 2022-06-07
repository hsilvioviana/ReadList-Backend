using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadList.Application.AutoMapper;
using ReadList.Application.CustomExceptions;
using ReadList.Application.ViewModels;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using ReadList.Services.Interfaces;
using ReadList.Services.Services;
using Xunit;

namespace ReadList.UnitTests.Services.Book
{
    public class Find
    {
        private static readonly Guid _userId = Guid.NewGuid();
        private static readonly Guid _bookId = Guid.NewGuid();

        [Fact]
        public async Task Find_Success()
        {
            // Arrange
            var service = Service();

            var viewModel = new FindBookViewModel()
            {
                Id = _bookId,
                UserId = _userId
            };

            // Act
            var book = await service.Find(viewModel);

            // Assert
            Assert.NotNull(book);
            Assert.Equal("O Pequeno Príncipe", book.Title);
            Assert.Equal(2, book.Genres.Count);
        }

        [Fact]
        public async Task Find_WhenBookNotFound()
        {
            // Arrange
            var service = Service();

            var viewModel = new FindBookViewModel()
            {
                Id = Guid.NewGuid(),
                UserId = _userId
            };

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await service.Find(viewModel));
        }

        [Fact]
        public async Task Find_WhenTryToFindBookOfAnotherUser()
        {
            // Arrange
            var service = Service();

            var viewModel = new FindBookViewModel()
            {
                Id = _bookId,
                UserId = Guid.NewGuid()
            };

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedActionException>(async () => await service.Find(viewModel));
        }

        private static IBookService Service()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("BookService.Find");
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

            var bookModel1 = new BookModel()
            {
                Id = _bookId,
                UserId = _userId,
                Title = "O Pequeno Príncipe",
                Author = "Antoine de Saint-Exupéry",
                ReleaseYear = 1943,
                ReadingYear = 2020,
                IsFiction = true,
                NumberOfPages = 132,
                CountryOfOrigin = "França",
                Language = "Português"
            };

            var bookModel2 = new BookModel()
            {
                Id = Guid.NewGuid(),
                UserId = _userId,
                Title = "Dom Casmurro",
                Author = "Machado de Assis",
                ReleaseYear = 1899,
                ReadingYear = 2021,
                IsFiction = true,
                NumberOfPages = 208,
                CountryOfOrigin = "Brasil",
                Language = "Português"
            };

            var bookModel3 = new BookModel()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Title = "Flores para Algernon",
                Author = "Daniel Keyes",
                ReleaseYear = 1959,
                ReadingYear = 2022,
                IsFiction = true,
                NumberOfPages = 288,
                CountryOfOrigin = "Estados Unidos",
                Language = "Português"
            };

            context.Book.Add(bookModel1);
            context.Book.Add(bookModel2);
            context.Book.Add(bookModel3);

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
                BookId = bookModel1.Id,
                GenreId = genreModel1.Id
            };

            var bookGenreRelation2 = new BookGenreRelationModel()
            {
                BookId = bookModel1.Id,
                GenreId = genreModel2.Id
            };

            context.BookGenreRelation.Add(bookGenreRelation1);
            context.BookGenreRelation.Add(bookGenreRelation2);

            context.SaveChanges();

            var repository = new BookRepository(context);

            var mapper = new Mapper(AutoMapperSetup.RegisterMapping());

            var bookGenreRelationRepository = new BookGenreRelationRepository(context);
            var bookGenreRelationService = new BookGenreRelationService(bookGenreRelationRepository, mapper);

            var genreRepository = new GenreRepository(context);
            var genreService = new GenreService(genreRepository, bookGenreRelationService, mapper);

            return new BookService(repository, genreService, mapper);
        }
    }
}
