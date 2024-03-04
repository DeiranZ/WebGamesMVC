using AutoMapper;
using Moq;
using WebGames.Application.ApplicationUser;
using WebGames.Application.Mappings;
using WebGames.Domain.Interfaces;
using Xunit;

namespace WebGames.Application.Genre.Commands.CreateGenre.Tests
{
    public class CreateGenreCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_CreateGenre_WhenUserIsAuthorized()
        {
            // arrange

            var command = new CreateGenreCommand()
            {
                Name = "Match 3",
                Description = "Match three tiles together!",
                EncodedName = "match-3"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "some@email.com", new List<string>() { "Admin" }, 1));

            var genreRepositoryMock = new Mock<IGenreRepository>();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new GenreMappingProfile()));
            var mapper = configuration.CreateMapper();

            var handler = new CreateGenreCommandHandler(genreRepositoryMock.Object, mapper, userContextMock.Object);

            // act

            await handler.Handle(command, CancellationToken.None);
            
            // assert

            genreRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.Genre>()), Times.Once);
        }

        [Fact()]
        public async Task Handle_DontCreateGenre_WhenUserIsNotAuthorized()
        {
            // arrange

            var command = new CreateGenreCommand()
            {
                Name = "Match 3",
                Description = "Match three tiles together!",
                EncodedName = "match-3"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "some@email.com", new List<string>() { "User" }, 1));

            var genreRepositoryMock = new Mock<IGenreRepository>();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new GenreMappingProfile()));
            var mapper = configuration.CreateMapper();

            var handler = new CreateGenreCommandHandler(genreRepositoryMock.Object, mapper, userContextMock.Object);

            // act

            await handler.Handle(command, CancellationToken.None);

            // assert

            genreRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.Genre>()), Times.Never);
        }
    }
}