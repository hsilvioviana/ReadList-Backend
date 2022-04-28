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
            #endregion

            #region Services
            services.AddScoped<IUserService, UserService>();
            #endregion

            #region DbContexts
            services.AddDbContext<PostgresDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Postgres")));
            #endregion
        }
    }
}
