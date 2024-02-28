using MediatR;
using WebGames.Application.Game;

namespace WebGames.Application.GameGenre.Queries.GetAllGamesOfGenre
{
    public class GetAllGamesOfGenreQuery : IRequest<IEnumerable<GameDto>>
    {
        public string GenreEncodedName { get; set; } = default!;

        public GetAllGamesOfGenreQuery(string genreEncodedName)
        {
            GenreEncodedName = genreEncodedName;
        }
    }
}
