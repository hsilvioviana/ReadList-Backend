﻿using Microsoft.EntityFrameworkCore;
using ReadList.Application.QueryParams;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using Xunit;

namespace ReadList.UnitTests.Repositories.Book
{
    public class SearchByUserId
    {
        private static readonly Guid _userId = Guid.NewGuid();
        private static readonly Guid _bookId = Guid.NewGuid();

        [Fact]
        public async Task SearchByUserId_WhenBooksNotFound()
        {
            // Arrange
            var repository = Repository();

            var notFoundId = Guid.NewGuid();

            // Act
            var books = await repository.SearchByUserId(notFoundId, null, null);

            // Assert
            Assert.Empty(books);
        }

        [Fact]
        public async Task SearchByUserId_Success()
        {
            // Arrange
            var repository = Repository();

            // Act
            var books = await repository.SearchByUserId(_userId, null, null);

            // Assert
            Assert.Single(books);
            Assert.Equal("O Pequeno Príncipe", books[0].Title);
            Assert.Equal(2, books[0].BookGenreRelations.Count);
            Assert.Equal("joao123", books[0].User.Username);
        }

        private static IBookRepository Repository()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("BookRepository.SearchByUserId");
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
