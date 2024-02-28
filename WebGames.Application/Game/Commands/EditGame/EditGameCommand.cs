using MediatR;
using WebGames.Application.Genre;

namespace WebGames.Application.Game.Commands.EditGame
{
    public class EditGameCommand : GameDto, IRequest
    {
        public IEnumerable<GenreDto> AllGenres { get; set; } = [];
    }
}
