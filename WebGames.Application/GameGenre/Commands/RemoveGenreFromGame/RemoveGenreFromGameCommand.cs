using MediatR;

namespace WebGames.Application.GameGenre.Commands.RemoveGenreFromGame
{
    public class RemoveGenreFromGameCommand : IRequest
    {
        public string GameEncodedname { get; set; } = default!;
        public string GenreEncodedname { get; set; } = default!;
    }
}
