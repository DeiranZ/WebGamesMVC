using Microsoft.Extensions.DependencyInjection;
using WebGames.Application.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using WebGames.Application.Game.Commands.CreateGame;
using System.Reflection;

namespace WebGames.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            
            services.AddAutoMapper(typeof(GameMappingProfile));

            services.AddValidatorsFromAssemblyContaining<CreateGameCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
