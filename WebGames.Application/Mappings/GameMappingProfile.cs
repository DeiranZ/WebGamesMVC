using AutoMapper;

namespace WebGames.Application.Mappings
{
    public class GameMappingProfile : Profile
    {
        public GameMappingProfile()
        {
            CreateMap<Application.Game.GameDto, Domain.Entities.Game>();
        }
    }
}
