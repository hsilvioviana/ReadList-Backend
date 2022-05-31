using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadList.Application.AutoMapper;
using ReadList.Application.ViewModels;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using ReadList.Services.Interfaces;
using ReadList.Services.Services;
using Xunit;

namespace ReadList.UnitTests.Services.Genre
{
    public class CreateMany
    {
        private static readonly Guid _userId = Guid.NewGuid();
        private static readonly Guid _book1Id = Guid.NewGuid();
        private static readonly Guid _book2Id = Guid.NewGuid();
        private static PostgresDbContext _context;

        [Fact]
        public async Task CreateMany_Success()
        {
            // Arrange
            var service = Service();

            var newGenres = new List<string>() { "Romance", "Ficção-Científica" };

            // Act
            await service.CreateMany(newGenres, _book1Id);

            var genres = _context.Genre.ToList();

            // Assert
            Assert.NotNull(genres);
            Assert.Equal(2, genres.Count);
        }

        [Fact]
        public async Task CreateMany_SuccessWhenGenresAreAlreadyInDatabase()
        {
            // Arrange
            var service = Service();

            var newGenres1 = new List<string>() { "Romance", "Ficção-Científica" };

            var newGenres2 = new List<string>() { "Ficção-Científica", "Aventura", "Suspense" };

            // Act
            await service.CreateMany(newGenres1, _book1Id);

            var genresBeforeSecondCreate = _context.Genre.ToList();

            await service.CreateMany(newGenres2, _book2Id);

            var genresAfterSecondCreate = _context.Genre.ToList();

            // Assert
            Assert.NotNull(genresBeforeSecondCreate);
            Assert.Equal(2, genresBeforeSecondCreate.Count);

            Assert.NotNull(genresAfterSecondCreate);
            Assert.Equal(4, genresAfterSecondCreate.Count);
        }

        private static IGenreService Service()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("GenreService.CreateMany");
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

            var bookModel1 = new BookModel()
            {
                Id = _book1Id,
                UserId = _userId,
                Title = "Flores para Algernon",
                Author = "Daniel Keyes",
                ReleaseYear = 1959,
                ReadingYear = 2022,
                IsFiction = true,
                NumberOfPages = 288,
                CountryOfOrigin = "Estados Unidos",
                Language = "Português"
            };

            var bookModel2 = new BookModel()
            {
                Id = Guid.NewGuid(),
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

            _context.Book.Add(bookModel1);
            _context.Book.Add(bookModel2);

            _context.SaveChanges();

            var mapper = new Mapper(AutoMapperSetup.RegisterMapping());

            var bookGenreRelationRepository = new BookGenreRelationRepository(_context);
            var bookGenreRelationService = new BookGenreRelationService(bookGenreRelationRepository, mapper);

            var repository = new GenreRepository(_context);

            return new GenreService(repository, bookGenreRelationService, mapper);
        }
    }
}
