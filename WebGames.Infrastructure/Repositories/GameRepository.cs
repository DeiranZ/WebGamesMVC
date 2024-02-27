using Microsoft.EntityFrameworkCore;
using WebGames.Domain.Entities;
using WebGames.Domain.Interfaces;
using WebGames.Infrastructure.Persistence;

namespace WebGames.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly WebGamesDbContext dbContext;

        public GameRepository(WebGamesDbContext dbContext)
        {
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
        }

        public async Task Delete(Game? game)
        {
            if (await dbContext.Games.ContainsAsync(game) == false) 
            {
                return;
            }

            dbContext.Games.Remove(game!);
            
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetAll()
		{
			return await dbContext.Games.ToListAsync();
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
