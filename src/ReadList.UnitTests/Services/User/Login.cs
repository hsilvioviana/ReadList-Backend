using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReadList.Application.AutoMapper;
using ReadList.Application.CustomExceptions;
using ReadList.Application.Utils;
using ReadList.Application.ViewModels;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using ReadList.Services.Interfaces;
using ReadList.Services.Services;
using Xunit;

namespace ReadList.UnitTests.Services.User
{
    public class Login
    {
        [Fact]
        public async Task Login_WithNullViewModel()
        {
            // Arrange
            var service = Service();

            // Act & Assert
            await Assert.ThrowsAsync<InvalidInputException>(async() => await service.Login(new LoginViewModel()));
        }

        [Fact]
        public async Task Login_WithIncorrectInput()
        {
            // Arrange
            var service = Service();

            var viewModel = new LoginViewModel()
            {
                Username = "x",
                Password = "x"
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await service.Login(viewModel));
        }

        [Fact]
        public async Task Login_WithUserNotFound()
        {
            // Arrange
            var service = Service();

            var viewModel = new LoginViewModel()
            {
                Username = "teste",
                Password = "123456",
            };

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await service.Login(viewModel));
        }

        [Fact]
        public async Task Login_WithIncorrectPassword()
        {
            // Arrange
            var service = Service();

            var viewModel = new LoginViewModel()
            {
                Username = "joao123",
                Password = "654321"
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await service.Login(viewModel));
        }

        [Fact]
        public async Task Login_Success()
        {
            // Arrange
            var service = Service();

            var viewModel = new LoginViewModel()
            {
                Username = "joao123",
                Password = "123456"
            };

            // Act
            var authentication = await service.Login(viewModel);

            // Assert
            Assert.NotNull(authentication);
            Assert.Equal("joao123", authentication.Username);
            Assert.NotNull(authentication.Token);
        }

        private static IUserService Service()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("Login");
            options = builder.Options;
            PostgresDbContext context = new PostgresDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var model = new UserModel()
            {
                Id = Guid.NewGuid(),
                Username = "joao123",
                Email = "joao123@email.com",
                Password = Security.Hash("123456"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            context.User.Add(model);
            context.SaveChanges();

            var inMemorySettings = new Dictionary<string, string>
            {
                {"JWT:SecretKey", "ip2o3nr5ol2uijkuhnroi1qkdjp2o"}
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var repository = new UserRepository(context);

            var mapper = new Mapper(AutoMapperSetup.RegisterMapping());

            return new UserService(configuration, repository, mapper);
        }
    }
}
