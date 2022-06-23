using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReadList.Application.AutoMapper;
using ReadList.Application.CustomExceptions;
using ReadList.Application.ViewModels;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using ReadList.Services.Interfaces;
using ReadList.Services.Services;
using Xunit;

namespace ReadList.UnitTests.Services.User
{
    public class Signup
    {
        [Fact]
        public async Task Signup_WithNullViewModel()
        {
            // Arrange
            var service = Service();

            // Act & Assert
            await Assert.ThrowsAsync<InvalidInputException>(async() => await service.SignUp(new SignUpViewModel()));
        }

        [Fact]
        public async Task Signup_WithIncorrectInput()
        {
            // Arrange
            var service = Service();

            var viewModel = new SignUpViewModel()
            {
                Username = "x",
                Email = "x",
                Password = "x"
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await service.SignUp(viewModel));
        }

        [Fact]
        public async Task Signup_WithUsernameAlreadyInUse()
        {
            // Arrange
            var service = Service();

            var viewModel = new SignUpViewModel()
            {
                Username = "joao123",
                Email = "teste@email.com",
                Password = "123456"
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await service.SignUp(viewModel));
        }

        [Fact]
        public async Task Signup_WithEmailAlreadyInUse()
        {
            // Arrange
            var service = Service();

            var viewModel = new SignUpViewModel()
            {
                Username = "teste",
                Email = "joao123@email.com",
                Password = "123456"
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await service.SignUp(viewModel));
        }

        [Fact]
        public async Task Signup_Success()
        {
            // Arrange
            var service = Service();

            var viewModel = new SignUpViewModel()
            {
                Username = "teste",
                Email = "teste@email.com",
                Password = "123456"
            };

            // Act
            var authentication = await service.SignUp(viewModel);

            // Assert
            Assert.NotNull(authentication);
            Assert.Equal("teste", authentication.Username);
            Assert.NotNull(authentication.Token);
        }

        private static IUserService Service()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("Signup");
            options = builder.Options;
            PostgresDbContext context = new PostgresDbContext(options);

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
