using WebGames.Domain.Entities;
using WebGames.Infrastructure.Persistence;
using WebGames.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebGames.Infrastructure.Extensions;

namespace WebGames.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IMemoryCache cache;
        private readonly WebGamesDbContext dbContext;

        public GenreRepository(IMemoryCache cache, WebGamesDbContext dbContext)
        {
            this.cache = cache;
            this.dbContext = dbContext;
        }

        public Task Commit()
        {
            cache.Remove(CacheHelpers.GenerateGenresCacheKey());
            return dbContext.SaveChangesAsync();
        }

        public async Task Create(Genre genre)
        {
            dbContext.Genres.Add(genre);
            await dbContext.SaveChangesAsync();
            cache.Remove(CacheHelpers.GenerateGenresCacheKey());
        }

        public async Task Delete(Genre? genre)
        {
            if (await dbContext.Genres.ContainsAsync(genre) == false)
            {
                return;
            }

            dbContext.Genres.Remove(genre!);

            await dbContext.SaveChangesAsync();
            cache.Remove(CacheHelpers.GenerateGenresCacheKey());
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return (await cache.GetOrCreateAsync(CacheHelpers.GenerateGenresCacheKey(), async entry =>
            {
                entry.SlidingExpiration = CacheHelpers.DefaultCacheDuration;
                return await dbContext.Genres.ToListAsync();
            })) ?? new List<Genre>();
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
