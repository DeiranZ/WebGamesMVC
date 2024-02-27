﻿using AutoMapper;
using WebGames.Application.Game;
using WebGames.Application.Game.Commands.DeleteGame;
using WebGames.Application.Game.Commands.EditGame;
using WebGames.Application.Genre;

namespace WebGames.Application.Mappings
{
    public class GameMappingProfile : Profile
    {
        public GameMappingProfile()
        {
            CreateMap<Application.Game.GameDto, Domain.Entities.Game>();

            CreateMap<Domain.Entities.Game, Application.Game.GameDto>();

            CreateMap<GameDto, EditGameCommand>();
            
            CreateMap<GameDto, DeleteGameCommand>();

            CreateMap<GenreDto, Domain.Entities.Genre>()
                .ReverseMap();
        }
    }
}
