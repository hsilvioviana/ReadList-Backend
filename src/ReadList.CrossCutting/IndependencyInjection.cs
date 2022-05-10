using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadList.Domain.Interfaces;
using ReadList.Infraestructure.Context;
using ReadList.Infraestructure.Repositories;
using ReadList.Services.Interfaces;
using ReadList.Services.Services;

namespace ReadList.CrossCutting
{
    public static class DependencyInjection
    {
        public static void AddDependencies (this IServiceCollection services, IConfiguration configuration)
        {
            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookGenreRelationRepository, BookGenreRelationRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            #endregion

            #region Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookGenreRelationService, BookGenreRelationService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            #endregion

            #region DbContexts
            services.AddDbContext<PostgresDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Postgres")));
            #endregion
        }
    }
}
