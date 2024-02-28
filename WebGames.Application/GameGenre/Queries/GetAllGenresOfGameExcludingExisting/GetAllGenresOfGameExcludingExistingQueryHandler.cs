using AutoMapper;
using MediatR;
using WebGames.Application.Genre;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.GameGenre.Queries.GetAllGenresOfGameExcludingExisting
{
    public class GetAllGenresOfGameExcludingExistingQueryHandler : IRequestHandler<GetAllGenresOfGameExcludingExistingQuery, IEnumerable<GenreDto>>
    {
        private readonly IGameGenreRepository gameGenreRepository;
        private readonly IMapper mapper;

        public GetAllGenresOfGameExcludingExistingQueryHandler(IGameGenreRepository gameGenreRepository, IMapper mapper)
        {
            this.gameGenreRepository = gameGenreRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GenreDto>> Handle(GetAllGenresOfGameExcludingExistingQuery request, CancellationToken cancellationToken)
        {
            var genresOfGame = await gameGenreRepository.GetAllGenresExcludingExistingOfGame(request.GameEncodedName);
            var dtos = mapper.Map<IEnumerable<GenreDto>>(genresOfGame);

            return dtos;
        }
    }
}
