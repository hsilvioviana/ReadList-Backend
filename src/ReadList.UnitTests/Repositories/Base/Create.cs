﻿using Microsoft.EntityFrameworkCore;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using Xunit;

namespace ReadList.UnitTests.Repositories.Base
{
    public class Create
    {
        [Fact]
        public async Task Create_Success()
        {
            // Arrange
            var repository = Repository();

            var model1 = new UserModel()
            {
                Id = Guid.NewGuid(),
                Username = "joao123",
                Email = "joao123@email.com",
                Password = "123456",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var model2 = new UserModel()
            {
                Id = Guid.NewGuid(),
                Username = "maria123",
                Email = "maria123@email.com",
                Password = "123456",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            // Act
            await repository.Create(model1);
            await repository.Create(model2);

            var users = await repository.Search();

            // Assert
            Assert.Equal(2, users.Count);
        }

        private static IBaseRepository<UserModel> Repository()
        {
            DbContextOptions<PostgresDbContext> options;
            var builder = new DbContextOptionsBuilder<PostgresDbContext>();
            builder.UseInMemoryDatabase("Create");
            options = builder.Options;
            var context = new PostgresDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return new BaseRepository<UserModel>(context);
        }
    }
}
