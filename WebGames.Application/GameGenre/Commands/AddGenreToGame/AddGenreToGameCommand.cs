using MediatR;

namespace WebGames.Application.GameGenre.Commands.AddGenreToGame
{
    public class AddGenreToGameCommand : IRequest
    {
        public string GameEncodedname { get; set; } = default!;
        public string GenreEncodedname { get; set; } = default!;
    }
}
