using Microsoft.EntityFrameworkCore;
using WebGames.Domain.Entities;
using WebGames.Domain.Interfaces;
using WebGames.Infrastructure.Persistence;

namespace WebGames.Infrastructure.Repositories
{
    public class GameGenreRepository : IGameGenreRepository
    {
        private readonly WebGamesDbContext dbContext;

        public GameGenreRepository(WebGamesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Game>> GetAllGamesOfGenre(string encodedName)
        {
            var genre = await dbContext.Genres.FirstAsync(c => c.EncodedName == encodedName);
            var allGames = await dbContext.Games.ToListAsync();
            var gameGenres = await dbContext.GameGenre.ToListAsync();
            var query = gameGenres.Where(c => c.GenreId == genre.Id)
                .Select(c => c.Game);

            return query;
        }

        public async Task<IEnumerable<Game>> GetAllGamesOfGenre(Genre genre)
        {
            var allGames = await dbContext.Games.ToListAsync();
            return allGames.Where(c => c.Genres.Contains(genre));
        }

        public async Task<IEnumerable<Genre>> GetAllGenresExcludingExistingOfGame(string encodedName)
        {
            var genresOfGame = await GetAllGenresOfGame(encodedName);
            var allGenres = await dbContext.Genres.ToListAsync();
            var query = allGenres.Except(genresOfGame);

            return query;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresOfGame(string encodedName)
        {
            var game = await dbContext.Games.FirstAsync(c => c.EncodedName == encodedName);
            var allGenres = await dbContext.Genres.ToListAsync();
            var gameGenres = await dbContext.GameGenre.ToListAsync();
            var query = gameGenres.Where(c => c.GameId == game.Id)
                .Select(c => c.Genre);

            return query;
        }

        public async Task RemoveGenreFromGame(string gameEncodedName, string genreEncodedName)
        {
            var game = await dbContext.Games.FirstAsync(c => c.EncodedName == gameEncodedName);
            var genre = await dbContext.Genres.FirstAsync(c => c.EncodedName == genreEncodedName);

            if (dbContext.GameGenre.Count() == 0) return;
            var gameGenre = await dbContext.GameGenre.FirstAsync(c => c.GameId == game.Id && c.GenreId == genre.Id);

            dbContext.GameGenre.Remove(gameGenre);
            await dbContext.SaveChangesAsync();
        }
    }
}
