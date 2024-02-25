using WebGames.Application.Game;

namespace WebGames.Application.Services
{
    public interface IGameService
    {
        Task Create(GameDto game);
    }
}