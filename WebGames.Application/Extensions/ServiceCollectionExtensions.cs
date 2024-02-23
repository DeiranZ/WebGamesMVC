using Microsoft.Extensions.DependencyInjection;
using WebGames.Application.Services;

namespace WebGames.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
        }
    }
}
