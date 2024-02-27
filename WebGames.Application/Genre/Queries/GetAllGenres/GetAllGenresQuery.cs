using MediatR;

namespace WebGames.Application.Genre.Queries.GetAllGenres
{
    public class GetAllGenresQuery : IRequest<IEnumerable<GenreDto>>
    {
    }
}
