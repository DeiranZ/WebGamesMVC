using Xunit;
using AutoMapper;
using WebGames.Application.Mappings;
using WebGames.Application.Genre;
using WebGames.Domain.Entities;
using FluentAssertions;

namespace WebGenres.Application.Mappings.Tests
{
    public class GenreMappingProfileTests
    {
        [Fact()]
        public void MappingProfile_ShouldMapGenreDtoToGenre()
        {
            // arrange

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<GenreMappingProfile>());
            var mapper = configuration.CreateMapper();

            var dto = new GenreDto
            {
                Name = "Test",
                Description = "TestDesc",
                EncodedName = "test"
            };

            // act

            var result = mapper.Map<Genre>(dto);

            // assert

            result.Should().NotBeNull();
            result.Name.Should().Be(dto.Name);
            result.Description.Should().Be(dto.Description);
            result.EncodedName.Should().Be(dto.EncodedName);
        }

        [Fact()]
        public void MappingProfile_ShouldMapGenreToGenreDto()
        {
            // arrange

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<GenreMappingProfile>());
            var mapper = configuration.CreateMapper();

            var genre = new Genre
            {
                Name = "Test",
                Description = "TestDesc",
            };

            genre.EncodeName();

            // act

            var result = mapper.Map<GenreDto>(genre);

            // assert

            result.Should().NotBeNull();
            result.Name.Should().Be(genre.Name);
            result.Description.Should().Be(genre.Description);
            result.EncodedName.Should().Be(genre.EncodedName);
        }
    }
}