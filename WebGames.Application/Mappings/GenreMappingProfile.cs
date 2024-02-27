using AutoMapper;
using WebGames.Application.Genre;
using WebGames.Application.Genre.Commands.DeleteGenre;
using WebGames.Application.Genre.Commands.EditGenre;

namespace WebGames.Application.Mappings
{
    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile()
        {
            CreateMap<GenreDto, Domain.Entities.Genre>()
                .ReverseMap();

            CreateMap<GenreDto, EditGenreCommand>();

            CreateMap<GenreDto, DeleteGenreCommand>();
        }
    }
}
