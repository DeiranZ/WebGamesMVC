using WebGames.Domain.Entities;

namespace WebGames.Domain.Interfaces
{
    public interface IGameGenreRepository
    {
        Task<IEnumerable<Game>> GetAllGamesOfGenre(Genre genre);
        Task<IEnumerable<Game>> GetAllGamesOfGenre(string encodedName);
        Task<IEnumerable<Genre>> GetAllGenresOfGame(string encodedName);
        Task<IEnumerable<Genre>> GetAllGenresExcludingExistingOfGame(string encodedName);
        Task RemoveGenreFromGame(string gameEncodedName, string genreEncodedName);
    }
}
