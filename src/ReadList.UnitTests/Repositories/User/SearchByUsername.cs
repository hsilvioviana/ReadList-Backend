using Microsoft.EntityFrameworkCore;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using Xunit;

namespace ReadList.UnitTests.Repositories.User
{
    public class SearchByUsername
    {
        [Fact]
        public async Task SearchByUsername_WhenUserNotExists()
        {
            // Arrange
            var repository = Repository();

            // Act
            var user = await repository.SearchByUsername("maria123");

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task SearchByUsername_WhenUserExists()
        {
            // Arrange
            var repository = Repository();

            // Act
            var user = await repository.SearchByUsername("joao123");

            // Assert
            Assert.NotNull(user);
            Assert.Equal("joao123", user.Username);
            Assert.Equal("joao123@email.com", user.Email);
            Assert.Equal("123456", user.Password);
        }

        private static IUserRepository Repository()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("UserRepository.SearchByUsername");
            options = builder.Options;
            var context = new PostgresDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var model = new UserModel()
            {
                Id = Guid.NewGuid(),
                Username = "joao123",
                Email = "joao123@email.com",
                Password = "123456",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            context.User.Add(model);
            context.SaveChanges();

            return new UserRepository(context);
        }
    }
}
