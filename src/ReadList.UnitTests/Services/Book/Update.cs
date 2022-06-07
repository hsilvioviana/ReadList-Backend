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
    public class Update
    {
        private static readonly Guid _userId = Guid.NewGuid();
        private static readonly Guid _bookId = Guid.NewGuid();

        [Fact]
        public async Task Update_Success()
        {
            // Arrange
            var service = Service();

            var viewModel = new UpdateBookViewModel()
            {
                Id = _bookId,
                UserId = _userId,
                Title = "Voo Noturno",
                Author = "Antoine de Saint-Exupéry",
                ReleaseYear = 1931,
                ReadingYear = 2021,
                IsFiction = true,
                Genres = new List<string>() { "Romance", "Suspense", "Ação" },
                NumberOfPages = 112,
                CountryOfOrigin = "França",
                Language = "Português"
            };

            var findViewModel = new FindBookViewModel()
            {
                Id = _bookId,
                UserId = _userId
            };

            // Act
            var bookBeforeUpdate = await service.Find(findViewModel);

            await service.Update(viewModel);

            var bookAfterUpdate = await service.Find(findViewModel);

            // Assert
            Assert.Equal(bookBeforeUpdate.Id, bookAfterUpdate.Id);

            Assert.NotNull(bookBeforeUpdate);
            Assert.Equal("O Pequeno Príncipe", bookBeforeUpdate.Title);
            Assert.Equal(2, bookBeforeUpdate.Genres.Count);

            Assert.NotNull(bookAfterUpdate);
            Assert.Equal("Voo Noturno", bookAfterUpdate.Title);
            Assert.Equal(3, bookAfterUpdate.Genres.Count);
        }

        [Fact]
        public async Task Update_WhenBookNotFound()
        {
            // Arrange
            var service = Service();

            var viewModel = new UpdateBookViewModel()
            {
                Id = Guid.NewGuid(),
                UserId = _userId,
                Title = "Voo Noturno",
                Author = "Antoine de Saint-Exupéry",
                ReleaseYear = 1931,
                ReadingYear = 2021,
                IsFiction = true,
                Genres = new List<string>() { "Romance", "Suspense", "Ação" },
                NumberOfPages = 112,
                CountryOfOrigin = "França",
                Language = "Português"
            };

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await service.Update(viewModel));
        }

        [Fact]
        public async Task Update_WhenTryToUpdateBookOfAnotherUser()
        {
            // Arrange
            var service = Service();

            var viewModel = new UpdateBookViewModel()
            {
                Id = _bookId,
                UserId = Guid.NewGuid(),
                Title = "Voo Noturno",
                Author = "Antoine de Saint-Exupéry",
                ReleaseYear = 1931,
                ReadingYear = 2021,
                IsFiction = true,
                Genres = new List<string>() { "Romance", "Suspense", "Ação" },
                NumberOfPages = 112,
                CountryOfOrigin = "França",
                Language = "Português"
            };

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedActionException>(async () => await service.Update(viewModel));
        }


        private static IBookService Service()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("BookService.Update");
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
