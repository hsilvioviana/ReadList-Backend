using Microsoft.EntityFrameworkCore;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using Xunit;

namespace ReadList.UnitTests.Repositories.Base
{
    public class Update
    {
        [Fact]
        public async Task Update_Success()
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

            var modelUpdated = new UserModel()
            {
                Id = model.Id,
                Username = "joaozinho123",
                Email = "joaozinho123@email.com",
                Password = "123456",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            // Act
            await repository.Create(model);

            await repository.Update(modelUpdated);

            var user = await repository.Find(model.Id);

            // Assert
            Assert.NotNull(user);
            Assert.Equal("joaozinho123@email.com", user.Email);
            Assert.Equal("joaozinho123", user.Username);
        }

        [Fact]
        public async Task Update_WithModelNotFound()
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

            var modelUpdated = new UserModel()
            {
                Id = notFoundId,
                Username = "joaozinho123",
                Email = "joaozinho123@email.com",
                Password = "123456",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            // Act
            await repository.Create(model);

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repository.Update(modelUpdated));
        }

        private static IBaseRepository<UserModel> Repository()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("BaseRepository.Update");
            options = builder.Options;
            var context = new PostgresDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return new BaseRepository<UserModel>(context);
        }
    }
}
