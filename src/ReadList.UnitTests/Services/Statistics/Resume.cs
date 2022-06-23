using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadList.Application.AutoMapper;
using ReadList.Application.QueryParams;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using ReadList.Services.Interfaces;
using ReadList.Services.Services;
using Xunit;

namespace ReadList.UnitTests.Services.Statistics
{
    public class Resume
    {
        private static readonly Guid _userId = Guid.NewGuid();

        [Fact]
        public async Task Resume_Success()
        {
            // Arrange
            var service = Service();

            var queryParam = new DateRangeQueryParam() { StartDate = null, EndDate = null };

            // Act
            var resume = await service.Resume(_userId, queryParam);

            // Assert
            Assert.NotNull(resume);
            Assert.Equal("Anos registrados: 2", resume.YearsWithMoreBooks);
            Assert.Equal("Autores registrados: 3", resume.MostReadAuthors);
            Assert.Equal("Tipos registrados: 1", resume.MostReadTypes);
            Assert.Equal("Gêneros registrados: 2", resume.MostReadGenres);
            Assert.Equal("Países registrados: 3", resume.MostReadCountries);
            Assert.Equal("Idiomas registrados: 1", resume.MostReadLanguages);
            Assert.Equal("Livros registrados: 3", resume.OldestBooks);
            Assert.Equal("Livros registrados: 3", resume.BiggestBooks);
        }

        [Fact]
        public async Task Resume_WhenUserDoesntHaveBooks()
        {
            // Arrange
            var service = Service();

            var userWithoutBooksId = Guid.NewGuid();

            var queryParam = new DateRangeQueryParam() { StartDate = null, EndDate = null };

            // Act
            var resume = await service.Resume(userWithoutBooksId, queryParam);

            // Assert
            Assert.NotNull(resume);
            Assert.Equal("Anos registrados: 0", resume.YearsWithMoreBooks);
            Assert.Equal("Autores registrados: 0", resume.MostReadAuthors);
            Assert.Equal("Tipos registrados: 0", resume.MostReadTypes);
            Assert.Equal("Gêneros registrados: 0", resume.MostReadGenres);
            Assert.Equal("Países registrados: 0", resume.MostReadCountries);
            Assert.Equal("Idiomas registrados: 0", resume.MostReadLanguages);
            Assert.Equal("Livros registrados: 0", resume.OldestBooks);
            Assert.Equal("Livros registrados: 0", resume.BiggestBooks);
        }

        private static IStatisticsService Service()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("StatisticsService.Resume");
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
                UserId = _userId,
                Title = "Flores para Algernon",
                Author = "Daniel Keyes",
                ReleaseYear = 1959,
                ReadingYear = 2021,
                IsFiction = true,
                NumberOfPages = 288,
                CountryOfOrigin = "Estados Unidos",
                Language = "Português"
            };

            var bookModel4 = new BookModel()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Title = "Fundação",
                Author = "Isaac Asimov",
                ReleaseYear = 1951,
                ReadingYear = 2022,
                IsFiction = true,
                NumberOfPages = 320,
                CountryOfOrigin = "Estados Unidos",
                Language = "Português"
            };

            context.Book.Add(bookModel1);
            context.Book.Add(bookModel2);
            context.Book.Add(bookModel3);
            context.Book.Add(bookModel4);

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

            var bookService = new BookService(repository, genreService, mapper);

            return new StatisticsService(bookService);
        }
    }
}
