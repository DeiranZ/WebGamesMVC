using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebGames.Domain.Entities;
using WebGames.Domain.Interfaces;
using WebGames.Infrastructure.Extensions;
using WebGames.Infrastructure.Persistence;

namespace WebGames.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly IMemoryCache cache;
        private readonly WebGamesDbContext dbContext;

        public GameRepository(IMemoryCache cache, WebGamesDbContext dbContext)
        {
            this.cache = cache;
            this.dbContext = dbContext;
        }

        public Task Commit()
        {
            return dbContext.SaveChangesAsync();
        }

        public async Task Create(Game game)
        {
            dbContext.Add(game);
            await dbContext.SaveChangesAsync();
            cache.Remove(CacheHelpers.GenerateAllGamesCacheKey());
        }

        public async Task Delete(Game? game)
        {
            if (await dbContext.Games.ContainsAsync(game) == false) 
            {
                return;
            }

            dbContext.Games.Remove(game!);
            
            await dbContext.SaveChangesAsync();

            cache.Remove(CacheHelpers.GenerateAllGamesCacheKey());
        }

        public async Task<IEnumerable<Game>> GetAll()
		{
            return (await cache.GetOrCreateAsync(CacheHelpers.GenerateAllGamesCacheKey(), async entry =>
            {
                entry.SlidingExpiration = CacheHelpers.DefaultCacheDuration;
                return await dbContext.Games.OrderByDescending(u => u.Id).ToListAsync();
            })) ?? new List<Game>();
		}

        public async Task<Game?> GetByEncodedName(string encodedName)
        {
            return await dbContext.Games.FirstAsync(c => c.EncodedName == encodedName);
        }

        public async Task<Game?> GetByName(string name)
		{
			return await dbContext.Games.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
		}

	}
}
