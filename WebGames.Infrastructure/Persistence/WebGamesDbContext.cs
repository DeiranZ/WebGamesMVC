using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebGames.Domain.Entities;
using WebGames.Domain.ApplicationUser;

namespace WebGames.Infrastructure.Persistence
{
    public class WebGamesDbContext : IdentityDbContext<ApplicationUser>
    {
        public WebGamesDbContext(DbContextOptions<WebGamesDbContext> options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GameGenre> GameGenre { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Game>()
                .HasMany(c => c.Genres)
                .WithMany(c => c.Games)
                .UsingEntity<GameGenre>();
        }
    }
}
