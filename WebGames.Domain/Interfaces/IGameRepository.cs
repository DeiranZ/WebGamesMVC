using WebGames.Domain.Entities;

namespace WebGames.Domain.Interfaces
{
    public interface IGameRepository
    {
        Task Create(Game game);
        Task Delete(Game? game);
        Task<Game?> GetByName(string name);
        Task<IEnumerable<Game>> GetAll();
        Task<Game?> GetByEncodedName(string encodedName);
        Task Commit();
    }
}
