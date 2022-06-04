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

namespace ReadList.UnitTests.Services.BookGenreRelation
{
    public class DeleteByBookId
    {
        private static readonly Guid _userId = Guid.NewGuid();
        private static readonly Guid _bookId = Guid.NewGuid();
        private static PostgresDbContext _context;

        [Fact]
        public async Task DeleteByBookId_Success()
        {
            // Arrange
            var service = Service();

            // Act
            var relationsBeforeReset = _context.BookGenreRelation.Where(bgr => bgr.BookId == _bookId).ToList();

            await service.DeleteByBookId(_bookId);

            var relationsAfterReset = _context.BookGenreRelation.Where(bgr => bgr.BookId == _bookId).ToList();

            var relationsOfAnotherBooks = _context.BookGenreRelation.Where(bgr => bgr.BookId != _bookId).ToList();

            // Assert
            Assert.NotNull(relationsBeforeReset);
            Assert.Equal(2, relationsBeforeReset.Count);

            Assert.NotNull(relationsAfterReset);
            Assert.Empty(relationsAfterReset);

            Assert.NotNull(relationsOfAnotherBooks);
            Assert.NotEmpty(relationsOfAnotherBooks);
        }

        private static IBookGenreRelationService Service()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("BookGenreRelationService.DeleteByBookId");
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

            _context.Book.Add(bookModel1);
            _context.Book.Add(bookModel2);
            _context.Book.Add(bookModel3);

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

            _context.BookGenreRelation.Add(bookGenreRelation1);
            _context.BookGenreRelation.Add(bookGenreRelation2);
            _context.BookGenreRelation.Add(bookGenreRelation3);

            _context.SaveChanges();

            var mapper = new Mapper(AutoMapperSetup.RegisterMapping());

            var repository = new BookGenreRelationRepository(_context);

            return new BookGenreRelationService(repository, mapper);
        }
    }
}
