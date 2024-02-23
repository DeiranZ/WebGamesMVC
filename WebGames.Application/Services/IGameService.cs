using WebGames.Domain.Entities;

namespace WebGames.Application.Services
{
    public interface IGameService
    {
        Task Create(Game game);
    }
}