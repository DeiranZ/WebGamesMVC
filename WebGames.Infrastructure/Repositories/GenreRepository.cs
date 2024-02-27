using WebGames.Domain.Entities;
using WebGames.Infrastructure.Persistence;
using WebGames.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebGames.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly WebGamesDbContext dbContext;

        public GenreRepository(WebGamesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task Commit()
        {
            return dbContext.SaveChangesAsync();
        }

        public async Task Create(Genre genre)
        {
            dbContext.Genres.Add(genre);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Genre? genre)
        {
            if (await dbContext.Genres.ContainsAsync(genre) == false)
            {
                return;
            }

            dbContext.Genres.Remove(genre!);

            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await dbContext.Genres.ToListAsync();
        }

        public async Task<Genre?> GetByEncodedName(string encodedName)
        {
            return await dbContext.Genres.FirstAsync(c => c.EncodedName == encodedName);
        }

        public async Task<Genre?> GetByName(string name)
        {
            return await dbContext.Genres.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }
    }
}
