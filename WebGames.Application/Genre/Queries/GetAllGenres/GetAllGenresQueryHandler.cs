using AutoMapper;
using MediatR;
using WebGames.Application.Game;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Genre.Queries.GetAllGenres
{
    public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<GenreDto>>
    {
        private readonly IGenreRepository genreRepository;
        private readonly IMapper mapper;

        public GetAllGenresQueryHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            this.genreRepository = genreRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GenreDto>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            var games = await genreRepository.GetAll();
            var genreDtos = mapper.Map<IEnumerable<GameDto>>(games);

            return genreDtos;
        }
    }
}
