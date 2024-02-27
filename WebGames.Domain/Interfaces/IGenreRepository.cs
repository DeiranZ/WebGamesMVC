using WebGames.Domain.Entities;

namespace WebGames.Domain.Interfaces
{
    public interface IGenreRepository
    {
        Task Create(Genre genre);
        Task Delete(Genre? genre);
        Task<Genre?> GetByName(string name);
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre?> GetByEncodedName(string encodedName);
        Task Commit();
    }
}