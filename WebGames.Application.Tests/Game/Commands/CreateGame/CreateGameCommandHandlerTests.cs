using Xunit;
using WebGames.Application.Game.Commands.CreateGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using WebGames.Application.ApplicationUser;
using WebGames.Application.Genre.Commands.CreateGenre;
using WebGames.Domain.Interfaces;
using WebGames.Application.Mappings;

namespace WebGames.Application.Game.Commands.CreateGame.Tests
{
    public class CreateGameCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_CreateGame_WhenUserIsAuthorized()
        {
            // arrange

            var command = new CreateGameCommand()
            {
                Name = "Cool Game",
                Description = "Some name",
                EncodedName = "cool-game",
                Source = "source.com"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "some@email.com", new List<string>() { "Admin" }, 1));

            var gameRepositoryMock = new Mock<IGameRepository>();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new GameMappingProfile()));
            var mapper = configuration.CreateMapper();

            var handler = new CreateGameCommandHandler(gameRepositoryMock.Object, mapper, userContextMock.Object);

            // act

            await handler.Handle(command, CancellationToken.None);

            // assert

            gameRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.Game>()), Times.Once);
        }

        [Fact()]
        public async Task Handle_DontCreateGame_WhenUserIsNotAuthorized()
        {
            // arrange

            var command = new CreateGameCommand()
            {
                Name = "Cool Game",
                Description = "Some name",
                EncodedName = "cool-game",
                Source = "source.com"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "some@email.com", new List<string>() { "User" }, 1));

            var gameRepositoryMock = new Mock<IGameRepository>();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new GameMappingProfile()));
            var mapper = configuration.CreateMapper();

            var handler = new CreateGameCommandHandler(gameRepositoryMock.Object, mapper, userContextMock.Object);

            // act

            await handler.Handle(command, CancellationToken.None);

            // assert

            gameRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.Game>()), Times.Never);
        }
    }
}