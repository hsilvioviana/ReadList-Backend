using Microsoft.EntityFrameworkCore;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using Xunit;

namespace ReadList.UnitTests.Repositories.Base
{
    public class Find
    {
        [Fact]
        public async Task Find_Success()
        {
            // Arrange
            var repository = Repository();

            var model = new UserModel()
            {
                Id = Guid.NewGuid(),
                Username = "joao123",
                Email = "joao123@email.com",
                Password = "123456",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            // Act
            await repository.Create(model);

            var user = await repository.Find(model.Id);

            // Assert
            Assert.NotNull(user);
            Assert.Equal("joao123", user.Username);
        }

        [Fact]
        public async Task Find_WithModelNotFound()
        {
            // Arrange
            var repository = Repository();

            var model = new UserModel()
            {
                Id = Guid.NewGuid(),
                Username = "joao123",
                Email = "joao123@email.com",
                Password = "123456",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var notFoundId = Guid.NewGuid();

            // Act
            await repository.Create(model);

            var user = await repository.Find(notFoundId);

            // Assert
            Assert.Null(user);
        }

        private static IBaseRepository<UserModel> Repository()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("BaseRepository.Find");
            options = builder.Options;
            var context = new PostgresDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return new BaseRepository<UserModel>(context);
        }
    }
}
