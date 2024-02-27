using MediatR;

namespace WebGames.Application.Genre.Commands.DeleteGenre
{
    public class DeleteGenreCommand : GenreDto, IRequest
    {
    }
}
