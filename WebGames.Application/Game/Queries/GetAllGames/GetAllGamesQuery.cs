using MediatR;

namespace WebGames.Application.Game.Queries.GetAllGames
{
    public class GetAllGamesQuery : IRequest<IEnumerable<GameDto>>
    {
    }
}
