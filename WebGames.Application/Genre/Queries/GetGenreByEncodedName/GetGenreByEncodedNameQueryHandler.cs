using AutoMapper;
using WebGames.Domain.Interfaces;
using MediatR;

namespace WebGames.Application.Genre.Queries.GetGenreByEncodedName
{
    public class GetGenreByEncodedNameQueryHandler : IRequestHandler<GetGenreByEncodedNameQuery, GenreDto>
    {
        private readonly IGenreRepository genreRepository;
        private readonly IMapper mapper;

        public GetGenreByEncodedNameQueryHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            this.genreRepository = genreRepository;
            this.mapper = mapper;
        }

        public async Task<GenreDto> Handle(GetGenreByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var genre = await genreRepository.GetByEncodedName(request.EncodedName);
            var dto = mapper.Map<GenreDto>(genre);

            return dto;
        }
    }
}
