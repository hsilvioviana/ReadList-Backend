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
            var service = Service();

            var ViewModel1 = new CreateBookGenreRelationViewModel()
            {
                BookId = _bookId,
                GenreId = _genreId1
            };

            var ViewModel2 = new CreateBookGenreRelationViewModel()
            {
                BookId = _bookId,
                GenreId = _genreId2
            };

            // Act
            var relationsBeforeCreate = _context.BookGenreRelation.Where(bgr => bgr.BookId == _bookId).ToList();

            await service.Create(ViewModel1);
            await service.Create(ViewModel2);

            var relationsAfterCreate = _context.BookGenreRelation.Where(bgr => bgr.BookId == _bookId).ToList();

            // Assert
            Assert.NotNull(relationsBeforeCreate);
            Assert.Empty(relationsBeforeCreate);

            Assert.NotNull(relationsAfterCreate);
            Assert.Equal(2, relationsAfterCreate.Count);
        }

        [Fact]
        public async Task Create_WhenTryToCreateDuplicate()
        {
            // Arrange
            var service = Service();

            var ViewModel = new CreateBookGenreRelationViewModel()
            {
                BookId = _bookId,
                GenreId = _genreId1
            };


            // Act
            var relationsBeforeCreate = _context.BookGenreRelation.Where(bgr => bgr.BookId == _bookId).ToList();

            await service.Create(ViewModel);

            var relationsBeforeTryToCreateDuplicate = _context.BookGenreRelation.Where(bgr => bgr.BookId == _bookId).ToList();

            // Act & Assert
            Assert.NotNull(relationsBeforeCreate);
            Assert.Empty(relationsBeforeCreate);

            Assert.NotNull(relationsBeforeTryToCreateDuplicate);
            Assert.Single(relationsBeforeTryToCreateDuplicate);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.Create(ViewModel));
        }

        private static IBookGenreRelationService Service()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("BookGenreRelationService.Create");
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
                Title = "Flores para Algernon",
                Author = "Daniel Keyes",
                ReleaseYear = 1959,
                ReadingYear = 2022,
                IsFiction = true,
                NumberOfPages = 288,
                CountryOfOrigin = "Estados Unidos",
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
                Name = "Suspense"
            };

            _context.Genre.Add(genreModel1);
            _context.Genre.Add(genreModel2);

            _context.SaveChanges();

            var mapper = new Mapper(AutoMapperSetup.RegisterMapping());

            var repository = new BookGenreRelationRepository(_context);

            return new BookGenreRelationService(repository, mapper);
        }
    }
}
