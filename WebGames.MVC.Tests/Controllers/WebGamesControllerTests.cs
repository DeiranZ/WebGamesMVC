using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using WebGames.Application.Game;
using Moq;
using MediatR;
using WebGames.Application.Game.Queries.GetAllGames;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using WebGames.Application.Genre;
using WebGames.Application.Genre.Queries.GetAllGenres;
using WebGames.Application.Game.Queries.GetGameByEncodedName;
using Microsoft.AspNetCore.Authorization;

namespace WebGames.MVC.Controllers.Tests
{
    public class WebGamesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;

        public WebGamesControllerTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory;
        }

        [Fact()]
        public async Task Index_ReturnsViewWithExpectedData_ForExistingGames()
        {
            // arrange

            var games = new List<GameDto>
            {
                new GameDto()
                {
                    Name = "Game 1"
                },

                new GameDto()
                {
                    Name = "Game 2"
                },

                new GameDto()
                {
                    Name = "Game 3"
                }
            };

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(x => x.Send(It.IsAny<GetAllGamesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(games);

            var client = factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services => services.AddScoped(_ => mediatorMock.Object)))
                .CreateClient();

            // act

            var response = await client.GetAsync("/WebGames/Index");

            // assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("<h1 class=\"mb-12\">All Games</h1>")
                .And.Contain("Game 1")
                .And.Contain("Game 2")
                .And.Contain("Game 3")
                .And.NotContain("<h2>There are no games yet.</h2>");
        }

        [Fact()]
        public async Task Index_ReturnsViewWithExpectedData_ForNoGames()
        {
            // arrange

            var games = new List<GameDto>();

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(x => x.Send(It.IsAny<GetAllGamesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(games);

            var client = factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services => services.AddScoped(_ => mediatorMock.Object)))
                .CreateClient();

            // act

            var response = await client.GetAsync("/WebGames/Index");

            // assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("<h1 class=\"mb-12\">All Games</h1>")
                .And.Contain("<h2>There are no games yet.</h2>");
        }

        [Fact()]
        public async Task Edit_GenresShowLoadingSpinner_WithExpectedEncodedName()
        {
            // arrange

            var genres = new List<GenreDto>
            {
                new GenreDto()
                {
                    Name = "Genre 1"
                },

                new GenreDto()
                {
                    Name = "Genre 2"
                },

                new GenreDto()
                {
                    Name = "Genre 3"
                }
            };

            var game = new GameDto() { EncodedName = "test" };
            
            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(x => x.Send(It.IsAny<GetAllGenresQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(genres);

            mediatorMock.Setup(x => x.Send(It.IsAny<GetGameByEncodedNameQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(game);

            var client = factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddScoped(_ => mediatorMock.Object);
                        
                        var authorizationDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(IAuthorizationHandler));
                        if (authorizationDescriptor != null)
                            services.Remove(authorizationDescriptor);

                        services.AddScoped<IAuthorizationHandler, TestAllowAnonymous>();
                    }))
                .CreateClient();

            // act

            var response = await client.GetAsync(game.EncodedName + "/edit");

            // assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain($"<div id=\"genres\" class=\"row\" data-encoded-name=\"{game.EncodedName}\">")
                .And.Contain("<div class=\"spinner-border m-5\" style=\"width: 3rem; height: 3rem;\" role=\"status\">")
                .And.Contain("<span class=\"visually-hidden\">Loading...</span>");

        }
    }

    public class TestAllowAnonymous : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach (IAuthorizationRequirement requirement in context.PendingRequirements.ToList())
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}