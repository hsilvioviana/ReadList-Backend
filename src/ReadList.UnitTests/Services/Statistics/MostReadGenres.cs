﻿using AutoMapper;
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
    public class MostReadGenres
    {
        private static readonly Guid _userId = Guid.NewGuid();

        [Fact]
        public async Task MostReadGenres_Success()
        {
            // Arrange
            var service = Service();

            var queryParam = new DateRangeQueryParam() { StartDate = null, EndDate = null };

            // Act
            var list = await service.MostReadGenres(_userId, queryParam);

            // Assert
            Assert.NotNull(list);
            Assert.Equal(3, list.Count);
            Assert.Equal("Aventura", list[0].Key);
            Assert.Equal(2, list[0].Count);
            Assert.Equal("Romance", list[1].Key);
            Assert.Equal(1, list[1].Count);
            Assert.Equal("Suspense", list[2].Key);
            Assert.Equal(1, list[2].Count);
        }

        [Fact]
        public async Task MostReadGenres_WhenUserDoesntHaveBooks()
        {
            // Arrange
            var service = Service();

            var userWithoutBooksId = Guid.NewGuid();

            var queryParam = new DateRangeQueryParam() { StartDate = null, EndDate = null };

            // Act
            var list = await service.MostReadGenres(userWithoutBooksId, queryParam);

            // Assert
            Assert.NotNull(list);
            Assert.Empty(list);
        }

        private static IStatisticsService Service()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("StatisticsService.MostReadGenres");
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

            var genreModel3 = new GenreModel()
            {
                Id = Guid.NewGuid(),
                Name = "Suspense"
            };

            context.Genre.Add(genreModel1);
            context.Genre.Add(genreModel2);
            context.Genre.Add(genreModel3);

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

            var bookGenreRelation3 = new BookGenreRelationModel()
            {
                BookId = bookModel2.Id,
                GenreId = genreModel2.Id
            };
            var bookGenreRelation4 = new BookGenreRelationModel()
            {
                BookId = bookModel2.Id,
                GenreId = genreModel3.Id
            };

            context.BookGenreRelation.Add(bookGenreRelation1);
            context.BookGenreRelation.Add(bookGenreRelation2);
            context.BookGenreRelation.Add(bookGenreRelation3);
            context.BookGenreRelation.Add(bookGenreRelation4);

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
