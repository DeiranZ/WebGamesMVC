using WebGames.Domain.Entities;
using WebGames.Infrastructure.Persistence;
using WebGames.Domain.Interfaces;

namespace WebGames.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly WebGamesDbContext dbContext;

        public GenreRepository(WebGamesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(Genre genre)
        {
            dbContext.Genres.Add(genre);
            await dbContext.SaveChangesAsync();
        }
    }
}
