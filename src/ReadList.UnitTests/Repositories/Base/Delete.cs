using Microsoft.EntityFrameworkCore;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using Xunit;

namespace ReadList.UnitTests.Repositories.Base
{
    public class Delete
    {
        [Fact]
        public async Task Delete_Success()
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

            var usersBeforeDelete = await repository.Search();

            await repository.Delete(model.Id);

            var usersAfterDelete = await repository.Search();

            // Assert
            Assert.Single(usersBeforeDelete);
            Assert.Empty(usersAfterDelete);
        }

        [Fact]
        public async Task Delete_WithModelNotFound()
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

            var usersBeforeDelete = await repository.Search();

            // Act & Assert
            Assert.Single(usersBeforeDelete);

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repository.Delete(Guid.NewGuid()));
        }

        private static IBaseRepository<UserModel> Repository()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("Delete");
            options = builder.Options;
            var context = new PostgresDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return new BaseRepository<UserModel>(context);
        }
    }
}
