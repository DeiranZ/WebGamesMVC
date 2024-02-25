using WebGames.Domain.Entities;

namespace WebGames.Domain.Interfaces
{
    public interface IGameRepository
    {
        Task Create(Game game);
        Task<Game?> GetByName(string name);
    }
}
