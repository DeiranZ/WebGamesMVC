using AutoMapper;
using MediatR;
using WebGames.Application.Genre;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.GameGenre.Queries.GetAllGenresOfGame
{
    public class GetAllGenresOfGameQueryHandler : IRequestHandler<GetAllGenresOfGameQuery, IEnumerable<GenreDto>>
    {
        private readonly IGameGenreRepository gameGenreRepository;
        private readonly IMapper mapper;

        public GetAllGenresOfGameQueryHandler(IGameGenreRepository gameGenreRepository, IMapper mapper)
        {
            this.gameGenreRepository = gameGenreRepository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GenreDto>> Handle(GetAllGenresOfGameQuery request, CancellationToken cancellationToken)
        {
            var genresOfGame = await gameGenreRepository.GetAllGenresOfGame(request.GameEncodedName);
            var dtos = mapper.Map<IEnumerable<GenreDto>>(genresOfGame);

            return dtos;
        }
    }
}
