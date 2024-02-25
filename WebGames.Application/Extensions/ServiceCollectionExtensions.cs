using Microsoft.Extensions.DependencyInjection;
using WebGames.Application.Game;
using WebGames.Application.Mappings;
using WebGames.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace WebGames.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
            
            services.AddAutoMapper(typeof(GameMappingProfile));

            services.AddValidatorsFromAssemblyContaining<GameDtoValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
