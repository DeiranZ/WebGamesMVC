using Microsoft.EntityFrameworkCore;
using WebGames.Domain.Entities;

namespace WebGames.Infrastructure.Persistence
{
    public class WebGamesDbContext : DbContext
    {
        public WebGamesDbContext(DbContextOptions<WebGamesDbContext> options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
    }
}
