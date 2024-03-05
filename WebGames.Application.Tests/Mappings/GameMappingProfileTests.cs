using Xunit;
using AutoMapper;
using WebGames.Application.Game;
using FluentAssertions;

namespace WebGames.Application.Mappings.Tests
{
    public class GameMappingProfileTests
    {
        [Fact()]
        public void MappingProfile_ShouldMapGameDtoToGame()
        {
            // arrange

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<GameMappingProfile>());
            var mapper = configuration.CreateMapper();

            var dto = new GameDto
            {
                Name = "Test",
                Description = "TestDesc",
                Source = "some.source",
                ImageSource = "image.source",
                EncodedName = "test"
            };

            // act

            var result = mapper.Map<Domain.Entities.Game>(dto);

            // assert

            result.Should().NotBeNull();
            result.Name.Should().Be(dto.Name);
            result.Description.Should().Be(dto.Description);
            result.Source.Should().Be(dto.Source);
            result.ImageSource.Should().Be(dto.ImageSource);
            result.EncodedName.Should().Be(dto.EncodedName);
        }

        [Fact()]
        public void MappingProfile_ShouldMapGameToGameDto()
        {
            // arrange

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<GameMappingProfile>());
            var mapper = configuration.CreateMapper();

            var game = new Domain.Entities.Game
            {
                Name = "Test",
                Description = "TestDesc",
                Source = "some.source",
                ImageSource = "image.source",
            };

            game.EncodeName();

            // act

            var result = mapper.Map<GameDto>(game);

            // assert

            result.Should().NotBeNull();
            result.Name.Should().Be(game.Name);
            result.Description.Should().Be(game.Description);
            result.Source.Should().Be(game.Source);
            result.ImageSource.Should().Be(game.ImageSource);
            result.EncodedName.Should().Be(game.EncodedName);
        }
    }
}