using WebGames.Domain.Entities;

namespace WebGames.Domain.Interfaces
{
    public interface IGameRepository
    {
        public Task Create(Game game);
    }
}
