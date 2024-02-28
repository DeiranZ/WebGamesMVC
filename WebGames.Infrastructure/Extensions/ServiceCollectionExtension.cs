using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebGames.Domain.Interfaces;
using WebGames.Infrastructure.Persistence;
using WebGames.Infrastructure.Repositories;
using WebGames.Infrastructure.Seeders;
using WebGames.Domain.ApplicationUser;
using Microsoft.AspNetCore.Identity;

namespace WebGames.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebGamesDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("WebGames")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<WebGamesDbContext>();

            services.AddScoped<GameSeeder>();
            
            services.AddScoped<RoleSeeder>();
            services.AddScoped<UserSeeder>();
            services.AddScoped<UserRoleSeeder>();

            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGameGenreRepository, GameGenreRepository>();
        }
    }
}
