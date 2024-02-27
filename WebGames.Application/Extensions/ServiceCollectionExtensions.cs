using Microsoft.Extensions.DependencyInjection;
using WebGames.Application.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using WebGames.Application.Game.Commands.CreateGame;
using System.Reflection;
using WebGames.Application.ApplicationUser;
using AutoMapper;

namespace WebGames.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                cfg.AddProfile(new GameMappingProfile());
                cfg.AddProfile(new GenreMappingProfile());
            }).CreateMapper()
            );

            services.AddValidatorsFromAssemblyContaining<CreateGameCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
