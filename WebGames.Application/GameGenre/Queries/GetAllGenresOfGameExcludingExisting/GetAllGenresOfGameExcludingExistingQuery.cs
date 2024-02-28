using MediatR;
using WebGames.Application.Game;
using WebGames.Application.Genre;

namespace WebGames.Application.GameGenre.Queries.GetAllGenresOfGameExcludingExisting
{
    public class GetAllGenresOfGameExcludingExistingQuery : IRequest<IEnumerable<GenreDto>>
    {
        public string GameEncodedName { get; set; }

        public GetAllGenresOfGameExcludingExistingQuery(string gameEncodedName)
        {
            GameEncodedName = gameEncodedName;
        }
    }
}
