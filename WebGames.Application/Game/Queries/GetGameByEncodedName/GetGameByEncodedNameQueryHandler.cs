using AutoMapper;
using MediatR;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Game.Queries.GetGameByEncodedName
{
    public class GetGameByEncodedNameQueryHandler : IRequestHandler<GetGameByEncodedNameQuery, GameDto>
    {
        private readonly IGameRepository gameRepository;
        private readonly IMapper mapper;

        public GetGameByEncodedNameQueryHandler(IGameRepository gameRepository, IMapper mapper)
        {
            this.gameRepository = gameRepository;
            this.mapper = mapper;
        }

        public async Task<GameDto> Handle(GetGameByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.GetByEncodedName(request.EncodedName);
            var dto = mapper.Map<GameDto>(game);

            return dto;
        }
    }
}
