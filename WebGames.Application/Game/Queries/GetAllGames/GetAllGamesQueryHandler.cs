using AutoMapper;
using MediatR;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Game.Queries.GetAllGames
{
    public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<GameDto>>
    {
        private readonly IGameRepository gameRepository;
        private readonly IMapper mapper;

        public GetAllGamesQueryHandler(IGameRepository gameRepository, IMapper mapper)
        {
            this.gameRepository = gameRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GameDto>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
        {
            var games = await gameRepository.GetAll();
            var gameDtos = mapper.Map<IEnumerable<GameDto>>(games);

            return gameDtos;
        }
    }
}
