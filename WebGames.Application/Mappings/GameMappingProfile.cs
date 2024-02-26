using AutoMapper;
using WebGames.Application.ApplicationUser;
using WebGames.Application.Game;
using WebGames.Application.Game.Commands.DeleteGame;
using WebGames.Application.Game.Commands.EditGame;

namespace WebGames.Application.Mappings
{
    public class GameMappingProfile : Profile
    {
        public GameMappingProfile(IUserContext userContext)
        {
            var user = userContext.GetCurrentUser();

            CreateMap<Application.Game.GameDto, Domain.Entities.Game>();

            CreateMap<Domain.Entities.Game, Application.Game.GameDto>()
                .ForMember(dto => dto.IsEditable, opt => opt.MapFrom(src => user != null && (user.IsInRole("Admin") || user.IsInRole("Moderator"))));

            CreateMap<GameDto, EditGameCommand>();
            
            CreateMap<GameDto, DeleteGameCommand>();
        }
    }
}
