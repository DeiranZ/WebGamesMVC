using MediatR;
using WebGames.Application.Genre;

namespace WebGames.Application.GameGenre.Queries.GetAllGenresOfGame
{
    public class GetAllGenresOfGameQuery : IRequest<IEnumerable<GenreDto>>
    {
        public GetAllGenresOfGameQuery(string gameEncodedName)
        {
            GameEncodedName = gameEncodedName;
        }

        public string GameEncodedName { get; set; }
    }
}
