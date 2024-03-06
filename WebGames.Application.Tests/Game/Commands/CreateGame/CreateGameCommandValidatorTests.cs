using Xunit;
using Moq;
using FluentValidation.TestHelper;
using WebGames.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace WebGames.Application.Game.Commands.CreateGame.Tests
{
    public class CreateGameCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            // arrange

            var repositoryMock = new Mock<IGameRepository>();

			IEnumerable<KeyValuePair<string, string>> appsettings = new Dictionary<string, string>
			{
				{ "ImageSizeLimit", "2097152" }
			};

			IConfiguration configuration = new ConfigurationBuilder()
				.AddInMemoryCollection(appsettings!)
				.Build();

			var validator = new CreateGameCommandValidator(repositoryMock.Object, configuration);

            var command = new CreateGameCommand()
            {
                Name = "Some Game Name",
                Description = "This is the description",
                Source = "some-link.com/home"
            };

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validate_WithInvalidCommand_ShouldHaveValidationErrors()
        {
            // arrange

            var repositoryMock = new Mock<IGameRepository>();

			IEnumerable<KeyValuePair<string, string>> appsettings = new Dictionary<string, string>
			{
				{ "ImageSizeLimit", "2097152" }
			};

			IConfiguration configuration = new ConfigurationBuilder()
				.AddInMemoryCollection(appsettings!)
				.Build();

			var validator = new CreateGameCommandValidator(repositoryMock.Object, configuration);

			var command = new CreateGameCommand()
            {
                Name = "",
                Description = new string('\t', 32768),
                Source = new string('\t', 32768)
            };

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.Source);
        }

        [Fact()]
        public void Validate_WithExistingName_ShouldHaveValidationError()
        {
            // arrange

            var repositoryMock = new Mock<IGameRepository>();

            IEnumerable<KeyValuePair<string, string>> appsettings = new Dictionary<string, string>
            {
                { "ImageSizeLimit", "2097152" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
				.AddInMemoryCollection(appsettings!)
				.Build();

			var validator = new CreateGameCommandValidator(repositoryMock.Object, configuration);

			var command = new CreateGameCommand()
            {
                Name = "",
                Description = new string('\t', 32768),
                Source = new string('\t', 32768)
            };

            repositoryMock.Setup(x => x.GetByName(command.Name).Result)
                .Returns(new Domain.Entities.Game());

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}