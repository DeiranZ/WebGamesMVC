using Xunit;
using Moq;
using WebGames.Domain.Interfaces;
using FluentValidation.TestHelper;
using WebGames.Application.Game.Commands.CreateGame;

namespace WebGames.Application.Genre.Commands.CreateGenre.Tests
{
    public class CreateGenreCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            // arrange

            var repositoryMock = new Mock<IGenreRepository>();

            var validator = new CreateGenreCommandValidator(repositoryMock.Object);

            var command = new CreateGenreCommand()
            {
                Name = "Match 3",
                Description = "Match three tiles together!"
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

            var repositoryMock = new Mock<IGenreRepository>();

            var validator = new CreateGenreCommandValidator(repositoryMock.Object);

            var command = new CreateGenreCommand()
            {
                Name = "",
                Description = new string('\t', 32768)
            };

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact()]
        public void Validate_WithExistingName_ShouldHaveValidationError()
        {
            // arrange

            var repositoryMock = new Mock<IGenreRepository>();


            var validator = new CreateGenreCommandValidator(repositoryMock.Object);

            var command = new CreateGenreCommand()
            {
                Name = "",
                Description = new string('\t', 32768),
            };

            repositoryMock.Setup(x => x.GetByName(command.Name).Result)
                .Returns(new Domain.Entities.Genre());

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}