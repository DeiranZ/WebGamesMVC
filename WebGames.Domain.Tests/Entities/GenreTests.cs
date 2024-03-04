using Xunit;
using FluentAssertions;

namespace WebGames.Domain.Entities.Tests
{
    public class GenreTests
    {
        [Fact()]
        public void EncodeName_ShouldSetEncodedName()
        {
            // arrange

            var genre = new Genre();
            genre.Name = "Some Genre";

            // act

            genre.EncodeName();

            // assert

            genre.EncodedName.Should().Be("some-genre");
        }

        [Fact()]
        public void EncodeName_ShouldThrowException_WhenNameIsNull()
        {
            // arrange

            var genre = new Genre();

            // act

            Action action = () => genre.EncodeName();

            // assert

            action.Invoking(a => a.Invoke())
                .Should().Throw<NullReferenceException>();
        }
    }
}