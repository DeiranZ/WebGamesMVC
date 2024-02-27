using MediatR;

namespace WebGames.Application.Genre.Commands.CreateGenre
{
    public class CreateGenreCommand : GenreDto, IRequest
    {
    }
}
