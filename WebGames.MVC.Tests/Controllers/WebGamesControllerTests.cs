using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using WebGames.Application.Game;
using Moq;
using MediatR;
using WebGames.Application.Game.Queries.GetAllGames;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;

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
    }
}