using WebGames.Domain.Entities;

namespace WebGames.Domain.Interfaces
{
    public interface IGenreRepository
    {
        Task Create(Genre genre);
    }
}