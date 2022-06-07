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
    public class Delete
    {
        private static readonly Guid _userId = Guid.NewGuid();
        private static readonly Guid _bookId = Guid.NewGuid();

        [Fact]
        public async Task Delete_Success()
        {
            // Arrange
            var service = Service();

            var viewModel = new DeleteBookViewModel()
            {
                Id = _bookId,
                UserId = _userId,
            };

            var findViewModel = new FindBookViewModel()
            {
                Id = _bookId,
                UserId = _userId
            };

            // Act
            var bookBeforeDelete = await service.Find(findViewModel);

            await service.Delete(viewModel);

            // Act & Assert
            Assert.NotNull(bookBeforeDelete);
            Assert.Equal("O Pequeno Príncipe", bookBeforeDelete.Title);

            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await service.Find(findViewModel));
        }

        [Fact]
        public async Task Delete_WhenBookNotFound()
        {
            // Arrange
            var service = Service();

            var viewModel = new DeleteBookViewModel()
            {
                Id = Guid.NewGuid(),
                UserId = _userId,
            };

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await service.Delete(viewModel));
        }

        [Fact]
        public async Task Delete_WhenTryToDeleteBookOfAnotherUser()
        {
            // Arrange
            var service = Service();

            var viewModel = new DeleteBookViewModel()
            {
                Id = _bookId,
                UserId = Guid.NewGuid(),
            };

            var findViewModel = new FindBookViewModel()
            {
                Id = _bookId,
                UserId = _userId
            };

            // Act & Assert
            var bookBeforeTryDelete = await service.Find(findViewModel);

            Assert.NotNull(bookBeforeTryDelete);
            Assert.Equal("O Pequeno Príncipe", bookBeforeTryDelete.Title);

            await Assert.ThrowsAsync<UnauthorizedActionException>(async () => await service.Delete(viewModel));

            var bookAfterTryDelete = await service.Find(findViewModel);

            Assert.NotNull(bookAfterTryDelete);
            Assert.Equal("O Pequeno Príncipe", bookAfterTryDelete.Title);
        }


        private static IBookService Service()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("BookService.Delete");
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
                ReadingYear = 2020,
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
