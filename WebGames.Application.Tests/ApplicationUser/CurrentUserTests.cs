using Xunit;
using FluentAssertions;

namespace WebGames.Application.ApplicationUser.Tests
{
    public class CurrentUserTests
    {
        [Fact()]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue()
        {
            // arrange

            var user = new CurrentUser("321",
                "some@email.com",
                new List<string> { "Admin", "User" },
                0);

            // act

            var isInRole = user.IsInRole("Admin");

            // assert

            isInRole.Should().BeTrue();
        }

        [Fact()]
        public void IsInRole_WithoutMatchingRole_ShouldReturnFalse()
        {
            // arrange

            var user = new CurrentUser("321",
                "some@email.com",
                new List<string> { "User" },
                0);

            // act

            var isInRole = user.IsInRole("Admin");

            // assert

            isInRole.Should().BeFalse();
        }

        [Fact()]
        public void IsInRole_WithNonMatchingCaseRole_ShouldReturnFalse()
        {
            // arrange

            var user = new CurrentUser("321",
                "some@email.com",
                new List<string> { "Admin", "User" },
                0);

            // act

            var isInRole = user.IsInRole("admin");

            // assert

            isInRole.Should().BeFalse();
        }
    }
}