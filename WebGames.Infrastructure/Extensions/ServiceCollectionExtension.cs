using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebGames.Infrastructure.Persistence;
using WebGames.Infrastructure.Seeders;

namespace WebGames.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebGamesDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("WebGames")));

            services.AddScoped<GameSeeder>();
        }
    }
}
