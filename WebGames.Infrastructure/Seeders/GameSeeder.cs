using WebGames.Domain.Entities;
using WebGames.Infrastructure.Persistence;

namespace WebGames.Infrastructure.Seeders
{
    public class GameSeeder
    {
        private readonly WebGamesDbContext dbContext;

        public GameSeeder(WebGamesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Games.Any())
                {
                    var exampleGame = new Game()
                    {
                        Name = "Example Game",
                        Description = "This is an example game"
                    };

                    exampleGame.EncodeName();

                    dbContext.Games.Add(exampleGame);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
