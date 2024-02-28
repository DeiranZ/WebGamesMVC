using AutoMapper;
using MediatR;
using WebGames.Application.Game;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.GameGenre.Queries.GetAllGamesOfGenre
{
    public class GetAllGamesOfGenreQueryHandler : IRequestHandler<GetAllGamesOfGenreQuery, IEnumerable<GameDto>>
    {
        private readonly IGameGenreRepository gameGenreRepository;
        private readonly IMapper mapper;

        public GetAllGamesOfGenreQueryHandler(IGameGenreRepository gameGenreRepository, IMapper mapper)
        {
            this.gameGenreRepository = gameGenreRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GameDto>> Handle(GetAllGamesOfGenreQuery request, CancellationToken cancellationToken)
        {
            var games = await gameGenreRepository.GetAllGamesOfGenre(request.GenreEncodedName);
            var gameDtos = mapper.Map<IEnumerable<GameDto>>(games);

            return gameDtos;
        }
    }
}
