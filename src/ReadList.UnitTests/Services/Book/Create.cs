using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadList.Application.AutoMapper;
using ReadList.Application.CustomExceptions;
using ReadList.Application.QueryParams;
using ReadList.Application.ViewModels;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using ReadList.Services.Interfaces;
using ReadList.Services.Services;
using Xunit;

namespace ReadList.UnitTests.Services.Book
{
    public class Create
    {
        private static readonly Guid _userId = Guid.NewGuid();

        [Fact]
        public async Task Create_Success()
        {
            // Arrange
            var service = Service();

            var viewModel = new CreateBookViewModel()
            {
                UserId = _userId,
                Title = "O Pequeno Príncipe",
                Author = "Antoine de Saint-Exupéry",
                ReleaseYear = 1943,
                ReadingYear = 2020,
                IsFiction = true,
                Genres = new List<string>() { "Romance", "Aventura" },
                NumberOfPages = 132,
                CountryOfOrigin = "França",
                Language = "Português"
            };

            // Act
            await service.Create(viewModel);

            var queryParam = new DateRangeQueryParam() { StartDate = null, EndDate = null };

            var books = await service.Search(_userId, queryParam);

            // Assert
            Assert.NotEmpty(books);
            Assert.Single(books);
            Assert.Equal("O Pequeno Príncipe", books[0].Title);
            Assert.Equal(2, books[0].Genres.Count);
        }

        [Fact]
        public async Task Create_WithNullViewModel()
        {
            // Arrange
            var service = Service();

            var viewModel = new CreateBookViewModel()
            {

            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await service.Create(viewModel));
        }

        [Fact]
        public async Task Create_WithIncorrectInput()
        {
            // Arrange
            var service = Service();

            var viewModel = new CreateBookViewModel()
            {
                UserId = _userId,
                Title = "x",
                Author = "Antoine de Saint-Exupéry",
                ReleaseYear = 1943,
                ReadingYear = 2020,
                IsFiction = true,
                Genres = new List<string>() { "Romance", "Aventura" },
                NumberOfPages = 132,
                CountryOfOrigin = "França",
                Language = "Português"
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await service.Create(viewModel));
        }

        private static IBookService Service()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("BookService.Create");
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
