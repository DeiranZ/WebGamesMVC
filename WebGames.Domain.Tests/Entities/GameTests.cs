using FluentAssertions;
using Xunit;

namespace WebGames.Domain.Entities.Tests
{
    public class GameTests
    {
        [Fact()]
        public void EncodeName_ShouldSetEncodedName()
        {
            // arrange

            var game = new Game();
            game.Name = "Cool Game";

            // act

            game.EncodeName();

            // assert

            game.EncodedName.Should().Be("cool-game");
        }

        [Fact()]
        public void EncodeName_ShouldThrowException_WhenNameIsNull()
        {
            // arrange

            var game = new Game();

            // act

            Action action = () => game.EncodeName();

            // assert

            action.Invoking(a => a.Invoke())
                .Should().Throw<NullReferenceException>();
        }
    }
}